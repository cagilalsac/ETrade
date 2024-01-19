#nullable disable

using Core.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    // 1. yöntem:
    public class ProductStore : Record // Ürün Mağaza ara ilişki entity'sini Record'dan miras alarak oluşturuyoruz ki ihtiyaç halinde
                                       // kendi repository ve service'lerinde kullanabilelim
    {
        public int ProductId { get; set; } // Ürün entity'sinden ürün id'yi taşıyoruz ve mağaza id ile birlikte primary key olarak ilk sırada set ediyoruz

        public Product Product { get; set; }

        public int StoreId { get; set; } // Mağaza entity'sinden mağaza id'yi taşıyoruz ve ürün id ile birlikte primary key olarak ikinci sırada set ediyoruz

        public Store Store { get; set; }
    }



    // 2. yöntem:
    //// Many to many ilişki için composite primary key tanımlama I. yöntem: II. yöntem için ETradeContext -> OnModelCreating methoduna bakılabilir.
    //[PrimaryKey(nameof(ProductId), nameof(StoreId))]
    //public class ProductStore // Ürün Mağaza ara ilişki entity'si, ürün ve mağaza arasında many to many ilişki olduğu için bu ara ilişkileri tutan entity'i oluşturuyoruz
    //{
    //    [Key]
    //    [Column(Order = 0)] // tanımlamak zorunlu değil
    //    public int ProductId { get; set; } // Ürün entity'sinden ürün id'yi taşıyoruz ve mağaza id ile birlikte primary key olarak ilk sırada set ediyoruz

    //    public Product Product { get; set; }

    //    [Key]
    //    [Column(Order = 1)] // tanımlamak zorunlu değil
    //    public int StoreId { get; set; } // Mağaza entity'sinden mağaza id'yi taşıyoruz ve ürün id ile birlikte primary key olarak ikinci sırada set ediyoruz

    //    public Store Store { get; set; }
    //}
}
