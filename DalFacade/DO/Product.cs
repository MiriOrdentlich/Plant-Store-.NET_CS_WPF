namespace DO;

public struct Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Category? Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
ID          =   {Id},
Name        =   {Name},
Price       =   {Price},
Category    =   {Category},
InStock     =   {InStock}
";

}
