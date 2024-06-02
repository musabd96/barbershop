using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("61c05b53-d7da-4387-a5fd-6708a1b1c052"), new DateOnly(2024, 6, 10), new Guid("b63619d7-278d-437a-b6e6-7f5398254443"), new Guid("6e9af2bc-a207-495a-8b4c-349e342d522c"), false, 20.0m, "Hair" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("61c05b53-d7da-4387-a5fd-6708a1b1c052"));
        }
    }
}
