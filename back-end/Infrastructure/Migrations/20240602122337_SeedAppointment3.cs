using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAppointment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("fe4b2d16-178e-415b-82cd-651e2b0829ad"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Appointment",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("21429651-05f5-48fc-9981-0838ebe244b7"), new DateOnly(2024, 6, 10), new Guid("b58d480d-53ea-4a67-b78b-6205a9b2f819"), new Guid("2efeb6a2-bbac-4cff-82fa-0518c1c46a1c"), false, 20.00m, "Cutting" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("21429651-05f5-48fc-9981-0838ebe244b7"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Appointment",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("fe4b2d16-178e-415b-82cd-651e2b0829ad"), new DateOnly(2024, 6, 10), new Guid("0ea74bf2-7672-43a2-a719-896bdeacc7ce"), new Guid("675a357b-1b3d-4bfd-9f9b-4340a184911c"), false, 20.00m, "Cutting" });
        }
    }
}
