namespace BikesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        [Key]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        public int? Customer_Id { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? total { get; set; }

        [StringLength(100)]
        public string Username { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
