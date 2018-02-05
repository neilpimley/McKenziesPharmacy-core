using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IFavouritesService
    {
        Task<IEnumerable<DrugPoco>> GetFavouriteDrugs(Guid customerId);
        Task<Favourite> GetFavourite(Guid id);
        Task<Favourite> AddFavourite(Favourite favouriteDrug);
        Task DeleteFavourite(Guid id);
    }
}