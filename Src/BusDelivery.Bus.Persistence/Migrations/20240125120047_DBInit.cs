using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusDelivery.Persistence.Migrations;

/// <inheritdoc />
public partial class DBInit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Bus",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                NumberOfSeat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OperateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Bus", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Office",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Lng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Office", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Route",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                StartPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OperateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Route", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Station",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OfficeId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Lng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Station", x => x.Id);
                table.ForeignKey(
                    name: "FK_Station_Office_OfficeId",
                    column: x => x.OfficeId,
                    principalTable: "Office",
                    principalColumn: "Id");
            });

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
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<int>(type: "int", nullable: false),
                OfficeId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Identity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Gentle = table.Column<int>(type: "int", nullable: false),
                DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DeviceVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                OS = table.Column<int>(type: "int", nullable: true),
                CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
                table.ForeignKey(
                    name: "FK_User_Office_OfficeId",
                    column: x => x.OfficeId,
                    principalTable: "Office",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_User_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "BusRoute",
            columns: table => new
            {
                RouteId = table.Column<int>(type: "int", nullable: false),
                BusId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BusRoute", x => new { x.RouteId, x.BusId });
                table.ForeignKey(
                    name: "FK_BusRoute_Bus_BusId",
                    column: x => x.BusId,
                    principalTable: "Bus",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BusRoute_Route_RouteId",
                    column: x => x.RouteId,
                    principalTable: "Route",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Coordinate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RouteId = table.Column<int>(type: "int", nullable: false),
                Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Lng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Stt = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Coordinate", x => x.Id);
                table.ForeignKey(
                    name: "FK_Coordinate_Route_RouteId",
                    column: x => x.RouteId,
                    principalTable: "Route",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Package",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BusId = table.Column<int>(type: "int", nullable: false),
                FromOfficeId = table.Column<int>(type: "int", nullable: false),
                ToOfficeId = table.Column<int>(type: "int", nullable: false),
                StationId = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                TotalWeight = table.Column<float>(type: "real", nullable: false),
                TotalPrice = table.Column<float>(type: "real", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Package", x => x.Id);
                table.ForeignKey(
                    name: "FK_Package_Bus_BusId",
                    column: x => x.BusId,
                    principalTable: "Bus",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Package_Office_FromOfficeId",
                    column: x => x.FromOfficeId,
                    principalTable: "Office",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Package_Office_ToOfficeId",
                    column: x => x.ToOfficeId,
                    principalTable: "Office",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Package_Station_StationId",
                    column: x => x.StationId,
                    principalTable: "Station",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "StationRoute",
            columns: table => new
            {
                StationId = table.Column<int>(type: "int", nullable: false),
                RouteId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StationRoute", x => new { x.RouteId, x.StationId });
                table.ForeignKey(
                    name: "FK_StationRoute_Route_RouteId",
                    column: x => x.RouteId,
                    principalTable: "Route",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_StationRoute_Station_StationId",
                    column: x => x.StationId,
                    principalTable: "Station",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Report",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreateBy = table.Column<int>(type: "int", nullable: false),
                TargetId = table.Column<int>(type: "int", nullable: false),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Report", x => x.Id);
                table.ForeignKey(
                    name: "FK_Report_User_CreateBy",
                    column: x => x.CreateBy,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Order",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PackageId = table.Column<int>(type: "int", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Weight = table.Column<float>(type: "real", nullable: false),
                Price = table.Column<float>(type: "real", nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Order", x => x.Id);
                table.ForeignKey(
                    name: "FK_Order_Package_PackageId",
                    column: x => x.PackageId,
                    principalTable: "Package",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_BusRoute_BusId",
            table: "BusRoute",
            column: "BusId");

        migrationBuilder.CreateIndex(
            name: "IX_Coordinate_RouteId",
            table: "Coordinate",
            column: "RouteId");

        migrationBuilder.CreateIndex(
            name: "IX_Order_PackageId",
            table: "Order",
            column: "PackageId");

        migrationBuilder.CreateIndex(
            name: "IX_Package_BusId",
            table: "Package",
            column: "BusId");

        migrationBuilder.CreateIndex(
            name: "IX_Package_FromOfficeId",
            table: "Package",
            column: "FromOfficeId");

        migrationBuilder.CreateIndex(
            name: "IX_Package_StationId",
            table: "Package",
            column: "StationId");

        migrationBuilder.CreateIndex(
            name: "IX_Package_ToOfficeId",
            table: "Package",
            column: "ToOfficeId");

        migrationBuilder.CreateIndex(
            name: "IX_Report_CreateBy",
            table: "Report",
            column: "CreateBy");

        migrationBuilder.CreateIndex(
            name: "IX_Station_OfficeId",
            table: "Station",
            column: "OfficeId");

        migrationBuilder.CreateIndex(
            name: "IX_StationRoute_StationId",
            table: "StationRoute",
            column: "StationId");

        migrationBuilder.CreateIndex(
            name: "IX_User_OfficeId",
            table: "User",
            column: "OfficeId");

        migrationBuilder.CreateIndex(
            name: "IX_User_RoleId",
            table: "User",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_Weather_OfficeId",
            table: "Weather",
            column: "OfficeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BusRoute");

        migrationBuilder.DropTable(
            name: "Coordinate");

        migrationBuilder.DropTable(
            name: "Order");

        migrationBuilder.DropTable(
            name: "Report");

        migrationBuilder.DropTable(
            name: "StationRoute");

        migrationBuilder.DropTable(
            name: "Weather");

        migrationBuilder.DropTable(
            name: "Package");

        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "Route");

        migrationBuilder.DropTable(
            name: "Bus");

        migrationBuilder.DropTable(
            name: "Station");

        migrationBuilder.DropTable(
            name: "Role");

        migrationBuilder.DropTable(
            name: "Office");
    }
}
