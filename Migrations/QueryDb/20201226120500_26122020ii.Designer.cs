﻿// <auto-generated />
using System;
using BillerClientConsole.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BillerClientConsole.Migrations.QueryDb
{
    [DbContext(typeof(QueryDbContext))]
    [Migration("20201226120500_26122020ii")]
    partial class _26122020ii
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BillerClientConsole.Models.QueryModel.Queries", b =>
                {
                    b.Property<Guid>("QueryID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("HasQuery");

                    b.Property<string>("applicationRef");

                    b.Property<string>("comment");

                    b.Property<string>("dateCreated");

                    b.Property<string>("description");

                    b.Property<string>("emailAddress");

                    b.Property<string>("officeid");

                    b.Property<string>("status");

                    b.Property<string>("tableName");

                    b.HasKey("QueryID");

                    b.ToTable("Queries");
                });

            modelBuilder.Entity("BillerClientConsole.Models.QueryModel.QueryHistory", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QueryCount");

                    b.Property<string>("applicationRef");

                    b.Property<string>("comment");

                    b.Property<string>("dateCreated");

                    b.Property<string>("description");

                    b.Property<string>("status");

                    b.Property<string>("tableName");

                    b.HasKey("id");

                    b.ToTable("QueryHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
