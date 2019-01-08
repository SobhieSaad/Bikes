namespace BikesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Carts = new HashSet<Cart>();
            CustomersComments = new HashSet<CustomersComment>();
            OrdersDetails = new HashSet<OrdersDetail>();
            ProductRates = new HashSet<ProductRate>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Category { get; set; }
        [Required]
        public double Price { get; set; }

        [StringLength(300)]
        public string ImageName { get; set; }

        [StringLength(100)]
        public string Producer { get; set; }

        [StringLength(2000)]
        public string Feature { get; set; }

        [StringLength(2000)]
        public string Brief { get; set; }

        public Nullable<int> AverageRate { get; set; }

        //public ProductRate ProRate { get; set; }

     //   public ProductCommentRate ProductCommentRate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomersComment> CustomersComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductRate> ProductRates { get; set; }

      
    }
}
