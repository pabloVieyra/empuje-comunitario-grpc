using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpujeComunitario.MessageFlow.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class createUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Creation_user_id",
                table: "DonationRequests",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Creation_user_id",
                table: "DonationOffers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creation_user_id",
                table: "DonationRequests");

            migrationBuilder.DropColumn(
                name: "Creation_user_id",
                table: "DonationOffers");
        }
    }
}
