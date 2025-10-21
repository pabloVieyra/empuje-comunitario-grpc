using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpujeComunitario.MessageFlow.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationItems_DonationOffers_OfferId",
                table: "DonationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationItems_DonationOffers_OfferId1",
                table: "DonationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationItems_DonationRequests_RequestId",
                table: "DonationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationItems_DonationRequests_RequestId1",
                table: "DonationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationItems_DonationTransfers_TransferId1",
                table: "DonationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationTransfers_DonationRequests_RequestId",
                table: "DonationTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAdhesions_SolidaryEvents_EventId",
                table: "VolunteerAdhesions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerAdhesions",
                table: "VolunteerAdhesions");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerAdhesions_EventId",
                table: "VolunteerAdhesions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolidaryEvents",
                table: "SolidaryEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonationRequests",
                table: "DonationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonationOffers",
                table: "DonationOffers");

            migrationBuilder.DropIndex(
                name: "IX_DonationItems_OfferId1",
                table: "DonationItems");

            migrationBuilder.DropIndex(
                name: "IX_DonationItems_RequestId1",
                table: "DonationItems");

            migrationBuilder.DropIndex(
                name: "IX_DonationItems_TransferId1",
                table: "DonationItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CancelledEvents",
                table: "CancelledEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CancelledDonation",
                table: "CancelledDonation");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VolunteerAdhesions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SolidaryEvents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DonationRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DonationOffers");

            migrationBuilder.DropColumn(
                name: "OfferId1",
                table: "DonationItems");

            migrationBuilder.DropColumn(
                name: "RequestId1",
                table: "DonationItems");

            migrationBuilder.DropColumn(
                name: "TransferId1",
                table: "DonationItems");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DonationTransfers",
                newName: "TransferId");

            migrationBuilder.AlterColumn<string>(
                name: "VolunteerId",
                table: "VolunteerAdhesions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VolunteerAdhesions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "VolunteerAdhesions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "DonationTransfers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerAdhesions",
                table: "VolunteerAdhesions",
                columns: new[] { "EventId", "VolunteerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolidaryEvents",
                table: "SolidaryEvents",
                column: "EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonationRequests",
                table: "DonationRequests",
                column: "RequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonationOffers",
                table: "DonationOffers",
                column: "OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CancelledEvents",
                table: "CancelledEvents",
                columns: new[] { "EventId", "OrgId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CancelledDonation",
                table: "CancelledDonation",
                columns: new[] { "RequestId", "OrgId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DonationItems_DonationOffers_OfferId",
                table: "DonationItems",
                column: "OfferId",
                principalTable: "DonationOffers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonationItems_DonationRequests_RequestId",
                table: "DonationItems",
                column: "RequestId",
                principalTable: "DonationRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonationTransfers_DonationRequests_RequestId",
                table: "DonationTransfers",
                column: "RequestId",
                principalTable: "DonationRequests",
                principalColumn: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAdhesions_SolidaryEvents_EventId",
                table: "VolunteerAdhesions",
                column: "EventId",
                principalTable: "SolidaryEvents",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationItems_DonationOffers_OfferId",
                table: "DonationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationItems_DonationRequests_RequestId",
                table: "DonationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationTransfers_DonationRequests_RequestId",
                table: "DonationTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAdhesions_SolidaryEvents_EventId",
                table: "VolunteerAdhesions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerAdhesions",
                table: "VolunteerAdhesions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolidaryEvents",
                table: "SolidaryEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonationRequests",
                table: "DonationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonationOffers",
                table: "DonationOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CancelledEvents",
                table: "CancelledEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CancelledDonation",
                table: "CancelledDonation");

            migrationBuilder.RenameColumn(
                name: "TransferId",
                table: "DonationTransfers",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VolunteerAdhesions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "VolunteerAdhesions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "VolunteerId",
                table: "VolunteerAdhesions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "VolunteerAdhesions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SolidaryEvents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "DonationTransfers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DonationRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DonationOffers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OfferId1",
                table: "DonationItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId1",
                table: "DonationItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransferId1",
                table: "DonationItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerAdhesions",
                table: "VolunteerAdhesions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolidaryEvents",
                table: "SolidaryEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonationRequests",
                table: "DonationRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonationOffers",
                table: "DonationOffers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CancelledEvents",
                table: "CancelledEvents",
                columns: new[] { "OrgId", "EventId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CancelledDonation",
                table: "CancelledDonation",
                columns: new[] { "OrgId", "RequestId" });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAdhesions_EventId",
                table: "VolunteerAdhesions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_OfferId1",
                table: "DonationItems",
                column: "OfferId1");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_RequestId1",
                table: "DonationItems",
                column: "RequestId1");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_TransferId1",
                table: "DonationItems",
                column: "TransferId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationItems_DonationOffers_OfferId",
                table: "DonationItems",
                column: "OfferId",
                principalTable: "DonationOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonationItems_DonationOffers_OfferId1",
                table: "DonationItems",
                column: "OfferId1",
                principalTable: "DonationOffers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationItems_DonationRequests_RequestId",
                table: "DonationItems",
                column: "RequestId",
                principalTable: "DonationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonationItems_DonationRequests_RequestId1",
                table: "DonationItems",
                column: "RequestId1",
                principalTable: "DonationRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationItems_DonationTransfers_TransferId1",
                table: "DonationItems",
                column: "TransferId1",
                principalTable: "DonationTransfers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationTransfers_DonationRequests_RequestId",
                table: "DonationTransfers",
                column: "RequestId",
                principalTable: "DonationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAdhesions_SolidaryEvents_EventId",
                table: "VolunteerAdhesions",
                column: "EventId",
                principalTable: "SolidaryEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
