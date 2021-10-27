using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialKpiApi.Migrations
{
    public partial class Added_UniqueConstraint_To_Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_Initials",
                table: "Employees",
                column: "Initials",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_Initials",
                table: "Employees");
        }
    }
}
