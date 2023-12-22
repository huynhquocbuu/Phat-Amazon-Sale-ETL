using System.Data;
using Dapper;
using ETLConsoleApp.Models;

namespace ETLConsoleApp.Services;

public class SeedMasterData
{
    private IDbConnection _connection;
    public SeedMasterData(IDbConnection connection)
    {
        _connection = connection;
    }
    public void SeedParameters()
    {
        string sql = @"
            INSERT INTO Parameters (ParameterType, ParameterName, ParameterDescription) 
            VALUES (@ParameterType, @ParameterName, @ParameterDescription)";
        Parameter aa = null;
        var parmameters = new List<Parameter>()
        {
            new Parameter()
            {
                ParameterType = "InventoryEvent",
                ParameterName = "Whse Transfers",
                ParameterDescription = "Transfers between warehouses"
            },
            new Parameter()
            {
                ParameterType = "InventoryEvent",
                ParameterName = "Shipments",
                ParameterDescription = "Customer shipments (orders)"
            },
            new Parameter()
            {
                ParameterType = "InventoryEvent",
                ParameterName = "Receipts",
                ParameterDescription = "Receipts of inbound inventory (Your shipments to Amazon)"
            },
            new Parameter()
            {
                ParameterType = "InventoryEvent",
                ParameterName = "Customer Returns",
                ParameterDescription = "Customer Returns"
            },
            new Parameter()
            {
                ParameterType = "InventoryEvent",
                ParameterName = "Adjustments",
                ParameterDescription = "corrections to the inventory balance like a miscount, reimbursement, found lost inventory or inventory lost, etc."
            },
        };

        _connection.Execute(sql: sql, param: parmameters);
    }
}



    //Adjustments = corrections to the inventory balance like a miscount, reimbursement, found lost inventory or inventory lost, etc.

