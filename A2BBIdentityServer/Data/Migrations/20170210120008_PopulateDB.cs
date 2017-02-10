using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace A2BBIdentityServer.Data.Migrations
{
    public partial class PopulateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            migrationBuilder.Sql("INSERT INTO \"AspNetRoles\"(\"Id\", \"Name\", \"NormalizedName\") VALUES ('" + guid1.ToString() + "', 'Admin', 'ADMIN')");
            migrationBuilder.Sql("INSERT INTO \"AspNetRoles\"(\"Id\", \"Name\", \"NormalizedName\") VALUES ('" + guid2.ToString() + "', 'User', 'USER')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM a2bb.\"AspNetRoles\" WHERE \"Name\" = 'Admin'");
            migrationBuilder.Sql("DELETE FROM a2bb.\"AspNetRoles\" WHERE \"Name\" = 'User'");
        }
    }
}
