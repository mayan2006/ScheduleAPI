using Microsoft.EntityFrameworkCore;
using Schedule.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Data
{
    public class DataContext:DbContext
    {
        public DbSet<Student> studentsList { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Uniform> uniforms { get; set; }

        public DataContext(DbContextOptions<DataContext> options):base(options) {} 


      
    }
}
