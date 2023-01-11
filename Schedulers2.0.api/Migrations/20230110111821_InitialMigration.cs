using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulers2._0.api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job_scheduler",
                columns: table => new
                {
                    jobScheduler_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    job_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    interval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    job_Id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "BIT", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "BIT", nullable: true),
                    version = table.Column<string>(type: "varchar(1)", nullable: true),
                    description = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_job_scheduler", x => x.jobScheduler_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_scheduler");
        }
    }
}
