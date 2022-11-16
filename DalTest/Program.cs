using DO;
using Dal;

public enum FirstChoice { Exit, Product, Order, OrderItem};
public enum SecondChoice { add = 1, delete, update, getById, GetAll, GetByProductAndOrder };

internal class Program
{
    // define a pass to the entities
    private static DalProduct dalProduct = new DalProduct();
    private static DalOrder dalOrder = new DalOrder();
    private static DalOrderItem dalOrderItem = new DalOrderItem();

    private static void ProductFunc()
    {
        Console.WriteLine(@"1 - add product
2 - delete product
3 - update product
4 - get product by ID
5 - get all products
enter your choice:");

        if (SecondChoice.TryParse(Console.ReadLine(), out SecondChoice c2) == false) throw new Exception("Your choice is not valid");
        
        switch (c2)
        {
            case SecondChoice.add:
                Product p = new Product();
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out int id) == false) throw new Exception("Your input is not valid");
                p.Id = id;
                Console.WriteLine("Enter name");
                p.Name = Console.ReadLine();
                Console.WriteLine("Enter price");
                if (double.TryParse(Console.ReadLine(), out double pr) == false) throw new Exception("Your input is not valid");
                p.Price = pr;
                Console.WriteLine("Enter category: 0-Chairs, 1-Tables, 2-BigStorage, 3-SmallStorage, 4-Beds");
                if (Category.TryParse(Console.ReadLine(), out Category c) == false) throw new Exception("Your input is not valid");
                p.Category = c;
                Console.WriteLine("Enter amount");
                if (int.TryParse(Console.ReadLine(), out int a) == false) throw new Exception("Your input is not valid");
                p.InStock = a;
                dalProduct.Add(p);
                break;

            case SecondChoice.delete:
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Your input is not valid");
                dalProduct.Delete(id);
                break;

            case SecondChoice.update:
                Product p2 = new Product();
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Your input is not valid");
                p2.Id = id;
                Console.WriteLine("Enter name");
                p2.Name = Console.ReadLine();
                Console.WriteLine("Enter price");
                if (double.TryParse(Console.ReadLine(), out pr) == false) throw new Exception("Your input is not valid");
                p2.Price = pr;
                Console.WriteLine("Enter category: 0-Chairs, 1-Tables, 2-BigStorage, 3-SmallStorage, 4-Beds");
                if (Category.TryParse(Console.ReadLine(), out c) == false) throw new Exception("Your input is not valid");
                p2.Category = c;
                Console.WriteLine("Enter amount");
                if (int.TryParse(Console.ReadLine(), out a) == false) throw new Exception("Your input is not valid");
                p2.InStock = a;
                dalProduct.Update(p2);
                break;

            case SecondChoice.getById:
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Your input is not valid");
                Console.WriteLine(dalProduct.GetById(id).ToString());
                break;

            case SecondChoice.GetAll:
                Product[] pArr = dalProduct.GetAll();
                foreach(Product temp in pArr)
                    Console.WriteLine(temp);
                break;

            default:
                throw new Exception("Invalid input");
        }
         
    }
    private static void OrderFunc()
    {
        Console.WriteLine(@"1 - add order
2 - delete order
3 - update order
4 - get order by ID
5 - get all orders
enter your choice:");

        if (SecondChoice.TryParse(Console.ReadLine(), out SecondChoice c2) == false) throw new Exception("Invalid input");
        switch (c2)
        {
            case SecondChoice.add:
                Order ord = new Order();
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out int id) == false) throw new Exception("Invalid input");
                ord.Id = id;
                Console.WriteLine("Enter customer name");
                ord.CustomerName = Console.ReadLine();
                DateTime date = DateTime.Now;
                ord.OrderDate = date;
                ord.ShipDate = date.AddHours(8);
                ord.DeliveryDate = date.AddDays(3);
                Console.WriteLine("Enter customer email");
                ord.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter customer adress");
                ord.CustomerAdress = Console.ReadLine();
                dalOrder.Add(ord);
                break;

            case SecondChoice.delete:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                dalOrder.Delete(id);
                break;

            case SecondChoice.update:
                Order ord2 = new Order();
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                ord2.Id = id;
                Console.WriteLine("Enter customer name");
                ord2.CustomerName = Console.ReadLine();
                date = DateTime.Now;
                ord2.OrderDate = date;
                ord2.ShipDate = date.AddHours(8);
                ord2.DeliveryDate = date.AddDays(3);
                Console.WriteLine("Enter customer email");
                ord2.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter customer adress");
                ord2.CustomerAdress = Console.ReadLine();
                dalOrder.Update(ord2);
                break;

            case SecondChoice.getById:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                Console.WriteLine(dalOrder.GetById(id).ToString());
                break;

            case SecondChoice.GetAll:
                Order[] pArr = dalOrder.GetAll();
                foreach (Order temp in pArr)
                    Console.WriteLine(temp);
                break;

            default:
                throw new Exception("Invalid input");
        }
    }
    private static void OrderItemsFunc()
    {        
        Console.WriteLine(@"1 - add order item
2 - delete order item
3 - update order item
4 - get order item by ID
5 - get all order items
6 - get by product ID and order ID
enter your choice:");
        
        if (SecondChoice.TryParse(Console.ReadLine(), out SecondChoice c2) == false) throw new Exception("Invalid input");
        switch (c2)
        {
            case SecondChoice.add:
                OrderItem oi = new OrderItem();
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out int id) == false) throw new Exception("Invalid input");
                oi.OrderID = id;
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                oi.ProductID = id;
                Console.WriteLine("Enter product amount");
                if (int.TryParse(Console.ReadLine(), out int a) == false) throw new Exception("Invalid input");
                oi.Amount = a; 
                Console.WriteLine("Enter product price");
                if (double.TryParse(Console.ReadLine(), out double price) == false) throw new Exception("Invalid input");
                oi.Price = price;
                dalOrderItem.Add(oi);
                break;
            
            case SecondChoice.delete:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                dalOrderItem.Delete(id);
                break;
            
            case SecondChoice.update: //NOTE: to check. smth didnt work with the id of the item itself. i dont realy remember
                OrderItem oi2 = new OrderItem();
                Console.WriteLine("Enter order item ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                oi2.Id = id; 
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                oi2.OrderID = id; 
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                oi2.ProductID = id;
                Console.WriteLine("Enter product amount");
                if (int.TryParse(Console.ReadLine(), out a) == false) throw new Exception("Invalid input");
                oi2.Amount = a;
                Console.WriteLine("Enter product price");
                if (double.TryParse(Console.ReadLine(), out price) == false) throw new Exception("Invalid input");
                oi2.Price = price;
                dalOrderItem.Update(oi2);
                break;

            case SecondChoice.getById: //NOTE: to check.
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                Console.WriteLine(dalOrderItem.GetByID(id).ToString());
                break;

            case SecondChoice.GetAll:  
                OrderItem[] pArr = dalOrderItem.GetAll();
                foreach (OrderItem temp in pArr)
                    Console.WriteLine(temp.ToString());
                break;

            case SecondChoice.GetByProductAndOrder: //NOTE: to check
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out int p_id) == false) throw new Exception("Invalid input");
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out int o_id) == false) throw new Exception("Invalid input");
                Console.WriteLine(dalOrderItem.GetByProductAndOrder(p_id,o_id).ToString());
                break;

            default:
                throw new Exception("Invalid input");
        }
    }

    private static void Main(string[] args)
    {

        bool stop = false;
        while(!stop)
        {
            try
            {
                Console.WriteLine(@"0 - Exit 
1 - Product
2 - Order
3 - Order item
enter your choice:");

                if (FirstChoice.TryParse(Console.ReadLine(), out FirstChoice c1) == false) throw new Exception("Invalid input");

                switch (c1)
                {
                    case FirstChoice.Exit:
                        stop = true;
                        break;

                    case FirstChoice.Product:
                        ProductFunc();
                        break;

                    case FirstChoice.Order:
                        OrderFunc();
                        break;

                    case FirstChoice.OrderItem:
                        OrderItemsFunc();
                        break;

                    default:
                        throw new Exception("Invalid input");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }            
      
    }   
}


