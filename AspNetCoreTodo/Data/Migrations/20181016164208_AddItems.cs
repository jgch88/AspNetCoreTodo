using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTodo.Data.Migrations
{
    public partial class AddItems : Migration
    {
        // This method runs when migration is applied. (Creating an "Items" table with properties)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    DueAt = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        // This method runs when undo-ing the migration. (Just drop the "Items" table)
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
