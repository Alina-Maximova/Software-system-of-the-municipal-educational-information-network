using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseGuide.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Добавляем DbSet для ваших моделей
        public DbSet<EducationalInstitution> EducationalInstitutions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Review> Reviews { get; set; }



        // Конструктор с конфигурацией
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Метод для настройки модели
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Здесь можно добавить конфигурации для ваших сущностей
            modelBuilder.Entity<EducationalInstitution>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<Service>()
                .Property(s => s.Description)
                .HasMaxLength(512);
        }
    }
}