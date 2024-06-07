using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("18a2b16e-f00b-4a6a-8cb8-56161f9a813d"));

            migrationBuilder.DeleteData(
                table: "Barber",
                keyColumn: "Id",
                keyValue: new Guid("4c2beb68-83a6-4415-9ec4-79d2c7afb9aa"));

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FistName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("2ec17936-cd80-4dd8-903a-da6f79d7a8eb"), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d83a7a92-2e09-45c0-93cc-01e9a7cd4e2f"), new Guid("5c760e6a-9435-4c70-a303-61b54b51227b"), false, 20.00m, "Cutting" });

            migrationBuilder.InsertData(
                table: "Barber",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("383ea96f-4089-41ab-a8cc-3b012ac6daa4"), "Mustafa" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Email", "FistName", "LastName", "Phone" },
                values: new object[] { new Guid("0ec23cff-ff5c-4655-ad27-8b78f76a14a7"), "musse@email.com", "Mustafa", "Abdulle", "0712345678" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("2ec17936-cd80-4dd8-903a-da6f79d7a8eb"));

            migrationBuilder.DeleteData(
                table: "Barber",
                keyColumn: "Id",
                keyValue: new Guid("383ea96f-4089-41ab-a8cc-3b012ac6daa4"));

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("18a2b16e-f00b-4a6a-8cb8-56161f9a813d"), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("20335de6-0f38-4921-a921-685b3f546231"), new Guid("f46a1e42-6cf3-4572-866b-53c3432e7069"), false, 20.00m, "Cutting" });

            migrationBuilder.InsertData(
                table: "Barber",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("4c2beb68-83a6-4415-9ec4-79d2c7afb9aa"), "Mustafa" });
        }
    }
}
