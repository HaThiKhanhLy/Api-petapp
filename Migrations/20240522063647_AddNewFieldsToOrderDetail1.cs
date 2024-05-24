using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetApps.api.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToOrderDetail1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipientName",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipientPhone",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "RecipientName",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "RecipientPhone",
                table: "OrderDetail");
        }
    }
}
