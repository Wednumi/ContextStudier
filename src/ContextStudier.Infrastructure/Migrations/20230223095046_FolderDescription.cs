using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContextStudier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FolderDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Folders");
        }
    }
}
