using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataBaseContext :DbContext
    {
        // Provider/connection configuration is intentionally done in Program.cs (DI).
        // This keeps the context portable for both MySQL and SQL Server setups.
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments {get; set;}
        public DbSet<Guide> Guides { get; set;}
        public DbSet<User>Users { get; set;}

        public DbSet<VideoChapter> VideoChapters { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Guide>().ToTable("Guides");
            modelBuilder.Entity<User>().ToTable("Users").HasData(
                new User { 
                    Id=1,
                    Email ="admin@admin.com", 
                    FirstName="admin", 
                    LastName="admin", 
                    Password="adm1234", 
                    UserName="adm",
                    IsAdmin=true,
                    IsActive=true, 
                    CreatedDate=DateTime.MinValue});
            base.OnModelCreating(modelBuilder);
        }

        /*
        // Optional SQL Server setup example (for contributors who continue with MSSQL):
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DenerMakine;Integrated Security=true");
        //     base.OnConfiguring(optionsBuilder);
        // }
        */

    }
}
