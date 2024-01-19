#nullable disable

using Core.Records.Bases;
using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    // 1. yöntem:
    public class UserDetail : Record // Kullanıcı Detayı entity'sini Record'dan miras alarak oluşturuyoruz ki ihtiyaç halinde
                                     // kendi repository ve service'lerinde kullanabilelim,
                                     // User ile arasında 2. yöntemden farklı olarak 1'e çoklu ilişki var
    {
        public int UserId { get; set; } // zorunlu, User ile 1 to many ilişki olduğu için User'ın Id'sini buraya UserId foreign key'i olarak taşıdık

        public User User { get; set; } // 1 veya daha çok kullanıcı detayı 1 kullanıyıca ait olmalı

        public Sex Sex { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; } // zorunlu

        [StringLength(25)]
        public string Phone { get; set; } // zorunlu değil

        [Required]
        [StringLength(750)]
        public string Address { get; set; } // zorunlu

        public int CountryId { get; set; } // ülke id, zorunlu

        public Country Country { get; set; } // Ülke ile Kullanıcı Detayı arasında 1 to many ilişki var, 1 kullanıcı detayının mutlaka 1 ülkesi olmalı

        public int CityId { get; set; } // şehir id, zorunlu

        public City City { get; set; } // Şehir ile Kullanıcı Detayı arasında 1 to many ilişki var, 1 kullanıcı detayının mutlaka 1 şehri olmalı
    }



    // 2. yöntem:
    //public class UserDetail // Kullanıcı Detayı, User ile ilişkili olduğu ve kendi Id'si olmayacağı için Record'dan miras almamalı,
    //					      // User ile arasında 1'e 1 ilişki var, genelde 1'e 1 ilişki veritabanındaki bir tablonun çok sayıda sütununun
    //						  // iki veya daha fazla tabloya parçalanmasıyla oluşturulur
    //{
    //	[Key]
    //	public int UserId { get; set; } // zorunlu, User ile 1 to 1 ilişki olduğu için User'ın Id'sini buraya UserId foreign key'i olarak taşıdık

    //	public User User { get; set; } // 1 kullanıcı detayı 1 kullanıyıca ait olmalı

    //	public Sex Sex { get; set; }

    //	[Required]
    //	[StringLength(250)]
    //	public string Email { get; set; } // zorunlu

    //	[StringLength(25)]
    //	public string Phone { get; set; } // zorunlu değil

    //	[Required]
    //	[StringLength(750)]
    //	public string Address { get; set; } // zorunlu

    //	public int CountryId { get; set; } // ülke id, zorunlu

    //	public Country Country { get; set; } // 1 to many ilişki, 1 kullanıcı detayının (1'e 1 ilişki olduğu için kullanıcının) mutlaka 1 ülkesi olmalı

    //	public int CityId { get; set; } // şehir id, zorunlu

    //	public City City { get; set; } // 1 to many ilişki, 1 kullanıcı detayının (1'e 1 ilişki olduğu için kullanıcının) mutlaka 1 şehri olmalı
    //}
}
