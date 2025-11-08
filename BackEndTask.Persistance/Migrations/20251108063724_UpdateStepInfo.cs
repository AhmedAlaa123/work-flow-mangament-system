using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndTask.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStepInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtrnalValidatorUrl",
                table: "Steps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsHasExternalValidator",
                table: "Steps",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtrnalValidatorUrl",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "IsHasExternalValidator",
                table: "Steps");
        }
    }
}
