using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusDelivery.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateDBDateTimeToString : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "User");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Station");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Route");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Office");

        migrationBuilder.AlterColumn<string>(
            name: "CreateTime",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 328, DateTimeKind.Local).AddTicks(6107));

        migrationBuilder.AlterColumn<string>(
            name: "CreateTime",
            table: "Report",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 326, DateTimeKind.Local).AddTicks(9437));

        migrationBuilder.AlterColumn<string>(
            name: "CreateTime",
            table: "Package",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 326, DateTimeKind.Local).AddTicks(6675));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "CreateTime",
            table: "User",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 328, DateTimeKind.Local).AddTicks(6107),
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "User",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "IsDeleted",
            table: "Station",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "False");

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Route",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreateTime",
            table: "Report",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 326, DateTimeKind.Local).AddTicks(9437),
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreateTime",
            table: "Package",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 326, DateTimeKind.Local).AddTicks(6675),
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Office",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }
}
