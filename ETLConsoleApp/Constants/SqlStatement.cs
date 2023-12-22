namespace ETLConsoleApp.Constants;

public class SqlStatement
{
    public static string InsertProductSql = @"
            INSERT INTO Products (Sku, FnSku, Asin, ProductName, Country, CurrencyCode, Price) 
            VALUES (@Sku, @FnSku, @Asin, @ProductName, @Country, @CurrencyCode, @Price)";

    public static string InsertSaleProductSql = @"
            INSERT INTO SaleProducts (Sku, Year, Month, Week, FromDate, ToDate, ReportName, ParentAsin, ChildAsin,
                                  SessionTotal, SessionTotalB2B, SessionPercentageTotal, SessionPercentageTotalB2B,
                                  PageViewsTotalB2B, PageViewsTotal, PageViewsPercentageTotalB2B, PageViewsPercentageTotal,
                                  FeaturedOfferPercentage, FeaturedOfferPercentageB2B, UnitsOrdered, UnitsOrderedB2B,
                                  UnitSessionPercentage, UnitSessionPercentageB2B, OrderedProductSales, OrderedProductSalesB2B,
                                  TotalOrderItems, TotalOrderItemsB2B) 
            VALUES (@Sku, @Year, @Month, @Week, @FromDate, @ToDate, @ReportName, @ParentAsin, @ChildAsin,
                                  @SessionTotal, @SessionTotalB2B, @SessionPercentageTotal, @SessionPercentageTotalB2B,
                                  @PageViewsTotalB2B, @PageViewsTotal, @PageViewsPercentageTotalB2B, @PageViewsPercentageTotal,
                                  @FeaturedOfferPercentage, @FeaturedOfferPercentageB2B, @UnitsOrdered, @UnitsOrderedB2B,
                                  @UnitSessionPercentage, @UnitSessionPercentageB2B, @OrderedProductSales, @OrderedProductSalesB2B,
                                  @TotalOrderItems, @TotalOrderItemsB2B)";

    public static string InsertInventorySql = @"
        INSERT INTO Inventories (Sku,SnapshotDate,Available,PendingRemovalQuantity,
InvAge0To90Days,InvAge91To180Days,InvAge181To270Days,InvAge271To365Days,,,,,,,,,,,,,,)
        VALUES (@Sku, )";
}


//  DATE,
//  int,
//  int,
//  int,
//  int,
//  int,
//  int,
// InvAge365PlusDays int,
// UnitsShippedT7 int,
// UnitsShippedT30 int,
// UnitsShippedT60 int,
// UnitsShippedT90 int,
// Alert varchar(500),
//
// YourPrice DECIMAL(20,2),
// SalePrice DECIMAL(20,2),
// LowestPriceNewPlusShipping DECIMAL(20,2),
// LowestPriceUsed DECIMAL(20,2),
// RecommendedAction varchar(500),
// HealthyInventoryLevel varchar(500),
// RecommendedSalesPrice DECIMAL(20,2),
// RecommendedSalesDurationDays int,
// RecommendedRemovalQuantity int,
// EstimatedCostSavingsOfRecommendedActions DECIMAL(20,2),
// SellThrough int,
// ItemVolume int,
// VolumeUnitMeasurement varchar(500),
// StorageType varchar(500),
// StorageVolume int,
// MarketPlace varchar(500),
// ProductGroup varchar(500),
// SaleRank int,
// DaysOfSupply int,
//     
// FbaInventoryLevelHealthStatus varchar(500),