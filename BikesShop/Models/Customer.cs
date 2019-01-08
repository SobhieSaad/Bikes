namespace BikesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
            CustomersComments = new HashSet<CustomersComment>();
            ProductRates = new HashSet<ProductRate>();
        }

        public int ID { get; set; }

        [Column("Marital Status")]
        [StringLength(255)]
        public string Marital_Status { get; set; }

        [StringLength(255)]
        public string Gender { get; set; }

        public double? Income { get; set; }

        public double? Children { get; set; }

        [StringLength(255)]
        public string Education { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }

        [Column("Home Owner")]
        [StringLength(255)]
        public string Home_Owner { get; set; }

        public double? Cars { get; set; }

        [Column("Commute Distance")]
        [StringLength(255)]
        public string Commute_Distance { get; set; }

        [StringLength(255)]
        public string Region { get; set; }

        public double? Age { get; set; }

        [StringLength(255)]
        public string Buy { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomersComment> CustomersComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductRate> ProductRates { get; set; }
    }
}
