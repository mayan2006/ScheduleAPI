using Microsoft.EntityFrameworkCore;
using Schedule.Core.Model;

namespace Schedule.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Student> studentsList { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Uniform> uniforms { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(student => student.Class)
                .WithMany(schoolClass => schoolClass.Students)
                .HasForeignKey(student => student.ClassId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Student>()
                .HasOne(student => student.Uniform)
                .WithOne(uniform => uniform.Student)
                .HasForeignKey<Uniform>(uniform => uniform.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Uniform>()
                .HasIndex(uniform => uniform.StudentId)
                .IsUnique();

            modelBuilder.Entity<Class>()
                .HasMany(schoolClass => schoolClass.Teachers)
                .WithMany(teacher => teacher.Classes);
        }
    }
}
