using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<IEnumerable<Shop>> GetShops();
        Task<IEnumerable<Title>> GetTitles();
        Task<IEnumerable<Practice>> GetPractices();
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<IEnumerable<Doctor>> GetDoctorsByPractice(Guid practiceIdD);
        Task<List<Address>> GetAddressesByPostcode(string postCode);
    }
}