using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.SeedingContext;
using WS.Database.Bootstrap.Seeding.Utility;
using WS.Database.Domain;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Company;
using WS.Database.Domain.Products;
using WS.Database.Domain.Tagging;

namespace WS.Database.Bootstrap.Seeding.Seeders.OrderedModules
{
    public class ProductSeeder : ISeeder
    {
        public int SeedOrder { get; set; }

        #region Ctor

        public ProductSeeder()
        {
            SeedOrder = 5;
        }

        #endregion

        public void SeedData(ISeedContext seedContext)
        {
            var tenant = seedContext.Tenant();

            AddBikes(seedContext, tenant);

            AddClothingProducts(seedContext, tenant);

            AddEquipmentProducts(seedContext, tenant);
        }

        #region Bikes

        public void AddBikes(ISeedContext seedContext, Tenant tenant)
        {
            var bikeList = GetBikeList(seedContext);

            foreach (var product in bikeList)
            {
                seedContext.Add(product.TimeStampNew().TimeStampTenant(tenant).Alias(product.Name));
            }
        }

        #region Build Bike List

        public List<Product> GetBikeList(ISeedContext seedContext)
        {
            var bikeList = new List<Product>();

            #region Mountain Bikes

            bikeList.AddRange(new List<Product>()
            {
                // TREK

                new Product()
                {
                    Name = "X-Caliber 9",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(35000,55000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek),
                    ProductTagValues = BuildTagValues(seedContext,
                        TagValueBuilder.Get(SeedingStrings.TagTypeBikeFrame, "Trek Caliber 29"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeBikeFrontFork, "Suntour M3030 27,5 65mm"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeBikeShifters, "Shimano Altus 3x8"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeFrontDerailleur, "Shimano Altus 3s"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeRearDerailleur, "Shimano ACERA 8s"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeCrankset, "Shimano M131 42/34/24T"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeBottomBracket, "FSA BB-7420"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeChain, "Shimano HG40 8sp"),
                        TagValueBuilder.Get(SeedingStrings.TagTypeCassette, "Shimano HG20 11-32T")
                    )
                },
                new Product()
                {
                    Name = "X-Caliber 5",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(25000,34000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name = "Superfly 9.9 SL",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(400000,450000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name = "Superfly 7",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(100000,120000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },

                // SPEICALIZED

                new Product()
                {
                    Name = "RockHopper",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(55000,80000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
                new Product()
                {
                    Name = "StumpJumper",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(80000,120000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
                new Product()
                {
                    Name = "Camber",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(80000,120000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
                new Product()
                {
                    Name = "Epic",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(120000,150000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },

                // GIANT

                new Product()
                {
                    Name = "Talon 29er",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(60000,150000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant)
                },
                new Product()
                {
                    Name = "Revel 29er",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(60000,150000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant)
                },
                new Product()
                {
                    Name = "Reign 27.5",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(60000,150000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant)
                },
                new Product()
                {
                    Name = "Trance SX 27.5",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(120000,150000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksMountain),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant)
                },
            });

            #endregion

            #region Road Bikes

            bikeList.AddRange(new List<Product>()
            {
                // TREK

                new Product()
                {
                    Name    = "Emonda SLR 10",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(250000, 350000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name    = "Emonda SLR 9",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(250000, 300000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name    = "Emonda SL 5",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(250000, 290000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name    = "Madone 2.1",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(150000, 200000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name    = "Madone 7.9",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(450000, 550000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },

                //GIANT

                new Product()
                {
                    Name = "Envie Advanced",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(350000, 450000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant)
                },
                new Product()
                {
                    Name = "TCR Advanced",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(550000, 650000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant)
                },
                new Product()
                {
                    Name = "Omnium",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(250000, 260000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant)
                },

                // SPECIALIZED
                new Product()
                {
                    Name = "S-Works Venge Dura Ace DI2",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(500000, 550000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
                new Product()
                {
                    Name = "Venge Pro Race",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(300000, 390000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
                new Product()
                {
                    Name = "Allez Pro",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(190000, 390000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
                new Product()
                {
                    Name = "S-Works Allez DI2",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(450000, 490000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksRoad),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
            });

            #endregion

            #region Hybrid Bikes

            bikeList.AddRange(new List<Product>()
            {
                // TREK
                new Product()
                {
                    Name = "Verve 4",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(120000,150000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksHybrid),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name = "Verve 3",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(90000,120000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksHybrid),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },

                //SPECIALIZED

                new Product()
                {
                    Name = "Sirrius Pro Carbon Disk",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(250000,350000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksHybrid),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },
                new Product()
                {
                    Name = "Sirrius Expert Carbon Disk",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(150000,200000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksHybrid),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                },

            });

            #endregion

            #region Cross Bikes

            bikeList.AddRange(new List<Product>()
            {
                //TREK

                new Product()
                {
                    Name = "Boone 9 Disc",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(500000,550000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksCross),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name = "Boone 7",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(400000,450000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksCross),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                },
                new Product()
                {
                    Name = "Crockett 7",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(120000,150000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBksCross),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek)
                }

            });

            #endregion

            return bikeList;
        }

        #endregion

        #endregion

        #region Clothing Products

        private void AddClothingProducts(ISeedContext seedContext, Tenant tenant)
        {
            var bikingClothes = GetClothingList(seedContext);

            foreach (var product in bikingClothes)
            {
                seedContext.Add(product.TimeStampNew().TimeStampTenant(tenant).Alias(product.Name));
            }
        }

        private List<Product> GetClothingList(ISeedContext seedContext)
        {
            var clothingList = new List<Product>();

            #region Bottoms

            clothingList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Short Bottoms",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(2500,5000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingBottoms),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBiemme)
                },
                new Product()
                {
                    Name = "Long Thermo Bottoms",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(2500,5000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingBottoms),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBiemme)
                },
                new Product()
                {
                    Name = "RXL Softshell Bib Tight with inForm Chamois",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(5500,7600),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingBottoms),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                }
            });

            #endregion

            #region Tops

            clothingList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "RXL",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(5500,7600),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingTops),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                new Product()
                {
                    Name = "Vella",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(2500,3500),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingTops),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                 new Product()
                {
                    Name = "RXL Thermal Long Sleeve Jersey",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(4900,5500),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingTops),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                }
            });

            #endregion

            #region Jackets

            clothingList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Winter Jacket",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(3400,4400),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingWindJackets),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBiemme)
                },
                new Product()
                {
                    Name = "Race Stormshell Jacket",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(6500,7600),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingWindJackets),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                }
            });

            #endregion

            #region Helments

            clothingList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Force Helmet",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(2500,3100),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingHelmets),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerForceCz)
                },
                 new Product()
                {
                    Name = "Quantum",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(2500,3100),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingHelmets),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                 new Product()
                {
                    Name = "Specter",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(7500,8100),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingHelmets),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                new Product()
                {
                    Name = "Oracle",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(11000,12000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingHelmets),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                }
            });

            #endregion

            #region Shoes

            clothingList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Race DLX Road WSD",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(6500,7500),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingShoes),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                  new Product()
                {
                    Name = "Race Mountain",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(5500,6500),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingShoes),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                   new Product()
                {
                    Name = "RL MTB",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(8000,9000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothingShoes),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                }
            });

            #endregion

            return clothingList;
        }

        #endregion

        #region Bike Equipment

        private void AddEquipmentProducts(ISeedContext seedContext, Tenant tenant)
        {
            var bikingEquipment = GetBikingEquipment(seedContext);

            foreach (var product in bikingEquipment)
            {
                seedContext.Add(product.TimeStampNew().TimeStampTenant(tenant).Alias(product.Name));
            }
        }

        private IEnumerable<Product> GetBikingEquipment(ISeedContext seedContext)
        {
            var equipmentList = new List<Product>();

            #region Forks

            equipmentList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "32 FLOAT 100 O/B RL Evolution Series",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(17000,19000),
                    PriceRegular = RandDataGenerator.GetRandomPrice(35000,39000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentForks),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerFox)
                },
                new Product()
                {
                    Name = "FOX 11 32 F100 REMOTE LOCKOUT FIT",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(30000,31000),
                    PriceRegular = RandDataGenerator.GetRandomPrice(61000,62000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentForks),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerFox)
                }
            });

            #endregion

            #region Front Gearset

            equipmentList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Shimano FC-5600",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(14000,16000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentFrontGearset),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerShimano)
                },
                 new Product()
                {
                    Name = "Shimano FC-M660-10",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(9000,11000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentFrontGearset),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerShimano)
                },
                 new Product()
                {
                    Name = "Shimano FC-M770-10",
                    Description = "With a stiff design, a reduction in weight and a touch of panache, the Shimano Deore XT FC-M770 mountain bike crankset offers everything you need to go rambling through the woods with speed and shifting efficiency!",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(15000,16000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentFrontGearset),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerShimano)
                }
            });

            #endregion

            #region Back Gearsets

            equipmentList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Sram PG730",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(700,800),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentBackGearset),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerSram)
                },
                new Product()
                {
                    Name = "Shimano SH 10 CSHG81 SLX, 11-34",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(3500,4000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentBackGearset),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerShimano)
                },
                new Product()
                {
                    Name = "Sram CS PG-970, 12-26",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(3000,3500),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentBackGearset),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerShimano)
                }
            });

            #endregion

            #region Inner Tubes

            equipmentList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Standard 29x2-2.4\" Inner Tube - Presta",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(400,450),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentInnerTubes),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                new Product()
                {
                    Name = "26\" CONTINENTAL SUPERSONIC",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(900,1100),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentInnerTubes),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerContinental)
                },
                new Product()
                {
                    Name = "20\" CONTINENTAL COMPACT",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(300,350),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentInnerTubes),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerContinental)
                }
            });

            #endregion

            #region Tires

            equipmentList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "XR4 26X2.2 comp",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(1500,1600),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentTires),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                new Product()
                {
                    Name = "R3 TLR Road 700x25c",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(900,1100),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentTires),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                new Product()
                {
                    Name = "Impac Crosspac",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(300,350),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentTires),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                }
            });

            #endregion

            #region Wheels

            equipmentList.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name = "Race X Lite TLR Front",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(20000,22000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentWheels),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                new Product()
                {
                    Name = "Race X Lite TLR Rear",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(34000,35000),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentWheels),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                },
                new Product()
                {
                    Name = "XXX TLR Disc 29",
                    PriceCurrent = RandDataGenerator.GetRandomPrice(2500,2900),
                    Category = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipmentWheels),
                    Manufacturer = seedContext.GetObjectForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager)
                }
            });

            #endregion

            return equipmentList;
        }

        #endregion

        #region Tagging Utilities

        private class TagValueBuilder
        {
            public string TagTypeAlias { get; set; }

            public string TagValue { get; set; }

            public static TagValueBuilder Get(string alias, string value)
            {
                return new TagValueBuilder()
                {
                    TagTypeAlias = alias,
                    TagValue = value
                };
            }
        }

        private List<ProductTagValue> BuildTagValues(ISeedContext seedContext, params TagValueBuilder[] builders)
        {
            return builders.Select(tagValueBuilder => new ProductTagValue()
            {
                TagType = seedContext.GetObjectForAlias<TagType>(tagValueBuilder.TagTypeAlias),
                Value = tagValueBuilder.TagValue
            }).ToList();
        }

        #endregion
    }
}
