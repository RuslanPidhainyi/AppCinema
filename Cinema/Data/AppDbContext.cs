using AppCinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 

namespace AppCinema.Data
{                               /*
                                 Wyfowalismy biblioteku "IdentityDbContext" i w parametry wrzucili obiekt klass <ApplicationUser> 
                                 <ApplicationUser> - ten obiekt klass przychowuje tylko jedne property - FullName.
                                 Propertis "FullName" - zostanie dodane, do naszej biblioteki "IdentityDbContext", za pomocą migracji pod nazwą "20230723133109_Identity_Adde.cs"
                                 */
    public class AppDbContext : IdentityDbContext<ApplicationUser> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor_Movie> Actors_Movies { get; set; }
         
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Producer> Producers { get; set; }


        //Ordered relation tables
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }



        //Many - to - many
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            
            modelBuilder.Entity<Actor_Movie>()
                        .HasOne(m => m.Movie)   
                        .WithMany(am => am.Actors_Movies)
                        .HasForeignKey(m =>  m.MovieId);

            modelBuilder.Entity<Actor_Movie>()
                        .HasOne(a => a.Actor)
                        .WithMany(am => am.Actors_Movies)
                        .HasForeignKey(a => a.ActorId);

            base.OnModelCreating(modelBuilder); //automatically determines(определяет) identifier
        }
    }
}
