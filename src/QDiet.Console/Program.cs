using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using QDiet.Domain.DataBase;
using QDiet.Domain.Models;
using QDiet.Domain.Models.DataBase;
using System.Security.Cryptography;
using System.Text;

StringBuilder logsStr;

using (StreamReader streamReader = new StreamReader("E:\\Проекты\\QDiet.Api\\src\\QDiet.Api\\Logs\\log2023-12-25.log"))
{
    logsStr = new StringBuilder((await streamReader.ReadToEndAsync()).Trim());
}

logsStr = logsStr.Remove(logsStr.Length - 1, 1);
logsStr.Append("\n]");

var logs = JsonConvert.DeserializeObject<List<LogModel>>(logsStr.ToString());

foreach (var log in logs)
{
    Console.WriteLine($"{log.LogTime:g}\n" +
                            $"{log.Type}\n" +
                            $"{log.Message}");
}
