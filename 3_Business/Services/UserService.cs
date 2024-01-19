using Core.Services.Bases;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IUserService : IService<UserModel> // hem kullanıcılar için login ve register işlemlerinin AccountService üzerinden hem de
														// adminlerin kullanıcı listeleme, detay görüntüleme, ekleme, güncelleme ve silme (CRUD)
														// işlemlerini yapacağı UserService için bu servisi IService'ten implemente ediyoruz
	{
	}

	public class UserService : IUserService
	{
		private readonly RepoBase<User> _userRepo; // kullanıcı servisi enjekte edilen kullanıcı repository'si üzerinden veritabanında işlemleri gerçekleştirecek

		public UserService(RepoBase<User> userRepo)
		{
			_userRepo = userRepo;
		}

		public Result Add(UserModel model)
		{
			if (_userRepo.Query().Any(u => u.UserName == model.UserName)) 
				return new ErrorResult("User can't be added because user with the same user name exists!");

			var entity = new User()
			{
				IsActive = model.IsActive,

				UserName = model.UserName, // kullanıcı adı hassas veri olduğundan trim'lemiyoruz yani model üzerinden nasıl geliyorsa onu kullanıyoruz

				Password = model.Password, // şifre hassas veri olduğundan trim'lemiyoruz yani model üzerinden nasıl geliyorsa onu kullanıyoruz,
										   // eğer istenirse şifre verisini açık olarak veritabanına kaydetmek yerine hash'leyerek şifreli olarak kaydedebiliriz

				RoleId = model.RoleId,



                // User UserDetail 1 to 1 ilişkisi için:
                //UserDetail = new UserDetail() // entity içerisindeki ilişkili UserDetail entity'sini de new'leyip özelliklerini model'deki
                //							    // UserDetail özelliklerine göre dolduruyoruz
                //{
                //	Address = model.UserDetail.Address.Trim(),
                //	CityId = model.UserDetail.CityId.Value, // UserDetailModel'de Required olduğu için Value kullanabiliriz
                //	CountryId = model.UserDetail.CountryId.Value, // UserDetailModel'de Required olduğu için Value kullanabiliriz
                //	Email = model.UserDetail.Email.Trim(),
                //	Phone = model.UserDetail.Phone?.Trim(), // UserDetailModel'da zorunlu olmadığı yani null gelebileceği için ? kullandık
                //	Sex = model.UserDetail.Sex
                //}
				// User UserDetail 1 to many ilişkisi için:
                UserDetails = new List<UserDetail>() // entity içerisindeki ilişkili UserDetails koleksiyonunu da new'leyip
													 // koleksiyon içerisindeki UserDetail entity'sini new'leyerek özelliklerini model'deki
													 // UserDetail özelliklerine göre dolduruyoruz
				{
					new UserDetail()
					{
						Address = model.UserDetail.Address.Trim(),
						CityId = model.UserDetail.CityId.Value, // UserDetailModel'de Required olduğu için Value kullanabiliriz
						CountryId = model.UserDetail.CountryId.Value, // UserDetailModel'de Required olduğu için Value kullanabiliriz
						Email = model.UserDetail.Email.Trim(),
						Phone = model.UserDetail.Phone?.Trim(), // UserDetailModel'da zorunlu olmadığı yani null gelebileceği için ? kullandık
						Sex = model.UserDetail.Sex
					}
                }
			};

			_userRepo.Add(entity);
			return new SuccessResult("User added successfully.");
		}

		public Result Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_userRepo.Dispose();
		}

		public IQueryable<UserModel> Query()
		{
			return _userRepo.Query()
				.OrderByDescending(u => u.IsActive) // önce kullanıcının aktifliğini azalan sıralıyoruz ki aktif olanları üstte görebilelim
				.ThenBy(u => u.UserName) // sonra aktiflik sıralamasına göre kullanıcı adlarına göre artan sıralıyoruz
				.Select(u => new UserModel()
				{
					Id = u.Id,
					IsActive = u.IsActive,
					Password = u.Password,
					RoleId = u.RoleId,
					UserName = u.UserName,
					Role = new RoleModel() // user entity'sindeki ilişkili Role entity'si üzerinden RoleModel tipindeki referans özelliğimizin
										   // Name özelliğini new'leyerek atıyoruz
					{
						Name = u.Role.Name
					},



					// User UserDetail 1 to 1 ilişkisi için:
					//UserDetail = new UserDetailModel() // user entity'sindeki ilişkili UserDetail entity'si üzerinden UserDetailModel tipindeki
					//								     // referans özelliğimizi new'leyerek içerisindeki özellikleri dolduruyoruz
					//{
					//	Address = u.UserDetail.Address,
					//	CityId = u.UserDetail.CityId,
					//	CountryId = u.UserDetail.CountryId,
					//	Email = u.UserDetail.Email,
					//	Phone = u.UserDetail.Phone,
					//	Sex = u.UserDetail.Sex,
					//	Country = new CountryModel() // user entity'sindeki ilişkili UserDetail entity'si içerisindeki Country referans özelliğini
					//								 // new'leyerek Name özelliğini atıyoruz
					//	{
					//		Name = u.UserDetail.Country.Name
					//	},
					//	City = new CityModel() // user entity'sindeki ilişkili UserDetail entity'si içerisindeki City referans özelliğini
					//						   // new'leyerek Name özelliğini atıyoruz
					//	{
					//		Name = u.UserDetail.City.Name
					//	}
					//}
					// User UserDetail 1 to many ilişkisi için:
					UserDetail = u.UserDetails.Select(ud => new UserDetailModel() // user entity'sindeki ilişkili UserDetails koleksiyonu üzerinden
																				  // UserDetailModel tipindeki UserDetail referans özelliğimize Select
																				  // ile projeksiyon yaparak ilk elemanı atıyoruz
					{
						Address = ud.Address,
						CityId = ud.CityId,
						CountryId = ud.CountryId,
						Email = ud.Email,
						Phone = ud.Phone,
						Sex = ud.Sex,
						Country = new CountryModel() // UserDetail tipindeki ud delegesi üzerinden Country referans özelliğini
													 // new'leyerek Name özelliğini atıyoruz
						{
							Name = ud.Country.Name
						},
						City = new CityModel() // UserDetail tipindeki ud delegesi üzerinden City referans özelliğini
                                               // new'leyerek Name özelliğini atıyoruz
                        {
                            Name = ud.City.Name
						}
					}).FirstOrDefault()
				});
		}

		public Result Update(UserModel model)
		{
			throw new NotImplementedException();
		}
	}
}
