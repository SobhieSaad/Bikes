namespace BikesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrdersDetail
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string OrderNumber { get; set; }

        public int? ProductId { get; set; }

        public decimal? Unitprice { get; set; }

        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }


    }
}
