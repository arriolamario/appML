public class CuotasDTO
{
    public string payment_method_id { get; set; }
    public string payment_type_id { get; set; }
    public Issuer issuer { get; set; }
    public string processing_mode { get; set; }
    public object merchant_account_id { get; set; }
    public Payer_Costs[] payer_costs { get; set; }
}

public class Issuer
{
}

public class Payer_Costs
{
    public int installments { get; set; }
    public float installment_rate { get; set; }
    public int discount_rate { get; set; }
    public string[] labels { get; set; }
    public string[] installment_rate_collector { get; set; }
    public int min_allowed_amount { get; set; }
    public int max_allowed_amount { get; set; }
    public string recommended_message { get; set; }
    public float installment_amount { get; set; }
    public double total_amount { get; set; }
}

