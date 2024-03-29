﻿#nullable disable

using Core.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class UserModel : Record
	{
		#region Entity'den Kopyalanan Özellikler
		[Required(ErrorMessage = "{0} is required!")]
		[MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
		[MaxLength(15, ErrorMessage = "{0} must be maximum {1} characters!")]
		[DisplayName("User Name")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "{0} is required!")]
		[MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
		[MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
		public string Password { get; set; }

		[DisplayName("Active")]
		public bool IsActive { get; set; }

		[DisplayName("Role")]
		public int RoleId { get; set; }
		#endregion



		#region Entity Referans Özelliklerine Karşılık Kullanacağımız Özellikler
		public RoleModel Role { get; set; }

		// User ile UserDetail entity'leri arasında 1 to many ilişki olsa da biz User entity'sindeki UserDetails koleksiyonuna
		// sadece tek bir UserDetail elemanı eklemek istediğimizden aşağıdaki şekilde UserDetail referans özelliğini yazıyoruz
		public UserDetailModel UserDetail { get; set; } // kullanıcı detaylarını tek yerden yönetebilmek için hem burada hem de
														// AccountRegisterModel'da referans özelliği olarak kullanıyoruz
		#endregion
	}
}
