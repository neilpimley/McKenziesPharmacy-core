using System;
using Pharmacy.Models;
using System.Collections.Generic;
using Pharmacy.Repositories;
using System.Net.Http;
using Pharmacy.tests.helper;
using Moq;
using Xunit;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;

namespace Pharmacy.Tests.ServiceTests
{
    public class TestDrugsService : IDisposable
    {
        private IDrugsService _drugsService;
        private List<DrugPoco> _drugPocos;
        private List<Drug> _drugs;
        private GenericRepository<Drug> _drugsRepository;
        private IUnitOfWork _unitOfWork;
        private HttpClient _client;

        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://localhost:50875/";

        private DataInitializer dataInitializer;

        private List<Drug> SetUpDrugs()
        {
            var drugs = dataInitializer.GetAllDrugs();
            foreach (Drug drug in drugs)
                drug.DrugId = Guid.NewGuid();
            return drugs;
        }

        [Fact]
        public void TestGetDrugs()
        {
            dataInitializer = new DataInitializer();
            _drugs = SetUpDrugs();
            _drugsRepository = SetUpDrugsRepository();

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.DrugRepository).Returns(_drugsRepository);
            _unitOfWork = unitOfWork.Object;

            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }

        private GenericRepository<Drug> SetUpDrugsRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Drug>>();

            // Setup mocking behavior
            mockRepo.Setup(x => x.Get(null, null, ""))
                .ReturnsAsync(_drugs);

            mockRepo.Setup(x => x.GetByID(It.IsAny<int>()))
            .ReturnsAsync(new Func<int, Drug>(
            id => _drugs.Find(p => p.DrugId.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Drug>())))
            .Callback(new Action<Drug>(newDrug =>
            {
                newDrug.DrugId = Guid.NewGuid();
                _drugs.Add(newDrug);
            }));

            mockRepo.Setup(p => p.Update(It.IsAny<Drug>()))
            .Callback(new Action<Drug>(drug =>
            {
                var oldDrug = _drugs.Find(a => a.DrugId == drug.DrugId);
                oldDrug = drug;
            }));

            // Return mock implementation object
            return mockRepo.Object;
        }

        
        public void Dispose()
        {
            _drugsService = null;
            _drugPocos = null;
            _drugs = null;
            _drugsRepository = null;

            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }
    }
}
