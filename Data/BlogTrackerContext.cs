using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogEventTracker.Models;

namespace BlogTracker.Data
{
    public class BlogTrackerContext : DbContext
    {
        public BlogTrackerContext (DbContextOptions<BlogTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<BlogEventTracker.Models.AdminInfo> AdminInfo { get; set; } = default!;

        public DbSet<BlogEventTracker.Models.EmpInfo>? EmpInfo { get; set; }

        public DbSet<BlogEventTracker.Models.BlogInfo>? BlogInfo { get; set; }
    }
}
