using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Carpooling.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    RatingAsDriver = table.Column<double>(type: "float", nullable: false),
                    RatingAsPassenger = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartPointCityId = table.Column<int>(type: "int", nullable: false),
                    EndPointCityId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FreeSpots = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Travels_Cities_EndPointCityId",
                        column: x => x.EndPointCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Travels_Cities_StartPointCityId",
                        column: x => x.StartPointCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Travels_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<double>(type: "float", maxLength: 5, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TravelId = table.Column<int>(type: "int", nullable: false),
                    UserFromId = table.Column<int>(type: "int", nullable: false),
                    UserToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Travels_TravelId",
                        column: x => x.TravelId,
                        principalTable: "Travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserFromId",
                        column: x => x.UserFromId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserToId",
                        column: x => x.UserToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelTravelTag",
                columns: table => new
                {
                    TravelTagsId = table.Column<int>(type: "int", nullable: false),
                    TravelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelTravelTag", x => new { x.TravelTagsId, x.TravelsId });
                    table.ForeignKey(
                        name: "FK_TravelTravelTag_Travels_TravelsId",
                        column: x => x.TravelsId,
                        principalTable: "Travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelTravelTag_TravelTags_TravelTagsId",
                        column: x => x.TravelTagsId,
                        principalTable: "TravelTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelUser",
                columns: table => new
                {
                    AppliedTravelsId = table.Column<int>(type: "int", nullable: false),
                    ApplyingPassengersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelUser", x => new { x.AppliedTravelsId, x.ApplyingPassengersId });
                    table.ForeignKey(
                        name: "FK_TravelUser_Travels_AppliedTravelsId",
                        column: x => x.AppliedTravelsId,
                        principalTable: "Travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelUser_Users_ApplyingPassengersId",
                        column: x => x.ApplyingPassengersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelUser1",
                columns: table => new
                {
                    PassengersId = table.Column<int>(type: "int", nullable: false),
                    TravelsAsPassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelUser1", x => new { x.PassengersId, x.TravelsAsPassengerId });
                    table.ForeignKey(
                        name: "FK_TravelUser1_Travels_TravelsAsPassengerId",
                        column: x => x.TravelsAsPassengerId,
                        principalTable: "Travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelUser1_Users_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Plovdiv" },
                    { 2, "Sofia" },
                    { 3, "Varna" },
                    { 4, "Pernik" },
                    { 5, "Radomir" },
                    { 6, "Montana" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "TravelTags",
                columns: new[] { "Id", "Tag" },
                values: new object[,]
                {
                    { 5, "No children" },
                    { 4, "No eating" },
                    { 6, "No stops along the way" },
                    { 2, "No luggage" },
                    { 1, "No smoking" },
                    { 3, "No pets" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "ProfilePictureName", "RatingAsDriver", "RatingAsPassenger", "UserStatus", "Username" },
                values: new object[,]
                {
                    { 12, "vankata.kirilov@gmail.com", "Ivan", "Kirilov", "Theadmin-1", "0234517022", "admin1Profilepic.jpg", 0.0, 0.0, 0, "admin1" },
                    { 11, "oleg.borisov@gmail.com", "Oleg", "Borisov", "Partytime!4", "0800302072", "partymanProfilepic.jpeg", 0.0, 3.8999999999999999, 0, "partyman" },
                    { 10, "kris.d@gmail.com", "Kristian", "Damqnov", "Passman^4", "0728633050", "kriso29ProfilePic.jpg", 5.0, 3.1000000000000001, 0, "kriso29" },
                    { 9, "p.krustev@gmail.com", "Pavel", "Krustev", "Thepass$2", "0977800152", "saintpafProfilepic.jpeg", 0.0, 2.5, 1, "saintpaf" },
                    { 8, "grigor.g43@gmail.com", "Grigor", "Georgiev", "Thegman!2", "08222616242", "grigs45Profilepic.jpeg", 5.0, 3.2999999999999998, 0, "grigs45" },
                    { 7, "emi.kovacheva@gmail.com", "Emilia", "Kovacheva", "Мilkaа#2", "0937122884", "emilia85Profilepic.jpeg", 0.0, 5.0, 0, "emilia85" },
                    { 6, "nikolova45@gmail.com", "Lia", "Nikolova", "Аleale!5", "0878819200", "lilka405Profile.jpeg", 0.0, 4.0999999999999996, 0, "lilka405" },
                    { 4, "martoS23@gmail.com", "Martin", "Simeonov", "marto&12", "08881729301", "itsmartoProfilepic.jpeg", 5.0, 3.0, 0, "itsmarto" },
                    { 3, "cantseeme@gmail.com", "John", "Cena", "No+vision3", "0902115402", "invisiblemanProfilepic.jpeg", 4.5, 4.5, 0, "invisibleman" },
                    { 2, "misteriouswrestler@gmail.com", "Rey", "Misterio", "Bigmistery*3", "0861422101", "wrestlerProfilePic.jpeg", 4.9000000000000004, 4.9000000000000004, 0, "prowrestler11" },
                    { 1, "gosho.t@gmail.com", "Georgi", "Todorov", "Randompass+12", "0923425084", "goshProfilePic.jpeg", 5.0, 5.0, 0, "Gosho23" },
                    { 13, "bojinkata@gmail.com", "Valeri", "Bojinov", "Malkokote&23", "0814171920", "coconutmaster72Profilepic.jpeg", 0.0, 0.0, 0, "coconutmaster72" },
                    { 5, "sashko32@gmail.com", "Aleks", "Aleksandrov", "weak^pass23", "0920816642", "SashkoBeatsProfilepic.jpeg", 0.0, 4.7000000000000002, 0, "SashkoBeats" },
                    { 14, "notstamatjoke@gmail.com", "Stamat", "Stamatov", "Demopass*2", "0939452370", "demoUserProfilepic.jpeg", 5.0, 4.5, 0, "demoUser" }
                });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RoleId", "UsersId" },
                values: new object[,]
                {
                    { 1, 14 },
                    { 1, 9 },
                    { 1, 8 },
                    { 1, 12 },
                    { 1, 7 },
                    { 2, 12 },
                    { 1, 6 },
                    { 1, 13 },
                    { 1, 11 },
                    { 1, 5 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 1, 1 },
                    { 2, 13 },
                    { 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "Id", "DepartureTime", "DriverId", "EndPointCityId", "FreeSpots", "IsCompleted", "StartPointCityId" },
                values: new object[,]
                {
                    { 5, new DateTime(2022, 1, 10, 10, 50, 0, 0, DateTimeKind.Unspecified), 14, 5, 4, true, 3 },
                    { 11, new DateTime(2021, 12, 11, 17, 0, 0, 0, DateTimeKind.Unspecified), 14, 6, 1, false, 4 },
                    { 12, new DateTime(2022, 3, 6, 13, 40, 0, 0, DateTimeKind.Unspecified), 14, 3, 4, false, 5 },
                    { 9, new DateTime(2021, 12, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 5, false, 4 },
                    { 13, new DateTime(2022, 2, 15, 13, 0, 0, 0, DateTimeKind.Unspecified), 14, 2, 4, false, 5 },
                    { 8, new DateTime(2021, 12, 18, 9, 30, 0, 0, DateTimeKind.Unspecified), 7, 1, 3, false, 5 },
                    { 7, new DateTime(2021, 12, 22, 23, 30, 0, 0, DateTimeKind.Unspecified), 6, 2, 4, false, 6 },
                    { 6, new DateTime(2022, 2, 11, 15, 40, 0, 0, DateTimeKind.Unspecified), 5, 6, 2, false, 1 },
                    { 4, new DateTime(2021, 12, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, 0, true, 2 },
                    { 3, new DateTime(2022, 3, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 4, false, 1 },
                    { 2, new DateTime(2022, 1, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 2, true, 2 },
                    { 10, new DateTime(2022, 1, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), 10, 6, 2, true, 3 },
                    { 1, new DateTime(2022, 2, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 3, false, 2 }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Comment", "Rating", "TravelId", "Type", "UserFromId", "UserToId" },
                values: new object[,]
                {
                    { 1, "Amazing person. It was cool to travel with you.", 4.9000000000000004, 1, 0, 1, 2 },
                    { 24, "Stamat rules", 5.0, 5, 1, 3, 14 },
                    { 23, "Stamat is the best", 5.0, 5, 1, 2, 14 },
                    { 22, "10/10 best stamat", 5.0, 5, 1, 1, 14 },
                    { 21, "Cool", 5.0, 5, 0, 14, 1 },
                    { 26, "Had a good time on the trip with you", 5.0, 4, 1, 10, 8 },
                    { 25, "Had a good time on the trip with you", 5.0, 4, 1, 11, 10 },
                    { 20, "Good stuff my guy", 5.0, 4, 0, 14, 2 },
                    { 18, null, 5.0, 4, 1, 10, 4 },
                    { 17, "Brought a pet even though it wasn't allowed. At least the pet was cute.", 3.1000000000000001, 4, 0, 4, 10 },
                    { 16, "Can't believe I was in a ride with John Cena.", 4.5, 3, 1, 8, 3 },
                    { 15, "Provided a pleasant and fast ride.", 4.5, 3, 1, 7, 3 },
                    { 14, null, 4.5, 3, 1, 6, 3 },
                    { 13, "Very noisy and sometimes even iritating.", 3.2999999999999998, 3, 0, 3, 8 },
                    { 12, null, 5.0, 3, 0, 3, 7 },
                    { 11, "Sometimes she complains a lot but other than that she was quite nice.", 4.0999999999999996, 3, 0, 3, 6 },
                    { 19, "Had a good time on the trip with you", 5.0, 4, 1, 14, 4 },
                    { 3, "Please do not try to bypass the rules and smoke when someone says it is forbidden.", 3.0, 1, 0, 1, 4 },
                    { 5, "Very chill.", 5.0, 1, 1, 3, 1 },
                    { 4, "Drives very safely.", 5.0, 1, 1, 2, 1 },
                    { 2, "Very quiet.", 4.5, 1, 0, 1, 3 },
                    { 7, "It's awesome to have him as company during a travel.", 5.0, 2, 0, 2, 1 },
                    { 6, "Drove us safely to our destination.", 5.0, 1, 1, 4, 1 },
                    { 9, "An awesome person and an awesome driver.", 4.9000000000000004, 2, 1, 1, 2 },
                    { 10, "Such a good driver, it's like he came out of Fast & Furious.", 4.9000000000000004, 2, 1, 5, 2 },
                    { 8, "Had a good laugh with you around.", 4.7000000000000002, 2, 0, 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "TravelTravelTag",
                columns: new[] { "TravelTagsId", "TravelsId" },
                values: new object[,]
                {
                    { 3, 11 },
                    { 1, 5 },
                    { 1, 4 },
                    { 4, 12 },
                    { 1, 13 },
                    { 1, 10 },
                    { 3, 2 },
                    { 3, 8 },
                    { 3, 7 },
                    { 2, 6 },
                    { 3, 4 },
                    { 4, 2 },
                    { 4, 9 },
                    { 2, 1 },
                    { 1, 1 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "TravelTravelTag",
                columns: new[] { "TravelTagsId", "TravelsId" },
                values: new object[,]
                {
                    { 4, 3 },
                    { 1, 3 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "TravelUser",
                columns: new[] { "AppliedTravelsId", "ApplyingPassengersId" },
                values: new object[,]
                {
                    { 9, 14 },
                    { 13, 2 },
                    { 13, 1 },
                    { 10, 14 },
                    { 3, 9 },
                    { 12, 3 },
                    { 12, 2 }
                });

            migrationBuilder.InsertData(
                table: "TravelUser1",
                columns: new[] { "PassengersId", "TravelsAsPassengerId" },
                values: new object[,]
                {
                    { 7, 3 },
                    { 1, 2 },
                    { 5, 12 },
                    { 4, 12 },
                    { 5, 2 },
                    { 4, 1 },
                    { 4, 5 },
                    { 1, 5 },
                    { 2, 5 },
                    { 8, 3 },
                    { 3, 1 },
                    { 3, 13 },
                    { 2, 1 },
                    { 11, 10 },
                    { 14, 10 },
                    { 6, 3 },
                    { 10, 9 },
                    { 14, 9 },
                    { 3, 5 },
                    { 5, 13 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TravelId",
                table: "Feedbacks",
                column: "TravelId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserFromId",
                table: "Feedbacks",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserToId",
                table: "Feedbacks",
                column: "UserToId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_DriverId",
                table: "Travels",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_EndPointCityId",
                table: "Travels",
                column: "EndPointCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_StartPointCityId",
                table: "Travels",
                column: "StartPointCityId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelTravelTag_TravelsId",
                table: "TravelTravelTag",
                column: "TravelsId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelUser_ApplyingPassengersId",
                table: "TravelUser",
                column: "ApplyingPassengersId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelUser1_TravelsAsPassengerId",
                table: "TravelUser1",
                column: "TravelsAsPassengerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "TravelTravelTag");

            migrationBuilder.DropTable(
                name: "TravelUser");

            migrationBuilder.DropTable(
                name: "TravelUser1");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TravelTags");

            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
