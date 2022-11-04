// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration.Attributes;
namespace CSV_Checksum;

public class CSV_Format 
{
    [Name("Filename")]
    public string name { get; set; } = string.Empty;

    [Name("Description")]
    public string description { get; set; } = string.Empty;


    [Optional]
    public string format { get; set; } = "CHIP-0007";

    [Optional]
    public string? minting_tool { get; set; } = "Team x";
    [Optional]
    public bool? sensitive_content { get; set; } = false;
    [Optional]
    public int? series_number { get; set; }
    [Optional]
    public int series_total { get; set; } = 526;
    
    [Optional]
    public IEnumerable<AttributeGeneral>? attributes { get; set; }

    
    [Name("Gender")]
    public string gender { get; set; } = string.Empty;

    public string UUID { get; set; } = string.Empty;
    public override string ToString()
    {

        return $"{format} ({name}) {UUID}";
    }

    [Name("SHA")]
    public string? Hash { get; set; }
}
