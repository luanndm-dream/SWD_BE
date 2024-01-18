using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusDelivery.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateNewDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Status",
            table: "Route");

        migrationBuilder.RenameColumn(
            name: "Status",
            table: "User",
            newName: "IsActive");

        migrationBuilder.RenameColumn(
            name: "Status",
            table: "Bus",
            newName: "IsActive");

        migrationBuilder.AlterColumn<string>(
            name: "OS",
            table: "User",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<int>(
            name: "Gentle",
            table: "User",
            type: "int",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(1)");

        migrationBuilder.AlterColumn<string>(
            name: "DeviceVersion",
            table: "User",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "DeviceId",
            table: "User",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreateTime",
            table: "User",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 328, DateTimeKind.Local).AddTicks(6107));

        migrationBuilder.AddColumn<string>(
            name: "HashPassword",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "User",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "Station",
            type: "bit",
            nullable: false,
            defaultValue: true);

        migrationBuilder.AddColumn<string>(
            name: "IsDeleted",
            table: "Station",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "False");

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "Route",
            type: "bit",
            nullable: false,
            defaultValue: false);

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
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 1, 17, 7, 43, 25, 353, DateTimeKind.Local).AddTicks(8832));

        migrationBuilder.AlterColumn<int>(
            name: "Status",
            table: "Package",
            type: "int",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldDefaultValue: "True");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreateTime",
            table: "Package",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 326, DateTimeKind.Local).AddTicks(6675),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 1, 17, 7, 43, 25, 353, DateTimeKind.Local).AddTicks(6054));

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Office",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AlterColumn<string>(
            name: "Lng",
            table: "Coordinate",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(double),
            oldType: "float");

        migrationBuilder.AlterColumn<string>(
            name: "Lat",
            table: "Coordinate",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(double),
            oldType: "float");

        migrationBuilder.CreateTable(
            name: "Weather",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OfficeId = table.Column<int>(type: "int", nullable: false),
                Temperature = table.Column<double>(type: "float", nullable: false),
                Humidity = table.Column<double>(type: "float", nullable: false),
                WindySpeed = table.Column<double>(type: "float", nullable: false),
                RecordAt = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Weather", x => x.Id);
                table.ForeignKey(
                    name: "FK_Weather_Office_OfficeId",
                    column: x => x.OfficeId,
                    principalTable: "Office",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Weather_OfficeId",
            table: "Weather",
            column: "OfficeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Weather");

        migrationBuilder.DropColumn(
            name: "CreateTime",
            table: "User");

        migrationBuilder.DropColumn(
            name: "HashPassword",
            table: "User");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "User");

        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "Station");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Station");

        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "Route");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Route");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Office");

        migrationBuilder.RenameColumn(
            name: "IsActive",
            table: "User",
            newName: "Status");

        migrationBuilder.RenameColumn(
            name: "IsActive",
            table: "Bus",
            newName: "Status");

        migrationBuilder.AlterColumn<string>(
            name: "OS",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Gentle",
            table: "User",
            type: "nvarchar(1)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<string>(
            name: "DeviceVersion",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "DeviceId",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255,
            oldNullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "Status",
            table: "Route",
            type: "bit",
            nullable: false,
            defaultValue: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreateTime",
            table: "Report",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 1, 17, 7, 43, 25, 353, DateTimeKind.Local).AddTicks(8832),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 326, DateTimeKind.Local).AddTicks(9437));

        migrationBuilder.AlterColumn<string>(
            name: "Status",
            table: "Package",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "True",
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreateTime",
            table: "Package",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 1, 17, 7, 43, 25, 353, DateTimeKind.Local).AddTicks(6054),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 1, 18, 17, 32, 4, 326, DateTimeKind.Local).AddTicks(6675));

        migrationBuilder.AlterColumn<double>(
            name: "Lng",
            table: "Coordinate",
            type: "float",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<double>(
            name: "Lat",
            table: "Coordinate",
            type: "float",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }
}
