using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;

namespace APP.Services
{
    public class CategoryService : Service<Category>, IService<CategoryRequest, CategoryQueryResponse>
    {
        public CategoryService(Db db) : base(db) // DO NOT FORGET TO CHANGE THE CONSTRUCTOR'S PARAMETER from "DbContext db" to "Db db"!
        {
        }

        public List<CategoryQueryResponse> GetList()
        {
            return Query().Select(entity => new CategoryQueryResponse()
            {
                Description = entity.Description,
                Id = entity.Id,
                Name = entity.Name
            }).ToList();
        }

        public CategoryQueryResponse GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Create(CategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Update(CategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
