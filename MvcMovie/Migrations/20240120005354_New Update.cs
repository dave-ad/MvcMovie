using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DIrector",
                table: "Movie",
                newName: "Director");

            migrationBuilder.RenameColumn(
                name: "Titile",
                table: "Movie",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "MovieImage",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Director",
                table: "Movie",
                newName: "DIrector");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Movie",
                newName: "Titile");

            migrationBuilder.AlterColumn<string>(
                name: "MovieImage",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
