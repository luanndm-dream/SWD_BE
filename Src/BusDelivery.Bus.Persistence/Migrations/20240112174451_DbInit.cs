using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusDelivery.Persistence.Migrations;

/// <inheritdoc />
public partial class DbInit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Bus",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                plateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                numberOfSeat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                operateTime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Bus", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "Offices",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                routeId = table.Column<int>(type: "int", nullable: false),
                name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                lat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                lng = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Offices", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "Routes",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                startPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                endPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                operateTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Routes", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "Stations",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                officeId = table.Column<int>(type: "int", nullable: false),
                name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                lng = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Stations", x => x.id);
                table.ForeignKey(
                    name: "FK_Stations_Offices_officeId",
                    column: x => x.officeId,
                    principalTable: "Offices",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Weathers",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                officeId = table.Column<int>(type: "int", nullable: false),
                temperature = table.Column<double>(type: "float", nullable: false),
                humidity = table.Column<double>(type: "float", nullable: false),
                windySpeed = table.Column<double>(type: "float", nullable: false),
                recordAt = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Weathers", x => x.id);
                table.ForeignKey(
                    name: "FK_Weathers_Offices_officeId",
                    column: x => x.officeId,
                    principalTable: "Offices",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                roleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                officeId = table.Column<int>(type: "int", nullable: false),
                name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                gentle = table.Column<string>(type: "nvarchar(1)", nullable: false),
                status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                deviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                deviceVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OS = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.id);
                table.ForeignKey(
                    name: "FK_Users_Offices_officeId",
                    column: x => x.officeId,
                    principalTable: "Offices",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Users_Roles_roleId",
                    column: x => x.roleId,
                    principalTable: "Roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "BusRoutes",
            columns: table => new
            {
                routeId = table.Column<int>(type: "int", nullable: false),
                busId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BusRoutes", x => new { x.routeId, x.busId });
                table.ForeignKey(
                    name: "FK_BusRoutes_Bus_busId",
                    column: x => x.busId,
                    principalTable: "Bus",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "FK_BusRoutes_Routes_routeId",
                    column: x => x.routeId,
                    principalTable: "Routes",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Coordinates",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                lat = table.Column<double>(type: "float", nullable: false),
                lng = table.Column<double>(type: "float", nullable: false),
                stt = table.Column<int>(type: "int", nullable: false),
                routeId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Coordinates", x => x.id);
                table.ForeignKey(
                    name: "FK_Coordinates_Routes_routeId",
                    column: x => x.routeId,
                    principalTable: "Routes",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Packages",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                officeId = table.Column<int>(type: "int", nullable: false),
                busId = table.Column<int>(type: "int", nullable: false),
                stationId = table.Column<int>(type: "int", nullable: false),
                quantity = table.Column<int>(type: "int", nullable: false),
                totalWeight = table.Column<float>(type: "real", nullable: false),
                totalPrice = table.Column<float>(type: "real", nullable: false),
                image = table.Column<int>(type: "int", nullable: false),
                note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "True"),
                createTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 1, 13, 0, 44, 51, 622, DateTimeKind.Local).AddTicks(4776))
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Packages", x => x.id);
                table.ForeignKey(
                    name: "FK_Packages_Bus_busId",
                    column: x => x.busId,
                    principalTable: "Bus",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Packages_Offices_officeId",
                    column: x => x.officeId,
                    principalTable: "Offices",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Packages_Stations_stationId",
                    column: x => x.stationId,
                    principalTable: "Stations",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "StationRoutes",
            columns: table => new
            {
                stationId = table.Column<int>(type: "int", nullable: false),
                routeId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StationRoutes", x => new { x.routeId, x.stationId });
                table.ForeignKey(
                    name: "FK_StationRoutes_Routes_routeId",
                    column: x => x.routeId,
                    principalTable: "Routes",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "FK_StationRoutes_Stations_stationId",
                    column: x => x.stationId,
                    principalTable: "Stations",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "RefreshTokens",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                clientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                deviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                expiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                createdOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshTokens", x => x.id);
                table.ForeignKey(
                    name: "FK_RefreshTokens_Users_userId",
                    column: x => x.userId,
                    principalTable: "Users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Reports",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                createTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 1, 13, 0, 44, 51, 623, DateTimeKind.Local).AddTicks(1325)),
                createBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                targetId = table.Column<int>(type: "int", nullable: false),
                type = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reports", x => x.id);
                table.ForeignKey(
                    name: "FK_Reports_Users_createBy",
                    column: x => x.createBy,
                    principalTable: "Users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                packageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                weight = table.Column<float>(type: "real", nullable: false),
                price = table.Column<float>(type: "real", nullable: false),
                note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.id);
                table.ForeignKey(
                    name: "FK_Orders_Packages_packageId",
                    column: x => x.packageId,
                    principalTable: "Packages",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserPackages",
            columns: table => new
            {
                userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                packageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserPackages", x => new { x.userId, x.packageId });
                table.ForeignKey(
                    name: "FK_UserPackages_Packages_packageId",
                    column: x => x.packageId,
                    principalTable: "Packages",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserPackages_Users_userId",
                    column: x => x.userId,
                    principalTable: "Users",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_BusRoutes_busId",
            table: "BusRoutes",
            column: "busId");

        migrationBuilder.CreateIndex(
            name: "IX_Coordinates_routeId",
            table: "Coordinates",
            column: "routeId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_packageId",
            table: "Orders",
            column: "packageId");

        migrationBuilder.CreateIndex(
            name: "IX_Packages_busId",
            table: "Packages",
            column: "busId");

        migrationBuilder.CreateIndex(
            name: "IX_Packages_officeId",
            table: "Packages",
            column: "officeId");

        migrationBuilder.CreateIndex(
            name: "IX_Packages_stationId",
            table: "Packages",
            column: "stationId");

        migrationBuilder.CreateIndex(
            name: "IX_RefreshTokens_clientId_deviceId",
            table: "RefreshTokens",
            columns: new[] { "clientId", "deviceId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_RefreshTokens_token",
            table: "RefreshTokens",
            column: "token",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_RefreshTokens_userId",
            table: "RefreshTokens",
            column: "userId");

        migrationBuilder.CreateIndex(
            name: "IX_Reports_createBy",
            table: "Reports",
            column: "createBy");

        migrationBuilder.CreateIndex(
            name: "IX_StationRoutes_stationId",
            table: "StationRoutes",
            column: "stationId");

        migrationBuilder.CreateIndex(
            name: "IX_Stations_officeId",
            table: "Stations",
            column: "officeId");

        migrationBuilder.CreateIndex(
            name: "IX_UserPackages_packageId",
            table: "UserPackages",
            column: "packageId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_officeId",
            table: "Users",
            column: "officeId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_roleId",
            table: "Users",
            column: "roleId");

        migrationBuilder.CreateIndex(
            name: "IX_Weathers_officeId",
            table: "Weathers",
            column: "officeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BusRoutes");

        migrationBuilder.DropTable(
            name: "Coordinates");

        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "RefreshTokens");

        migrationBuilder.DropTable(
            name: "Reports");

        migrationBuilder.DropTable(
            name: "StationRoutes");

        migrationBuilder.DropTable(
            name: "UserPackages");

        migrationBuilder.DropTable(
            name: "Weathers");

        migrationBuilder.DropTable(
            name: "Routes");

        migrationBuilder.DropTable(
            name: "Packages");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Bus");

        migrationBuilder.DropTable(
            name: "Stations");

        migrationBuilder.DropTable(
            name: "Roles");

        migrationBuilder.DropTable(
            name: "Offices");
    }
}
