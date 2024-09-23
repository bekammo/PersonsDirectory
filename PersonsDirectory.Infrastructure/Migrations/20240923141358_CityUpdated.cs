using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonsDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Persons_PersonId",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedIndividuals_Persons_PersonId",
                table: "RelatedIndividuals");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Persons",
                newName: "City");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "RelatedIndividuals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PhoneNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Persons_PersonId",
                table: "PhoneNumbers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedIndividuals_Persons_PersonId",
                table: "RelatedIndividuals",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Persons_PersonId",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedIndividuals_Persons_PersonId",
                table: "RelatedIndividuals");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Persons",
                newName: "CityId");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "RelatedIndividuals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PhoneNumbers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Persons_PersonId",
                table: "PhoneNumbers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedIndividuals_Persons_PersonId",
                table: "RelatedIndividuals",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
