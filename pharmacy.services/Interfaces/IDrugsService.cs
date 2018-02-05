using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IDrugsService
    {
        Task<IEnumerable<DrugPoco>> GetDrugs(Guid customerId, string drugName);
    }
}