using Microsoft.EntityFrameworkCore.Migrations;

namespace PetClinicApp.Migrations
{
    public partial class Clinic_AddressFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clinics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clinics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clinics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Clinics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Clinics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Clinics");
        }
    }
}
