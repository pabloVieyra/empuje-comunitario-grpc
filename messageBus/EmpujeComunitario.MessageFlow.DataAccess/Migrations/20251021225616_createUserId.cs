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
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Create_user_id",
                table: "DonationOffers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creation_user_id",
                table: "DonationRequests");

            migrationBuilder.DropColumn(
                name: "Create_user_id",
                table: "DonationOffers");
        }
    }
}
