using System.Data;
using System.Reflection;
using Dapper;
using ETLConsoleApp.Constants;
using ETLConsoleApp.Models;
using Microsoft.Data.SqlClient;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ETLConsoleApp.Services;

public class EtlService
{
    private IDbConnection _connection;
    private string _fileDirectory;
    public EtlService(IDbConnection connection, string fileDirectory)
    {
        _connection = connection;
        _fileDirectory = fileDirectory;
        
        
        
        
    }

    public int EtlProduct()
    {
        int eff = 0;
        //eff += EtlProductsFromExcel();
        //eff += EtlSaleProductsFromExcel();
        //eff += EtlInventoryFromText();
        eff += EtlInventoriesFromExcel();
        return eff;
    }
    
    private ISheet ReadExcelFile(string filePath, string sheetName)
    {
        IWorkbook wb = new XSSFWorkbook(filePath);
        return wb.GetSheet(sheetName);
    }
    #region EtlProductsFromExcel
    private int EtlProductsFromExcel()
    {
        int eff = 0;
        ISheet sheet = ReadExcelFile(
            filePath: Path.Combine(_fileDirectory, "AmazonSaleData.xlsx"),
            sheetName: "008_143238019710");
        int firstRow = 2;
        
        if (sheet != null)
        {
            int rowCount = sheet.LastRowNum; // This may not be valid row count.
            // If first row is table head, i starts from 1
            for (int i = firstRow; i <= rowCount; i++)
            {
                IRow curRow = sheet.GetRow(i);
                //cell start from 0
                //var sku = curRow.GetCell(4).StringCellValue.Trim();//SIMFBA10010
                var product = new Product()
                {
                    Sku = curRow.GetCell(3).StringCellValue.Trim(),
                    FnSku = curRow.GetCell(2).StringCellValue.Trim(),
                    Asin = curRow.GetCell(3).StringCellValue.Trim(),
                    ProductName = curRow.GetCell(1).StringCellValue.Trim(),
                    Country = curRow.GetCell(0).StringCellValue.Trim(),
                    CurrencyCode = curRow.GetCell(8).StringCellValue.Trim(),
                    Price = (decimal) curRow.GetCell(9).NumericCellValue/100,
                };
                eff += _connection.Execute(sql: SqlStatement.InsertProductSql, param: product);
            }
        }

        return eff;
    }
    #endregion


    #region EtlSaleProductsFromExcel
    private int EtlSaleProductsFromExcel()
    {
        int eff = 0;
        ISheet sheet = ReadExcelFile(
            filePath: Path.Combine(_fileDirectory, "ANALYZE.xlsx"),
            sheetName: "003_Sales");
        int firstRow = 2;
        if (sheet != null)
        {
            int rowCount = sheet.LastRowNum; // This may not be valid row count.
            // If first row is table head, i starts from 1
            for (int i = firstRow; i <= rowCount; i++)
            {
                IRow curRow = sheet.GetRow(i);

                var saleProduct = new SaleProduct();
                saleProduct.Year = (int)curRow.GetCell(1).NumericCellValue;
                saleProduct.Month = (int)curRow.GetCell(2).NumericCellValue;
                saleProduct.FromDate = curRow.GetCell(3).DateCellValue;
                saleProduct.ToDate = curRow.GetCell(4).DateCellValue;
                saleProduct.Week = curRow.GetCell(5).StringCellValue;
                saleProduct.ReportName = curRow.GetCell(6).StringCellValue.Trim();
                saleProduct.ParentAsin = curRow.GetCell(7).StringCellValue.Trim();
                saleProduct.ChildAsin = curRow.GetCell(8).StringCellValue.Trim();
                saleProduct.Sku = curRow.GetCell(10).StringCellValue.Trim();
                saleProduct.SessionTotal = int.Parse(curRow.GetCell(11).StringCellValue.Trim().Replace(",",""));
                saleProduct.SessionTotalB2B = int.Parse(curRow.GetCell(12).StringCellValue.Trim().Replace(",",""));
                saleProduct.SessionPercentageTotal =
                    float.Parse(s: curRow.GetCell(13).StringCellValue.Trim().Replace("%", ""));
                saleProduct.SessionPercentageTotalB2B =
                    float.Parse(s: curRow.GetCell(14).StringCellValue.Trim().Replace("%", ""));
                saleProduct.PageViewsTotal = int.Parse(curRow.GetCell(15).StringCellValue.Trim().Replace(",",""));
                saleProduct.PageViewsTotalB2B = int.Parse(curRow.GetCell(16).StringCellValue.Trim().Replace(",",""));
                saleProduct.PageViewsPercentageTotal =
                    float.Parse(s: curRow.GetCell(17).StringCellValue.Trim().Replace("%", ""));
                saleProduct.PageViewsPercentageTotalB2B =
                    float.Parse(s: curRow.GetCell(18).StringCellValue.Trim().Replace("%", ""));
                saleProduct.FeaturedOfferPercentage =
                    float.Parse(s: curRow.GetCell(19).StringCellValue.Trim().Replace("%", ""));
                saleProduct.FeaturedOfferPercentageB2B =
                    float.Parse(s: curRow.GetCell(20).StringCellValue.Trim().Replace("%", ""));
                saleProduct.UnitsOrdered = int.Parse(curRow.GetCell(21).StringCellValue.Trim().Replace(",",""));
                saleProduct.UnitsOrderedB2B = int.Parse(curRow.GetCell(22).StringCellValue.Trim().Replace(",",""));
                saleProduct.UnitSessionPercentage =
                    float.Parse(s: curRow.GetCell(23).StringCellValue.Trim().Replace("%", ""));
                saleProduct.UnitSessionPercentageB2B =
                    float.Parse(s: curRow.GetCell(24).StringCellValue.Trim().Replace("%", ""));
                saleProduct.OrderedProductSales =
                    decimal.Parse(curRow.GetCell(25).StringCellValue.Trim().Replace("$", ""));
                saleProduct.OrderedProductSalesB2B =
                    decimal.Parse(curRow.GetCell(26).StringCellValue.Trim().Replace("$", ""));
                saleProduct.TotalOrderItems = (int) curRow.GetCell(27).NumericCellValue;
                saleProduct.TotalOrderItemsB2B = (int) curRow.GetCell(28).NumericCellValue;
                
                eff += _connection.Execute(sql: SqlStatement.InsertSaleProductSql, param: saleProduct);
            }
        }

        return eff;
    }
    #endregion
    
    
    private int EtlInventoryFromText()
    {
        string filePath = Path.Combine(_fileDirectory, "InventoryReport12-19-2023.txt");
        IEnumerable<string> lines = File.ReadLines(filePath);
        //Console.WriteLine(String.Join(Environment.NewLine, lines));
        lines = File.ReadAllLines(filePath);
        //Console.WriteLine(String.Join(Environment.NewLine, lines));
        return 0;
    }

    private int EtlInventoriesFromExcel()
    {
        int eff = 0;
        ISheet sheet = ReadExcelFile(
            filePath: Path.Combine(_fileDirectory, "AmazonSaleData.xlsx"),
            sheetName: "010_143307019711");
        int firstRow = 2;
        if (sheet != null)
        {
            int rowCount = sheet.LastRowNum; // This may not be valid row count.
            // If first row is table head, i starts from 1
            for (int i = firstRow; i <= rowCount; i++)
            {
                IRow curRow = sheet.GetRow(i);

                var inventory = new Inventory();
                inventory.SnapshotDate = curRow.GetCell(0).DateCellValue;
                inventory.Sku = curRow.GetCell(1).StringCellValue.Trim();
                inventory.Available = (int) curRow.GetCell(6).NumericCellValue;
                inventory.PendingRemovalQuantity = (int) curRow.GetCell(7).NumericCellValue;
                inventory.InvAge0To90Days = (int) curRow.GetCell(8).NumericCellValue;
                inventory.InvAge91To180Days = (int) curRow.GetCell(9).NumericCellValue;
                inventory.InvAge181To270Days = (int) curRow.GetCell(10).NumericCellValue;
                inventory.InvAge271To365Days = (int) curRow.GetCell(11).NumericCellValue;
                inventory.InvAge365PlusDays = (int) curRow.GetCell(12).NumericCellValue;
                inventory.UnitsShippedT7 = (int) curRow.GetCell(14).NumericCellValue;
                inventory.UnitsShippedT30 = (int) curRow.GetCell(15).NumericCellValue;
                inventory.UnitsShippedT60 = (int) curRow.GetCell(16).NumericCellValue;
                inventory.UnitsShippedT90 = (int) curRow.GetCell(17).NumericCellValue;
                
                
                inventory.Alert = curRow.GetCell(18).StringCellValue;
                inventory.YourPrice = (decimal) curRow.GetCell(19).NumericCellValue;
                inventory.SalePrice = (decimal) curRow.GetCell(20).NumericCellValue;
                inventory.LowestPriceNewPlusShipping = (decimal) curRow.GetCell(21).NumericCellValue;
                inventory.LowestPriceUsed = (decimal) curRow.GetCell(22).NumericCellValue;
                inventory.RecommendedAction = curRow.GetCell(23).StringCellValue;
                inventory.HealthyInventoryLevel = curRow.GetCell(24).StringCellValue;
                inventory.RecommendedSalesPrice = (decimal) curRow.GetCell(25).NumericCellValue;
                inventory.RecommendedSalesDurationDays = (int) curRow.GetCell(26).NumericCellValue;
                inventory.RecommendedRemovalQuantity = (int) curRow.GetCell(27).NumericCellValue;
                inventory.EstimatedCostSavingsOfRecommendedActions = (decimal) curRow.GetCell(28).NumericCellValue;
                inventory.SellThrough = (int) curRow.GetCell(29).NumericCellValue;
                inventory.ItemVolume = (int) curRow.GetCell(30).NumericCellValue;
                inventory.VolumeUnitMeasurement = curRow.GetCell(31).StringCellValue;
                inventory.StorageType = curRow.GetCell(32).StringCellValue;
                inventory.StorageVolume = (int) curRow.GetCell(33).NumericCellValue;
                inventory.MarketPlace = curRow.GetCell(34).StringCellValue;
                inventory.ProductGroup = curRow.GetCell(35).StringCellValue;
                inventory.SaleRank = (int) curRow.GetCell(36).NumericCellValue;
                inventory.DaysOfSupply = (int) curRow.GetCell(37).NumericCellValue;
                inventory.FbaInventoryLevelHealthStatus = curRow.GetCell(75).StringCellValue;
                
                eff += _connection.Execute(sql: SqlStatement.InsertInventorySql, param: inventory);
            }
        }

        return eff;
    }
}