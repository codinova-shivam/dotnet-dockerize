using System.Collections.Generic;
using System.Threading.Tasks;
using practices.Models;
using practices.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace practices.Repository {
    public class BlogRepository : IBlogRepository {

        private readonly ApiContext _context;
        private readonly IMapper _mapper;


        public BlogRepository(ApiContext context,IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BlogModel>> GetAll(){
            var blogs = await _context.Blogs.ToListAsync();

            return _mapper.Map<List<BlogModel>>(blogs);
        }


        public async Task<BlogModel> Create(BlogModel blogModel){
            var blog = new Blog()
            {
                Title = blogModel.Title
            };

            _context.Blogs.Add(blog);

            await _context.SaveChangesAsync();

            return _mapper.Map<BlogModel>(blog);
        }
    }
}