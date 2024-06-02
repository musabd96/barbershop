using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAppointment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("802c273d-3c75-42aa-947c-b5fe84dac9d4"));

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("fe4b2d16-178e-415b-82cd-651e2b0829ad"), new DateOnly(2024, 6, 10), new Guid("0ea74bf2-7672-43a2-a719-896bdeacc7ce"), new Guid("675a357b-1b3d-4bfd-9f9b-4340a184911c"), false, 20.00m, "Cutting" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("fe4b2d16-178e-415b-82cd-651e2b0829ad"));

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppointmentDate", "BarberId", "CustomerId", "IsCancelled", "Price", "Service" },
                values: new object[] { new Guid("802c273d-3c75-42aa-947c-b5fe84dac9d4"), new DateOnly(2024, 6, 10), new Guid("11decb96-f979-48a5-b30a-5fd3de1def74"), new Guid("5a09893a-bf2e-44a5-9ccd-095b832445af"), false, 20.0m, "Hair" });
        }
    }
}
