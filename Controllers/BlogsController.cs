using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using practices.Models;
using practices.Data;
using practices.Repository;


namespace practices.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BlogsController : ControllerBase
    {

        private readonly IBlogRepository _blogRepository;

        public BlogsController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet(Name = "GetAllBlogs")]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _blogRepository.GetAll();
            return Ok(blogs);
        }


        [HttpPost("")]
        public async Task<ActionResult<BlogModel>> Create([FromBody] BlogModel blogModel)
        {
            var blog = await _blogRepository.Create(blogModel);
            return Ok(blog);
        }
    }
}