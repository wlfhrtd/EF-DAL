using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Province = table.Column<string>(type: "varchar(65)", unicode: false, maxLength: 65, nullable: true),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    WebSite = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ProfilePrivacyLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    ContactPrivacyLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    ProfileViews = table.Column<long>(type: "bigint", nullable: false),
                    ProfileLastViewDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Rating = table.Column<byte>(type: "tinyint", nullable: true, defaultValueSql: "((3))"),
                    AvatarURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    FileUploadsInBytes = table.Column<int>(type: "int", nullable: false),
                    FileUploadQuotaInBytes = table.Column<int>(type: "int", nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ShowChatStatus = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    AllowChatSounds = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "ArtistSkill",
                columns: table => new
                {
                    ArtistTalentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    TalentName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SkillLevel = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((3))"),
                    Details = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Styles = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__A9AD4EAAFEE755FA", x => x.ArtistTalentID);
                    table.ForeignKey(
                        name: "FK_ArtistSkill_Artist",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSkill_ArtistId",
                table: "ArtistSkill",
                column: "ArtistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSkill");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
