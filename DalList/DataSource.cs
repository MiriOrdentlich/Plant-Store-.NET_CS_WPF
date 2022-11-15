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
    internal static class Config
    {
        internal static int indexProduct = 0;
        internal static int indexOrder = 0;
        internal static int indexOrderItem = 0;
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int nextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int nextOrderItemNumber { get => s_nextOrderItemNumber++; }
    }
    internal static Product[] ProductArr { get; } = new Product[50];
    internal static Order[] OrderArr { get; } = new Order[100];
    internal static OrderItem[] OrderItemArr { get; } = new OrderItem[200];

    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();

    }
    private static string[,] productsNames = new string[5, 3] { { "chairs", "chair", "bed" }, { "table", "a", "b" }, { "accesorise/small stuff", "c", "d" }, {"closet and dresser","e","f"},{"shelfs","de","ge" } };

    private static void createAndInitProducts()
    {
        int[] priceFrom = { 200, 4000, 20, 1000, 100 };
        int[] priceTo = { 500, 6000, 150, 5000, 400 };
        for (int i = 0; i < 10; i++)
        {
            int index_category = s_rand.Next(4);
            int index_name = s_rand.Next(3);
            ProductArr[i] = new Product()
            {
                Id = i + 100000,
                Name = productsNames[index_category, index_name],
                Price = s_rand.Next(priceFrom[index_category], priceTo[index_category]),
                Category = (Category)index_category,
                InStock = s_rand.Next(50),
            };
            Config.indexProduct++;
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
          
            days = s_rand.Next(1,3);
            TimeSpan timeSpan1 = new TimeSpan(days, 0, 0, 0);
            days = s_rand.Next(3, 7);
            TimeSpan timeSpan2 = new TimeSpan(days, 0, 0, 0);

            OrderArr[i] = new Order()
            {
                Id = Config.nextOrderNumber,
                CustomerName = fstName + " " + lstName,
                CustomerAdress = adresses[s_rand.Next(9)],
                CustomerEmail = fstName + lstName + "@gmail.com",
                OrderDate = orderDate,
                ShipDate = orderDate + timeSpan1,
                DeliveryDate = orderDate + timeSpan2,                
            };
            Config.indexOrder++;
        }
    }
    private static void createAndInitOrderItems()
    {
        
        for (int i = 0; i < 50; i++)
        {
            OrderItemArr[i] = new OrderItem()
            {
                Id = Config.nextOrderItemNumber,
                //ProductID =
                //OrderID =
                //Price
                //Amount
            };
            Config.indexOrderItem++;
        }
    }
}

