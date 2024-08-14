using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace astro_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Otp",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Otp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OtpExpiry",
                table: "Users");
        }
    }
}
