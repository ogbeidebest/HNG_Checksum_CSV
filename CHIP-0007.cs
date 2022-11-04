using CsvHelper.Configuration.Attributes;

namespace CSV_Checksum;

[Delimiter(",")]  // Set CultureInfo to InvariantCulture
public class CHIP_0007
{
    [Optional]
    public string format { get; set; } = "CHIP-0007";
    [Name("Filename")]
    public string name { get; set; } = string.Empty;

    [Name("Description")]
    public string description { get; set; } = string.Empty;
    [Optional]
    public string minting_tool { get; set; } = "Team y";
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

    
}

public class Collection_C
{
    public string name { get; set; } = "Zuri NFT Tickets for Free Lunch";
    public string id { get; set; } = "b774f676-c1d5-422e-beed-00ef5510c64d";
    public IEnumerable<AttributeCollection> attributes { get; set; } = new List<AttributeCollection>() { new AttributeCollection() };
}

public class AttributeGeneral
{

    public string trait_type { get; set; } = "gender";
    public string value { get; set; } = "";
}

public class AttributeCollection
{

    public string type { get; set; } = "description";
    public string value { get; set; } = "Accomplishments during HNGi9.";
}


