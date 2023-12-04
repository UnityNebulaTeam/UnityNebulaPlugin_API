using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebulaPlugin.Api.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class DatabasesTableKeyIdentifierAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyIdentifier",
                table: "Databases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyIdentifier",
                table: "Databases");
        }
    }
}
