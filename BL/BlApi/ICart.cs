﻿using BO;
namespace BlApi;

public interface ICart
{
    Cart AddItem(Cart cart, int productId);
    Cart UpdateItemAmount(Cart cart, int productId, int amount);
    BO.Order ConfirmCart(Cart cart, string name, string email, string address);
}
