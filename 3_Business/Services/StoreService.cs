using Core.Services.Bases;
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IStoreService : IService<StoreModel>
    {
    }

    public class StoreService : IStoreService
    {
        private readonly RepoBase<Store> _storeRepo;
        private readonly RepoBase<ProductStore> _productStoreRepo;

        public StoreService(RepoBase<Store> storeRepo, RepoBase<ProductStore> productStoreRepo)
        {
            _storeRepo = storeRepo;
            _productStoreRepo = productStoreRepo;
        }

        public Result Add(StoreModel model)
        {
            // bir mağaza adı üzerinden hem sanal hem de sanal olmayan mağaza oluşturulabilmesini sağlayabilmek
            // ve bir mağazanın hem adı hem de sanal mı verisi üzerinden tek bir kaydının olabilmesi için
            // aşağıda Store Name ve IsVirtual üzerinden validasyon yapıyoruz
            if (Query().Any(s => s.Name.ToLower() == model.Name.ToLower().Trim() && s.IsVirtual == model.IsVirtual))
                return new ErrorResult("Store can't be added because store with the same name exists!");

            var entity = new Store()
            {
                IsVirtual = model.IsVirtual,
                Name = model.Name.Trim()
            };
            _storeRepo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            _productStoreRepo.Delete(s => s.StoreId == id);
            _storeRepo.Delete(s => s.Id == id);
            return new SuccessResult();
        }

        public void Dispose()
        {
            _storeRepo.Dispose();
        }

        public IQueryable<StoreModel> Query()
        {
            return _storeRepo.Query().OrderBy(s => s.IsVirtual).ThenBy(s => s.Name).Select(s => new StoreModel()
            {
                Id = s.Id,
                Name = s.Name,
                IsVirtual = s.IsVirtual,
                VirtualDisplay = s.IsVirtual ? "Yes" : "No"
            });
        }

        public Result Update(StoreModel model)
        {
            // bir mağaza adı üzerinden hem sanal hem de sanal olmayan mağaza oluşturulabilmesini sağlayabilmek
            // ve bir mağazanın hem adı hem de sanal mı verisi üzerinden tek bir kaydının olabilmesi için
            // aşağıda Store Name ve IsVirtual üzerinden validasyon yapıyoruz
            if (Query().Any(s => s.Name.ToLower() == model.Name.ToLower().Trim() && s.IsVirtual == model.IsVirtual && s.Id != model.Id))
                return new ErrorResult("Store can't be updated because store with the same name exists!");

            var entity = new Store()
            {
                Id = model.Id,
                IsVirtual = model.IsVirtual,
                Name = model.Name.Trim()
            };
            _storeRepo.Update(entity);
            return new SuccessResult();
        }
    }
}
