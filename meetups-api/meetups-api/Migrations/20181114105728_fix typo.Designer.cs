﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using meetupsApi.Models;

namespace meetupsApi.Migrations
{
    [DbContext(typeof(MeetupsApiContext))]
    [Migration("20181114105728_fix typo")]
    partial class fixtypo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("meetupsApi.Domain.Entity.ConnpassEventDataEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EventDescription");

                    b.Property<string>("EventTitle");

                    b.Property<string>("EventUrl");

                    b.Property<double>("Lat");

                    b.Property<double>("Lon");

                    b.HasKey("Id");

                    b.ToTable("ConnpassEventDataEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
