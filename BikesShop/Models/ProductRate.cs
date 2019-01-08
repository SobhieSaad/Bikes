namespace BikesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductRate")]
    public partial class ProductRate
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int Rate { get; set; }

        public DateTime RateDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
