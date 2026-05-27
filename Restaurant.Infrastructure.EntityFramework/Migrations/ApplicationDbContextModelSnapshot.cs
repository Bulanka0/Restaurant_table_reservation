
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Restaurant.Infrastructure.EntityFramework.Data;

#nullable disable

namespace Restaurant.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Domain.Entities.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Restaurant.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Restaurant.Domain.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ConfirmedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GuestsCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("TableId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ConfirmedBy");

                    b.HasIndex("TableId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Restaurant.Domain.Entities.Table", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("Seats")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Restaurant.Domain.Entities.WorkingHours", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("CloseTime")
                        .HasColumnType("interval");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<bool>("IsWorkingDay")
                        .HasColumnType("boolean");

                    b.Property<TimeSpan>("OpenTime")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.ToTable("WorkingHours");
                });

            modelBuilder.Entity("Restaurant.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("Restaurant.Domain.Entities.Client", "Client")
                        .WithMany("_reservations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Domain.Entities.Admin", null)
                        .WithMany()
                        .HasForeignKey("ConfirmedBy");

                    b.HasOne("Restaurant.Domain.Entities.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Restaurant.Domain.Entities.Client", b =>
                {
                    b.Navigation("_reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
