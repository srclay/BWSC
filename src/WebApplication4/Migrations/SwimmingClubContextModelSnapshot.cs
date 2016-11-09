using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BWSC.Data;

namespace BWSC.Migrations
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

            modelBuilder.Entity("BWSC.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageFileName");

                    b.Property<int>("ProductOptionGroupID");

                    b.Property<decimal>("SellingPrice");

                    b.Property<string>("ShortName");

                    b.HasKey("ID");

                    b.HasIndex("ProductOptionGroupID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BWSC.Models.ProductOptionGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GroupName");

                    b.HasKey("ID");

                    b.ToTable("ProductOptionGroup");
                });

            modelBuilder.Entity("BWSC.Models.ProductOptionValue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("ProductOptionGroupID");

                    b.HasKey("ID");

                    b.HasIndex("ProductOptionGroupID");

                    b.ToTable("ProductOptionValue");
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

            modelBuilder.Entity("BWSC.Models.Product", b =>
                {
                    b.HasOne("BWSC.Models.ProductOptionGroup", "ProductOptionGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductOptionGroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BWSC.Models.ProductOptionValue", b =>
                {
                    b.HasOne("BWSC.Models.ProductOptionGroup", "ProductOptionGroup")
                        .WithMany("ProductOptionValues")
                        .HasForeignKey("ProductOptionGroupID")
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
