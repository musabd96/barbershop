using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("dd5960f1-8b3d-4282-9125-30a20af46b82"));

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("691cbf7e-9cdc-49a4-a1e6-ebc6d1544092"), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bac198d9-2453-4178-8f5b-7ea6dabde951"), new Guid("f6be4dcd-fa9b-45c2-bfd2-aad40c1f6d4e"), false, 20.00m, "Cutting" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("691cbf7e-9cdc-49a4-a1e6-ebc6d1544092"));

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("dd5960f1-8b3d-4282-9125-30a20af46b82"), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9a72fd72-c327-4a78-b36b-0c72882970f5"), new Guid("99040076-dbd4-4b2c-aa12-d4ff9a459573"), false, 20.00m, "Cutting" });
        }
    }
}
