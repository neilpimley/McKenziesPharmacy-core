
using Pharmacy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using getAddress.Sdk;
using getAddress.Sdk.Api.Requests;
using getAddress.Sdk.Api.Responses;
using Microsoft.Extensions.Options;
using NLog;
using Pharmacy.Exceptions;
using Pharmacy.Models;
using Pharmacy.Repositories.Interfaces;
using Twilio.Exceptions;

namespace Pharmacy.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly string _apiKey;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RegisterService(IUnitOfWork unitOfWork, IOptions<ServiceSettings> serviceSettings)
        {
            _unitOfWork = unitOfWork;
            _apiKey = serviceSettings.Value.GetAddressApiKey;
        }

        public async Task<IEnumerable<Shop>> GetShops()
        {
            try
            {
                return await _unitOfWork.ShopRepository.Get();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                logger.Error("GetShops - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Shop).Name);
            }
        }

        public async Task<IEnumerable<Title>> GetTitles()
        {
            try
            {
                return await _unitOfWork.TitleRepository.Get();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                logger.Error("GetTitles - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Title).Name);
            }
        }

        public async Task<IEnumerable<Practice>> GetPractices()
        {
            try
            {
                return await _unitOfWork.PracticeRepository.Get();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                logger.Error("GetPractices - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Practice).Name);
            }
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            try
            {
                return await _unitOfWork.DoctorRepository.Get();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                logger.Error("GetDoctors - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Doctor).Name);
            }
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByPractice(Guid practiceId)
        {
            try
            {
                return await _unitOfWork.DoctorRepository.Get(d => d.PracticeId == practiceId);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                logger.Error("GetShops - Error: {0}", ex.Message);
                throw new DataRetrieverException("Shops");
            }
        }

        
        public async Task<List<Models.Address>> GetAddressesByPostcode(string postCode)
        {
            var apiKey = new ApiKey(_apiKey);
            var addresses = new List<Models.Address>();

            using (var api = new GetAddesssApi(apiKey))
            {
                try
                {
                    var result = await api.Address.Get(new GetAddressRequest(postCode));

                    if (result.IsSuccess)
                    {
                        var successfulResult = (GetAddressResponse.Success) result;

                        addresses = successfulResult.Addresses.Select(a => new Models.Address()
                        {
                            AddressLine1 = a.Line1,
                            AddressLine2 = a.Line2,
                            AddressLine3 = a.Line3,
                            Town = a.TownOrCity,
                            County = a.County,
                            Postcode = postCode
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    logger.Error("GetAddressesByPostcode failed for '{0}'. Error: {1}", postCode, ex.Message );
                    throw new ApiConnectionException("GetPostcodeApi");
                }
            }
            return addresses;
        }

        

    }
}