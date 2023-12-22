using System.Security.Principal;
using NPOI.SS.Formula.Functions;

namespace ETLConsoleApp.Models;

public class Inventory
{
    public long Id { get; set; }
    
    public DateTime SnapshotDate { get; set; }      //0
    public string Sku { get; set; }                 //1
    public int Available { get; set; }              //6
    public int PendingRemovalQuantity { get; set; } //7
    public int InvAge0To90Days { get; set; }        //8
    public int InvAge91To180Days { get; set; }      //9
    public int InvAge181To270Days { get; set; }     //10
    public int InvAge271To365Days { get; set; }     //11
    public int InvAge365PlusDays { get; set; }      //12
    public int UnitsShippedT7 { get; set; }         //14
    public int UnitsShippedT30 { get; set; }        //15
    public int UnitsShippedT60 { get; set; }        //16
    public int UnitsShippedT90 { get; set; }        //17
    public string Alert { get; set; }               //18
    public decimal YourPrice { get; set; }          //19
    public decimal SalePrice { get; set; }          //20
    public decimal LowestPriceNewPlusShipping { get; set; } //21
    public decimal LowestPriceUsed { get; set; }    //22
    
    public string RecommendedAction { get; set; }   //23
    public string HealthyInventoryLevel { get; set; } //24
    
    public decimal RecommendedSalesPrice { get; set; }  //25

    public int RecommendedSalesDurationDays { get; set; }   //26
    public int RecommendedRemovalQuantity { get; set; } //27
    
    public decimal EstimatedCostSavingsOfRecommendedActions { get; set; }   //28
    
    public int SellThrough { get; set; }                //29
    public int ItemVolume { get; set; }                 //30

    public string VolumeUnitMeasurement { get; set; }   //31
    public string StorageType { get; set; }             //32

    public int StorageVolume { get; set; }              //33
    
    public string MarketPlace { get; set; }             //34
    public string ProductGroup { get; set; }            //35
    
    public int SaleRank { get; set; }                   //36
    public int DaysOfSupply { get; set; }               //37
    public string FbaInventoryLevelHealthStatus { get; set; }   //75
}
