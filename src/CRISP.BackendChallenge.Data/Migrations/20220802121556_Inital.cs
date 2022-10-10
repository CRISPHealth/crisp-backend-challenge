using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRISP.BackendChallenge.Data.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "EndDate", "Name", "StartDate" },
                values: new object[] { 1, 1, null, "John Doe", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "EndDate", "Name", "StartDate" },
                values: new object[] { 2, 2, null, "Jane Doe", new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "EndDate", "Name", "StartDate" },
                values: new object[] { 3, 1, null, "Joe Doe", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "EndDate", "Name", "StartDate" },
                values: new object[] { 4, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leroy Jenkins", new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 1, 1, new DateTime(2022, 7, 2, 8, 15, 55, 879, DateTimeKind.Local).AddTicks(2166) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 2, 1, new DateTime(2022, 6, 2, 8, 15, 55, 879, DateTimeKind.Local).AddTicks(2195) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 3, 1, new DateTime(2022, 5, 2, 8, 15, 55, 879, DateTimeKind.Local).AddTicks(2197) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 4, 2, new DateTime(2022, 7, 2, 8, 15, 55, 879, DateTimeKind.Local).AddTicks(2199) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 5, 2, new DateTime(2022, 6, 2, 8, 15, 55, 879, DateTimeKind.Local).AddTicks(2200) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 6, 3, new DateTime(2022, 7, 2, 8, 15, 55, 879, DateTimeKind.Local).AddTicks(2202) });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EmployeeId",
                table: "Logins",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
