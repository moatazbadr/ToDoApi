using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    public partial class lol8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "todoItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_todoItems_StudentId",
                table: "todoItems",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_todoItems_students_StudentId",
                table: "todoItems",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoItems_students_StudentId",
                table: "todoItems");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropIndex(
                name: "IX_todoItems_StudentId",
                table: "todoItems");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "todoItems");
        }
    }
}
