// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Reflection;
using ETLConsoleApp.Services;
using Microsoft.Data.SqlClient;

//string fileDirectory = @"E:\PrivatePlace\SoftwareProjects\Phat-Amazon-Sale-ETL\Docs";
string fileDirectory = Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "AppData");

string connectionString = @"Server=221.133.9.10,1483;Database=AmazonSaleETL;User Id=sa;Password=Khdt@199;Multipleactiveresultsets=true;Encrypt=False";
IDbConnection sqlConnection = new SqlConnection(connectionString);

//var seedMasterData = new SeedMasterData(sqlConnection);
//seedMasterData.SeedParameters();

var etlService = new EtlService(sqlConnection, fileDirectory);
etlService.EtlProduct();

Console.WriteLine("Hello, OK!");

if ((sqlConnection.State & ConnectionState.Open) != 0)
{
    sqlConnection.Close();
}
sqlConnection.Dispose();
Console.ReadKey();
