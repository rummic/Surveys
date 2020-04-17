using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Surveys.DataAccess.Migrations
{
    public partial class MovedUserToSurveyQuestionAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_UserId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Surveys");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SurveyQuestionAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionAnswers_UserId",
                table: "SurveyQuestionAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestionAnswers_Users_UserId",
                table: "SurveyQuestionAnswers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestionAnswers_Users_UserId",
                table: "SurveyQuestionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestionAnswers_UserId",
                table: "SurveyQuestionAnswers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SurveyQuestionAnswers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Surveys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_UserId",
                table: "Surveys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
