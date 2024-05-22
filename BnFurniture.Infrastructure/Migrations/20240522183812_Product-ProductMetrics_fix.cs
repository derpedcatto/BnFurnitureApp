using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BnFurniture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductProductMetrics_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("1caf88d1-87c4-451d-9cd3-54cf9d62b5f0"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("2e3d6c34-f099-4a60-934e-c377ffcdd113"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("3591b2d7-d541-4330-9fbf-f52d1b179c18"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("f774da6c-effb-4ee9-b40a-cc1a934dafbc"));

            migrationBuilder.DeleteData(
                table: "User_UserRole",
                keyColumn: "Id",
                keyValue: new Guid("9b5b3e45-3760-4e27-9b2d-55539cd83baa"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("63510989-c806-4dc9-96a1-c942be1ceaaa"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("a2280f81-e34a-4461-ba5d-e27b6635143a"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("c63ea19a-39e0-49cd-9226-5462f9cb47eb"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("dbae7b6e-0715-4be9-bb59-73b50fcc4703"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e"));

            migrationBuilder.DropColumn(
                name: "MetricId",
                table: "Product");

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("9e49e22c-7764-42d8-8cbb-43b9406eb56d"), null, "DashboardAccess" },
                    { new Guid("a09b70fd-994e-4d3e-a499-e773f7b16eb5"), null, "DeleteAccess" },
                    { new Guid("d4909cc4-09e9-43a6-9969-0271c67de75b"), null, "CreateAccess" },
                    { new Guid("dfb1cf23-efdd-42af-add7-fb9fc5981de6"), null, "UpdateAccess" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"),
                column: "RegisteredAt",
                value: new DateTime(2024, 5, 22, 21, 38, 11, 934, DateTimeKind.Local).AddTicks(1826));

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("bbcfc446-04a0-4d10-be24-b62952cba221"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRole_Permission",
                columns: new[] { "Id", "PermissionId", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("97e11eae-c153-4c39-850f-3cd91a5b8048"), new Guid("dfb1cf23-efdd-42af-add7-fb9fc5981de6"), new Guid("bbcfc446-04a0-4d10-be24-b62952cba221") },
                    { new Guid("d9d321d8-c2f4-4d18-aaf3-20c421d74b51"), new Guid("a09b70fd-994e-4d3e-a499-e773f7b16eb5"), new Guid("bbcfc446-04a0-4d10-be24-b62952cba221") },
                    { new Guid("e042fee8-4ffe-4dad-bcb1-3d9480b54743"), new Guid("9e49e22c-7764-42d8-8cbb-43b9406eb56d"), new Guid("bbcfc446-04a0-4d10-be24-b62952cba221") },
                    { new Guid("f6857d5e-5097-431e-a97e-f42e554bdff1"), new Guid("d4909cc4-09e9-43a6-9969-0271c67de75b"), new Guid("bbcfc446-04a0-4d10-be24-b62952cba221") }
                });

            migrationBuilder.InsertData(
                table: "User_UserRole",
                columns: new[] { "Id", "UserId", "UserRoleId" },
                values: new object[] { new Guid("0b299401-867c-4bce-8f9e-318b36d8964c"), new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"), new Guid("bbcfc446-04a0-4d10-be24-b62952cba221") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("97e11eae-c153-4c39-850f-3cd91a5b8048"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("d9d321d8-c2f4-4d18-aaf3-20c421d74b51"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("e042fee8-4ffe-4dad-bcb1-3d9480b54743"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("f6857d5e-5097-431e-a97e-f42e554bdff1"));

            migrationBuilder.DeleteData(
                table: "User_UserRole",
                keyColumn: "Id",
                keyValue: new Guid("0b299401-867c-4bce-8f9e-318b36d8964c"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("9e49e22c-7764-42d8-8cbb-43b9406eb56d"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("a09b70fd-994e-4d3e-a499-e773f7b16eb5"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("d4909cc4-09e9-43a6-9969-0271c67de75b"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("dfb1cf23-efdd-42af-add7-fb9fc5981de6"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("bbcfc446-04a0-4d10-be24-b62952cba221"));

            migrationBuilder.AddColumn<Guid>(
                name: "MetricId",
                table: "Product",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("63510989-c806-4dc9-96a1-c942be1ceaaa"), null, "UpdateAccess" },
                    { new Guid("a2280f81-e34a-4461-ba5d-e27b6635143a"), null, "CreateAccess" },
                    { new Guid("c63ea19a-39e0-49cd-9226-5462f9cb47eb"), null, "DeleteAccess" },
                    { new Guid("dbae7b6e-0715-4be9-bb59-73b50fcc4703"), null, "DashboardAccess" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"),
                column: "RegisteredAt",
                value: new DateTime(2024, 5, 22, 11, 21, 55, 272, DateTimeKind.Local).AddTicks(9460));

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRole_Permission",
                columns: new[] { "Id", "PermissionId", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("1caf88d1-87c4-451d-9cd3-54cf9d62b5f0"), new Guid("a2280f81-e34a-4461-ba5d-e27b6635143a"), new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e") },
                    { new Guid("2e3d6c34-f099-4a60-934e-c377ffcdd113"), new Guid("63510989-c806-4dc9-96a1-c942be1ceaaa"), new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e") },
                    { new Guid("3591b2d7-d541-4330-9fbf-f52d1b179c18"), new Guid("c63ea19a-39e0-49cd-9226-5462f9cb47eb"), new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e") },
                    { new Guid("f774da6c-effb-4ee9-b40a-cc1a934dafbc"), new Guid("dbae7b6e-0715-4be9-bb59-73b50fcc4703"), new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e") }
                });

            migrationBuilder.InsertData(
                table: "User_UserRole",
                columns: new[] { "Id", "UserId", "UserRoleId" },
                values: new object[] { new Guid("9b5b3e45-3760-4e27-9b2d-55539cd83baa"), new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"), new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e") });
        }
    }
}
