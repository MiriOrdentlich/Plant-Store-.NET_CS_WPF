using DO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Linq;

namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new();

    internal static List<Product?> ProductList { get; } = new List<Product?>();
    internal static List<Order?> OrderList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderListList { get; } = new List<OrderItem?>();
    

    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();

    }
    private static void createAndInitProducts()
    {
        string[] namesArray = { "table", "chair", "picture", "closet", "bed", "shelf", "dresser", "sink", "plant", "door" };

        for (int i = 0; i < 10; i++)
        {
            ProductList.Add(
                new Product()
                {
                    Id = i,
                    Name = namesArray[s_rand.Next(namesArray.Length)],
                    Price = s_rand.Next(200),
                    



                }

    }
    }
}
