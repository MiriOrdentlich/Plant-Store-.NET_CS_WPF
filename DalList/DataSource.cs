using DalApi;
using DO;
namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new();


    //internal static class Config

    internal const int s_startOrderNumber = 100000;
    private static int s_nextOrderNumber = s_startOrderNumber;
    internal static int nextOrderNumber { get => s_nextOrderNumber++; }
    internal const int s_startOrderItemNumber = 100000;
    private static int s_nextOrderItemNumber = s_startOrderItemNumber;
    internal static int nextOrderItemNumber { get => s_nextOrderItemNumber++; }


    internal static List<Product?> ProductsList { get; } = new List<Product?>(); //empty list for products
    internal static List<Order?> OrdersList { get; } = new List<Order?>(); //empty list for orders
    internal static List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>(); //empty list for orders items
    internal static List<User?> UsersList { get; } = new List<User?>(); //empty list for users



    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();
        createAndInitUsers();

        XmlTools.SaveListToXMLSerializer(ProductsList, "products");
        XmlTools.SaveListToXMLSerializer(OrdersList, "orders");
        XmlTools.SaveListToXMLSerializer(OrderItemsList, "orderItems");
        XmlTools.SaveListToXMLSerializer(UsersList, "users");
    }

    //the 6 arrays match to the 6 categories in Category by index. i did it in a way that we can later on choose
    //a price (also suited by index) that make sense to the product value
    //productNames devided to: 6 categories, each category has 3 elements.
    private static string[,] productsNames = new string[6, 3] 
    //category: flowering, Ferns, CactiAndSucculents, Trees, HangingAndClimbing, Edibles
    { { "Florence", "Francisca", "Vinnie" },
            { "Bertie", "Pele", "Toby" },
                { "Ian", "Lyla", "Val" },
                    { "Ariel", "Big Ken", "Laura tree" },
                        { "Lizzie", "Marge", "Peggy" },
                            { "Kai", "Leon", "Vivi" }};

    private static void createAndInitProducts()
    {
        int[] priceFrom = { 50, 40, 30, 70, 90, 100 };
        int[] priceTo = { 70, 80, 60, 100, 150, 180 };
        for (int i = 0; i < 15; i++)
        {
            int index_category = s_rand.Next(6);
            int index_name = s_rand.Next(3);

            ProductsList.Add(
               new Product()
               {
                   Id = i + 100000,// id is 6 digits
                   Name = productsNames[index_category, index_name],
                   Price = s_rand.Next(priceFrom[index_category], priceTo[index_category]),
                   Category = (Category)index_category,
                   InStock = s_rand.Next(50),
                   ImageRelativeName = @"\pics\" + productsNames[index_category, index_name] + ".jpeg"
               });
        }
    }
    private static void createAndInitOrders()
    {
        string[] firstName = { "Sara", "Rivka", "Rachel", "Leah", "Avraham", "Izzac", "Jakob" };
        string[] lastName = { "Cohen", "Levi", "Goldstein", "Peretz", "Fridman", "Mizrachi", "Biton" };
        string[] addresses = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        for (int i = 0; i < 20; i++)
        {
            string fstName = firstName[s_rand.Next(7)];
            string lstName = lastName[s_rand.Next(7)];

            int days = s_rand.Next(21, 200);

            DateTime orderDate = DateTime.Now.AddDays(-days); // order date is berfore current date
            DateTime? deliveryDate = null;
            DateTime? shipDate = null;
            TimeSpan timeSpan;
            if (i < 0.8 * 20) // 5% doesnt get delivery and ship date
            {
                days = s_rand.Next(10, 20);
                timeSpan = new TimeSpan(days, 0, 0, 0);
                shipDate = orderDate + timeSpan;
            }
            if (i < 0.8 * 0.6 * 20)
            {
                days = s_rand.Next(1, 10);
                timeSpan = new TimeSpan(days, 0, 0, 0);
                if (shipDate != null)
                    deliveryDate = shipDate + timeSpan;
                else
                    deliveryDate = DateTime.MinValue + timeSpan;
            }
            //create and add order to OrdersList
            OrdersList.Add(
                new Order()
                {
                    Id = nextOrderNumber,
                    CustomerName = fstName + " " + lstName,
                    CustomerAddress = addresses[s_rand.Next(10)],
                    CustomerEmail = fstName + lstName + "@gmail.com",
                    OrderDate = orderDate,
                    ShipDate = shipDate,
                    DeliveryDate = deliveryDate
                });
        }
    }
    private static void createAndInitOrderItems()
    {
        // for every order there's 1-4 items. so we run on the OrdersList and add to OrederItemArr
        // the number of products from that order.
        int count = 0;
        for (int i = 0; i < 40; i++)
        {
            if (count == 20)
                count = 0;
            int numOfOrders = s_rand.Next(1, 4);
            for (int j = 0; j < numOfOrders; j++)
            {
                int indexProduct = s_rand.Next(9);
                int amount = s_rand.Next(10);

                OrderItemsList.Add(
                    new OrderItem()
                    {
                        Id = nextOrderItemNumber,
                        ProductID = ProductsList.ElementAt(indexProduct)?.Id ?? 0,
                        OrderID = OrdersList.ElementAt(count)?.Id ?? 0,
                        Price = ProductsList.ElementAt(indexProduct)?.Price ?? 0,
                        Amount = amount,
                    });
            }
            count++;


        }
    }
    private static void createAndInitUsers()
    {
        //Define managers: (=>user type)

        UsersList.Add(
                    new User()
                    {
                        isManager = true,
                        Name = "Naama",
                        Address = "House",
                        Email = "email@gmail.com",
                        Password = "abc",
                    });

        UsersList.Add(
                    new User()
                    {
                        isManager = true,
                        Name = "Miri",
                        Address = "House",
                        Email = "email@gmail.com",
                        Password = "psw"
                    });
    }
}

