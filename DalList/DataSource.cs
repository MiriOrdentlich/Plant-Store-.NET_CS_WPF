using DO;
using System.ComponentModel.DataAnnotations;
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


    //internal static class Config

    internal static int indexProduct = 0;
    internal static int indexOrder = 0;
    internal static int indexOrderItem = 0;

    internal const int s_startOrderNumber = 100000;
    private static int s_nextOrderNumber = s_startOrderNumber;
    internal static int nextOrderNumber { get => s_nextOrderNumber++; }
    internal const int s_startOrderItemNumber = 100000;
    private static int s_nextOrderItemNumber = s_startOrderItemNumber;
    internal static int nextOrderItemNumber { get => s_nextOrderItemNumber++; }

    internal static Product[] ProductArr { get; } = new Product[50];
    internal static Order[] OrderArr { get; } = new Order[100];
    internal static OrderItem[] OrderItemArr { get; } = new OrderItem[200];



    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();

    }

    private static string[,] productsNames = new string[5, 3] { { "chair", "barstool", "armchair" }, { "dining table", "desk", "coffee table" }, { "library", "closet", "wardrobes" }, { "cabinet", "drawer", "nightstand" }, { "bed", "playpen", "sofa" } };

    private static void createAndInitProducts()
    {
        int[] priceFrom = { 500, 4000, 1500, 700, 900 };
        int[] priceTo = { 1000, 8000, 6000, 2500, 7500 };
        for (int i = 0; i < 10; i++)
        {
            int index_category = s_rand.Next(4);
            int index_name = s_rand.Next(2);

            ProductArr[i] = new Product()
            {
                Id = i + 100000,
                Name = productsNames[index_category, index_name],
                Price = s_rand.Next(priceFrom[index_category], priceTo[index_category]),
                Category = (Category)index_category,
                InStock = s_rand.Next(50),
            };
            indexProduct++;
        }
    }
    private static void createAndInitOrders()
    {
        string[] firstName = { "Sara", "Rivka", "Rachel", "Leah", "Avraham", "Izzac", "Jakob" };
        string[] lastName = { "Cohen", "Levi", "Goldstein", "Peretz", "Fridman", "Mizrachi", "Biton" };
        string[] adresses = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        for (int i = 0; i < 20; i++)
        {
            string fstName = firstName[s_rand.Next(6)];
            string lstName = lastName[s_rand.Next(6)];

            int days = s_rand.Next(1000);

            DateTime orderDate = DateTime.Now.AddDays(-days);
            DateTime? deliveryDate;
            DateTime? shipDate;
            if (i % 5 == 0)
            {
                deliveryDate = null;
                shipDate = null;
            }
            else
            {
                days = s_rand.Next(1, 3);
                TimeSpan timeSpan = new TimeSpan(days, 0, 0, 0);
                deliveryDate = orderDate + timeSpan;
                if ((i + 2) % 3 == 0)
                    shipDate = null;
                else
                {
                    days = s_rand.Next(3, 7);
                    timeSpan = new TimeSpan(days, 0, 0, 0);
                    shipDate = orderDate + timeSpan;
                }

            }
            OrderArr[i] = new Order()
            {
                Id = nextOrderNumber,
                CustomerName = fstName + " " + lstName,
                CustomerAdress = adresses[s_rand.Next(9)],
                CustomerEmail = fstName + lstName + "@gmail.com",
                OrderDate = orderDate,
                ShipDate = shipDate,
                DeliveryDate = deliveryDate,                
            };
            indexOrder++;
        }
    }
    private static void createAndInitOrderItems()
    {
        // for every order there's 1-4 items. so we run on the OrderArr and add to OrederItemArr
        // the number of products from that order.
        int count = 0;
        for (int i = 0; i < 40; i++)
        {
            if(count == 20)
                count = 0;
            int numOfOrders = s_rand.Next(1, 4);
            for(int j = 0; j < numOfOrders; j++)
            {
                int indexProduct = s_rand.Next(9);
                int amount = s_rand.Next(10);

                OrderItemArr[i] = new OrderItem()
                {
                    Id = nextOrderItemNumber,
                    ProductID = ProductArr[indexProduct].Id,
                    OrderID = OrderArr[count].Id,
                    Price = ProductArr[indexProduct].Price,
                    Amount = amount,
                };
            }
            indexOrderItem++;
            count++;
        }
    }
}

