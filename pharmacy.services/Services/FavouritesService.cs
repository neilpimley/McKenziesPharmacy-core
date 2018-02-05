using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using NLog;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;

namespace Pharmacy.Services
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public FavouritesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Favourite> GetFavourite(Guid id) {
            return await _unitOfWork.FavouriteRepository.GetByID(id);
        }

        public async Task<IEnumerable<DrugPoco>> GetFavouriteDrugs(Guid customerId)
        {
            logger.Info("GetFavouriteDrugs - CustomerId: {0}", customerId);
            return (from d in await _unitOfWork.DrugRepository.Get()
                    join f in await _unitOfWork.FavouriteRepository.Get() on d.DrugId equals f.DrugId
                    where f.CustomerId == customerId
                    select new DrugPoco()
                    {
                        DrugId = d.DrugId,
                        DrugName = d.DrugName,
                        FavouriteId = f.FavouriteId
                    });
        }

        public async Task<Favourite> AddFavourite(Favourite favouriteDrug)
        {
            logger.Info("AddFavourite - CustomerId:{0}, DrugId:{1}", favouriteDrug.CustomerId, favouriteDrug.DrugId);
            var exists = await _unitOfWork.FavouriteRepository
                .Get(f => f.DrugId == favouriteDrug.CustomerId
                    && f.CustomerId == favouriteDrug.CustomerId);

            if (exists.Any())
            {
                throw new Exception("Favourite already exists");
            }
            else
            { 
                favouriteDrug.FavouriteId = Guid.NewGuid();
                _unitOfWork.FavouriteRepository.Insert(favouriteDrug);
                try
                {
                    await _unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    logger.Error("AddFavourite - {0}", ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return favouriteDrug;
        }
        public async Task DeleteFavourite(Guid id)
        {
            logger.Info("DeleteFavourite - FavouritID:{0}", id);
            _unitOfWork.FavouriteRepository.Delete(id);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logger.Error("DeleteFavourite - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}