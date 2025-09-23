using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RefactorOrgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExposureSummaries_Organizations_OrgId",
                table: "ExposureSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskReports_Organizations_OrgId",
                table: "RiskReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Risks_Organizations_OrgId",
                table: "Risks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Organization");

            migrationBuilder.AlterColumn<string>(
                name: "OrgId",
                table: "Risks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Risks",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationOrgId",
                table: "Risks",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrgId",
                table: "RiskReports",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationOrgId",
                table: "RiskReports",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrgId",
                table: "ExposureSummaries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationOrgId",
                table: "ExposureSummaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Risks_OrganizationOrgId",
                table: "Risks",
                column: "OrganizationOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskReports_OrganizationOrgId",
                table: "RiskReports",
                column: "OrganizationOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_ExposureSummaries_OrganizationOrgId",
                table: "ExposureSummaries",
                column: "OrganizationOrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExposureSummaries_AspNetUsers_OrgId",
                table: "ExposureSummaries",
                column: "OrgId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExposureSummaries_Organization_OrganizationOrgId",
                table: "ExposureSummaries",
                column: "OrganizationOrgId",
                principalTable: "Organization",
                principalColumn: "OrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskReports_AspNetUsers_OrgId",
                table: "RiskReports",
                column: "OrgId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskReports_Organization_OrganizationOrgId",
                table: "RiskReports",
                column: "OrganizationOrgId",
                principalTable: "Organization",
                principalColumn: "OrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_AspNetUsers_OrgId",
                table: "Risks",
                column: "OrgId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_Organization_OrganizationOrgId",
                table: "Risks",
                column: "OrganizationOrgId",
                principalTable: "Organization",
                principalColumn: "OrgId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExposureSummaries_AspNetUsers_OrgId",
                table: "ExposureSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_ExposureSummaries_Organization_OrganizationOrgId",
                table: "ExposureSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskReports_AspNetUsers_OrgId",
                table: "RiskReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskReports_Organization_OrganizationOrgId",
                table: "RiskReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Risks_AspNetUsers_OrgId",
                table: "Risks");

            migrationBuilder.DropForeignKey(
                name: "FK_Risks_Organization_OrganizationOrgId",
                table: "Risks");

            migrationBuilder.DropIndex(
                name: "IX_Risks_OrganizationOrgId",
                table: "Risks");

            migrationBuilder.DropIndex(
                name: "IX_RiskReports_OrganizationOrgId",
                table: "RiskReports");

            migrationBuilder.DropIndex(
                name: "IX_ExposureSummaries_OrganizationOrgId",
                table: "ExposureSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "OrganizationOrgId",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "OrganizationOrgId",
                table: "RiskReports");

            migrationBuilder.DropColumn(
                name: "OrganizationOrgId",
                table: "ExposureSummaries");

            migrationBuilder.RenameTable(
                name: "Organization",
                newName: "Organizations");

            migrationBuilder.AlterColumn<long>(
                name: "OrgId",
                table: "Risks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Risks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<long>(
                name: "OrgId",
                table: "RiskReports",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "OrgId",
                table: "ExposureSummaries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "OrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExposureSummaries_Organizations_OrgId",
                table: "ExposureSummaries",
                column: "OrgId",
                principalTable: "Organizations",
                principalColumn: "OrgId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskReports_Organizations_OrgId",
                table: "RiskReports",
                column: "OrgId",
                principalTable: "Organizations",
                principalColumn: "OrgId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_Organizations_OrgId",
                table: "Risks",
                column: "OrgId",
                principalTable: "Organizations",
                principalColumn: "OrgId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
