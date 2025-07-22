public class StockRecord
{
    public string Symbol { get => "ABCD"; }

    private decimal[] prices = new decimal[] { 105.1m, 103.12m, 106.93m, 104.5m, 103.7m };
    public decimal Average { get => prices.Sum() / prices.Length; }
    public decimal High { get => prices.Max(); }
    public decimal Low { get => prices.Min(); }
    public int Length { get => prices.Length; }

    public decimal this[int index] { get => prices[index]; }

    public decimal this[string day]
    {
        get
        {
            return day switch
            {
                "Monday" => prices[0],
                "Tuesday" => prices[1],
                "Wednesday" => prices[2],
                "Thursday" => prices[3],
                "Friday" => prices[4],
                _ => 0,
            };
        }
    }
}
