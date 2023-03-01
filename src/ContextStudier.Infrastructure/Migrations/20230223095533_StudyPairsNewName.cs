using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContextStudier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudyPairsNewName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPairs_Folders_FolderId",
                table: "StudyPairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyPairs",
                table: "StudyPairs");

            migrationBuilder.RenameTable(
                name: "StudyPairs",
                newName: "Cards");

            migrationBuilder.RenameIndex(
                name: "IX_StudyPairs_FolderId",
                table: "Cards",
                newName: "IX_Cards_FolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Folders_FolderId",
                table: "Cards",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Folders_FolderId",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "StudyPairs");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_FolderId",
                table: "StudyPairs",
                newName: "IX_StudyPairs_FolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyPairs",
                table: "StudyPairs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPairs_Folders_FolderId",
                table: "StudyPairs",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
