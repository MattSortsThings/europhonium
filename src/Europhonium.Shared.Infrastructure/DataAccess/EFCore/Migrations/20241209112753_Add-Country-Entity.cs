using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Europhonium.Shared.Infrastructure.DataAccess.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country_code = table.Column<string>(type: "char(2)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country_competing_broadcast",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    competing_broadcast_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country_competing_broadcast", x => x.id);
                    table.ForeignKey(
                        name: "fk_country_competing_broadcast_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "country_participating_contest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    participating_contest_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country_participating_contest", x => x.id);
                    table.ForeignKey(
                        name: "fk_country_participating_contest_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "country_voting_broadcast",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voting_broadcast_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country_voting_broadcast", x => x.id);
                    table.ForeignKey(
                        name: "fk_country_voting_broadcast_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_country_country_code",
                table: "country",
                column: "country_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_country_competing_broadcast_country_id_competing_broadcast_id",
                table: "country_competing_broadcast",
                columns: new[] { "country_id", "competing_broadcast_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_country_participating_contest_country_id_participating_contest_id",
                table: "country_participating_contest",
                columns: new[] { "country_id", "participating_contest_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_country_voting_broadcast_country_id_voting_broadcast_id",
                table: "country_voting_broadcast",
                columns: new[] { "country_id", "voting_broadcast_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "country_competing_broadcast");

            migrationBuilder.DropTable(
                name: "country_participating_contest");

            migrationBuilder.DropTable(
                name: "country_voting_broadcast");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
