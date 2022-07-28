using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRISP.BackendChallenge.Migrations
{
    public partial class inital : Migration
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
                    Department = table.Column<int>(type: "INTEGER", nullable: false)
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
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { 1, 1, "John Doe" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { 2, 2, "Jane Doe" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { 3, 1, "Joe Doe" });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 1, 1, new DateTime(2022, 6, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9477) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 2, 1, new DateTime(2022, 5, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9514) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 3, 1, new DateTime(2022, 4, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9516) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 4, 2, new DateTime(2022, 6, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9519) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 5, 2, new DateTime(2022, 5, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9521) });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate" },
                values: new object[] { 6, 3, new DateTime(2022, 6, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9523) });

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
