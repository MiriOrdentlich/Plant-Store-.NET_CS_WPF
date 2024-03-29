﻿using BlApi;
using BO;
using DalApi;
using DO;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;

   // private static int Index = 1000000; //index for orderItem id



    /// <summary>
    /// add new item to cart
    /// </summary>
    /// <param name="cart">the cart in which we want to add/update the product</param>
    /// <param name="productId">the product ID</param>
    /// <returns>updated cart</returns>
    /// <exception cref="Exceptions"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Cart AddItem(BO.Cart cart, int productId)
    {
        try
        {
            var orderItem = (from item in cart.Items
                             where item.ProductID == productId
                             select item).FirstOrDefault();

            var product = dal.Product.Get(x => x?.Id == productId);
            if (orderItem is null)
            {
                if (product.InStock > 0)
                {
                    cart.Items = (cart.Items?.Append(new BO.OrderItem
                    { 
                        //Id = 111111,
                        Name = product.Name,
                        Price = product.Price,
                        ProductID = product.Id,
                        Amount = 1,
                        TotalPrice = product.Price
                    }))?.ToList();
                    cart.TotalPrice += product.Price;
                }
                else
                {
                    //exception product not in stock
                    throw new BO.BlNotInStockException(0, product.Name ?? "");
                }
            }
            else //if there is an order item for the product in cart
            {
                if (product.InStock < orderItem.Amount + 1)
                {
                    //exception not enough of product in stock
                    throw new BO.BlNotInStockException(-1, product.Name!);
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
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    /// <summary>
    /// update a cart item amount. return the new updated cart
    /// </summary>
    /// <param name="cart">the cart that the item to update in it</param>
    /// <param name="productId"></param>
    /// <param name="amount"></param>
    /// <returns>updated cart</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Cart UpdateItemAmount(BO.Cart cart, int productId, int amount)
    {
        try
        {
            var orderItem = cart.Items!.Where(x => x.ProductID == productId).FirstOrDefault() ??
                throw new BO.BlMissingEntityException("Product", productId);//product doesnt exist in cart
            var product = dal.Product.Get(x => x?.Id == productId);
            if (amount == 0)
            {
                cart.TotalPrice -= orderItem.TotalPrice; //remove the total of the order item from cart total
                ((List<BO.OrderItem?>)cart.Items!).Remove(orderItem);//remove order item from cart
            }
            else if (orderItem.Amount < amount) //the amount of the item got bigger
            {
                if (product.InStock < amount) //amount would be the updated number of products we need for orderItem
                {
                    //exception not enough of product in stock
                    throw new BO.BlNotInStockException(-1, product.Name!);
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
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    private bool checkEmail(string email)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        return match.Success;
    }

    /// <summary>
    /// make sure the cart details are correct and if so, make an order for the cart
    /// </summary>
    /// <param name="cart">the cart to confirm and make order from</param>
    /// <param name="name">the client name</param>
    /// <param name="email">the client email</param>
    /// <param name="address">the client address</param>
    /// <returns>the confirmed order with detail according to given cart</returns>
    /// <exception cref="Exceptions"></exception>
    public BO.Order ConfirmCart(BO.Cart cart, string name, string email, string address) //WHAT THE USE OF THE PARAMETERS
    {
        try
        {
            //check for every order item in Items: products exist, there are enough from each in stock, amounts positive
            cart.Items?.Select(item =>
            item.Amount > dal.Product.Get(x => x?.Id == item.ProductID).InStock ? throw new BO.BlNotInStockException(item.Amount, name) : //there isn't enough from product in stock
                item.Amount <= 0 ? throw new BO.BlInvalidEntityException(item.ProductID, name, 0) : 0);

            //check if address, name aren't empty and if email is empty or according to format (<string>@gmail.com)
            if (cart.CustomerAddress is null)
                throw new BO.BlInvalidEntityException("Address", 1); 
            if (cart.CustomerEmail == "" || !checkEmail(cart.CustomerEmail!))
                throw new BO.BlInvalidEntityException("Email Address", 1);
            if (cart.CustomerName is null)
                throw new BO.BlInvalidEntityException("Name", 1);
            
            //in case all details are correct:
            //create a new DO.Order, try to add the order and get an order id in return
            int DOorderId = dal.Order.Add(new DO.Order()
            {
                //Id get updated in func Add
                CustomerName = cart.CustomerName,
                CustomerAddress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null
            });


            //cart.Items?.Select(x => 
            //x.Amount > dal.Product.GetById(x.ProductID).InStock ? throw new BO.BlNotInStockException(x.Amount, name) : //there isn't enough from product in stock
            //    x.Amount <= 0 ? throw new BO.BlInvalidEntityException(x.ProductID, name, 0) : 0);

            //for every Product item in cart update the proper product amount in cart and create a new order Item::
            foreach (var BOorderItem in cart.Items!)
            {
                int orderItemId = dal.OrderItem.Add(new DO.OrderItem()
                {
                    OrderID = DOorderId,
                    Amount = BOorderItem.Amount,
                    Price = BOorderItem.Price,
                    ProductID = BOorderItem.ProductID
                });
                DO.OrderItem DOorderItem = dal.OrderItem.Get(x=> x?.Id == orderItemId);
                DO.Product DOproduct = dal.Product.Get(x => x?.Id == DOorderItem.ProductID);
                DOproduct.InStock -= DOorderItem.Amount; //take off from stock the amount of products in the order
                dal.Product.Update(DOproduct);
            }

            //create the order:
            BO.Order newOrder = new BO.Order()
            {
                Id = DOorderId,
                CustomerAddress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                CustomerName = cart.CustomerName,
                OrderDate = DateTime.Now,
                DeliveryDate = null,
                ShipDate = null,
                TotalPrice = cart.TotalPrice,
                Status = BO.OrderStatus.Confirmed, 
                Items = cart.Items
            };

            return newOrder;
        }
        catch (DO.DalDoesNotExistIdException ex )
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }        
    }
}
