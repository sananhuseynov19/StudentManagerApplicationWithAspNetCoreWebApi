﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentManagerApplicationWithWebApi.Data;

#nullable disable

namespace StudentManagerApplicationWithWebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenderId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("GenderId");

                    b.ToTable("Genders", (string)null);
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<int>("Age")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasMaxLength(10)
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("StudentId");

                    b.HasIndex("GenderId");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.StudentCourse", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourse", (string)null);
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                    b.Property<int>("Age")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("TeacherId");

                    b.HasIndex("GenderId");

                    b.ToTable("Teachers", (string)null);
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.TeacherCourses", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("TeacherCourse", (string)null);
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.TeacherStudent", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("TeacherStudent", (string)null);
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Student", b =>
                {
                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Gender", "Gender")
                        .WithMany("Students")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.StudentCourse", b =>
                {
                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Course", "Courses")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Student", "Students")
                        .WithMany("StudentCourse")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Teacher", b =>
                {
                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Gender", "Gender")
                        .WithMany("Teachers")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.TeacherCourses", b =>
                {
                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Course", "Courses")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Teacher", "Teachers")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.TeacherStudent", b =>
                {
                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Student", "Students")
                        .WithMany("TeacherStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("StudentManagerApplicationWithWebApi.Data.Entities.Teacher", "Teachers")
                        .WithMany("TeacherStudents")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Course", b =>
                {
                    b.Navigation("StudentCourses");

                    b.Navigation("TeacherCourses");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Gender", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Student", b =>
                {
                    b.Navigation("StudentCourse");

                    b.Navigation("TeacherStudents");
                });

            modelBuilder.Entity("StudentManagerApplicationWithWebApi.Data.Entities.Teacher", b =>
                {
                    b.Navigation("TeacherCourses");

                    b.Navigation("TeacherStudents");
                });
#pragma warning restore 612, 618
        }
    }
}
