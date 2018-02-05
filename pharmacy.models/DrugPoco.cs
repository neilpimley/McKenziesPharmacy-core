using System;

namespace Pharmacy.Models.Pocos
{
    public class DrugPoco : Drug
    {
        public bool IsFavourite { get; set; }
        public Guid FavouriteId { get; set; }
        public Guid OrderLineId { get; set; }
        public int Qty { get; set; }
    }
}