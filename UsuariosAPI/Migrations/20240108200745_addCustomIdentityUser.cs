using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class addCustomIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "b65ba2e4-71a6-402f-b1d0-45c654fbb9d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "a5b687c0-fd81-44e2-82d9-a71b1669a7f0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de102ba7-881d-4bc8-ab75-43ef0206218c", "AQAAAAEAACcQAAAAELNg5s5xerN4zIia7Gbauupwh5D7tIRwC12f992OunvoKjE+Cd0aj9QqcFHnvlyc3A==", "d49ec020-4731-47b0-960b-5fb996bc8a6a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "be2521ba-6b25-4652-bb2f-11cfde9085aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "8a915cd2-4bc3-41ef-8ddf-54434fec17de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1a7d625-cae1-4264-8b51-27db5234a5f6", "AQAAAAEAACcQAAAAELI8/bPKwN5siWbI7xykvxGFLAu4bs56Bokw6WWz3TLuPFVfFnbQoPLy1ND7QRwalg==", "e4a83b9c-95f0-422f-8a49-8beb2737ff32" });
        }
    }
}
