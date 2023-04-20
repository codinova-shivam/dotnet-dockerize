using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using practices.Models;

namespace practices.Data{
    public class ApiContext: IdentityDbContext<ApplicationUser> {

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
            {}

        public DbSet<Blog> Blogs { get; set; }        
    }
}
    
