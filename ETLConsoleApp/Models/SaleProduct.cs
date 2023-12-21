namespace ETLConsoleApp.Models;

public class SaleProduct
{
    public int Id { get; set; }
    
    public int Year { get; set; }                           //1
    public int Month { get; set; }                          //2
    
    public DateTime FromDate { get; set; }                  //3
    public DateTime ToDate { get; set; }                    //4
    public string Week { get; set; }                        //5
    public string ReportName { get; set; }                  //6
    public string ParentAsin { get; set; }                  //7
    public string ChildAsin { get; set; }                   //8
    public string Sku { get; set; }                         //10
    public int SessionTotal { get; set; }                   //11
    public int SessionTotalB2B { get; set; }                //12
    public float SessionPercentageTotal { get; set; }       //13
    public float SessionPercentageTotalB2B { get; set; }    //14
    public int PageViewsTotal { get; set; }                 //15
    public int PageViewsTotalB2B { get; set; }              //16
    public float PageViewsPercentageTotal { get; set; }     //17
    public float PageViewsPercentageTotalB2B { get; set; }  //18
    public float FeaturedOfferPercentage { get; set; }      //19
    public float FeaturedOfferPercentageB2B { get; set; }   //20
    public int UnitsOrdered { get; set; }                   //21
    public int UnitsOrderedB2B { get; set; }                //22
    public float UnitSessionPercentage { get; set; }        //23
    public float UnitSessionPercentageB2B { get; set; }     //24
    public decimal OrderedProductSales { get; set; }        //25
    public decimal OrderedProductSalesB2B { get; set; }     //26
    public int TotalOrderItems { get; set; }                //27
    public int TotalOrderItemsB2B { get; set; }             //28
}

