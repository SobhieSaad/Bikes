namespace BikesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomersComment
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Comment { get; set; }

        public DateTime CommentDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
