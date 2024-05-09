using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padrrif.Migrations
{
    /// <inheritdoc />
    public partial class Thrid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComiteeId",
                table: "User",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Comittee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comittee", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EducationLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OwnerShipType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerShipType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Village", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Damage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DocumentNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EducationLevelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FamilyMembers = table.Column<int>(type: "int", nullable: false),
                    ChildrenUnderEighteen = table.Column<int>(type: "int", nullable: false),
                    IsOtherProfession = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MonthlyIncomeFromAgriculture = table.Column<double>(type: "double", nullable: false),
                    ReliancePercentage = table.Column<double>(type: "double", nullable: false),
                    LandNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BasinNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VillageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegionType = table.Column<int>(type: "int", nullable: false),
                    OwnershipTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ContractorName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContractorId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ContractDuration = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EventDuration = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalArea = table.Column<double>(type: "double", nullable: true),
                    DamageRateLastFiveYears = table.Column<double>(type: "double", nullable: true),
                    Latitude = table.Column<double>(type: "double", nullable: true),
                    Longitude = table.Column<double>(type: "double", nullable: true),
                    IsIsraeliArmyChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSeparationWallChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMilitaryZoneChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMarketClosedChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SettlementName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyOrFactoryName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSnowChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsStrongWindChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsHailChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFloodChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDroughtChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFrostChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsExtremeTemperatureDropChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsExtremeTemperatureRiseChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OtherTabTwoText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAnimalTremblingChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsBirdFluChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsLocustChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsHarmfulWeedsChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    otherTabThreeText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Damage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Damage_EducationLevel_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Damage_OwnerShipType_OwnershipTypeId",
                        column: x => x.OwnershipTypeId,
                        principalTable: "OwnerShipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Damage_Village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "Village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnimalDamage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DamageType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Insurance = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalNumber = table.Column<int>(type: "int", nullable: true),
                    AffectedNumber = table.Column<int>(type: "int", nullable: true),
                    ProductionRate = table.Column<double>(type: "double", nullable: true),
                    PriceBeforeDamage = table.Column<double>(type: "double", nullable: true),
                    InsuranceProvider = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DamageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalDamage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalDamage_Damage_DamageId",
                        column: x => x.DamageId,
                        principalTable: "Damage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FarmFacilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FacilitiesType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DamageFacilitiesType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryFacilities = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LicenseFacilities = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InsuranceFacilities = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalAffectedArea = table.Column<double>(type: "double", nullable: true),
                    ActualDamageArea = table.Column<double>(type: "double", nullable: true),
                    LicenseProvider = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InsuranceProvider = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DamageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmFacilities_Damage_DamageId",
                        column: x => x.DamageId,
                        principalTable: "Damage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FisheryDamage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FishDamageType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Insurance = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalNumber = table.Column<int>(type: "int", nullable: true),
                    AffectedNumber = table.Column<int>(type: "int", nullable: true),
                    ProductionRate = table.Column<double>(type: "double", nullable: true),
                    PriceBeforeDamage = table.Column<double>(type: "double", nullable: true),
                    InsuranceProvider = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DamageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FisheryDamage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FisheryDamage_Damage_DamageId",
                        column: x => x.DamageId,
                        principalTable: "Damage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlantDamage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CropType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CropDamage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CropAge = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IrrigationMethod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IrrigationOption = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtherTextFieldValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CropTypeMethod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CropTypeOption = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtherCropTextFieldValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalAffectedArea = table.Column<double>(type: "double", nullable: true),
                    ActualDamageArea = table.Column<double>(type: "double", nullable: true),
                    PercentageDamage = table.Column<double>(type: "double", nullable: true),
                    EstimatedProductionRate = table.Column<double>(type: "double", nullable: true),
                    EstimatedPrice = table.Column<double>(type: "double", nullable: true),
                    FruitingStage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Insurance = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InsuranceProvider = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DamageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDamage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantDamage_Damage_DamageId",
                        column: x => x.DamageId,
                        principalTable: "Damage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DailyWorkHours = table.Column<double>(type: "double", nullable: true),
                    WeeklyWorkHours = table.Column<double>(type: "double", nullable: true),
                    DamageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkHours_Damage_DamageId",
                        column: x => x.DamageId,
                        principalTable: "Damage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_User_ComiteeId",
                table: "User",
                column: "ComiteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalDamage_DamageId",
                table: "AnimalDamage",
                column: "DamageId");

            migrationBuilder.CreateIndex(
                name: "IX_Damage_EducationLevelId",
                table: "Damage",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Damage_OwnershipTypeId",
                table: "Damage",
                column: "OwnershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Damage_VillageId",
                table: "Damage",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmFacilities_DamageId",
                table: "FarmFacilities",
                column: "DamageId");

            migrationBuilder.CreateIndex(
                name: "IX_FisheryDamage_DamageId",
                table: "FisheryDamage",
                column: "DamageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDamage_DamageId",
                table: "PlantDamage",
                column: "DamageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_DamageId",
                table: "WorkHours",
                column: "DamageId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Comittee_ComiteeId",
                table: "User",
                column: "ComiteeId",
                principalTable: "Comittee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Comittee_ComiteeId",
                table: "User");

            migrationBuilder.DropTable(
                name: "AnimalDamage");

            migrationBuilder.DropTable(
                name: "Comittee");

            migrationBuilder.DropTable(
                name: "FarmFacilities");

            migrationBuilder.DropTable(
                name: "FisheryDamage");

            migrationBuilder.DropTable(
                name: "PlantDamage");

            migrationBuilder.DropTable(
                name: "WorkHours");

            migrationBuilder.DropTable(
                name: "Damage");

            migrationBuilder.DropTable(
                name: "EducationLevel");

            migrationBuilder.DropTable(
                name: "OwnerShipType");

            migrationBuilder.DropTable(
                name: "Village");

            migrationBuilder.DropIndex(
                name: "IX_User_ComiteeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ComiteeId",
                table: "User");
        }
    }
}
