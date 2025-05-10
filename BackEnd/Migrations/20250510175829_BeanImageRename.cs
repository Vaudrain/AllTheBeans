using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeansAPI.Migrations
{
    /// <inheritdoc />
    public partial class BeanImageRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Beans",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Beans",
                newName: "ImageUrl");
        }
    }
}
