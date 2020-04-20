using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Surveys.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    AnswerType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatorEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Password = table.Column<byte[]>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SurveyId = table.Column<Guid>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SurveyQuestionId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    SubmittedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionAnswers_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AnswerType", "Text" },
                values: new object[,]
                {
                    { new Guid("14c2d0fb-3ef3-4ecf-8ea2-9cef40447fb7"), 0, "What is your company size?" },
                    { new Guid("30e01f7a-7a75-41ee-bd15-60988f7a3313"), 0, "What is your IT team size (if any)?" },
                    { new Guid("f67f87af-3bd2-4bb9-9c00-519a44a110a3"), 0, "What is your growth ambition?" },
                    { new Guid("51e2951b-df94-409f-a730-0d2b4b6a59f2"), 0, "Do you own/maintain your own IT?" }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "Id", "CreatorEmail", "Name" },
                values: new object[] { new Guid("1fa77e5a-edff-4b82-bbea-f2233cc34c88"), "john@john.com", "Main Survey" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "Salt" },
                values: new object[] { new Guid("b1db141e-da43-462f-961a-d89a834983fa"), "john@john.com", "John", new byte[] { 170, 149, 246, 231, 104, 39, 0, 227, 245, 61, 14, 132, 233, 180, 216, 24, 213, 225, 232, 126, 101, 119, 44, 240, 216, 192, 200, 31, 156, 67, 99, 41 }, "Admin", new byte[] { 101, 97, 155, 140, 8, 254, 33, 192, 53, 185, 85, 83, 2, 138, 87, 253, 187, 29, 241, 64, 135, 22, 201, 10, 105, 18, 109, 1, 253, 73 } });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "QuestionId", "SurveyId" },
                values: new object[,]
                {
                    { new Guid("dbb90875-f14c-4b90-a5b0-f6d9bf6aa39f"), new Guid("14c2d0fb-3ef3-4ecf-8ea2-9cef40447fb7"), new Guid("1fa77e5a-edff-4b82-bbea-f2233cc34c88") },
                    { new Guid("51e773c9-aa9a-4c5d-a05c-eaa4b44eb1fc"), new Guid("30e01f7a-7a75-41ee-bd15-60988f7a3313"), new Guid("1fa77e5a-edff-4b82-bbea-f2233cc34c88") },
                    { new Guid("c44a8d02-1d2b-41a9-80ed-f133f7c8d39b"), new Guid("f67f87af-3bd2-4bb9-9c00-519a44a110a3"), new Guid("1fa77e5a-edff-4b82-bbea-f2233cc34c88") },
                    { new Guid("22517fb4-23a7-4b27-92cf-05498da0fb03"), new Guid("51e2951b-df94-409f-a730-0d2b4b6a59f2"), new Guid("1fa77e5a-edff-4b82-bbea-f2233cc34c88") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionAnswers_SurveyQuestionId",
                table: "SurveyQuestionAnswers",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionAnswers_UserId",
                table: "SurveyQuestionAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyQuestionAnswers");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
