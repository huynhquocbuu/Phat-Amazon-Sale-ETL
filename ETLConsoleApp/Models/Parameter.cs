using NPOI.SS.Formula.Functions;

namespace ETLConsoleApp.Models;

public class Parameter
{
    public long Id { get; set; }
    public string ParameterType { get; set; }
    public string ParameterName { get; set; }
    public string ParameterDescription { get; set; }
}