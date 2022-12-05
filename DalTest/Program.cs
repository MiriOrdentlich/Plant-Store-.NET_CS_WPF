//Naama Schweitzer 325447654 , Miri Ordentlich 213687346
//We did the bonus

namespace Dal;
using DO;
using Dal;
using DalApi;

public enum FirstChoice { Exit, Product, Order, OrderItem};
public enum SecondChoice { add = 1, delete, update, getById, GetAll, GetByProductAndOrder, GetAllOrderProducts };

internal class Program
{
    // define a pass to the entities
    private static IDal dal = new DalList();

    private static void ProductFunc()
    {
        Console.WriteLine(@"1 - add product
2 - delete product
3 - update product
4 - get product by ID
5 - get all products
enter your choice:"); // print

        if (SecondChoice.TryParse(Console.ReadLine(), out SecondChoice c2) == false) //checks if the choice is valid
            throw new Exception("Your choice is not valid");
        
        switch (c2)
        {
            case SecondChoice.add:
                Product p = new Product(); //creating a new Product
                Console.WriteLine("Enter product ID"); //ask for details from user
                if (int.TryParse(Console.ReadLine(), out int id) == false) //if not valid
                {

                    throw new Exception("Your input is not valid");
                }
                p.Id = id;
                Console.WriteLine("Enter name");
                p.Name = Console.ReadLine();
                Console.WriteLine("Enter price"); // print
                if (double.TryParse(Console.ReadLine(), out double pr) == false) //checks if the choice is valid
                    throw new Exception("Your input is not valid");
                p.Price = pr;
                Console.WriteLine("Enter category: 0-Chairs, 1-Tables, 2-BigStorage, 3-SmallStorage, 4-Beds");
                if (Category.TryParse(Console.ReadLine(), out Category c) == false) //checks if its not valid
                    throw new Exception("Your input is not valid");
                p.Category = c;
                Console.WriteLine("Enter amount"); //ask for an amount
                if (int.TryParse(Console.ReadLine(), out int a) == false) //checks if the choice is valid
                    throw new Exception("Your input is not valid");
                p.InStock = a;
                dal.Product.Add(p);
                break;

            case SecondChoice.delete:
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //if not valid
                    throw new Exception("Your input is not valid");
                dal.Product.Delete(id); //make a delete
                break;

            case SecondChoice.update:
                Product p2 = new Product();
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //if not valid
                    throw new Exception("Your input is not valid");
                p2.Id = id;
                Console.WriteLine("Enter name"); // print
                p2.Name = Console.ReadLine();
                Console.WriteLine("Enter price");
                if (double.TryParse(Console.ReadLine(), out pr) == false) //checks if the input is valid
                    throw new Exception("Your input is not valid");
                p2.Price = pr;
                Console.WriteLine("Enter category: 0-Chairs, 1-Tables, 2-BigStorage, 3-SmallStorage, 4-Beds");
                if (Category.TryParse(Console.ReadLine(), out c) == false) //checks if the input is valid
                    throw new Exception("Your input is not valid");
                p2.Category = c;
                Console.WriteLine("Enter amount");
                if (int.TryParse(Console.ReadLine(), out a) == false) //checks if the input is valid
                    throw new Exception("Your input is not valid");
                p2.InStock = a;
                dal.Product.Update(p2); //updating the changes the user gave
                break;

            case SecondChoice.getById: 
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //checks if the input is valid
                    throw new Exception("Your input is not valid");
                Console.WriteLine(dal.Product.GetById(id).ToString()); // find the wanted product
                break;

            case SecondChoice.GetAll:
                IEnumerable<Product?> pArr = dal.Product.GetAll(); //create an arr with the current products and use the func GetAll
                foreach(var temp in pArr)
                    Console.WriteLine(temp);
                break;

            default:
                throw new Exception("Invalid input"); // for invalid choice
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
            case SecondChoice.add: //initalize the choise
                Order ord = new Order();
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out int id) == false) 
                    throw new Exception("Invalid input"); //throw if not valid
                ord.Id = id;
                Console.WriteLine("Enter customer name");// print
                ord.CustomerName = Console.ReadLine();
                DateTime date = DateTime.Now; // initalize the new date
                ord.OrderDate = date;
                ord.ShipDate = date.AddHours(8);
                ord.DeliveryDate = date.AddDays(3);
                Console.WriteLine("Enter customer email"); // print
                ord.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter customer adress");// print
                ord.CustomerAddress = Console.ReadLine();
                dal.Order.Add(ord);
                break;

            case SecondChoice.delete:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //checks if the input is valid
                    throw new Exception("Invalid input");
                dal.Order.Delete(id); //deleting
                break;

            case SecondChoice.update:
                Order ord2 = new Order();
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //checks if the input is valid
                    throw new Exception("Invalid input");
                ord2.Id = id;
                Console.WriteLine("Enter customer name");
                ord2.CustomerName = Console.ReadLine();
                date = DateTime.Now;
                ord2.OrderDate = date;
                ord2.ShipDate = date.AddHours(8);
                ord2.DeliveryDate = date.AddDays(3);
                Console.WriteLine("Enter customer email");// print
                ord2.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter customer adress");
                ord2.CustomerAddress = Console.ReadLine();
                dal.Order.Update(ord2); //upating
                break;

            case SecondChoice.getById:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //checks if the input is valid
                    throw new Exception("Invalid input");
                Console.WriteLine(dal.Order.GetById(id).ToString());//find the order we want to update
                break;

            case SecondChoice.GetAll:
                IEnumerable<Order?> pArr = dal.Order.GetAll();
                foreach (var temp in pArr)
                    Console.WriteLine(temp);
                break;

            default:
                throw new Exception("Invalid input"); //throw if the input isnt valid
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
7 - get all products from certain order
enter your choice:");
        // print
        if (SecondChoice.TryParse(Console.ReadLine(), out SecondChoice c2) == false) throw new Exception("Invalid input");
        switch (c2)
        {
            case SecondChoice.add:
                OrderItem oi = new OrderItem();
                Console.WriteLine("Enter order ID"); // print
                if (int.TryParse(Console.ReadLine(), out int id) == false) throw new Exception("Invalid input");
                oi.OrderID = id;
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //check if the input is valid
                    throw new Exception("Invalid input");
                oi.ProductID = id;
                Console.WriteLine("Enter product amount");
                if (int.TryParse(Console.ReadLine(), out int a) == false) throw new Exception("Invalid input");
                oi.Amount = a; 
                Console.WriteLine("Enter product price");// print
                if (double.TryParse(Console.ReadLine(), out double price) == false)//check if the input is valid
                    throw new Exception("Invalid input");
                oi.Price = price;
                dal.OrderItem.Add(oi); //Adding
                break;
            
            case SecondChoice.delete:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) //check if the input is valid
                    throw new Exception("Invalid input");
                dal.OrderItem.Delete(id); //delete
                break;
            
            case SecondChoice.update: 
                OrderItem oi2 = new OrderItem();
                Console.WriteLine("Enter order item ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                oi2.Id = id; 
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                oi2.OrderID = id; 
                Console.WriteLine("Enter product ID");// print
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                oi2.ProductID = id;
                Console.WriteLine("Enter product amount");
                if (int.TryParse(Console.ReadLine(), out a) == false) throw new Exception("Invalid input");
                oi2.Amount = a;
                Console.WriteLine("Enter product price");
                if (double.TryParse(Console.ReadLine(), out price) == false) throw new Exception("Invalid input");
                oi2.Price = price;
                dal.OrderItem.Update(oi2); //updating the changes the user gave
                break;

            case SecondChoice.getById: 
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new Exception("Invalid input");
                Console.WriteLine(dal.OrderItem.GetById(id).ToString());
                break;

            case SecondChoice.GetAll:  
                IEnumerable<OrderItem?> pArr = dal.OrderItem.GetAll();
                foreach (var temp in pArr)
                    Console.WriteLine(temp.ToString());
                break;

            case SecondChoice.GetByProductAndOrder: 
                Console.WriteLine("Enter order ID"); // print
                if (int.TryParse(Console.ReadLine(), out int o_id) == false)//check if the input is valid
                    throw new Exception("Invalid input");
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out int p_id) == false) throw new Exception("Invalid input");
                Console.WriteLine(dal.OrderItem.GetByProductAndOrder(p_id,o_id).ToString());
                break;

            case SecondChoice.GetAllOrderProducts:
                Console.WriteLine("Enter order ID"); // print
                if (int.TryParse(Console.ReadLine(), out o_id) == false)//check if the input is valid
                    throw new Exception("Invalid input");
                IEnumerable<OrderItem?> oArr = dal.OrderItem.GetAllOrderProducts(o_id);
                foreach (var temp in oArr)
                {
                    if(temp.Id != 0)
                        Console.WriteLine(temp.ToString());
                }
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
enter your choice:"); // print

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


