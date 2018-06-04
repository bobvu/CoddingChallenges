using Microsoft.EntityFrameworkCore.Migrations;

namespace CodingChallenges.Migrations
{
    public partial class SecondDbseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ManagerId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ManagerId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
