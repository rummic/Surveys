﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Surveys.DataAccess;

namespace Surveys.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200416211219_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Surveys.Domain.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Surveys.Domain.Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Surveys.Domain.SurveyQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestions");
                });

            modelBuilder.Entity("Surveys.Domain.SurveyQuestionAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnswerType")
                        .HasColumnType("int");

                    b.Property<Guid?>("SurveyQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SurveyQuestionId");

                    b.ToTable("SurveyQuestionAnswers");
                });

            modelBuilder.Entity("Surveys.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Surveys.Domain.Survey", b =>
                {
                    b.HasOne("Surveys.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Surveys.Domain.SurveyQuestion", b =>
                {
                    b.HasOne("Surveys.Domain.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("Surveys.Domain.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("Surveys.Domain.SurveyQuestionAnswer", b =>
                {
                    b.HasOne("Surveys.Domain.SurveyQuestion", "SurveyQuestion")
                        .WithMany()
                        .HasForeignKey("SurveyQuestionId");
                });
#pragma warning restore 612, 618
        }
    }
}