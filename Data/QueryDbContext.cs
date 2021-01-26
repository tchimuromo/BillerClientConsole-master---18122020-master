using BillerClientConsole.Models.QueryModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Data
{
    public class QueryDbContext:DbContext
    {
        public QueryDbContext()
        {
        }

        public QueryDbContext(DbContextOptions<QueryDbContext> options):base(options)
        {

        }
        public DbSet<Queries> Queries { get; set; }
        public DbSet<QueryHistory> QueryHistory { get; set; }
    }
}
