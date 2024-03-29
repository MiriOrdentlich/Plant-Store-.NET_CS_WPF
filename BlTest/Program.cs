﻿//Naama Schweitzer 325447654 , Miri Ordentlich 213687346

using BO;

namespace BlTest;

public enum FirstChoice { Exit, Product, Order, Cart };
public enum CartOptions { add = 1, update, confirm };
public enum OrderOptions { getOrders = 1, getOrderInfo, updateOrderShipping, updateOrderDelivery, trackOrder };
public enum ProductOptions { getProductsManager = 1, getProductManager, getProductClient, addProduct, deleteProduct, updateProduct };

internal class Program
{
    // define a pass to the entities
    private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

    private static void ProductFunc(Cart myCart)
    {
        Console.WriteLine(@"1 - get all products (for manager)
2 - get product by ID
3 - get product by ID (for client)
4 - add product
5 - delete product
6 - update product
enter your choice:"); // print
        if (ProductOptions.TryParse(Console.ReadLine(), out ProductOptions c2) == false) //checks if the choice is valid
            throw new BO.BlInvalidEntityException("choice",1);
        //define vriables to use later for inputs:
        int id, amount;
        double price;
        string? name;
        BO.Category category;
        switch (c2)
        {

            case ProductOptions.getProductsManager:
                IEnumerable<BO.ProductForList?> productsList = bl.Product.GetListedProducts();
                foreach (var temp in productsList)
                    Console.WriteLine(temp);                
                break;

            case ProductOptions.getProductManager:
                Console.WriteLine("Enter ID of the product to get"); 
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("id", 1);
                Console.WriteLine(bl.Product.GetByIdM(id).ToString());
                break;

            case ProductOptions.getProductClient:              
                Console.WriteLine("Enter ID of the product to get");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("id", 1);
                myCart = bl.Cart.AddItem(myCart, id);
                Console.WriteLine(bl.Product.GetByIdC(id, myCart)); //print productItem
                break;

            case ProductOptions.addProduct:
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("id", 1);             
                Console.WriteLine("Enter name");
                name = Console.ReadLine();
                Console.WriteLine("Enter price"); // print
                if (double.TryParse(Console.ReadLine(), out price) == false)
                    throw new BO.BlInvalidEntityException("price", 1);
                Console.WriteLine("Enter amount"); 
                if (int.TryParse(Console.ReadLine(), out amount) == false) 
                    throw new BO.BlInvalidEntityException("amount", 1);
                Console.WriteLine("Enter catgory");
                if (BO.Category.TryParse(Console.ReadLine(), out category) == false)
                    throw new BO.BlInvalidEntityException("catgory", 1);
                bl.Product.AddProduct(id, name!, category, price, amount);
                break;

            case ProductOptions.deleteProduct:
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("id", 1);
                bl.Product.DeleteProduct(id);
                break;

            case ProductOptions.updateProduct:
                BO.Product product = new BO.Product();
                Console.WriteLine("Enter product ID");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("id", 1);
                product.Id = id;
                Console.WriteLine("Enter name");
                product.Name = Console.ReadLine();
                Console.WriteLine("Enter price"); // print
                if (double.TryParse(Console.ReadLine(), out price) == false)
                    throw new BO.BlInvalidEntityException("price", 1);
                product.Price = price;
                Console.WriteLine("Enter amount");
                if (int.TryParse(Console.ReadLine(), out amount) == false)
                    throw new BO.BlInvalidEntityException("amount", 1);
                product.InStock = amount;
                Console.WriteLine("Enter catgory");
                if (BO.Category.TryParse(Console.ReadLine(), out category) == false)
                    throw new BO.BlInvalidEntityException("catgory", 1);
                product.Category = category;
                bl.Product.UpdateProduct(product);
                break;    
                
            default:
                throw new BO.BlInvalidEntityException("input", 1);// for invalid choice
        }
    }
    private static void OrderFunc(Cart myCart)
    {
        Console.WriteLine(@"1 - get all orders
2 - get order details
3 - update order shipping date
4 - update order delivery date
5 - follow order
enter your choice:");

        if (OrderOptions.TryParse(Console.ReadLine(), out OrderOptions c2) == false) throw new BO.BlInvalidEntityException("input", 1);
        switch (c2)
        {
            case OrderOptions.getOrders: 
                IEnumerable<BO.OrderForList?> orderList = bl.Order.getOrdersList();
                foreach (var temp in orderList)
                    Console.WriteLine(temp);
                break;

            case OrderOptions.getOrderInfo:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out int id) == false)
                    throw new BO.BlInvalidEntityException("order Id", 1);// for invalid choice
                Console.WriteLine(bl.Order.GetOrderInfo(id));
                break;

            case OrderOptions.updateOrderShipping:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("order Id", 1); //throw if not valid
                Console.WriteLine(bl.Order.UpdateOrderShipping(id));
                break;

            case OrderOptions.updateOrderDelivery:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("order Id", 1); //throw if not valid
                Console.WriteLine(bl.Order.UpdateOrderDelivery(id));
                break;

            case OrderOptions.trackOrder:
                Console.WriteLine("Enter order ID");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new BO.BlInvalidEntityException("order Id", 1); //throw if not valid
                Console.WriteLine(bl.Order.TrackOrder(id));
                break;

            default:
                throw new BO.BlInvalidEntityException("input", 1); //throw if the input isnt valid
        }
    }
    private static void CartFunc(Cart myCart)
    {
        Console.WriteLine(@"1 - add order item to cart
2 - update an order in cart
3 - confirm cart and commit order
enter your choice:");// print instructions

        if (CartOptions.TryParse(Console.ReadLine(), out CartOptions c2) == false) throw new BO.BlInvalidEntityException("input", 1);
        int id, amount;
        string? name, email, address, str = "@gmail.com", hlp;
        switch(c2)
        {
            case CartOptions.add:                
                Console.WriteLine("Enter product Id to add");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new BO.BlInvalidEntityException("product Id", 1);
                myCart = bl.Cart.AddItem(myCart, id);
                Console.WriteLine(myCart);
                break;

            case CartOptions.update:
                Console.WriteLine("Enter product Id to update its amount");
                if (int.TryParse(Console.ReadLine(), out id) == false) throw new BO.BlInvalidEntityException("product Id", 1);
                Console.WriteLine("Enter the new amount");
                if (int.TryParse(Console.ReadLine(), out amount) == false) throw new BO.BlInvalidEntityException("amount", 1);
                myCart = bl.Cart.UpdateItemAmount(myCart, id, amount);
                Console.WriteLine(myCart);
                break;

            case CartOptions.confirm:
                Console.WriteLine("Enter customer name");
                name = Console.ReadLine() ?? throw new BO.BlInvalidEntityException("Name", 1);
                Console.WriteLine("Enter customer address");
                address = Console.ReadLine() ?? throw new BO.BlInvalidEntityException("Address", 1);
                Console.WriteLine("Enter customer email");
                hlp = Console.ReadLine();
                if (hlp != null && hlp.Contains(str))
                    email = hlp;
                else
                    throw new BO.BlInvalidEntityException("Email", 1);
                var order = bl.Cart.ConfirmCart(myCart, name, email, address);
                Console.WriteLine(order);
                break;

            default:
                throw new BO.BlInvalidEntityException("input", 1);
        }
    }

    private static void Main(string[] args)
    {
        Cart myCart = new Cart()
        {
            CustomerAddress = "Petach Tikva",
            CustomerEmail = "bla@gmail.com",
            CustomerName = "bla",
            Items = new List<BO.OrderItem>(),
            TotalPrice = 0       
        };
        myCart = bl.Cart.AddItem(myCart, 100000);


        bool stop = false;
        while (!stop)
        {
            try
            {
                Console.WriteLine(@"0 - Exit 
1 - Product
2 - Order
3 - Cart
enter your choice:"); // print

                if (FirstChoice.TryParse(Console.ReadLine(), out FirstChoice c1) == false) throw new BO.BlInvalidEntityException("input", 1);

                switch (c1)
                {
                    case FirstChoice.Exit:
                        stop = true;
                        break;

                    case FirstChoice.Product:
                        ProductFunc(myCart);
                        break;

                    case FirstChoice.Order:
                        OrderFunc(myCart);
                        break;

                    case FirstChoice.Cart:
                        CartFunc(myCart);
                        break;

                    default:
                        throw new BO.BlInvalidEntityException("input", 1);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

    }
}


