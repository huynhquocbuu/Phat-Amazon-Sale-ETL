use AmazonSaleETL;
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