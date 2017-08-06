public class MedioPago
{
    public string id { get; set; }
    public string name { get; set; }
    public string payment_type_id { get; set; }
    public string status { get; set; }
    public string secure_thumbnail { get; set; }
    public string thumbnail { get; set; }
    public string deferred_capture { get; set; }
    public Setting[] settings { get; set; }
    public string[] additional_info_needed { get; set; }
    public float min_allowed_amount { get; set; }
    public int max_allowed_amount { get; set; }
    public int accreditation_time { get; set; }
    public object[] financial_institutions { get; set; }
    public string[] processing_modes { get; set; }
}

public class Setting
{
    public Card_Number card_number { get; set; }
    public Bin bin { get; set; }
    public Security_Code security_code { get; set; }
}

public class Card_Number
{
    public string validation { get; set; }
    public int length { get; set; }
}

public class Bin
{
    public string pattern { get; set; }
    public string installments_pattern { get; set; }
    public string exclusion_pattern { get; set; }
}

public class Security_Code
{
    public int length { get; set; }
    public string card_location { get; set; }
    public string mode { get; set; }
}
