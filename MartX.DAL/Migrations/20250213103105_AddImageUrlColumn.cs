using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartX.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c10c9801-9957-4018-8e48-0c7812d47b50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f528fe4-e9c1-4773-bc7f-73e9ab540422", "AQAAAAIAAYagAAAAEMY5dorgvKmXuiKjknzQ432vlNXuUyBnD60Y6J5uqJzxs7A38pFj+KdEKq4uMMpR6w==", "a01c43c6-b29e-47e7-af29-6ceb2be44c6f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Brands");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c10c9801-9957-4018-8e48-0c7812d47b50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42810ce1-0f16-4517-b877-7f465bbdb2da", "AQAAAAIAAYagAAAAEIltDVAqn8vx0ecOiDibUypNcmOHNNavIkyLTLI+fbxBc9Tw92mo4//ZRtyGPhvRZQ==", "6ab85ff0-468a-41b9-9681-a2ffd66c0030" });
        }
    }
}
