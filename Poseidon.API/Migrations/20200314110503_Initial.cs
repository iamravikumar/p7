using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poseidon.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BidList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Type = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    BidQuantity = table.Column<double>(nullable: true),
                    AskQuantity = table.Column<double>(nullable: true),
                    Bid = table.Column<double>(nullable: true),
                    Ask = table.Column<double>(nullable: true),
                    Benchmark = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    BidListDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Commentary = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Security = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Trader = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Book = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    CreationName = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RevisionName = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    RevisionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DealName = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    DealType = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    SourceListId = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Side = table.Column<string>(unicode: false, maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurvePoint",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurveId = table.Column<short>(nullable: true),
                    AsOfDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Term = table.Column<double>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurvePoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoodysRating = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    SandPRating = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    FitchRating = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    OrderNumber = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Rating_pk", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "RuleName",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Json = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Template = table.Column<string>(unicode: false, maxLength: 512, nullable: true),
                    SqlStr = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    SqlPart = table.Column<string>(unicode: false, maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RuleName_pk", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Type = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    BuyQuantity = table.Column<double>(nullable: true),
                    SellQuantity = table.Column<double>(nullable: true),
                    BuyPrice = table.Column<decimal>(type: "money", nullable: true),
                    SellPrice = table.Column<decimal>(type: "money", nullable: true),
                    TradeDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Security = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Trader = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Benchmark = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Book = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    CreationName = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RevisionName = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    RevisionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DealName = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    DealType = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    SourceListId = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Side = table.Column<string>(unicode: false, maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Password = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    FullName = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    Role = table.Column<string>(unicode: false, maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Users_pk", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "Rating_Id_uindex",
                table: "Rating",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RuleName_Id_uindex",
                table: "RuleName",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Users_Id_uindex",
                table: "User",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidList");

            migrationBuilder.DropTable(
                name: "CurvePoint");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "RuleName");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
