using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentManager.Data.Migrations
{
    public partial class RemoveUniqueContstraintOnClassName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classes_Name",
                table: "Classes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Classes_Name",
                table: "Classes",
                column: "Name",
                unique: true);
        }
    }
}
