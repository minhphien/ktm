﻿// <auto-generated />
using System;
using KMS.Product.Ktm.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KMS.Product.Ktm.Repository.Migrations
{
    [DbContext(typeof(KtmDbContext))]
    partial class KtmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AssignmentId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssigneeComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssigneeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("MentorComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("StatusId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.AssignmentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AssignmentItemId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssigneeComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("MentorComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("CheckListItemId");

                    b.HasIndex("StatusId");

                    b.ToTable("AssignmentItems");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CheckListId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("CheckLists");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.CheckListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CheckListItemId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CheckListId");

                    b.HasIndex("ItemId");

                    b.ToTable("CheckListItems");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EmployeeId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentTeam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeBadgeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeRoleId")
                        .HasColumnType("int");

                    b.Property<string>("FirstMidName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MentorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("SlackUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeRoleId");

                    b.HasIndex("MentorId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.EmployeeRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EmployeeRoleId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "Default"
                        });
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.EmployeeTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EmployeeTeamId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReleseadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("TeamID");

                    b.ToTable("EmployeeTeams");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ItemId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tilte")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Kudo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("KudoId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("KudoDetailId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KudoDetailId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Kudos");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.KudoDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("KudoDetailId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("KudoTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("SlackEmoji")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KudoTypeId");

                    b.ToTable("KudoDetails");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.KudoType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("KudoTypeId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KudoTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeName = "Default"
                        });
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StatusId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "To Do"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "In Progress"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Done"
                        });
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TeamId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Assignment", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.Employee", "Assignee")
                        .WithMany("Assignments")
                        .HasForeignKey("AssigneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.Status", "Status")
                        .WithMany("Assignments")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.AssignmentItem", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.Assignment", "Assignment")
                        .WithMany("AssignmentItems")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.CheckListItem", "CheckListItem")
                        .WithMany("AssignmentItems")
                        .HasForeignKey("CheckListItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.Status", "Status")
                        .WithMany("AssignmentItems")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.CheckList", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.Team", "Team")
                        .WithMany("CheckLists")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.CheckListItem", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.CheckList", "CheckList")
                        .WithMany("CheckListItems")
                        .HasForeignKey("CheckListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.Item", "Item")
                        .WithMany("CheckListItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Employee", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.EmployeeRole", "EmployeeRole")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeeRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.Employee", "Mentor")
                        .WithMany("Mentee")
                        .HasForeignKey("MentorId");
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.EmployeeTeam", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.Employee", "Employee")
                        .WithMany("EmployeeTeams")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.Team", "Team")
                        .WithMany("EmployeeTeams")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Item", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.Employee", "Creator")
                        .WithMany("CheckListItems")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.Kudo", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.KudoDetail", "KudoDetail")
                        .WithMany("Kudos")
                        .HasForeignKey("KudoDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.Employee", "Receiver")
                        .WithMany("KudoReceives")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KMS.Product.Ktm.Entities.Models.Employee", "Sender")
                        .WithMany("KudoSends")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KMS.Product.Ktm.Entities.Models.KudoDetail", b =>
                {
                    b.HasOne("KMS.Product.Ktm.Entities.Models.KudoType", "KudoType")
                        .WithMany("KudoDetails")
                        .HasForeignKey("KudoTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
