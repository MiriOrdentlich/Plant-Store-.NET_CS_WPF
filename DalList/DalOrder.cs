﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalOrder
{

    public int Add(Order order) //create
    {
        if (DataSource.indexOrder == DataSource.OrderArr.Length) //check if the array is full
        {
            throw new Exception("There is no more space for new orders");
        }
        for (int i = 0; i < DataSource.indexOrder; i++)
        {

            if (DataSource.OrderArr[i].Id == order.Id)
            {
                throw new Exception("The identifying number already exists");
            }
        }
        DataSource.OrderArr[DataSource.indexOrder++] = order;
        return order.Id;
    }
    public Order GetById(int id) //Request
    {
        Order p;
        for (int i = 0; i < DataSource.indexOrder; i++)
        {
            p = DataSource.OrderArr[i];
            if (p.Id == id) //search the id
            {
                return p;
            }


        }
        throw new Exception("Id Number doesn't exist");

    }
    public void Update(Order order)
    {
        for (int i = 0; i < DataSource.indexOrder; i++)
        {
            if (DataSource.OrderArr[i].Id == order.Id)
            {
                DataSource.OrderArr[i] = order; //updating the order
                return;
            }
        }
        throw new Exception("The identifying number doesn't exist");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.indexOrder; i++)
        {
            if (DataSource.OrderArr[i].Id == id)
            {
                DataSource.OrderArr[i] = DataSource.OrderArr[--DataSource.indexOrder];
                return;
            }
        }
        throw new Exception("Order doesn't exist");
    }
    public Order[] GetAll()
    {
        Order[] onlyOrders = new Order[DataSource.indexOrder];
        for (int i = 0; i < onlyOrders.Length; i++)
        {
            onlyOrders[i] = DataSource.OrderArr[i]; //copy all the orders
        }
        return onlyOrders;
    }
}
    

