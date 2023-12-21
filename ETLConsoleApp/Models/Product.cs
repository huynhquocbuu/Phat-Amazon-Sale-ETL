namespace ETLConsoleApp.Models;

public class Product
{
    public string Sku { get; set; }//Key, 3
    public string FnSku { get; set; }//2
    public string Asin { get; set; }//4
    public string ProductName { get; set; }//1
    public string Country { get; set; }//0
    public string CurrencyCode { get; set; }//8
    public decimal Price { get; set; }//9
}