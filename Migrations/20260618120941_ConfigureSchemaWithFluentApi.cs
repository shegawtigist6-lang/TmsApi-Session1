using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmsApi.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureSchemaWithFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Courses_CourseId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Courses_CourseId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Students_StudentId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assessments",
                table: "Assessments");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "tms_students");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "tms_enrollments");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "tms_courses");

            migrationBuilder.RenameTable(
                name: "Certificates",
                newName: "tms_certificates");

            migrationBuilder.RenameTable(
                name: "Assessments",
                newName: "tms_assessments");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentId",
                table: "tms_enrollments",
                newName: "IX_tms_enrollments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "tms_enrollments",
                newName: "IX_tms_enrollments_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_StudentId",
                table: "tms_certificates",
                newName: "IX_tms_certificates_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_CourseId",
                table: "tms_certificates",
                newName: "IX_tms_certificates_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Assessments_CourseId",
                table: "tms_assessments",
                newName: "IX_tms_assessments_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "tms_students",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tms_students",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "tms_courses",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "tms_courses",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "tms_certificates",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "tms_assessments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tms_students",
                table: "tms_students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tms_enrollments",
                table: "tms_enrollments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tms_courses",
                table: "tms_courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tms_certificates",
                table: "tms_certificates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tms_assessments",
                table: "tms_assessments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tms_students_RegistrationNumber",
                table: "tms_students",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tms_assessments_tms_courses_CourseId",
                table: "tms_assessments",
                column: "CourseId",
                principalTable: "tms_courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tms_certificates_tms_courses_CourseId",
                table: "tms_certificates",
                column: "CourseId",
                principalTable: "tms_courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tms_certificates_tms_students_StudentId",
                table: "tms_certificates",
                column: "StudentId",
                principalTable: "tms_students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tms_enrollments_tms_courses_CourseId",
                table: "tms_enrollments",
                column: "CourseId",
                principalTable: "tms_courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tms_enrollments_tms_students_StudentId",
                table: "tms_enrollments",
                column: "StudentId",
                principalTable: "tms_students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tms_assessments_tms_courses_CourseId",
                table: "tms_assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_tms_certificates_tms_courses_CourseId",
                table: "tms_certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_tms_certificates_tms_students_StudentId",
                table: "tms_certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_tms_enrollments_tms_courses_CourseId",
                table: "tms_enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_tms_enrollments_tms_students_StudentId",
                table: "tms_enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tms_students",
                table: "tms_students");

            migrationBuilder.DropIndex(
                name: "IX_tms_students_RegistrationNumber",
                table: "tms_students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tms_enrollments",
                table: "tms_enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tms_courses",
                table: "tms_courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tms_certificates",
                table: "tms_certificates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tms_assessments",
                table: "tms_assessments");

            migrationBuilder.RenameTable(
                name: "tms_students",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "tms_enrollments",
                newName: "Enrollments");

            migrationBuilder.RenameTable(
                name: "tms_courses",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "tms_certificates",
                newName: "Certificates");

            migrationBuilder.RenameTable(
                name: "tms_assessments",
                newName: "Assessments");

            migrationBuilder.RenameIndex(
                name: "IX_tms_enrollments_StudentId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_tms_enrollments_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_tms_certificates_StudentId",
                table: "Certificates",
                newName: "IX_Certificates_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_tms_certificates_CourseId",
                table: "Certificates",
                newName: "IX_Certificates_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_tms_assessments_CourseId",
                table: "Assessments",
                newName: "IX_Assessments_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Students",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Courses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Certificates",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Assessments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assessments",
                table: "Assessments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Courses_CourseId",
                table: "Assessments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Courses_CourseId",
                table: "Certificates",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Students_StudentId",
                table: "Certificates",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
