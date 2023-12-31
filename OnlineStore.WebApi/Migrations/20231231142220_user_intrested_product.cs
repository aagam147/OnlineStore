using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class user_intrested_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IntrestedProduct",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntrestedProduct",
                table: "AspNetUsers");
        }
    }
}
