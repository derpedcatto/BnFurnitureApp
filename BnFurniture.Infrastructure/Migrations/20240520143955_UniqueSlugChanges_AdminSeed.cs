using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BnFurniture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueSlugChanges_AdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductType_Slug",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicValue_Slug",
                table: "CharacteristicValue");

            migrationBuilder.DropIndex(
                name: "IX_Characteristic_Slug",
                table: "Characteristic");

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

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Characteristic",
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
                    { new Guid("1cbc57bc-38b2-4f4a-8419-cfc60434b5a0"), null, "DashboardAccess" },
                    { new Guid("8b059966-fcb1-4bb2-b260-8d50d64d952a"), null, "UpdateAccess" },
                    { new Guid("981446f2-1508-40a0-9a71-549703d56dd6"), null, "DeleteAccess" },
                    { new Guid("a61281ac-2bc2-498e-905c-a7912f7b1c3f"), null, "CreateAccess" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastLoginAt", "LastName", "Password", "PhoneNumber", "RegisteredAt" },
                values: new object[] { new Guid("cf824881-8bec-4d9e-95a3-5deef16065b5"), null, "sashavannovski@gmail.com", "Oleksandr", null, "Vannovskyi", "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", null, new DateTime(2024, 5, 20, 17, 39, 55, 316, DateTimeKind.Local).AddTicks(6578) });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("200609fb-4fc1-4762-aa61-6a4fcad74465"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRole_Permission",
                columns: new[] { "Id", "PermissionId", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("58330000-c041-48f3-bfd8-f7730f6b3d87"), new Guid("8b059966-fcb1-4bb2-b260-8d50d64d952a"), new Guid("200609fb-4fc1-4762-aa61-6a4fcad74465") },
                    { new Guid("84cd3358-c191-4782-bcd3-96cbfdf7609e"), new Guid("a61281ac-2bc2-498e-905c-a7912f7b1c3f"), new Guid("200609fb-4fc1-4762-aa61-6a4fcad74465") },
                    { new Guid("c2ea9855-859b-47bf-89c8-159b8a2c2438"), new Guid("1cbc57bc-38b2-4f4a-8419-cfc60434b5a0"), new Guid("200609fb-4fc1-4762-aa61-6a4fcad74465") },
                    { new Guid("dc7a3092-4f49-4eb0-859c-9755d8f0d847"), new Guid("981446f2-1508-40a0-9a71-549703d56dd6"), new Guid("200609fb-4fc1-4762-aa61-6a4fcad74465") }
                });

            migrationBuilder.InsertData(
                table: "User_UserRole",
                columns: new[] { "Id", "UserId", "UserRoleId" },
                values: new object[] { new Guid("278ddc2f-972a-48ea-9692-896762a4b2fa"), new Guid("cf824881-8bec-4d9e-95a3-5deef16065b5"), new Guid("200609fb-4fc1-4762-aa61-6a4fcad74465") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("58330000-c041-48f3-bfd8-f7730f6b3d87"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("84cd3358-c191-4782-bcd3-96cbfdf7609e"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("c2ea9855-859b-47bf-89c8-159b8a2c2438"));

            migrationBuilder.DeleteData(
                table: "UserRole_Permission",
                keyColumn: "Id",
                keyValue: new Guid("dc7a3092-4f49-4eb0-859c-9755d8f0d847"));

            migrationBuilder.DeleteData(
                table: "User_UserRole",
                keyColumn: "Id",
                keyValue: new Guid("278ddc2f-972a-48ea-9692-896762a4b2fa"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("1cbc57bc-38b2-4f4a-8419-cfc60434b5a0"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("8b059966-fcb1-4bb2-b260-8d50d64d952a"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("981446f2-1508-40a0-9a71-549703d56dd6"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("a61281ac-2bc2-498e-905c-a7912f7b1c3f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cf824881-8bec-4d9e-95a3-5deef16065b5"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("200609fb-4fc1-4762-aa61-6a4fcad74465"));

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ProductType",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "CharacteristicValue",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Characteristic",
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
                name: "IX_CharacteristicValue_Slug",
                table: "CharacteristicValue",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characteristic_Slug",
                table: "Characteristic",
                column: "Slug",
                unique: true);
        }
    }
}
