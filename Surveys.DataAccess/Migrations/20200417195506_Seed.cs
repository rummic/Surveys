using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Surveys.DataAccess.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Surveys");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "SurveyQuestionAnswers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Text" },
                values: new object[,]
                {
                    { new Guid("7080dd98-06eb-4467-a4d9-2cab8ac4b219"), "What is your company size?" },
                    { new Guid("5d99444a-52a0-4d5c-afb1-bbc527fc9cec"), "What is your IT team size (if any)?" },
                    { new Guid("d7f1514d-c016-48fc-bbb4-ef19ef20012d"), "What is your growth ambition?" },
                    { new Guid("1335a953-7dcc-44a4-9f0f-609d88b02628"), "Do you own/maintain your own IT?" }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                column: "Id",
                value: new Guid("a44924ea-3a2a-41d0-ac4b-c9e34aae1160"));

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "QuestionId", "SurveyId" },
                values: new object[] { new Guid("95419993-816b-4e4f-80f8-07f23486184c"), new Guid("7080dd98-06eb-4467-a4d9-2cab8ac4b219"), new Guid("a44924ea-3a2a-41d0-ac4b-c9e34aae1160") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("1335a953-7dcc-44a4-9f0f-609d88b02628"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("5d99444a-52a0-4d5c-afb1-bbc527fc9cec"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("d7f1514d-c016-48fc-bbb4-ef19ef20012d"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("95419993-816b-4e4f-80f8-07f23486184c"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("7080dd98-06eb-4467-a4d9-2cab8ac4b219"));

            migrationBuilder.DeleteData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("a44924ea-3a2a-41d0-ac4b-c9e34aae1160"));

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "SurveyQuestionAnswers");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
