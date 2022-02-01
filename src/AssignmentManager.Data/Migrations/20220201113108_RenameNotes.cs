using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentManager.Data.Migrations
{
    public partial class RenameNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Assignments",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Assignments",
                newName: "Notes");
        }
    }
}
