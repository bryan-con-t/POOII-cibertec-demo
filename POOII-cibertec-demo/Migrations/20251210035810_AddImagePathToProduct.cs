using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POOII_cibertec_demo.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Products",
                newName: "isCompleted");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Products",
                newName: "cantidad");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "Products",
                newName: "fechaRegistro");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                maxLength: 260,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "isCompleted",
                table: "Products",
                newName: "IsCompleted");

            migrationBuilder.RenameColumn(
                name: "cantidad",
                table: "Products",
                newName: "Cantidad");

            migrationBuilder.RenameColumn(
                name: "fechaRegistro",
                table: "Products",
                newName: "FechaRegistro");
        }

    }
}
