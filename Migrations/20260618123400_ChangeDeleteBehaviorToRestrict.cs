using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmsApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviorToRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tms_enrollments_tms_courses_CourseId",
                table: "tms_enrollments");

            migrationBuilder.AddForeignKey(
                name: "FK_tms_enrollments_tms_courses_CourseId",
                table: "tms_enrollments",
                column: "CourseId",
                principalTable: "tms_courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tms_enrollments_tms_courses_CourseId",
                table: "tms_enrollments");

            migrationBuilder.AddForeignKey(
                name: "FK_tms_enrollments_tms_courses_CourseId",
                table: "tms_enrollments",
                column: "CourseId",
                principalTable: "tms_courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
