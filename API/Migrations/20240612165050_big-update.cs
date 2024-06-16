using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class bigupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Speciality_SpecialityId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plan_Audience_AudienceId",
                table: "lesson_plan");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plan_Group_GroupId",
                table: "lesson_plan");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plan_Subject_SubjectId",
                table: "lesson_plan");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Subject_SubjectId",
                table: "TeacherSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Teacher_TeacherId",
                table: "TeacherSubject");

            migrationBuilder.DropTable(
                name: "Audience");

            migrationBuilder.DropTable(
                name: "GroupTeacher");

            migrationBuilder.DropTable(
                name: "LessonTeacher");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropTable(
                name: "AudienceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubject",
                table: "TeacherSubject");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubject_SubjectId",
                table: "TeacherSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_plan",
                table: "lesson_plan");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plan_AudienceId",
                table: "lesson_plan");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plan_GroupId",
                table: "lesson_plan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_SpecialityId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "AudienceId",
                table: "lesson_plan");

            migrationBuilder.DropColumn(
                name: "WeekNumber",
                table: "lesson_plan");

            migrationBuilder.RenameTable(
                name: "TeacherSubject",
                newName: "TeacherSubjects");

            migrationBuilder.RenameTable(
                name: "Teacher",
                newName: "Teachers");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "lesson_plan",
                newName: "Lessons");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubject_TeacherId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_TeacherId");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Teachers",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Teachers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teachers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Shortname",
                table: "Subjects",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "isDistantce",
                table: "Lessons",
                newName: "IsRemote");

            migrationBuilder.RenameColumn(
                name: "Weekday",
                table: "Lessons",
                newName: "WeekOrderNumber");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Lessons",
                newName: "ScheduleId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Lessons",
                newName: "RnnId");

            migrationBuilder.RenameIndex(
                name: "IX_lesson_plan_SubjectId",
                table: "Lessons",
                newName: "IX_Lessons_ScheduleId");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "Groups",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Groups",
                newName: "GroupCode");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TeacherSubjects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonGroupId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubjects",
                table: "TeacherSubjects",
                columns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClassroomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    ScheduleType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonGroups_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YearBegin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearBegin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    ClassroomTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classrooms_ClassroomTypes_ClassroomTypeId",
                        column: x => x.ClassroomTypeId,
                        principalTable: "ClassroomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonGroupTeachers",
                columns: table => new
                {
                    LessonGroupId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Subgroup = table.Column<string>(type: "text", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonGroupTeachers", x => new { x.LessonGroupId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_LessonGroupTeachers_LessonGroups_LessonGroupId",
                        column: x => x.LessonGroupId,
                        principalTable: "LessonGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonGroupTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademicYear = table.Column<int>(type: "integer", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    ScheduleStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_ScheduleStatuses_ScheduleStatusId",
                        column: x => x.ScheduleStatusId,
                        principalTable: "ScheduleStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Changes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonNumber = table.Column<int>(type: "integer", nullable: false),
                    ClassroomId = table.Column<int>(type: "integer", nullable: false),
                    IsRemote = table.Column<bool>(type: "boolean", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    LessonGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Changes_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Changes_LessonGroups_LessonGroupId",
                        column: x => x.LessonGroupId,
                        principalTable: "LessonGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ClassroomId",
                table: "Lessons",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonGroupId",
                table: "Lessons",
                column: "LessonGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_ClassroomId",
                table: "Changes",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_LessonGroupId",
                table: "Changes",
                column: "LessonGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_ClassroomTypeId",
                table: "Classrooms",
                column: "ClassroomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroups_GroupId",
                table: "LessonGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroups_SubjectId",
                table: "LessonGroups",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroupTeachers_TeacherId",
                table: "LessonGroupTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ScheduleStatusId",
                table: "Schedules",
                column: "ScheduleStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Classrooms_ClassroomId",
                table: "Lessons",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_LessonGroups_LessonGroupId",
                table: "Lessons",
                column: "LessonGroupId",
                principalTable: "LessonGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Schedules_ScheduleId",
                table: "Lessons",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Classrooms_ClassroomId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_LessonGroups_LessonGroupId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Schedules_ScheduleId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "Changes");

            migrationBuilder.DropTable(
                name: "LessonGroupTeachers");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "YearBegin");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "LessonGroups");

            migrationBuilder.DropTable(
                name: "ScheduleStatuses");

            migrationBuilder.DropTable(
                name: "ClassroomTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubjects",
                table: "TeacherSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ClassroomId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LessonGroupId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonGroupId",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "TeacherSubjects",
                newName: "TeacherSubject");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Teacher");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "lesson_plan");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubject",
                newName: "IX_TeacherSubject_TeacherId");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Teacher",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Teacher",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Teacher",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Subject",
                newName: "Shortname");

            migrationBuilder.RenameColumn(
                name: "WeekOrderNumber",
                table: "lesson_plan",
                newName: "Weekday");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "lesson_plan",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "RnnId",
                table: "lesson_plan",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "IsRemote",
                table: "lesson_plan",
                newName: "isDistantce");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_ScheduleId",
                table: "lesson_plan",
                newName: "IX_lesson_plan_SubjectId");

            migrationBuilder.RenameColumn(
                name: "GroupCode",
                table: "Group",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Group",
                newName: "SpecialityId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TeacherSubject",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AudienceId",
                table: "lesson_plan",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeekNumber",
                table: "lesson_plan",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubject",
                table: "TeacherSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_plan",
                table: "lesson_plan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AudienceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    IsGeneral = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    IsGeneral = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonTeacher_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonTeacher_lesson_plan_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lesson_plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Shortname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AudienceTypeId = table.Column<int>(type: "integer", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audience_AudienceType_AudienceTypeId",
                        column: x => x.AudienceTypeId,
                        principalTable: "AudienceType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_SubjectId",
                table: "TeacherSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plan_AudienceId",
                table: "lesson_plan",
                column: "AudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plan_GroupId",
                table: "lesson_plan",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_SpecialityId",
                table: "Group",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Audience_AudienceTypeId",
                table: "Audience",
                column: "AudienceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacher_GroupId",
                table: "GroupTeacher",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacher_SubjectId",
                table: "GroupTeacher",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacher_TeacherId",
                table: "GroupTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTeacher_LessonId",
                table: "LessonTeacher",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTeacher_TeacherId",
                table: "LessonTeacher",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Speciality_SpecialityId",
                table: "Group",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plan_Audience_AudienceId",
                table: "lesson_plan",
                column: "AudienceId",
                principalTable: "Audience",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plan_Group_GroupId",
                table: "lesson_plan",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plan_Subject_SubjectId",
                table: "lesson_plan",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Subject_SubjectId",
                table: "TeacherSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Teacher_TeacherId",
                table: "TeacherSubject",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
