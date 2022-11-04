// See https://aka.ms/new-console-template for more information
// use this to map CHIP_0007 to CSVFormat
using AutoMapper;
using CsvHelper;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.Json;
using static System.Console;

namespace CSV_Checksum;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            string pathOfFile = args[0];
            string CSVPath = Path.Combine(args[0], args[1]);

            // Automapper Config
            var csvrecords = new List<CSV_Format>();
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<CHIP_0007, CSV_Format>());
            var mapper = new Mapper(config);

            List<string> checksums = new List<string>();
            List<CSV_Format> csvFormats = new List<CSV_Format>();

            CreateJsonFiles(pathOfFile, CSVPath, mapper, csvFormats);

            FlushToFile(CSVPath, csvFormats);


            Console.WriteLine($"Completed, check {pathOfFile} for files that were generated");

        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("Too many inputs, or too few inputs. Check that you pass 2 arguments", ex);
        }
    }

    private static void CreateJsonFiles(string pathToFolder, string pathToCSV, Mapper mapper, List<CSV_Format> csvFormats)
    {
        // Calculate checksum for each new JSON file
        using (var filestream = new FileStream(pathToCSV, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1024 * 20, true))
        using (var reader = new StreamReader(filestream))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<CHIP_0007>();
            foreach (var item in records)
            {
                var json = JsonSerializer.Serialize(item);
                var sha_256 = string.Empty;
                var pathToCHIPJson = Path.Combine(pathToFolder, item.name ?? $"undefined_{Guid.NewGuid()}.json");

                // create the json
                using (var writer = new StreamWriter(pathToCHIPJson))
                {
                    writer.WriteLine(json);
                }
                // generate sha_256 for this particular file
                using (var filestream2 = new FileStream(pathToCHIPJson, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    sha_256 = GetChecksum(filestream2);
                   
                }

                var csvRecord = mapper.Map<CHIP_0007, CSV_Format>(item);
                csvRecord.Hash = sha_256;
             
                csvFormats.Add(csvRecord);

            }
        }
    }
    private static string GetChecksum(FileStream stream)
    {
        var sha = new SHA256Managed();
        byte[] checksum = sha.ComputeHash(stream);
        return BitConverter.ToString(checksum).Replace("-", String.Empty);
    }

    private static void FlushToFile(string pathToCSV, List<CSV_Format> csvFormats)
    {
        using (var read_csv = new StreamWriter(pathToCSV))
        using (var csv_2 = new CsvWriter(read_csv, CultureInfo.InvariantCulture))
        {
            csv_2.WriteRecords(csvFormats);
        }
    }


}