using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class CriandoRoleRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "8a915cd2-4bc3-41ef-8ddf-54434fec17de");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "be2521ba-6b25-4652-bb2f-11cfde9085aa", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1a7d625-cae1-4264-8b51-27db5234a5f6", "AQAAAAEAACcQAAAAELI8/bPKwN5siWbI7xykvxGFLAu4bs56Bokw6WWz3TLuPFVfFnbQoPLy1ND7QRwalg==", "e4a83b9c-95f0-422f-8a49-8beb2737ff32" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "069c6bae-0131-46de-81b5-4ed7589d2c37");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a35a91d6-ec27-4579-8971-96dba682bba5", "AQAAAAEAACcQAAAAEIuI+fsHne/DYy4RiP15JytWjxuOhPUD7L2oZYNKMnGlHuDH2shtBCtEG7cK6Znt9Q==", "bb41ab02-566b-4b04-a874-6e589f9819bd" });
        }
    }
}
