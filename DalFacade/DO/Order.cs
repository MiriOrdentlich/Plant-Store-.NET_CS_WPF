﻿namespace DO;


public struct Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; } // => confirm date
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public override string ToString() => $@"
ID              =   {Id},
CustomerName    =   {CustomerName},
CustomerEmail   =   {CustomerEmail},
CustomerAdress  =   {CustomerAddress},
OrderDate       =   {OrderDate},
ShipDate        =   {ShipDate},
DeliveryDate    =   {DeliveryDate}
";
}
