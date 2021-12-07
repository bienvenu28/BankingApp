using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.Migrations
{
    public partial class Add_Several_Default_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "amount", "firstName", "lastName", "pin" },
                values: new object[] { 1, 200.19999999999999, "John", "Dylan", "12340" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "amount", "firstName", "lastName", "pin" },
                values: new object[] { 2, 200000.20000000001, "Remi", "Gold", "4560" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "amount", "firstName", "lastName", "pin" },
                values: new object[] { 3, 100000.0, "Bobby", "Haang", "7777" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
