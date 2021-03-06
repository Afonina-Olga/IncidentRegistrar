// <auto-generated />
using System;
using IncidentRegistrar.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IncidentRegistrar.UI.Migrations
{
    [DbContext(typeof(IncidentRegistrarDbContext))]
    [Migration("20220213133332_User")]
    partial class User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.14");

            modelBuilder.Entity("IncidentRegistrar.UI.Models.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IncidentType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ResolutionType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("IncidentRegistrar.UI.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("IncidentRegistrar.UI.Models.ParticipantIncident", b =>
                {
                    b.Property<int>("ParticipantId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IncidentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ParticipantId", "IncidentId");

                    b.HasIndex("IncidentId");

                    b.ToTable("ParticipantIncident");
                });

            modelBuilder.Entity("IncidentRegistrar.UI.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("ConvictionsCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("IncidentRegistrar.UI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IncidentRegistrar.UI.Models.Participant", b =>
                {
                    b.HasOne("IncidentRegistrar.UI.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("IncidentRegistrar.UI.Models.ParticipantIncident", b =>
                {
                    b.HasOne("IncidentRegistrar.UI.Models.Incident", "Incident")
                        .WithMany()
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IncidentRegistrar.UI.Models.Participant", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Incident");

                    b.Navigation("Participant");
                });
#pragma warning restore 612, 618
        }
    }
}
