using System.Collections.Generic;
using System.Threading.Tasks;
using practices.Models;

namespace practices.Repository {
    public interface IBlogRepository {

        Task<List<BlogModel>> GetAll();

        Task<BlogModel> Create(BlogModel blogModel);
        
    }
}