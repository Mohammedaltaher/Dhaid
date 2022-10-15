using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class updatedISCOUNTSaMOUNT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Discounts");

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Discounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Discounts");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
