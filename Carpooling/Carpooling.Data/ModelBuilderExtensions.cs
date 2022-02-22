using Carpooling.Data.Models;
using Carpooling.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Carpooling.Data
{
    public static class ModelBuilderExtensions
    {
        public static IEnumerable<City> Cities { get; }
        public static IEnumerable<Feedback> Feedbacks { get; }
        public static IEnumerable<Travel> Travels { get; }
        public static IEnumerable<User> Users { get; }
        public static IEnumerable<TravelTag> TravelTags { get; }
        public static IEnumerable<Role> Roles { get; }

        static ModelBuilderExtensions()
        {
            Cities = new List<City>()
            {
                new City() { Id = 1, Name = "Plovdiv" },
                new City() { Id = 2, Name = "Sofia" },
                new City() { Id = 3, Name = "Varna" },
                new City() { Id = 4, Name = "Pernik" },
                new City() { Id = 5, Name = "Radomir" },
                new City() { Id = 6, Name = "Montana" },
            };
            Roles = new List<Role>()
            {
                new Role() { Id = 1, Name = "User" },
                new Role() { Id = 2, Name = "Admin" },
            };
            Users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Username = "Gosho23",
                    FirstName = "Georgi",
                    LastName = "Todorov",
                    Email = "gosho.t@gmail.com",
                    Password = "Randompass+12",
                    PhoneNumber = "0923425084",
                    ProfilePictureName = "goshProfilePic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 5,
                    RatingAsPassenger = 5
                }, // user 1 
                new User()
                {
                    Id = 2,
                    Username = "prowrestler11",
                    FirstName = "Rey",
                    LastName = "Misterio",
                    Email = "misteriouswrestler@gmail.com",
                    Password = "Bigmistery*3",
                    PhoneNumber = "0861422101",
                    ProfilePictureName = "wrestlerProfilePic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 4.9,
                    RatingAsPassenger = 4.9
                }, // user 2 
                new User()
                {
                    Id = 3,
                    Username = "invisibleman",
                    FirstName = "John",
                    LastName = "Cena",
                    Email = "cantseeme@gmail.com",
                    Password = "No+vision3",
                    PhoneNumber = "0902115402",
                    ProfilePictureName = "invisiblemanProfilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 4.5,
                    RatingAsPassenger = 4.5
                }, // user 3
                new User()
                {
                    Id = 4,
                    Username = "itsmarto",
                    FirstName = "Martin",
                    LastName = "Simeonov",
                    Email = "martoS23@gmail.com",
                    Password = "marto&12",
                    PhoneNumber = "08881729301",
                    ProfilePictureName = "itsmartoProfilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 5,
                    RatingAsPassenger = 3
                }, // user 4
                new User()
                {
                    Id = 5,
                    Username = "SashkoBeats",
                    FirstName = "Aleks",
                    LastName = "Aleksandrov",
                    Email = "sashko32@gmail.com",
                    Password = "weak^pass23",
                    PhoneNumber = "0920816642",
                    ProfilePictureName = "SashkoBeatsProfilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 0,
                    RatingAsPassenger = 4.7
                }, // user 5
                new User()
                {
                    Id = 6,
                    Username = "lilka405",
                    FirstName = "Lia",
                    LastName = "Nikolova",
                    Email = "nikolova45@gmail.com",
                    Password = "Аleale!5",
                    PhoneNumber = "0878819200",
                    ProfilePictureName = "lilka405Profile.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 0,
                    RatingAsPassenger = 4.1
                }, // user 6
                new User()
                {
                    Id = 7,
                    Username = "emilia85",
                    FirstName = "Emilia",
                    LastName = "Kovacheva",
                    Email = "emi.kovacheva@gmail.com",
                    Password = "Мilkaа#2",
                    PhoneNumber = "0937122884",
                    ProfilePictureName = "emilia85Profilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 0,
                    RatingAsPassenger = 5
                }, // user 7
                new User()
                {
                    Id = 8,
                    Username = "grigs45",
                    FirstName = "Grigor",
                    LastName = "Georgiev",
                    Email = "grigor.g43@gmail.com",
                    Password = "Thegman!2",
                    PhoneNumber = "08222616242",
                    ProfilePictureName = "grigs45Profilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 5,
                    RatingAsPassenger = 3.3
                }, // user 8
                new User()
                {
                    Id = 9,
                    Username = "saintpaf",
                    FirstName = "Pavel",
                    LastName = "Krustev",
                    Email = "p.krustev@gmail.com",
                    Password = "Thepass$2",
                    PhoneNumber = "0977800152",
                    ProfilePictureName = "saintpafProfilepic.jpeg",
                    UserStatus = UserStatus.Blocked,
                    RatingAsDriver = 0,
                    RatingAsPassenger = 2.5
                }, // user 9, blocked
                new User()
                {
                    Id = 10,
                    Username = "kriso29",
                    FirstName = "Kristian",
                    LastName = "Damqnov",
                    Email = "kris.d@gmail.com",
                    Password = "Passman^4",
                    PhoneNumber = "0728633050",
                    ProfilePictureName = "kriso29ProfilePic.jpg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 5,
                    RatingAsPassenger = 3.1
                }, // user 10
                new User()
                {
                    Id = 11,
                    Username = "partyman",
                    FirstName = "Oleg",
                    LastName = "Borisov",
                    Email = "oleg.borisov@gmail.com",
                    Password = "Partytime!4",
                    PhoneNumber = "0800302072",
                    ProfilePictureName = "partymanProfilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 0,
                    RatingAsPassenger = 3.9
                }, // user 11
                new User()
                {
                    Id = 12,
                    Username = "admin1",
                    FirstName = "Ivan",
                    LastName = "Kirilov",
                    Email = "vankata.kirilov@gmail.com",
                    Password = "Theadmin-1",
                    PhoneNumber = "0234517022",
                    ProfilePictureName = "admin1Profilepic.jpg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 0,
                    RatingAsPassenger = 0
                }, // admin 12
                new User()
                {
                    Id = 13,
                    Username = "coconutmaster72",
                    FirstName = "Valeri",
                    LastName = "Bojinov",
                    Email = "bojinkata@gmail.com",
                    Password = "Malkokote&23",
                    PhoneNumber = "0814171920",
                    ProfilePictureName = "coconutmaster72Profilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 0,
                    RatingAsPassenger = 0
                }, // admin 13
                new User()
                {
                    Id = 14,
                    Username = "demoUser",
                    FirstName = "Stamat",
                    LastName = "Stamatov",
                    Email = "notstamatjoke@gmail.com",
                    Password = "Demopass*2",
                    PhoneNumber = "0939452370",
                    ProfilePictureName = "demoUserProfilepic.jpeg",
                    UserStatus = UserStatus.Active,
                    RatingAsDriver = 5,
                    RatingAsPassenger = 4.5
                }, // stamat a.k.a. user for demo
            };
            Travels = new List<Travel>()
            {
                new Travel()
                {
                    Id = 1,
                    StartPointCityId = 2,
                    EndPointCityId = 1,
                    DepartureTime = new DateTime(2022, 2, 20, 18, 0, 0),
                    DriverId = 1,
                    FreeSpots = 3,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 2,
                    StartPointCityId = 2,
                    EndPointCityId = 1,
                    DepartureTime = new DateTime(2022, 1, 20, 18, 0, 0),
                    DriverId = 2,
                    FreeSpots = 2,
                    IsCompleted = true,
                },
                new Travel()
                {
                    Id = 3,
                    StartPointCityId = 1,
                    EndPointCityId = 2,
                    DepartureTime = new DateTime(2022, 3, 20, 18, 0, 0),
                    DriverId = 3,
                    FreeSpots = 4,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 4,
                    StartPointCityId = 2,
                    EndPointCityId = 1,
                    DepartureTime = new DateTime(2021, 12, 20, 18, 0, 0),
                    DriverId = 4,
                    FreeSpots = 0,
                    IsCompleted = true,
                },
                new Travel()
                {
                    Id = 5,
                    StartPointCityId = 3,
                    EndPointCityId = 5,
                    DepartureTime = new DateTime(2022, 1, 10, 10, 50, 00),
                    DriverId = 14,
                    FreeSpots = 4,
                    IsCompleted = true,
                },
                new Travel()
                {
                    Id = 6,
                    StartPointCityId = 1,
                    EndPointCityId = 6,
                    DepartureTime = new DateTime(2022, 2, 11, 15, 40, 00),
                    DriverId = 5,
                    FreeSpots = 2,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 7,
                    StartPointCityId = 6,
                    EndPointCityId = 2,
                    DepartureTime = new DateTime(2021, 12, 22, 23, 30, 00),
                    DriverId = 6,
                    FreeSpots = 4,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 8,
                    StartPointCityId = 5,
                    EndPointCityId = 1,
                    DepartureTime = new DateTime(2021, 12, 18, 9, 30, 00),
                    DriverId = 7,
                    FreeSpots = 3,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 9,
                    StartPointCityId = 4,
                    EndPointCityId = 2,
                    DepartureTime = new DateTime(2021, 12, 15, 12, 00, 00),
                    DriverId = 8,
                    FreeSpots = 5,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 10,
                    StartPointCityId = 3,
                    EndPointCityId = 6,
                    DepartureTime = new DateTime(2022, 1, 8, 10, 00, 00),
                    DriverId = 10,
                    FreeSpots = 2,
                    IsCompleted = true,
                },
                new Travel()
                {
                    Id = 11,
                    StartPointCityId = 4,
                    EndPointCityId = 6,
                    DepartureTime = new DateTime(2021, 12, 11, 17, 00, 00),
                    DriverId = 14,
                    FreeSpots = 1,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 12,
                    StartPointCityId = 5,
                    EndPointCityId = 3,
                    DepartureTime = new DateTime(2022, 3, 6, 13, 40, 00),
                    DriverId = 14,
                    FreeSpots = 4,
                    IsCompleted = false,
                },
                new Travel()
                {
                    Id = 13,
                    StartPointCityId = 5,
                    EndPointCityId = 2,
                    DepartureTime = new DateTime(2022, 2, 15, 13, 00, 00),
                    DriverId = 14,
                    FreeSpots = 4,
                    IsCompleted = false,
                }
            };
            Feedbacks = new List<Feedback>()
            {
                // travel 1
                // in travel 1 the passengers are wtih ids: 2,3,4
                new Feedback()
                {
                    Id = 1,
                    Comment = "Amazing person. It was cool to travel with you.",
                    Rating = 4.9,
                    TravelId = 1,
                    Type = FeedbackType.Driver,
                    UserFromId = 1,
                    UserToId = 2
                },
                new Feedback()
                {
                    Id = 2,
                    Comment = "Very quiet.",
                    Rating = 4.5,
                    TravelId = 1,
                    Type = FeedbackType.Driver,
                    UserFromId = 1,
                    UserToId = 3
                },
                new Feedback()
                {
                    Id = 3,
                    Comment = "Please do not try to bypass the rules and smoke when someone says it is forbidden.",
                    Rating = 3,
                    TravelId = 1,
                    Type = FeedbackType.Driver,
                    UserFromId = 1,
                    UserToId = 4
                },
                // from passengers
                new Feedback()
                {
                    Id = 4,
                    Comment = "Drives very safely.",
                    Rating = 5,
                    TravelId = 1,
                    Type = FeedbackType.Passenger,
                    UserFromId = 2,
                    UserToId = 1
                },
                new Feedback()
                {
                    Id = 5,
                    Comment = "Very chill.",
                    Rating = 5,
                    TravelId = 1,
                    Type = FeedbackType.Passenger,
                    UserFromId = 3,
                    UserToId = 1
                },
                new Feedback()
                {
                    Id = 6,
                    Comment = "Drove us safely to our destination.",
                    Rating = 5,
                    TravelId = 1,
                    Type = FeedbackType.Passenger,
                    UserFromId = 4,
                    UserToId = 1
                },
                // travel 2
                // in travel 2 the passengers are wtih ids: 1,5
                new Feedback()
                {
                    Id = 7,
                    Comment = "It's awesome to have him as company during a travel.",
                    Rating = 5,
                    TravelId = 2,
                    Type = FeedbackType.Driver,
                    UserFromId = 2,
                    UserToId = 1
                },
                new Feedback()
                {
                    Id = 8,
                    Comment = "Had a good laugh with you around.",
                    Rating = 4.7,
                    TravelId = 2,
                    Type = FeedbackType.Driver,
                    UserFromId = 2,
                    UserToId = 5
                },
                // from passengers
                new Feedback()
                {
                    Id = 9,
                    Comment = "An awesome person and an awesome driver.",
                    Rating = 4.9,
                    TravelId = 2,
                    Type = FeedbackType.Passenger,
                    UserFromId = 1,
                    UserToId = 2
                },
                new Feedback()
                {
                    Id = 10,
                    Comment = "Such a good driver, it's like he came out of Fast & Furious.",
                    Rating = 4.9,
                    TravelId = 2,
                    Type = FeedbackType.Passenger,
                    UserFromId = 5,
                    UserToId = 2
                },
                //travel 3
                // in travel 3 the passengers are wtih ids: 6,7,8,9
                new Feedback()
                {
                    Id = 11,
                    Comment = "Sometimes she complains a lot but other than that she was quite nice.",
                    Rating = 4.1,
                    TravelId = 3,
                    Type = FeedbackType.Driver,
                    UserFromId = 3,
                    UserToId = 6
                },
                new Feedback()
                {
                    Id = 12,
                    Comment = null,
                    Rating = 5,
                    TravelId = 3,
                    Type = FeedbackType.Driver,
                    UserFromId = 3,
                    UserToId = 7
                },
                new Feedback()
                {
                    Id = 13,
                    Comment = "Very noisy and sometimes even iritating.",
                    Rating = 3.3,
                    TravelId = 3,
                    Type = FeedbackType.Driver,
                    UserFromId = 3,
                    UserToId = 8
                },
                // from passengers
                new Feedback()
                {
                    Id = 14,
                    Comment = null,
                    Rating = 4.5,
                    TravelId = 3,
                    Type = FeedbackType.Passenger,
                    UserFromId = 6,
                    UserToId = 3
                },
                new Feedback()
                {
                    Id = 15,
                    Comment = "Provided a pleasant and fast ride.",
                    Rating = 4.5,
                    TravelId = 3,
                    Type = FeedbackType.Passenger,
                    UserFromId = 7,
                    UserToId = 3
                },
                new Feedback()
                {
                    Id = 16,
                    Comment = "Can't believe I was in a ride with John Cena.",
                    Rating = 4.5,
                    TravelId = 3,
                    Type = FeedbackType.Passenger,
                    UserFromId = 8,
                    UserToId = 3
                },
                // travel 4
                // in travel 4 the passengers are wtih ids: 10,11
                new Feedback()
                {
                    Id = 17,
                    Comment = "Brought a pet even though it wasn't allowed. At least the pet was cute.",
                    Rating = 3.1,
                    TravelId = 4,
                    Type = FeedbackType.Driver,
                    UserFromId = 4,
                    UserToId = 10
                },
                new Feedback()
                {
                    Id = 18,
                    Comment = null,
                    Rating = 5,
                    TravelId = 4,
                    Type = FeedbackType.Passenger,
                    UserFromId = 10,
                    UserToId = 4
                },
                new Feedback()
                {
                    Id = 19,
                    Comment = "Had a good time on the trip with you",
                    Rating = 5,
                    TravelId = 4,
                    Type = FeedbackType.Passenger,
                    UserFromId = 14,
                    UserToId = 4
                },
                new Feedback()
                {
                    Id = 20,
                    Comment = "Good stuff my guy",
                    Rating = 5,
                    TravelId = 4,
                    Type = FeedbackType.Driver,
                    UserFromId = 14,
                    UserToId = 2
                },
                new Feedback()
                {
                    Id = 21,
                    Comment = "Cool",
                    Rating = 5,
                    TravelId = 5,
                    Type = FeedbackType.Driver,
                    UserFromId = 14,
                    UserToId = 1
                },
                new Feedback()
                {
                    Id = 22,
                    Comment = "10/10 best stamat",
                    Rating = 5,
                    TravelId = 5,
                    Type = FeedbackType.Passenger,
                    UserFromId = 1,
                    UserToId = 14
                },
                new Feedback()
                {
                    Id = 23,
                    Comment = "Stamat is the best",
                    Rating = 5,
                    TravelId = 5,
                    Type = FeedbackType.Passenger,
                    UserFromId = 2,
                    UserToId = 14
                },
                new Feedback()
                {
                    Id = 24,
                    Comment = "Stamat rules",
                    Rating = 5,
                    TravelId = 5,
                    Type = FeedbackType.Passenger,
                    UserFromId = 3,
                    UserToId = 14
                },
                new Feedback()
                {
                    Id = 25,
                    Comment = "Had a good time on the trip with you",
                    Rating = 5,
                    TravelId = 4,
                    Type = FeedbackType.Passenger,
                    UserFromId = 11,
                    UserToId = 10
                },
                new Feedback()
                {
                    Id = 26,
                    Comment = "Had a good time on the trip with you",
                    Rating = 5,
                    TravelId = 4,
                    Type = FeedbackType.Passenger,
                    UserFromId = 10,
                    UserToId = 8
                }
                // user 11 hasn't left a feedback will be used as a manual test
            };
            TravelTags = new List<TravelTag>()
            {
                new TravelTag() { Id = 1, Tag = "No smoking" },
                new TravelTag() { Id = 2, Tag = "No luggage" },
                new TravelTag() { Id = 3, Tag = "No pets" },
                new TravelTag() { Id = 4, Tag = "No eating" },
                new TravelTag() { Id = 5, Tag = "No children" },
                new TravelTag() { Id = 6, Tag = "No stops along the way" }
            };
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(Cities);
            modelBuilder.Entity<Role>().HasData(Roles);
            modelBuilder.Entity<User>().HasData(Users);
            modelBuilder.Entity<TravelTag>().HasData(TravelTags);
            modelBuilder.Entity<Travel>().HasData(Travels);
            modelBuilder.Entity<Feedback>().HasData(Feedbacks);
            // seeding many to many between users and roles
            modelBuilder.Entity<User>()
                        .HasMany(user => user.Roles)
                        .WithMany(role => role.Users)
                        .UsingEntity(userRole => userRole.HasData(new { UsersId = 1, RolesId = 1 },
                                                                  new { UsersId = 2, RolesId = 1 },
                                                                  new { UsersId = 3, RolesId = 1 },
                                                                  new { UsersId = 4, RolesId = 1 },
                                                                  new { UsersId = 5, RolesId = 1 },
                                                                  new { UsersId = 6, RolesId = 1 },
                                                                  new { UsersId = 7, RolesId = 1 },
                                                                  new { UsersId = 8, RolesId = 1 },
                                                                  new { UsersId = 9, RolesId = 1 },
                                                                  new { UsersId = 10, RolesId = 1 },
                                                                  new { UsersId = 11, RolesId = 1 },
                                                                  new { UsersId = 12, RolesId = 1 },
                                                                  new { UsersId = 13, RolesId = 1 },
                                                                  new { UsersId = 12, RolesId = 2 },
                                                                  new { UsersId = 13, RolesId = 2 },
                                                                  new { UsersId = 14, RolesId = 1 }));
            // many to many between users and travels in which they are passengers
            modelBuilder.Entity<User>()
                        .HasMany(user => user.TravelsAsPassenger)
                        .WithMany(travel => travel.Passengers)
                        .UsingEntity(userTravel => userTravel.HasData(new { PassengersId = 1, TravelsAsPassengerId = 2 },
                                                                      new { PassengersId = 2, TravelsAsPassengerId = 1 },
                                                                      new { PassengersId = 3, TravelsAsPassengerId = 1 },
                                                                      new { PassengersId = 4, TravelsAsPassengerId = 1 },
                                                                      new { PassengersId = 5, TravelsAsPassengerId = 2 },
                                                                      new { PassengersId = 6, TravelsAsPassengerId = 3 },
                                                                      new { PassengersId = 7, TravelsAsPassengerId = 3 },
                                                                      new { PassengersId = 8, TravelsAsPassengerId = 3 },
                                                                      new { PassengersId = 1, TravelsAsPassengerId = 5 },
                                                                      new { PassengersId = 2, TravelsAsPassengerId = 5 },
                                                                      new { PassengersId = 3, TravelsAsPassengerId = 5 },
                                                                      new { PassengersId = 4, TravelsAsPassengerId = 5 },
                                                                      new { PassengersId = 4, TravelsAsPassengerId = 12 },
                                                                      new { PassengersId = 5, TravelsAsPassengerId = 12 },
                                                                      new { PassengersId = 3, TravelsAsPassengerId = 13 },
                                                                      new { PassengersId = 5, TravelsAsPassengerId = 13 },
                                                                      new { PassengersId = 14, TravelsAsPassengerId = 10 },
                                                                      new { PassengersId = 11, TravelsAsPassengerId = 10 },
                                                                      new { PassengersId = 14, TravelsAsPassengerId = 9 },
                                                                      new { PassengersId = 10, TravelsAsPassengerId = 9 }));
            // many to mnay between users and travels to which they have applied
            modelBuilder.Entity<User>()
                        .HasMany(user => user.AppliedTravels)
                        .WithMany(travel => travel.ApplyingPassengers)
                        .UsingEntity(userTravel1 => userTravel1.HasData(new { ApplyingPassengersId = 9, AppliedTravelsId = 3 },
                                                                        new { ApplyingPassengersId = 1, AppliedTravelsId = 13 },
                                                                        new { ApplyingPassengersId = 2, AppliedTravelsId = 13 },
                                                                        new { ApplyingPassengersId = 2, AppliedTravelsId = 12 },
                                                                        new { ApplyingPassengersId = 3, AppliedTravelsId = 12 },
                                                                        new { ApplyingPassengersId = 14, AppliedTravelsId = 10 },
                                                                        new { ApplyingPassengersId = 14, AppliedTravelsId = 9 }));
            // seeding many to many between travels and traveltags
            modelBuilder.Entity<Travel>()
                        .HasMany(travel => travel.TravelTags)
                        .WithMany(travelTag => travelTag.Travels)
                        .UsingEntity(travelTravelTags => travelTravelTags.HasData(new { TravelsId = 1, TravelTagsId = 1 },
                                                                                  new { TravelsId = 1, TravelTagsId = 2 },
                                                                                  new { TravelsId = 1, TravelTagsId = 3 },
                                                                                  new { TravelsId = 2, TravelTagsId = 3 },
                                                                                  new { TravelsId = 2, TravelTagsId = 4 },
                                                                                  new { TravelsId = 3, TravelTagsId = 1 },
                                                                                  new { TravelsId = 3, TravelTagsId = 3 },
                                                                                  new { TravelsId = 3, TravelTagsId = 4 },
                                                                                  new { TravelsId = 4, TravelTagsId = 1 },
                                                                                  new { TravelsId = 4, TravelTagsId = 3 },
                                                                                  new { TravelsId = 5, TravelTagsId = 1 },
                                                                                  new { TravelsId = 6, TravelTagsId = 2 },
                                                                                  new { TravelsId = 7, TravelTagsId = 3 },
                                                                                  new { TravelsId = 8, TravelTagsId = 3 },
                                                                                  new { TravelsId = 9, TravelTagsId = 4 },
                                                                                  new { TravelsId = 10, TravelTagsId = 1 },
                                                                                  new { TravelsId = 11, TravelTagsId = 3 },
                                                                                  new { TravelsId = 12, TravelTagsId = 4 },
                                                                                  new { TravelsId = 13, TravelTagsId = 1 }));
        }
    }
}