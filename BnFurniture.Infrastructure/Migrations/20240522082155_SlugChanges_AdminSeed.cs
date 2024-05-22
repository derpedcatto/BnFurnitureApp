using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BnFurniture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SlugChanges_AdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductType_Slug",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_ProductArticle_Slug",
                table: "ProductArticle");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicValue_Slug",
                table: "CharacteristicValue");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "ProductArticle");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ProductType",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "CharacteristicValue",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastLoginAt", "LastName", "Password", "PhoneNumber", "RegisteredAt" },
                values: new object[] { new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"), null, "sashavannovski@gmail.com", "Oleksandr", null, "Vannovskyi", "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", null, new DateTime(2024, 5, 22, 11, 21, 55, 272, DateTimeKind.Local).AddTicks(9460) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("e45834c7-bd1a-4ba6-9c13-e9ee2c74fd3e"));

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ProductType",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "ProductArticle",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "CharacteristicValue",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_Slug",
                table: "ProductType",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductArticle_Slug",
                table: "ProductArticle",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValue_Slug",
                table: "CharacteristicValue",
                column: "Slug",
                unique: true);
        }
    }
}
