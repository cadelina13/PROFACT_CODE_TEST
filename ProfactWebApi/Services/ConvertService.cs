using Newtonsoft.Json;

namespace ProfactWebApi.Services
{
    public interface IConvertService
    {
        public string ConvertCSV();
    }
    public class ConvertService : IConvertService
    {
        public string ConvertCSV()
        {
            var dir = Environment.CurrentDirectory;
            var csvFilePath = Path.Combine(dir, "Data", "Address_Points.csv");
            string csvData = System.IO.File.ReadAllText(csvFilePath);
            var csvRows = csvData.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var header = csvRows[0].Split(',');
            var data = new List<Dictionary<string, string>>();
            for (int i = 1; i < csvRows.Length; i++)
            {
                var row = csvRows[i].Split(',');
                var dict = new Dictionary<string, string>();
                for (int j = 0; j < header.Length; j++)
                {
                    try
                    {
                        dict.Add(header[j].Trim(), row[j].Trim());
                    }
                    catch (Exception) { }
                }
                data.Add(dict);
            }
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }

}
