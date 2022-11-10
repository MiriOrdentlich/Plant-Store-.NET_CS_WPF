namespace DO;

public struct OrderItem
{
    public int Id { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
ID        =   {Id},
ProductID =   {ProductID},
OrderID   =   {OrderID},
Price  =   {Price},
Amount  =   {Amount}
";
}
