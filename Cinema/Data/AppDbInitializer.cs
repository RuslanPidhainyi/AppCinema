using AppCinema.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCinema.Data
{
    public class AppDbInitializer
    {
        //Seeding your DB (wypewnienie BazyDanych)
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {   
                //context - this is DataBase. 
                //sending data to the DB or receiving data from the DB  
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();



                //Cinema
                if(!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRl6FH-8gOI_JfqQ3PGSD3UeAjxj07YVcgEA&usqp=CAU",
                            Description = "This is desription of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 2",
                            Logo = "https://images-platform.99static.com//y4omlb4-isR_-uTDo7t2ydxuuWY=/180x206:833x859/fit-in/500x500/99designs-contests-attachments/124/124765/attachment_124765740",
                            Description = "This is desription of the seccond cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 3",
                            Logo="https://scalebranding.com/wp-content/uploads/2021/02/c-cinema-logo.jpg",
                            Description="This is desription of the third cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 4",
                            Logo = "https://media.istockphoto.com/id/1174620670/vector/fast-movie-symbol-cinema-symbol-design-template.jpg?s=612x612&w=0&k=20&c=GuU5A6hOmHlwc1iayHpFQ8iLXrJ0WC_34k9TujJQ8_4=",
                            Description = "This is desription of the fourth cinema",
                        },
                        new Cinema()
                        {
                            Name = "Cinema 5",
                            Logo = "https://dynamic.brandcrowd.com/asset/logo/3783f1e2-5e96-4e5f-b5e1-9d696166423d/logo-search-grid-1x?logoTemplateVersion=1&v=637607022951470000",
                            Description = "This is desription of the fifth cinema"
                        }
                    });

                    context.SaveChanges();

                }
                //Actors
                if(!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor 1",
                            Bio = "This is Bio of the first actor",
                            ProfilePictureURL = "https://www.nicepng.com/png/detail/153-1531345_ocean-waves-actor-logo.png"
                        },

                        new Actor()
                        {
                            FullName = "Actor 2",
                            Bio = "This is Bio of the seccond actor",
                            ProfilePictureURL = "https://www.nicepng.com/png/detail/153-1531345_ocean-waves-actor-logo.png"
                        },

                        new Actor()
                        {
                            FullName = "Actor 3",
                            Bio = "This is Bio of the third actor",
                            ProfilePictureURL = "https://www.nicepng.com/png/detail/153-1531345_ocean-waves-actor-logo.png"
                        },

                        new Actor()
                        {
                            FullName = "Actor 4",
                            Bio = "This is Bio of the fourth actor",
                            ProfilePictureURL = "https://www.nicepng.com/png/detail/153-1531345_ocean-waves-actor-logo.png"
                        },

                        new Actor()
                        {
                            FullName = "Actor 5",
                            Bio = "This is Bio of the fifth actor",
                            ProfilePictureURL = "https://www.nicepng.com/png/detail/153-1531345_ocean-waves-actor-logo.png"
                        }
                    });

                    context.SaveChanges();
                }
                //Producers
                if(!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    { 
                        new Producer()
                        {
                            FullName = "Produser 1",
                            Bio = "This is Bio of the first produser",
                            ProfilePictureURL = "https://cdn.dribbble.com/users/230290/screenshots/3852257/media/85c96a9bd387b439bc9e2897ae0a573f.jpg?compress=1&resize=400x300&vertical=top"
                        },

                        new Producer()
                        {
                            FullName = "Produser 2",
                            Bio = "This is Bio of the first produser",
                            ProfilePictureURL = "https://cdn.dribbble.com/users/230290/screenshots/3852257/media/85c96a9bd387b439bc9e2897ae0a573f.jpg?compress=1&resize=400x300&vertical=top"
                        },

                        new Producer()
                        {
                            FullName = "Produser 3",
                            Bio = "This is Bio of the first produser",
                            ProfilePictureURL = "https://cdn.dribbble.com/users/230290/screenshots/3852257/media/85c96a9bd387b439bc9e2897ae0a573f.jpg?compress=1&resize=400x300&vertical=top"
                        },

                        new Producer()
                        {
                            FullName = "Produser 4",
                            Bio = "This is Bio of the first produser",
                            ProfilePictureURL = "https://cdn.dribbble.com/users/230290/screenshots/3852257/media/85c96a9bd387b439bc9e2897ae0a573f.jpg?compress=1&resize=400x300&vertical=top"
                        },
                        new Producer()
                        {
                            FullName = "Produser 5",
                            Bio = "This is Bio of the first produser",
                            ProfilePictureURL = "https://cdn.dribbble.com/users/230290/screenshots/3852257/media/85c96a9bd387b439bc9e2897ae0a573f.jpg?compress=1&resize=400x300&vertical=top"
                        }


                    });

                    context.SaveChanges();


                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Pianista",
                            Description = "This is the Pianasta movie description",
                            Price = 10,
                            ImageURL = "https://fwcdn.pl/fpo/22/25/32225/7519150.3.jpg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = Enums.MovieCategory.Dramy

                        },

                        new Movie()
                        {
                            Name = "Кіборги(Heroes never die)",
                            Description = "This is the Кіборги(Heroes never die) movie description",
                            Price = 12,
                            ImageURL = "https://upload.wikimedia.org/wikipedia/uk/thumb/8/8f/Kiborhy_%28Ukr_poster_-1%2C_2017%29.jpg/250px-Kiborhy_%28Ukr_poster_-1%2C_2017%29.jpg",
                            StartDate = DateTime.Now.AddDays(7),
                            EndDate = DateTime.Now.AddDays(15),
                            CinemaId = 2,
                            ProducerId = 3,
                            MovieCategory = Enums.MovieCategory.Military

                        },

                        new Movie()
                        {
                            Name = "Fast and furious 3",
                            Description = "This is the Fast and furious 3 movie description",
                            Price = 19,
                            ImageURL = "https://m.media-amazon.com/images/I/817SAcsGb1L.jpg",
                            StartDate = DateTime.Now.AddDays(9),
                            EndDate = DateTime.Now.AddDays(21),
                            CinemaId = 3,
                            ProducerId = 4,
                            MovieCategory = Enums.MovieCategory.Sports

                        },

                        new Movie()
                        {
                            Name = "The Place Beyond the Pines",
                            Description = "This is the The Place Beyond the Pines movie description",
                            Price = 11,
                            ImageURL = "https://m.media-amazon.com/images/M/MV5BMjc1OTEwNjU4N15BMl5BanBnXkFtZTcwNzUzNDIwOQ@@._V1_FMjpg_UX1000_.jpg",
                            StartDate = DateTime.Now.AddDays(1),
                            EndDate = DateTime.Now.AddDays(19),
                            CinemaId = 4,
                            ProducerId = 1,
                            MovieCategory = Enums.MovieCategory.Dramy

                        },

                        new Movie()
                        {
                            Name = "The Ring",
                            Description = "This is the The Ring movie description",
                            Price = 11,
                            ImageURL = "https://fwcdn.pl/fpo/45/65/34565/7531778.3.jpg",
                            StartDate = DateTime.Now.AddDays(1),
                            EndDate = DateTime.Now.AddDays(19),
                            CinemaId = 5,
                            ProducerId = 2,
                            MovieCategory = Enums.MovieCategory.Horror

                        }


                    });
                    context.SaveChanges();
                }
                //Actors_Movies
                if(!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie() 
                        {
                            ActorId = 1,
                            MovieId = 1,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 1,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 1,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 1,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 1,
                        },



                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 2,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 2,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 2,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 2,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 2,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 3,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 3,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 3,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 3,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 3,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 4,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 4,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 4,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 4,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 4,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 5,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 5,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 5,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 5,
                        },

                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 5,
                        }


                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
