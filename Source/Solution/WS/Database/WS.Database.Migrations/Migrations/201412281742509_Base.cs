namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailabilityMetricTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AvailabilityMetricUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        AvailabilityMetricTemplateId = c.Int(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AvailabilityMetricTemplates", t => t.AvailabilityMetricTemplateId)
                .ForeignKey("dbo.AvailabilityMetricUnits", t => t.ParentId)
                .Index(t => t.AvailabilityMetricTemplateId, name: "IX_AvailabilityMetricTemplate_Id")
                .Index(t => t.ParentId, name: "IX_Parent_Id");
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Stock = c.Int(nullable: false),
                        PriceRegular = c.Double(nullable: false),
                        PriceCurrent = c.Double(nullable: false),
                        ViewCount = c.Long(nullable: false),
                        IsOnSale = c.Boolean(nullable: false),
                        IsFeatured = c.Boolean(nullable: false),
                        IsTopSeller = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        AvailabilityMetricTemplateId = c.Int(),
                        CategoryId = c.Int(),
                        TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AvailabilityMetricTemplates", t => t.AvailabilityMetricTemplateId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .Index(t => t.AvailabilityMetricTemplateId, name: "IX_AvailabilityMetricTemplate_Id")
                .Index(t => t.CategoryId, name: "IX_Category_Id")
                .Index(t => t.TenantId, name: "IX_Tenant_Id");
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        ParentId = c.Int(),
                        ManufacturerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .Index(t => t.ParentId, name: "IX_Parent_Id")
                .Index(t => t.ManufacturerId, name: "IX_Manufacturer_Id");
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPrimary = c.Boolean(nullable: false),
                        DateUploaded = c.DateTime(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        ImageId = c.Int(),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppImages", t => t.ImageId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ImageId, name: "IX_Image_Id")
                .Index(t => t.ProductId, name: "IX_Product_Id");
            
            CreateTable(
                "dbo.AppImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Uri = c.String(),
                        Type = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMetricValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        AvailabilityMetricUnitId = c.Int(),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AvailabilityMetricUnits", t => t.AvailabilityMetricUnitId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.AvailabilityMetricUnitId, name: "IX_AvailabilityMetricUnit_Id")
                .Index(t => t.ProductId, name: "IX_Product_Id");
            
            CreateTable(
                "dbo.ProductTagValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        ProductId = c.Int(),
                        TagTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.TagTypes", t => t.TagTypeId)
                .Index(t => t.ProductId, name: "IX_Product_Id")
                .Index(t => t.TagTypeId, name: "IX_TagType_Id");
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        IsClient = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        LogoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppImages", t => t.LogoId)
                .Index(t => t.LogoId, name: "IX_Logo_Id");
            
            CreateTable(
                "dbo.BusinessDomains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TenantCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsTenantOwner = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        CategoryId = c.Int(),
                        TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .Index(t => t.CategoryId, name: "IX_Category_Id")
                .Index(t => t.TenantId, name: "IX_Tenant_Id");
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Locale = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TenantManufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsTenantOwner = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        ManufacturerId = c.Int(),
                        TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .Index(t => t.ManufacturerId, name: "IX_Manufacturer_Id")
                .Index(t => t.TenantId, name: "IX_Tenant_Id");
            
            CreateTable(
                "dbo.TenantMetricTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsTenantOwner = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        AvailabilityMetricTemplateId = c.Int(),
                        TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AvailabilityMetricTemplates", t => t.AvailabilityMetricTemplateId)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .Index(t => t.AvailabilityMetricTemplateId, name: "IX_AvailabilityMetricTemplate_Id")
                .Index(t => t.TenantId, name: "IX_Tenant_Id");
            
            CreateTable(
                "dbo.TenantTagTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsTenantOwner = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        TagTypeId = c.Int(),
                        TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TagTypes", t => t.TagTypeId)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .Index(t => t.TagTypeId, name: "IX_TagType_Id")
                .Index(t => t.TenantId, name: "IX_Tenant_Id");
            
            CreateTable(
                "dbo.TenantUrlHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        TenantId = c.Int(),
                        UrlId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .ForeignKey("dbo.AppUrls", t => t.UrlId)
                .Index(t => t.TenantId, name: "IX_Tenant_Id")
                .Index(t => t.UrlId, name: "IX_Url_Id");
            
            CreateTable(
                "dbo.AppUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        CountryId = c.Int(),
                        TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .Index(t => t.CountryId, name: "IX_Country_Id")
                .Index(t => t.TenantId, name: "IX_Tenant_Id");
            
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateSaved = c.DateTime(nullable: false),
                        IsFavourite = c.Boolean(nullable: false),
                        Alias = c.String(),
                        Status = c.Boolean(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DateModified = c.DateTime(),
                        XmlData = c.String(),
                        IsSystem = c.Boolean(),
                        ProductId = c.Int(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ProductId, name: "IX_Product_Id")
                .Index(t => t.UserId, name: "IX_User_Id");
            
            CreateTable(
                "dbo.CategoryTagTypes",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        TagTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.TagTypeId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.TagTypes", t => t.TagTypeId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.TagTypeId);
            
            CreateTable(
                "dbo.TenantBusinessDomains",
                c => new
                    {
                        TenantId = c.Int(nullable: false),
                        BusinessDomainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TenantId, t.BusinessDomainId })
                .ForeignKey("dbo.Tenants", t => t.TenantId, cascadeDelete: true)
                .ForeignKey("dbo.BusinessDomains", t => t.BusinessDomainId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.BusinessDomainId);
            
            CreateTable(
                "dbo.TenantCountries",
                c => new
                    {
                        TenantId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TenantId, t.CountryId })
                .ForeignKey("dbo.Tenants", t => t.TenantId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.UserProducts", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Users", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.Users", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TenantUrlHistories", "UrlId", "dbo.AppUrls");
            DropForeignKey("dbo.TenantUrlHistories", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.TenantTagTypes", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.TenantTagTypes", "TagTypeId", "dbo.TagTypes");
            DropForeignKey("dbo.TenantMetricTemplates", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.TenantMetricTemplates", "AvailabilityMetricTemplateId", "dbo.AvailabilityMetricTemplates");
            DropForeignKey("dbo.TenantManufacturers", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.TenantManufacturers", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Tenants", "LogoId", "dbo.AppImages");
            DropForeignKey("dbo.TenantCountries", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TenantCountries", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.TenantCategories", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.TenantCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TenantBusinessDomains", "BusinessDomainId", "dbo.BusinessDomains");
            DropForeignKey("dbo.TenantBusinessDomains", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.ProductTagValues", "TagTypeId", "dbo.TagTypes");
            DropForeignKey("dbo.ProductTagValues", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductMetricValues", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductMetricValues", "AvailabilityMetricUnitId", "dbo.AvailabilityMetricUnits");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ImageId", "dbo.AppImages");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CategoryTagTypes", "TagTypeId", "dbo.TagTypes");
            DropForeignKey("dbo.CategoryTagTypes", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Products", "AvailabilityMetricTemplateId", "dbo.AvailabilityMetricTemplates");
            DropForeignKey("dbo.AvailabilityMetricUnits", "ParentId", "dbo.AvailabilityMetricUnits");
            DropForeignKey("dbo.AvailabilityMetricUnits", "AvailabilityMetricTemplateId", "dbo.AvailabilityMetricTemplates");
            DropIndex("dbo.TenantCountries", new[] { "CountryId" });
            DropIndex("dbo.TenantCountries", new[] { "TenantId" });
            DropIndex("dbo.TenantBusinessDomains", new[] { "BusinessDomainId" });
            DropIndex("dbo.TenantBusinessDomains", new[] { "TenantId" });
            DropIndex("dbo.CategoryTagTypes", new[] { "TagTypeId" });
            DropIndex("dbo.CategoryTagTypes", new[] { "CategoryId" });
            DropIndex("dbo.UserProducts", "IX_User_Id");
            DropIndex("dbo.UserProducts", "IX_Product_Id");
            DropIndex("dbo.Users", "IX_Tenant_Id");
            DropIndex("dbo.Users", "IX_Country_Id");
            DropIndex("dbo.TenantUrlHistories", "IX_Url_Id");
            DropIndex("dbo.TenantUrlHistories", "IX_Tenant_Id");
            DropIndex("dbo.TenantTagTypes", "IX_Tenant_Id");
            DropIndex("dbo.TenantTagTypes", "IX_TagType_Id");
            DropIndex("dbo.TenantMetricTemplates", "IX_Tenant_Id");
            DropIndex("dbo.TenantMetricTemplates", "IX_AvailabilityMetricTemplate_Id");
            DropIndex("dbo.TenantManufacturers", "IX_Tenant_Id");
            DropIndex("dbo.TenantManufacturers", "IX_Manufacturer_Id");
            DropIndex("dbo.TenantCategories", "IX_Tenant_Id");
            DropIndex("dbo.TenantCategories", "IX_Category_Id");
            DropIndex("dbo.Tenants", "IX_Logo_Id");
            DropIndex("dbo.ProductTagValues", "IX_TagType_Id");
            DropIndex("dbo.ProductTagValues", "IX_Product_Id");
            DropIndex("dbo.ProductMetricValues", "IX_Product_Id");
            DropIndex("dbo.ProductMetricValues", "IX_AvailabilityMetricUnit_Id");
            DropIndex("dbo.ProductImages", "IX_Product_Id");
            DropIndex("dbo.ProductImages", "IX_Image_Id");
            DropIndex("dbo.Categories", "IX_Manufacturer_Id");
            DropIndex("dbo.Categories", "IX_Parent_Id");
            DropIndex("dbo.Products", "IX_Tenant_Id");
            DropIndex("dbo.Products", "IX_Category_Id");
            DropIndex("dbo.Products", "IX_AvailabilityMetricTemplate_Id");
            DropIndex("dbo.AvailabilityMetricUnits", "IX_Parent_Id");
            DropIndex("dbo.AvailabilityMetricUnits", "IX_AvailabilityMetricTemplate_Id");
            DropTable("dbo.TenantCountries");
            DropTable("dbo.TenantBusinessDomains");
            DropTable("dbo.CategoryTagTypes");
            DropTable("dbo.UserProducts");
            DropTable("dbo.Users");
            DropTable("dbo.AppUrls");
            DropTable("dbo.TenantUrlHistories");
            DropTable("dbo.TenantTagTypes");
            DropTable("dbo.TenantMetricTemplates");
            DropTable("dbo.TenantManufacturers");
            DropTable("dbo.Countries");
            DropTable("dbo.TenantCategories");
            DropTable("dbo.BusinessDomains");
            DropTable("dbo.Tenants");
            DropTable("dbo.ProductTagValues");
            DropTable("dbo.ProductMetricValues");
            DropTable("dbo.AppImages");
            DropTable("dbo.ProductImages");
            DropTable("dbo.TagTypes");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.AvailabilityMetricUnits");
            DropTable("dbo.AvailabilityMetricTemplates");
        }
    }
}
