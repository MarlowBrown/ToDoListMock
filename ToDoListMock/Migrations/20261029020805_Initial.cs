using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoListMock.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketStatuses",
                columns: table => new
                {
                    TicketStatusId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatuses", x => x.TicketStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SprintNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PointValue = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketStatusId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketStatuses_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "TicketStatuses",
                        principalColumn: "TicketStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "TicketStatusId", "Name" },
                values: new object[,]
                {
                    { "done", "Done" },
                    { "in_progress", "In Progress" },
                    { "qa", "Quality Assurance" },
                    { "todo", "To Do" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketStatusId",
                table: "Tickets",
                column: "TicketStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketStatuses");
        }
    }
}
