// İstenirse direkt Program.cs altına kodlar yazılabilir.
// Bu kodlar aslında Program class'ının Main methodu içerisinde çalışır,
// ancak bu şekilde Program class'ını ve Main methodunu oluşturmaya gerek yoktur.
#region Bağımlılıkların Yönetimi (Inversion of Control - IoC)
RepoBase<MyEntity> myRepo = new Repo<MyEntity>();
IMyService myService = new MyService(myRepo);
MyController myController = new MyController(myService);
myController.Index();
#endregion



#region Core Katmanı

    #region Records
    public abstract class RecordBase
    {
    }
    #endregion

    #region DataAccess
    public abstract class RepoBase<TEntity> where TEntity : RecordBase, new()
    {
        public void Query()
        {
            Console.WriteLine("Entity sorgusu DbSet üzerinden oluşturuldu.");
        }
    }
    #endregion

    #region Business
    public interface IService<TModel> where TModel : RecordBase, new()
    {
        void Query();
    }
    #endregion

#endregion



#region DataAccess Katmanı

    #region Entities
    public class MyEntity : RecordBase
    {
    }
    #endregion

    #region Repositories
    public class Repo<TEntity> : RepoBase<TEntity> where TEntity : RecordBase, new()
    {
    }
    #endregion

#endregion



#region Business Katmanı

    #region Models
    public class MyModel : RecordBase
    {
    }
    #endregion

    #region Services
    public interface IMyService : IService<MyModel>
    {
    }

    public class MyService : IMyService
    {
        #region Constructor Dependency Injection
        private readonly RepoBase<MyEntity> _myRepo;

        public MyService(RepoBase<MyEntity> myRepo)
        {
            _myRepo = myRepo;
        }
        #endregion

        public void Query()
        {
            _myRepo.Query();
            Console.WriteLine("Model sorgusu repository üzerinden dönen entity sorgusu üzerinden oluşturuldu.");
        }
    }
    #endregion

#endregion



#region Application (MvcWebUI) veya Restful Servis (WebApi) Katmanları
public class MyController
{
    #region Constructor Dependency Injection
    private readonly IMyService _myService;

    public MyController(IMyService myService)
    {
        _myService = myService;
    }
    #endregion

    public void Index()
    {
        _myService.Query();
        Console.WriteLine("Service üzerinden dönen model sorgusu listeye dönüştürülüp kullanıcıya sunuldu.");
    }
}
#endregion