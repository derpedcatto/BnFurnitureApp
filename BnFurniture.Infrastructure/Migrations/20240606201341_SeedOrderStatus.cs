using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BnFurniture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("009e6ea6-210a-4ed6-9b25-05f966090c8e"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("1132623c-6631-4ef8-aebc-e858c6e65634"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("856ce9b2-9ed1-4408-931e-0e5f577679c7"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("f395d588-76f2-4b45-9b54-2a9d48ea52cb"));

            migrationBuilder.DeleteData(
                table: "User_UserRole",
                keyColumn: "Id",
                keyValue: new Guid("2efae994-ec3d-4fdf-84ea-ab10d9028406"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("312471d4-9393-408a-9d71-b84c43e8dc42"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("6807b0d3-e10d-47a3-b8ca-551f9acdf114"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("6c0e4ed2-bf6d-4abb-b3bf-9c9a5811fa88"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("77a78f12-7016-43d5-8d98-c28135fd3335"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("b5748bed-999c-477b-b401-efb6d03672bf"));

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "CharacteristicValue",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Обрабатывается" },
                    { 2, "Комплектуется" },
                    { 3, "Передан в службу доставки" },
                    { 4, "Доставляется" },
                    { 5, "Ожидает клиента в пунтке самовывоза" },
                    { 6, "Выполнен" },
                    { 7, "Отменён" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("38f9001a-0559-444f-bcfc-e3e6842b15d0"), null, "DeleteAccess" },
                    { new Guid("6d686a66-ce10-4570-a38a-7bd11c5552d4"), null, "DashboardAccess" },
                    { new Guid("836a338f-df53-47e4-89c3-5bef622a5100"), null, "UpdateAccess" },
                    { new Guid("fd2d5e1f-9e8c-413e-a9b9-6050e06a5f15"), null, "CreateAccess" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"),
                column: "RegisteredAt",
                value: new DateTime(2024, 6, 6, 23, 13, 39, 354, DateTimeKind.Local).AddTicks(9039));

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("983b8cba-0ef6-47d2-a140-f6fad0c37b99"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRole_Permission",
                columns: new[] { "Id", "PermissionId", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("10572301-e650-453f-8788-97e9a72c9924"), new Guid("6d686a66-ce10-4570-a38a-7bd11c5552d4"), new Guid("983b8cba-0ef6-47d2-a140-f6fad0c37b99") },
                    { new Guid("60ff4faf-93fb-4dfd-b263-ee923b46fe3b"), new Guid("fd2d5e1f-9e8c-413e-a9b9-6050e06a5f15"), new Guid("983b8cba-0ef6-47d2-a140-f6fad0c37b99") },
                    { new Guid("9bcaee7c-12f3-465e-b711-4e80bdffc343"), new Guid("836a338f-df53-47e4-89c3-5bef622a5100"), new Guid("983b8cba-0ef6-47d2-a140-f6fad0c37b99") },
                    { new Guid("e3073405-1b66-485b-842d-3bd3035a6f88"), new Guid("38f9001a-0559-444f-bcfc-e3e6842b15d0"), new Guid("983b8cba-0ef6-47d2-a140-f6fad0c37b99") }
                });

            migrationBuilder.InsertData(
                table: "User_UserRole",
                columns: new[] { "Id", "UserId", "UserRoleId" },
                values: new object[] { new Guid("dd524a86-9a5e-48cc-ba72-2782fbef9346"), new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"), new Guid("983b8cba-0ef6-47d2-a140-f6fad0c37b99") });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValue_Slug",
                table: "CharacteristicValue",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacteristicValue_Slug",
                table: "CharacteristicValue");

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("10572301-e650-453f-8788-97e9a72c9924"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("60ff4faf-93fb-4dfd-b263-ee923b46fe3b"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("9bcaee7c-12f3-465e-b711-4e80bdffc343"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("e3073405-1b66-485b-842d-3bd3035a6f88"));

            migrationBuilder.DeleteData(
                table: "User_UserRole",
                keyColumn: "Id",
                keyValue: new Guid("dd524a86-9a5e-48cc-ba72-2782fbef9346"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("38f9001a-0559-444f-bcfc-e3e6842b15d0"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("6d686a66-ce10-4570-a38a-7bd11c5552d4"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("836a338f-df53-47e4-89c3-5bef622a5100"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("fd2d5e1f-9e8c-413e-a9b9-6050e06a5f15"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("983b8cba-0ef6-47d2-a140-f6fad0c37b99"));

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
                    { new Guid("312471d4-9393-408a-9d71-b84c43e8dc42"), null, "DeleteAccess" },
                    { new Guid("6807b0d3-e10d-47a3-b8ca-551f9acdf114"), null, "DashboardAccess" },
                    { new Guid("6c0e4ed2-bf6d-4abb-b3bf-9c9a5811fa88"), null, "UpdateAccess" },
                    { new Guid("77a78f12-7016-43d5-8d98-c28135fd3335"), null, "CreateAccess" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"),
                column: "RegisteredAt",
                value: new DateTime(2024, 5, 23, 13, 52, 20, 762, DateTimeKind.Local).AddTicks(981));

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("b5748bed-999c-477b-b401-efb6d03672bf"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRole_Permission",
                columns: new[] { "Id", "PermissionId", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("009e6ea6-210a-4ed6-9b25-05f966090c8e"), new Guid("77a78f12-7016-43d5-8d98-c28135fd3335"), new Guid("b5748bed-999c-477b-b401-efb6d03672bf") },
                    { new Guid("1132623c-6631-4ef8-aebc-e858c6e65634"), new Guid("6807b0d3-e10d-47a3-b8ca-551f9acdf114"), new Guid("b5748bed-999c-477b-b401-efb6d03672bf") },
                    { new Guid("856ce9b2-9ed1-4408-931e-0e5f577679c7"), new Guid("312471d4-9393-408a-9d71-b84c43e8dc42"), new Guid("b5748bed-999c-477b-b401-efb6d03672bf") },
                    { new Guid("f395d588-76f2-4b45-9b54-2a9d48ea52cb"), new Guid("6c0e4ed2-bf6d-4abb-b3bf-9c9a5811fa88"), new Guid("b5748bed-999c-477b-b401-efb6d03672bf") }
                });

            migrationBuilder.InsertData(
                table: "User_UserRole",
                columns: new[] { "Id", "UserId", "UserRoleId" },
                values: new object[] { new Guid("2efae994-ec3d-4fdf-84ea-ab10d9028406"), new Guid("adad13d5-2468-4f9c-9ddc-b0940569df8a"), new Guid("b5748bed-999c-477b-b401-efb6d03672bf") });
        }
    }
}
