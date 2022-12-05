﻿using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    DalApi.IDal dal = new Dal.DalList();

    /// <summary>
    /// add new item to cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Cart AddItem(BO.Cart cart, int productId)
    {
        var orderItem = cart.Items.Where(x => x.ProductID == productId).FirstOrDefault();
        var product = dal.Product.GetById(productId);
        if (orderItem is null)
        {
            if (product.InStock > 0) 
            {
                cart.Items.Append(new BO.OrderItem
                { /*Id =??*/
                    Name = product.Name,
                    Price = product.Price,
                    ProductID = product.Id,
                    Amount = 1,
                    TotalPrice = product.Price
                });
                cart.TotalPrice += product.Price;
            }
            else
            {
                //exception product not in stock
            }
        }
        else //if there is an order item for the product in cart
        {
            if (product.InStock < orderItem.Amount + 1)
            {
                //exception not enough of product in stock
            }
            else
            {
                orderItem.Amount++;
                orderItem.TotalPrice += product.Price;
                cart.TotalPrice += product.Price;
            }
        }
        return cart;
    }

    /// <summary>
    /// update a cart item amount. return the new updated cart
    /// </summary>
    /// <param name="cart">the cart to update tha</param>
    /// <param name="productId"></param>
    /// <param name="amount"></param>
    /// <returns>return updated cart</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Cart UpdateItemAmount(BO.Cart cart, int productId, int amount)
    {
        var orderItem = cart.Items.Where(x => x.ProductID == productId).FirstOrDefault() ?? throw new Exception();//product doesnt exist in cart
        var product = dal.Product.GetById(productId);
        if (amount == 0)
        {
            cart.TotalPrice -= orderItem.TotalPrice; //remove the total of the order item from cart total
            cart.Items.ToList().Remove(orderItem); //remove order item from cart
        }
        else if (orderItem.Amount < amount) //the amount of the item got bigger
        {
            if (product.InStock < amount) //amount would be the updated number of products we need for orderItem
            {
                //exception not enough of product in stock
            }
            else
            {
                //we add to the total the difference between the old and new amount multified by the product's price
                orderItem.TotalPrice += product.Price * (amount - orderItem.Amount);
                cart.TotalPrice += product.Price * (amount - orderItem.Amount);
                orderItem.Amount = amount; //update the new amount
            }
        }
        else if (orderItem.Amount > amount) // new amount is smaller than the old one
        {
            //we subtract from the total the difference between the old and new amount multified by the product's price
            orderItem.TotalPrice -= product.Price * (orderItem.Amount - amount);
            cart.TotalPrice -= product.Price * (orderItem.Amount - amount); 
            orderItem.Amount = amount; //update the new amount
        }
        return cart;
    }

    /// <summary>
    /// make sure the cart details are correct and if so, make an order for the cart
    /// </summary>
    /// <param name="cart">the cart to confirm and make order from</param>
    /// <param name="name">the client name</param>
    /// <param name="email">the client email</param>
    /// <param name="adress">the client adress</param>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order ConfirmCart(BO.Cart cart, string name, string email, string adress) //WHAT THE USE OF THE PARAMETERS
    {
        //check every order item in Items: products exist, there are enough from each in stock, amounts positive
        foreach(var orderItem in cart.Items)
        {
            var product = dal.Product.GetById(orderItem.ProductID);
            if (orderItem.Amount > product.InStock)
                throw new Exception(); //there isn't enough from product in stock  INCORRET EXCEPTION
            if (orderItem.Amount <= 0)
                throw new Exception(); //amount isn't positive INCORRET EXCEPTION
        }

        //**********************INCORRECT EXCEPTIONS !!!!***************
        //check if address, name aren't empty and if email is empty or according to format (<string>@gmail.com)
        if (cart.CustomerAddress is null)
            throw new Exception();
        if (cart.CustomerEmail is not null) //NEED TO CHECK IF ACCORDING TO FORMAT 
            throw new Exception();
        if (cart.CustomerName is null)
            throw new Exception();

        //in case all details are correct:

        //create a new DO.Order, try to add the order and get an order id in return
        int DOorderId = dal.Order.Add(new DO.Order()
        {
            CustomerName = cart.CustomerName,
            CustomerAdress = cart.CustomerAddress,
            CustomerEmail = cart.CustomerEmail,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null,
        });

        foreach (var BOorderItem in cart.Items)
        {
            int orderItemId = dal.OrderItem.Add(new DO.OrderItem()
            {
                OrderID = DOorderId,
                Amount = BOorderItem.Amount,
                Price = BOorderItem.Price, //PRICE OR TOTAL???????
                ProductID = BOorderItem.ProductID     
            });
            DO.OrderItem DOorderItem = dal.OrderItem.GetById(orderItemId);
            DO.Product product = dal.Product.GetById(DOorderItem.ProductID);
            product.InStock -= DOorderItem.Amount; //take off from stock the amount of products in the order
            dal.Product.Update(product);
        }

        BO.Order newOrder = new BO.Order()
        {
            Id = DOorderId,
            CustomerAdress = cart.CustomerAddress,
            CustomerEmail = cart.CustomerEmail,
            CustomerName = cart.CustomerName,
            OrderDate = DateTime.Now,
            DeliveryDate = null,
            ShipDate = null,
            TotalPrice = cart.TotalPrice,
            Status = BO.OrderStatus.Ordered, //?????
            Items = cart.Items
        };
        return newOrder;
    }
}
