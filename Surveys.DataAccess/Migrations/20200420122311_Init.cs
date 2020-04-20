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
                    Text = table.Column<string>(nullable: true)
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
                    AnswerType = table.Column<int>(nullable: false),
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
                columns: new[] { "Id", "Text" },
                values: new object[,]
                {
                    { new Guid("b3dc99bb-3cc2-4cc8-9289-7a82daa32f8b"), "What is your company size?" },
                    { new Guid("ca5e5523-3e9a-4712-a900-74a167895487"), "What is your IT team size (if any)?" },
                    { new Guid("20fc4144-61ec-4561-b322-f1730adb80f6"), "What is your growth ambition?" },
                    { new Guid("1ec6e5b9-cc4e-40ad-bfb5-733280df65f7"), "Do you own/maintain your own IT?" }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "Id", "CreatorEmail", "Name" },
                values: new object[] { new Guid("06ea9bbf-af38-4d52-b570-c1fd60042f57"), "john@john.com", "Main Survey" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "Salt" },
                values: new object[] { new Guid("c6418729-897d-44d1-b353-9cd1d83d1709"), "john@john.com", "John", new byte[] { 57, 146, 187, 206, 203, 10, 69, 3, 122, 169, 255, 93, 30, 13, 153, 99, 154, 177, 152, 37, 34, 98, 98, 86, 220, 93, 54, 231, 124, 30, 229, 67 }, "Admin", new byte[] { 161, 124, 253, 120, 4, 245, 161, 124, 67, 155, 41, 134, 127, 29, 170, 113, 108, 20, 20, 220, 127, 13, 113, 167, 54, 219, 47, 202, 47, 164 } });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "QuestionId", "SurveyId" },
                values: new object[,]
                {
                    { new Guid("7c54220c-f910-481a-9157-d5923bb01ff5"), new Guid("b3dc99bb-3cc2-4cc8-9289-7a82daa32f8b"), new Guid("06ea9bbf-af38-4d52-b570-c1fd60042f57") },
                    { new Guid("68d6a3c1-be2d-4a7b-a51a-d2a0adf028de"), new Guid("ca5e5523-3e9a-4712-a900-74a167895487"), new Guid("06ea9bbf-af38-4d52-b570-c1fd60042f57") },
                    { new Guid("53d2cb10-5388-4fef-a45f-ec49ad962a11"), new Guid("20fc4144-61ec-4561-b322-f1730adb80f6"), new Guid("06ea9bbf-af38-4d52-b570-c1fd60042f57") },
                    { new Guid("20e3c921-fad3-43d8-b28f-1a16b0d77662"), new Guid("1ec6e5b9-cc4e-40ad-bfb5-733280df65f7"), new Guid("06ea9bbf-af38-4d52-b570-c1fd60042f57") }
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
