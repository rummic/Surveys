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
    [Migration("20200417195506_Seed")]
    partial class Seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Surveys.Domain.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7080dd98-06eb-4467-a4d9-2cab8ac4b219"),
                            Text = "What is your company size?"
                        },
                        new
                        {
                            Id = new Guid("5d99444a-52a0-4d5c-afb1-bbc527fc9cec"),
                            Text = "What is your IT team size (if any)?"
                        },
                        new
                        {
                            Id = new Guid("d7f1514d-c016-48fc-bbb4-ef19ef20012d"),
                            Text = "What is your growth ambition?"
                        },
                        new
                        {
                            Id = new Guid("1335a953-7dcc-44a4-9f0f-609d88b02628"),
                            Text = "Do you own/maintain your own IT?"
                        });
                });

            modelBuilder.Entity("Surveys.Domain.Entities.Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Surveys");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a44924ea-3a2a-41d0-ac4b-c9e34aae1160")
                        });
                });

            modelBuilder.Entity("Surveys.Domain.Entities.SurveyQuestion", b =>
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("95419993-816b-4e4f-80f8-07f23486184c"),
                            QuestionId = new Guid("7080dd98-06eb-4467-a4d9-2cab8ac4b219"),
                            SurveyId = new Guid("a44924ea-3a2a-41d0-ac4b-c9e34aae1160")
                        });
                });

            modelBuilder.Entity("Surveys.Domain.Entities.SurveyQuestionAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnswerType")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SurveyQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SurveyQuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("SurveyQuestionAnswers");
                });

            modelBuilder.Entity("Surveys.Domain.Entities.User", b =>
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

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Surveys.Domain.Entities.SurveyQuestion", b =>
                {
                    b.HasOne("Surveys.Domain.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("Surveys.Domain.Entities.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("Surveys.Domain.Entities.SurveyQuestionAnswer", b =>
                {
                    b.HasOne("Surveys.Domain.Entities.SurveyQuestion", "SurveyQuestion")
                        .WithMany()
                        .HasForeignKey("SurveyQuestionId");

                    b.HasOne("Surveys.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
