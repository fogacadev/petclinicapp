using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetClinicApp.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    Descripton = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistoryTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReminderTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(255)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicServices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    ClinicId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicServices_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    BornIn = table.Column<DateTime>(type: "date", nullable: false),
                    AnimalId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    ExpiresOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HistoryTypeId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PetId = table.Column<long>(nullable: false),
                    ClinicId = table.Column<long>(nullable: true),
                    Attachment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_MedicalHistoryTypes_HistoryTypeId",
                        column: x => x.HistoryTypeId,
                        principalTable: "MedicalHistoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReminderDate = table.Column<DateTime>(type: "date", nullable: false),
                    ReminderTypeId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true),
                    PetId = table.Column<long>(nullable: false),
                    Finished = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reminders_ReminderTypes_ReminderTypeId",
                        column: x => x.ReminderTypeId,
                        principalTable: "ReminderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicServices_ClinicId",
                table: "ClinicServices",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_ClinicId",
                table: "MedicalHistories",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_HistoryTypeId",
                table: "MedicalHistories",
                column: "HistoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PetId",
                table: "MedicalHistories",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AnimalId",
                table: "Pets",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_PetId",
                table: "Reminders",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_ReminderTypeId",
                table: "Reminders",
                column: "ReminderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicServices");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "MedicalHistoryTypes");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "ReminderTypes");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
