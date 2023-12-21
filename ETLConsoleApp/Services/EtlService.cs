using System.Data;
using System.Reflection;
using Dapper;
using ETLConsoleApp.Models;
using Microsoft.Data.SqlClient;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ETLConsoleApp.Services;

public class EtlService
{
    private string _insertProductQuery;
    private string _insertSaleProductQuery;
    private IDbConnection _connection;
    //private string? _excelFilePath;
    public EtlService()
    {
        string connectionString = @"Server=221.133.9.10,1483;Database=AmazonSaleETL;User Id=sa;Password=Khdt@199;Multipleactiveresultsets=true;Encrypt=False";
        _connection = new SqlConnection(connectionString);
        //_excelFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //_excelFilePath = Path.Combine(_excelFilePath, @"Docs\AmazonSaleData.xlsx");
        _insertProductQuery = @"
            INSERT INTO Products (Sku, FnSku, Asin, ProductName, Country, CurrencyCode, Price) 
            VALUES (@Sku, @FnSku, @Asin, @ProductName, @Country, @CurrencyCode, @Price)";

        _insertSaleProductQuery = @"
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
                                  @TotalOrderItems, @TotalOrderItemsB2B)
        ";
        
        
    }

    public int EtlProduct()
    {
        int eff = 0;
        //eff += EtlProductsFromExcel();
        eff += EtlSaleProductsFromExcel();
        return eff;
    }

    private ISheet ReadExcelFile(string filePath, string sheetName)
    {
        IWorkbook wb = new XSSFWorkbook(filePath);
        return wb.GetSheet(sheetName);
    }
    private int EtlProductsFromExcel()
    {
        int eff = 0;
        ISheet sheet = ReadExcelFile(
            filePath: @"E:\PrivatePlace\SoftwareProjects\Phat-Amazon-Sale-ETL\Docs\AmazonSaleData.xlsx",
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
                eff += _connection.Execute(sql: _insertProductQuery, param: product);
            }
        }

        return eff;
    }

    private int EtlSaleProductsFromExcel()
    {
        int eff = 0;
        ISheet sheet = ReadExcelFile(
            filePath: @"E:\PrivatePlace\SoftwareProjects\Phat-Amazon-Sale-ETL\Docs\ANALYZE.xlsx",
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
                
                eff += _connection.Execute(sql: _insertSaleProductQuery, param: saleProduct);
            }
        }

        return eff;
    }
}