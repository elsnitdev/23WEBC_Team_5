using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core_website.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTagsFromSanPham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "SanPham");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
