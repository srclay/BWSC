using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BWSC.Data;

namespace WebApplication4.Migrations
{
    [DbContext(typeof(SwimmingClubContext))]
    partial class SwimmingClubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BWSC.Models.CartItem", b =>
                {
                    b.Property<string>("ItemId");

                    b.Property<string>("CartId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("ItemId");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("BWSC.Models.Coach", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<int>("SquadID");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Surname");

                    b.HasKey("ID");

                    b.HasIndex("SquadID");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("BWSC.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 70);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 160);

                    b.Property<bool>("HasBeenShipped");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 160);

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("PaymentTransactionId");

                    b.Property<string>("Phone")
                        .HasAnnotation("MaxLength", 24);

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<decimal>("Total");

                    b.Property<string>("Username");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BWSC.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<double?>("UnitPrice");

                    b.Property<string>("Username");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("BWSC.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageFileName");

                    b.Property<decimal>("SellingPrice");

                    b.Property<string>("ShortName");

                    b.HasKey("ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BWSC.Models.Squad", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Squads");
                });

            modelBuilder.Entity("BWSC.Models.Swimmer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ASANumber");

                    b.Property<DateTime>("DOB");

                    b.Property<string>("FirstName");

                    b.Property<int>("SquadID");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Surname");

                    b.Property<byte[]>("photo");

                    b.HasKey("ID");

                    b.HasIndex("SquadID");

                    b.ToTable("Swimmers");
                });

            modelBuilder.Entity("BWSC.Models.CartItem", b =>
                {
                    b.HasOne("BWSC.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BWSC.Models.Coach", b =>
                {
                    b.HasOne("BWSC.Models.Squad", "Squad")
                        .WithMany("Coaches")
                        .HasForeignKey("SquadID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BWSC.Models.OrderDetail", b =>
                {
                    b.HasOne("BWSC.Models.Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BWSC.Models.Swimmer", b =>
                {
                    b.HasOne("BWSC.Models.Squad", "Squad")
                        .WithMany("Swimmers")
                        .HasForeignKey("SquadID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
