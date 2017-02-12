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
            Guid adminRoleGuid = Guid.NewGuid();
            Guid userRoleGuid = Guid.NewGuid();
            migrationBuilder.Sql("INSERT INTO \"AspNetRoles\"(\"Id\", \"Name\", \"NormalizedName\") VALUES ('" + adminRoleGuid.ToString() + "', 'Admin', 'ADMIN')");
            migrationBuilder.Sql("INSERT INTO \"AspNetRoles\"(\"Id\", \"Name\", \"NormalizedName\") VALUES ('" + userRoleGuid.ToString() + "', 'User', 'USER')");

            Guid firstAdminGuid = Guid.NewGuid();
            migrationBuilder.Sql("INSERT INTO a2bb.\"AspNetUsers\"(" +
                "\"Id\", " +
                "\"AccessFailedCount\", " +
                "\"ConcurrencyStamp\", " +
                "\"Email\", " +
                "\"EmailConfirmed\", " +
                "\"LockoutEnabled\", " +
                "\"LockoutEnd\", " +
                "\"NormalizedEmail\", " +
                "\"NormalizedUserName\", " +
                "\"PasswordHash\", " +
                "\"PhoneNumber\", " +
                "\"PhoneNumberConfirmed\", " +
                "\"SecurityStamp\", " +
                "\"TwoFactorEnabled\", " +
                "\"UserName\") " +

                "VALUES (" +
                "'" + firstAdminGuid.ToString() + "', " +
                "0, " +
                "NULL, " +
                "NULL, " +
                "TRUE, " +
                "TRUE, " +
                "NULL, " +
                "NULL, " +
                "'ADMIN', " +
                "'AQAAAAEAACcQAAAAEPj+GyOUWfy6DX0G6NIFFCXt2pujSCHYBAb0GjM9g8LgIDZj9JvLHmezk72OjGmAMw==', " +
                "NULL, " +
                "TRUE, " +
                "NULL, " +
                "FALSE, " +
                "'Admin')");

            migrationBuilder.Sql("INSERT INTO a2bb.\"AspNetUserRoles\"(\"UserId\", \"RoleId\") " +
                "VALUES ('" + firstAdminGuid + "', '" + adminRoleGuid + "')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM a2bb.\"AspNetRoles\" WHERE \"Name\" = 'Admin'");
            migrationBuilder.Sql("DELETE FROM a2bb.\"AspNetRoles\" WHERE \"Name\" = 'User'");

            migrationBuilder.Sql("DELETE FROM a2bb.\"AspNetUsers\" WHERE \"Name\" = 'Admin'");
        }
    }
}
