using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfoTrack_CounterAPI.Migrations
{
    /// <inheritdoc />
    public partial class createrows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SearchEngine",
                columns: new[] { "Id", "EngineName", "Url", "UrlExtractionSyntax" },
                values: new object[,]
                {
                    { new Guid("2c409c7c-6a91-4a73-806a-3bb48d38e319"), "Bing", "https://www.bing.com/search?num=", "<cite>(.*?)</cite>" },
                    { new Guid("ae410443-42d5-4d73-966a-07afc573c338"), "Google", "https://www.google.co.uk/search?count=", "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SearchEngine",
                keyColumn: "Id",
                keyValue: new Guid("2c409c7c-6a91-4a73-806a-3bb48d38e319"));

            migrationBuilder.DeleteData(
                table: "SearchEngine",
                keyColumn: "Id",
                keyValue: new Guid("ae410443-42d5-4d73-966a-07afc573c338"));
        }
    }
}
