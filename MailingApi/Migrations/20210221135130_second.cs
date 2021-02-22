using Microsoft.EntityFrameworkCore.Migrations;

namespace MailingApi.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailConsumer",
                columns: table => new
                {
                    con_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    con_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    con_groupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailConsumer", x => x.con_id);
                });

            migrationBuilder.CreateTable(
                name: "MailingGroup",
                columns: table => new
                {
                    grp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grp_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grp_ownerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingGroup", x => x.grp_id);
                });

            migrationBuilder.CreateTable(
                name: "MailUser",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usr_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usr_password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailUser", x => x.usr_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailConsumer");

            migrationBuilder.DropTable(
                name: "MailingGroup");

            migrationBuilder.DropTable(
                name: "MailUser");
        }
    }
}
