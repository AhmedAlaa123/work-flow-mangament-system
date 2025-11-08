using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndTask.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addUserForStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_AspNetRoles_RoleId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_RoleId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Steps");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "WorkFlowSteps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowSteps_UserId",
                table: "WorkFlowSteps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowSteps_AspNetUsers_UserId",
                table: "WorkFlowSteps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowSteps_AspNetUsers_UserId",
                table: "WorkFlowSteps");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowSteps_UserId",
                table: "WorkFlowSteps");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkFlowSteps");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Steps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RoleId",
                table: "Steps",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_AspNetRoles_RoleId",
                table: "Steps",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
