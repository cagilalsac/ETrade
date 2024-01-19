using Core.Records.Bases;
using System.Linq.Expressions;

namespace Core.Repositories.Bases
{
    /// <summary>
    /// TR: new'lenebilen referans tip olarak TEntity için Record'dan miras alan entity tipini kullanacak ve
    /// ileride oluşturulabilecek tüm repository'ler için temel olarak kullanılabilecek interface.
    /// Dispose işlemleri için IDisposable interface'ini implemente eder.
    /// EN: An interface that can use only types inherited from Record through TEntity as an instantiable reference type.
    /// This interface will be the base for all repositories which may be created in the future and
    /// implements the IDisposable interface for Dispose operations.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepoBase<TEntity> : IDisposable where TEntity : Record, new()
    {
        /// <summary>
        /// TR: Entity tipindeki kayıtları getiren sorgu method tanımı.
        /// EN: A query method definition that retrieves records of type entity.
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// TR: Entity tipindeki kayıtların ilgili tablosuna eklenmesini sağlayan method tanımı.
        /// Eğer save parametresi true gönderilirse entity parametresi ilgili entity kümesine eklenerek
        /// ekleme işlemi veritabanına yansıtılır, save parametresi false gönderilirse
        /// çoklu entity ekleme işlemlerinde önce ilgili entity kümesine entity'ler eklenebilir ve
        /// daha sonra Save methodu çağrılarak bu kümedeki tüm eklemeler tek seferde veritabanına yansıtılabilir.
        /// EN: A method definition for adding records of type Entity to the relevant table. 
        /// If the save parameter is set to true, the entity parameter is added to the corresponding entity set 
        /// and the insert operation is reflected to the database. If the save parameter is set to false, 
        /// in case of multiple entity insertions, entities can first be added to the relevant entity set 
        /// and then the Save method can be called to reflect all the insertions to the database at once.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        void Add(TEntity entity, bool save = true);

        /// <summary>
        /// TR: Entity tipindeki kayıtların ilgili tablosunda güncellenmesini sağlayan method tanımı.
        /// Eğer save parametresi true gönderilirse entity parametresi ilgili entity kümesinde güncellenerek
        /// güncelleme işlemi veritabanına yansıtılır, save parametresi false gönderilirse
        /// çoklu entity güncelleme işlemlerinde önce ilgili entity kümesinde entity'ler güncellenebilir ve
        /// daha sonra Save methodu çağrılarak bu kümedeki tüm güncellemeler tek seferde veritabanına yansıtılabilir.
        /// EN: A method definition for updating records of type Entity in the relevant table. 
        /// If the save parameter is set to true, the entity parameter is updated in the corresponding entity set 
        /// and the update operation is reflected to the database. If the save parameter is set to false, 
        /// in case of multiple entity updates, entities can first be updated in the relevant entity set
        /// and then the Save method can be called to reflect all the updates to the database at once.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        void Update(TEntity entity, bool save = true);

        /// <summary>
        /// TR: Entity tipindeki kayıtların ilgili tablosundan gönderilen koşul parametresi üzerinden silinmesini 
        /// sağlayan method tanımı. Eğer save parametresi true gönderilirse entity ilgili entity kümesinden silinerek 
        /// silme işlemi veritabanına yansıtılır, save parametresi false gönderilirse çoklu entity silme işlemlerinde 
        /// önce ilgili entity kümesinden koşul parametresine uyan entity'ler silinir, daha sonra Save methodu çağrılarak 
        /// bu entity kümesindeki tüm silmeler tek seferde veritabanına yansıtılabilir.
        /// EN: A method definition for deleting records of type Entity from the relevant table based on the provided 
        /// predicate parameter. If the save parameter is set to true, the entity is deleted from the corresponding entity set 
        /// and the deletion operation is reflected to the database. If the save parameter is set to false, 
        /// in case of multiple entity deletions, entities that match the predicate parameter are first deleted from the 
        /// relevant entity set, and then the Save method can be called to reflect all the deletions to the database at once.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="save"></param>
        void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true);

        /// <summary>
        /// TR: Add, Update veya Delete methodları çağrıldıktan sonra entity kümeleri üzerinden yapılan tüm işlemlerin
        /// tek seferde veritabanına yansıtılmasını (Unit of Work) sağlayan method tanımı. Yapılan işlem veya işlemler 
        /// sonucunda ilgili entity tablolarında etkilenen satır sayısını geri döner. İhtiyaç halinde 
        /// exception loglamaları bu method içerisinde gerçekleştirilebilir. 
        /// EN: A method definition that allows all operations performed on entity sets, such as Add, Update, or Delete, 
        /// to be reflected to the database at once (Unit of Work). This method returns the number of affected rows 
        /// in the relevant entity tables as a result of the performed operation or operations. If necessary, 
        /// exception logging can be performed within this method.
        /// </summary>
        /// <returns>int</returns>
        int Save();
    }
}
