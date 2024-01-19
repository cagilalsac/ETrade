using Core.Records.Bases;
using Core.Results.Bases;

namespace Core.Services.Bases
{
    /// <summary>
    /// TR: new'lenebilen referans tip olarak TModel için Record'dan miras alan model tipini kullanacak ve kullanıcı ile etkileşimde bulunacak 
    /// model ile veritabanıyla bağlantılı entity dönüşümlerinin gerçekleştirildiği, aynı zamanda veri formatlama ve validasyon 
    /// gibi işlemlerin yapılabileceği tüm veritabanı servis sınıflarının interface'i.
    /// EN: An interface, which can use only types inherited from Record through TModel as an instantiable reference type, for all database service classes 
    /// that allow interactions with the user through models and perform transformations between models and database related entities. 
    /// It also provides the ability to perform data formatting and validation processes.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IService<TModel> : IDisposable where TModel : Record, new()
    {
        /// <summary>
        /// TR: Enjekte edilen repository üzerinden entity tipindeki kayıtları modele projekte ederek getiren sorgu method tanımı.
        /// EN: A query method definition that, through the injected repository retrieves records of the entity type and maps them to a model.
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<TModel> Query();

        /// <summary>
        /// TR: Parametre olarak gönderilen model üzerinden ihtiyaca göre validasyon, veri formatlama vb. işlemleri gerçekleştirildikten sonra
        /// entity dönüşümü yapılarak enjekte edilen repository üzerinden veritabanına ekleme işlemi yapan ve sonuç olarak 
        /// başarılı veya başarısız işlem sonucu dönen method tanımı.
        /// EN: A method definition that, after performing validations, data formatting, and other necessary operations based on 
        /// the provided model as a parameter, transforms it into an entity and performs an insertion operation into the database 
        /// through the injected repository. The method returns the result as either successful or unsuccessful operation.
        /// </summary>
        /// <returns>Result</returns>
        Result Add(TModel model);

        /// <summary>
        /// TR: Parametre olarak gönderilen model üzerinden ihtiyaca göre validasyon, veri formatlama vb. işlemleri gerçekleştirildikten sonra
        /// entity dönüşümü yapılarak enjekte edilen repository üzerinden veritabanında güncelleme işlemi yapan ve sonuç olarak 
        /// başarılı veya başarısız işlem sonucu dönen method tanımı.
        /// EN: A method definition that, after performing validations, data formatting, and other necessary operations based on 
        /// the provided model as a parameter, transforms it into an entity and performs an update operation into the database 
        /// through the injected repository. The method returns the result as either successful or unsuccessful operation.
        /// </summary>
        /// <returns>Result</returns>
        Result Update(TModel model);

        /// <summary>
        /// TR: Parametre olarak gönderilen id birincil anahtarı değeri üzerinden enjekte edilen repository üzerinden veritabanında
        /// silme işlemi yapan ve sonuç olarak başarılı veya başarısız işlem sonucu dönen method tanımı.
        /// EN: A method definition that takes an id parameter based on the primary key value provided, performs a deletion operation 
        /// in the database through the injected repository. The method returns the result as either a successful or unsuccessful operation.
        /// </summary>
        /// <returns>Result</returns>
        Result Delete(int id);
    }
}
