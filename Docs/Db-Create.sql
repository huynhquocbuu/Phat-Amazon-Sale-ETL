use AmazonSaleETL;

CREATE TABLE Parameters
(
    Id bigint IDENTITY(1,1) NOT NULL,
    ParameterType varchar(500),
    ParameterName varchar(500),
    ParameterDescription nvarchar(4000),
);

CREATE TABLE Products
(
    Sku varchar(500) not null primary key,
    FnSku varchar(500),
    Asin varchar(500),
    ProductName varchar(max),
    Country varchar(500),
    CurrencyCode varchar(500),
    Price DECIMAL(20,2)
);

CREATE TABLE Inventories
(
    Id bigint IDENTITY(1,1) NOT NULL,
    Sku varchar(500) not null,
    SnapshotDate DATE,
    Available int,
    PendingRemovalQuantity int,
    InvAge0To90Days int,
    InvAge91To180Days int,
    InvAge181To270Days int,
    InvAge271To365Days int,
    InvAge365PlusDays int,
    UnitsShippedT7 int,
    UnitsShippedT30 int,
    UnitsShippedT60 int,
    UnitsShippedT90 int,
    Alert varchar(500),

    YourPrice DECIMAL(20,2),
    SalePrice DECIMAL(20,2),
    LowestPriceNewPlusShipping DECIMAL(20,2),
    LowestPriceUsed DECIMAL(20,2),
    RecommendedAction varchar(500),
    HealthyInventoryLevel varchar(500),
    RecommendedSalesPrice DECIMAL(20,2),
    RecommendedSalesDurationDays int,
    RecommendedRemovalQuantity int,
    EstimatedCostSavingsOfRecommendedActions DECIMAL(20,2),
    SellThrough int,
    ItemVolume int,
    VolumeUnitMeasurement varchar(500),
    StorageType varchar(500),
    StorageVolume int,
    MarketPlace varchar(500),
    ProductGroup varchar(500),
    SaleRank int,
    DaysOfSupply int,
    
    FbaInventoryLevelHealthStatus varchar(500),
);
ALTER TABLE Inventories
    ADD CONSTRAINT FK_Inventory_Products
        FOREIGN KEY (Sku) REFERENCES Products(Sku);


CREATE TABLE SaleProducts
(
    Id bigint IDENTITY(1,1) NOT NULL,
    Sku varchar(500) not null,
    Year int not null,
    Month int not null,
    Week varchar(500),
    FromDate DATE,
    ToDate DATE,
    ReportName varchar(500),
    ParentAsin varchar(500),
    ChildAsin varchar(500),
    SessionTotal int,
    SessionTotalB2B int,
    SessionPercentageTotal float,
    SessionPercentageTotalB2B float,
    PageViewsTotalB2B int,
    PageViewsTotal int,
    PageViewsPercentageTotalB2B float,
    PageViewsPercentageTotal float,
    FeaturedOfferPercentage float,
    FeaturedOfferPercentageB2B float,
    UnitsOrdered int,
    UnitsOrderedB2B int,
    UnitSessionPercentage float,
    UnitSessionPercentageB2B float,

    OrderedProductSales DECIMAL(20,2),
    OrderedProductSalesB2B DECIMAL(20,2),
    TotalOrderItems int,
    TotalOrderItemsB2B int,
);
ALTER TABLE SaleProducts
    ADD CONSTRAINT FK_SaleProducts_Products
    FOREIGN KEY (Sku) REFERENCES Products(Sku);

CREATE TABLE InventoryEvent
(
    Id  bigint IDENTITY (1,1) NOT NULL,
    Sku varchar(500)          not null
);

ALTER TABLE InventoryEvent
    ADD CONSTRAINT FK_InventoryEvent_Products
        FOREIGN KEY (Sku) REFERENCES Products(Sku);