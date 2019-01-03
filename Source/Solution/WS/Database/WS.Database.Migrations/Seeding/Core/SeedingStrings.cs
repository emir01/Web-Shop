namespace WS.Database.Bootstrap.Seeding.SeedingContext
{
    /// <summary>
    /// Contains alias strings for the core entities
    /// seeded by the context.
    /// </summary>
    public static class SeedingStrings
    {
        #region Tenant

        public static string SystemTenantAlias = "SystemTenant";

        #endregion

        #region System User

        public static string SystemUser = "SystemUser";

        #endregion

        #region Business Domains

        public static string BusinessDomainBikeStore = "bikestore";
        public static string BusinessDomainBikeEquipmentStore = "bikeequipmentstore";
        public static string BusinessDomainBikeClothingStore = "bikeclothingstore";

        #endregion

        #region Categories

        public static string CAT_Product = "ctgr_product";

        public static string CategoryBks = "ctgr_bikes";
        public static string CategoryBksRoad = "ctgr_bikes_road";
        public static string CategoryBksMountain = "ctgr_bikes_mountain";
        public static string CategoryBksHybrid = "ctgr_bikes_hybrid";
        public static string CategoryBksCross = "ctgr_bikes_cross";

        public static string CategoryBikeEquipment = "ctgr_bikesequipment";
        public static string CategoryBikeEquipmentForks = "ctgr_bikesequipment_forks";
        public static string CategoryBikeEquipmentWheels = "ctgr_bikesequipment_wheels";
        public static string CategoryBikeEquipmentTires = "ctgr_bikesequipment_tires";
        public static string CategoryBikeEquipmentInnerTubes = "ctgr_bikesequipment_tires";
        public static string CategoryBikeEquipmentFrontGearset = "ctgr_bikesequipment_frontGearset";
        public static string CategoryBikeEquipmentBackGearset = "ctgr_bikesequipment_backGearset";

        public static string CategoryBikeClothing = "ctgr_bikeclothing";
        public static string CategoryBikeClothingTops = "ctgr_bikeclothing_tops";
        public static string CategoryBikeClothingBottoms = "ctgr_bikeclothing_bottoms";
        public static string CategoryBikeClothingShoes = "ctgr_bikeclothing_shoes";
        public static string CategoryBikeClothingWindJackets = "ctgr_bikeclothing_windJackets";
        public static string CategoryBikeClothingHelmets = "ctgr_bikeclothing_helmepts";

        #endregion

        #region Manufacturers

        public static string ManufacturerTrek = "manfctr_trek";
        public static string ManufacturerBontager = "manfctr_bontager";
        public static string ManufacturerGiant = "manfctr_giant";
        public static string ManufacturerForceCz = "manfctr_forceCz";
        public static string ManufacturerBiemme = "manfctr_biemee";
        public static string ManufacturerSpecialized = "manfctr_specialized";
        public static string ManufacturerGt = "manfctr_gt";
        public static string ManufacturerCube = "manfctr_cube";

        public static string ManufacturerShimano = "manfctr_shimano";
        public static string ManufacturerSram = "manfctr_sram";

        public static string ManufacturerSuntour = "manfctr_suntour";
        public static string ManufacturerRockShox = "manfctr_rockshox";
        public static string ManufacturerFox = "manfctr_fox";

        public static string ManufacturerMichelin = "manfctr_michelin";
        public static string ManufacturerContinental = "manfctr_continental";

        public static string ManufacturerGiro = "manfctr_giro";

        #endregion

        #region Tag Types

        public static string TagTypeBikeFrame = "tag_bike_frame";
        public static string TagTypeBikeFrontFork= "tag_bike_frontfork";
        public static string TagTypeBikeShifters = "tag_bike_shifters";
        public static string TagTypeFrontDerailleur = "tag_bike_frontderailleur";
        public static string TagTypeRearDerailleur = "tag_bike_rearderailleur";

        public static string TagTypeCrankset = "tag_bike_crankset";
        public static string TagTypeBottomBracket = "tag_bike_bottombracket";
        public static string TagTypeChain = "tag_bike_chain";
        public static string TagTypeCassette = "tag_bike_cassette";

        #endregion


    }
}


