using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAtrioApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmployment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Persons_PersonId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Employments");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_PersonId",
                table: "Employments",
                newName: "IX_Employments_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employments",
                table: "Employments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employments_Persons_PersonId",
                table: "Employments",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employments_Persons_PersonId",
                table: "Employments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employments",
                table: "Employments");

            migrationBuilder.RenameTable(
                name: "Employments",
                newName: "Jobs");

            migrationBuilder.RenameIndex(
                name: "IX_Employments_PersonId",
                table: "Jobs",
                newName: "IX_Jobs_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Persons_PersonId",
                table: "Jobs",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
