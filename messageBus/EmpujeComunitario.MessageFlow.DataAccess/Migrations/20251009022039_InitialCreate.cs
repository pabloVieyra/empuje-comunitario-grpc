using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmpujeComunitario.MessageFlow.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelledDonation",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrgId = table.Column<Guid>(type: "uuid", nullable: false),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledDonation", x => new { x.OrgId, x.RequestId });
                });

            migrationBuilder.CreateTable(
                name: "CancelledEvents",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrgId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledEvents", x => new { x.OrgId, x.EventId });
                });

            migrationBuilder.CreateTable(
                name: "DonationOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    DonationOrganizationId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequesterOrgId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolidaryEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrgId = table.Column<string>(type: "text", nullable: false),
                    NameEvent = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateTimeEvent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolidaryEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonationTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    DonationOrgId = table.Column<string>(type: "text", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationTransfers_DonationRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "DonationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerAdhesions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrgId = table.Column<string>(type: "text", nullable: false),
                    VolunteerId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerAdhesions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerAdhesions_SolidaryEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "SolidaryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonationItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequestId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: true),
                    OfferId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    TransferId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransferId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationItems_DonationOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "DonationOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationItems_DonationOffers_OfferId1",
                        column: x => x.OfferId1,
                        principalTable: "DonationOffers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonationItems_DonationRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "DonationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationItems_DonationRequests_RequestId1",
                        column: x => x.RequestId1,
                        principalTable: "DonationRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonationItems_DonationTransfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "DonationTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationItems_DonationTransfers_TransferId1",
                        column: x => x.TransferId1,
                        principalTable: "DonationTransfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_OfferId",
                table: "DonationItems",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_OfferId1",
                table: "DonationItems",
                column: "OfferId1");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_RequestId",
                table: "DonationItems",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_RequestId1",
                table: "DonationItems",
                column: "RequestId1");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_TransferId",
                table: "DonationItems",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_TransferId1",
                table: "DonationItems",
                column: "TransferId1");

            migrationBuilder.CreateIndex(
                name: "IX_DonationTransfers_RequestId",
                table: "DonationTransfers",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAdhesions_EventId",
                table: "VolunteerAdhesions",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelledDonation");

            migrationBuilder.DropTable(
                name: "CancelledEvents");

            migrationBuilder.DropTable(
                name: "DonationItems");

            migrationBuilder.DropTable(
                name: "VolunteerAdhesions");

            migrationBuilder.DropTable(
                name: "DonationOffers");

            migrationBuilder.DropTable(
                name: "DonationTransfers");

            migrationBuilder.DropTable(
                name: "SolidaryEvents");

            migrationBuilder.DropTable(
                name: "DonationRequests");
        }
    }
}
