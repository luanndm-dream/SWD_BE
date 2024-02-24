using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusDelivery.Persistence.Migrations;

/// <inheritdoc />
public partial class AddAvatarToUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Avatar",
            table: "User",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "CreateTime",
            table: "Package",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Avatar",
            table: "User");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreateTime",
            table: "Package",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }
}
