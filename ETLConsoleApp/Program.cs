// See https://aka.ms/new-console-template for more information

using ETLConsoleApp.Services;

EtlService etlService = new EtlService();
etlService.EtlProduct();

Console.WriteLine("Hello, OK!");
Console.ReadKey();
