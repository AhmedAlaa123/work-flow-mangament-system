using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndTask.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addOrderInFlowSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowSteps_WorkFlowSteps_PreviousStepId",
                table: "WorkFlowSteps");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowSteps_PreviousStepId",
                table: "WorkFlowSteps");

            migrationBuilder.DropColumn(
                name: "PreviousStepId",
                table: "WorkFlowSteps");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "WorkFlowSteps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "WorkFlowSteps");

            migrationBuilder.AddColumn<Guid>(
                name: "PreviousStepId",
                table: "WorkFlowSteps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowSteps_PreviousStepId",
                table: "WorkFlowSteps",
                column: "PreviousStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowSteps_WorkFlowSteps_PreviousStepId",
                table: "WorkFlowSteps",
                column: "PreviousStepId",
                principalTable: "WorkFlowSteps",
                principalColumn: "Id");
        }
    }
}
