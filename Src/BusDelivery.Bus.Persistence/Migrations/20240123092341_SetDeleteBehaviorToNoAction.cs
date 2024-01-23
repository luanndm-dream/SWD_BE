using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusDelivery.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetDeleteBehaviorToNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusRoute_Route_RouteId",
                table: "BusRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinate_Route_RouteId",
                table: "Coordinate");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Package_PackageId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Bus_BusId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Office_OfficeId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_CreateBy",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_Office_OfficeId",
                table: "Station");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Office_OfficeId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Weather_Office_OfficeId",
                table: "Weather");

            migrationBuilder.RenameColumn(
                name: "OfficeId",
                table: "Package",
                newName: "ToOfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_OfficeId",
                table: "Package",
                newName: "IX_Package_ToOfficeId");

            migrationBuilder.AddColumn<int>(
                name: "FromOfficeId",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Package_FromOfficeId",
                table: "Package",
                column: "FromOfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRoute_Route_RouteId",
                table: "BusRoute",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinate_Route_RouteId",
                table: "Coordinate",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Package_PackageId",
                table: "Order",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Bus_BusId",
                table: "Package",
                column: "BusId",
                principalTable: "Bus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Office_FromOfficeId",
                table: "Package",
                column: "FromOfficeId",
                principalTable: "Office",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Office_ToOfficeId",
                table: "Package",
                column: "ToOfficeId",
                principalTable: "Office",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_CreateBy",
                table: "Report",
                column: "CreateBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Office_OfficeId",
                table: "Station",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Office_OfficeId",
                table: "User",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Weather_Office_OfficeId",
                table: "Weather",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusRoute_Route_RouteId",
                table: "BusRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinate_Route_RouteId",
                table: "Coordinate");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Package_PackageId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Bus_BusId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Office_FromOfficeId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Office_ToOfficeId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_CreateBy",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_Office_OfficeId",
                table: "Station");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Office_OfficeId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Weather_Office_OfficeId",
                table: "Weather");

            migrationBuilder.DropIndex(
                name: "IX_Package_FromOfficeId",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "FromOfficeId",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "ToOfficeId",
                table: "Package",
                newName: "OfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_ToOfficeId",
                table: "Package",
                newName: "IX_Package_OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRoute_Route_RouteId",
                table: "BusRoute",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinate_Route_RouteId",
                table: "Coordinate",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Package_PackageId",
                table: "Order",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Bus_BusId",
                table: "Package",
                column: "BusId",
                principalTable: "Bus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Office_OfficeId",
                table: "Package",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_CreateBy",
                table: "Report",
                column: "CreateBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Office_OfficeId",
                table: "Station",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Office_OfficeId",
                table: "User",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weather_Office_OfficeId",
                table: "Weather",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
