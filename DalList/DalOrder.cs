using DO;
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
        if (DataSource.Config.indexOrder == DataSource.OrderArr.Length)
        {
            throw new Exception("There is no more space for new orders");
        }
        for (int i = 0; i < DataSource.Config.indexOrder; i++)
        {

            if (DataSource.OrderArr[i].Id == order.Id)
            {
                throw new Exception("The identifying number already exists");
            }
        }
        DataSource.OrderArr[DataSource.Config.indexOrder++] = order;
        return order.Id;
    }
    public Order GetById(int id) //Request
    {
        Order p;
        for (int i = 0; i < DataSource.Config.indexOrder; i++)
        {
            p = DataSource.OrderArr[i];
            if (p.Id == id)
            {
                return p;
            }


        }
        throw new Exception("Id Number doesn't exist");

    }
    public void Update(Order order)
    {
        for (int i = 0; i < DataSource.Config.indexOrder; i++)
        {
            if (DataSource.OrderArr[i].Id == order.Id)
            {
                DataSource.OrderArr[i] = order;
                return;
            }
        }
        throw new Exception("The identifying number doesn't exist");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.indexOrder; i++)
        {
            if (DataSource.OrderArr[i].Id == id)
            {
                DataSource.OrderArr[i] = DataSource.OrderArr[--DataSource.Config.indexOrder];
                return;
            }
        }
        throw new Exception("Order doesn't exist");


        // NOTE: see my note at the product.delete func - same reason

        //int a = -1;
        //for (int i = 0; i < 100; i++)
        //{

        //    if (DataSource.OrderArr[i].Id == id)
        //    {
        //        a = i;
        //    }
        //}
        //if (a != -1)
        //{
        //    int j = a;
        //    for (; j < DataSource.Config.indexOrder - 2; j++)
        //    {
        //        DataSource.OrderArr[j] = DataSource.OrderArr[j + 1];

        //    }
        //    DataSource.Config.indexOrder--;
        //}
        //else
        //    throw new Exception("The identifying number doesn't exists");
    }
    public Order[] GetAll()
    {
        Order[] onlyOrders = new Order[DataSource.Config.indexOrder];
        for (int i = 0; i < onlyOrders.Length; i++)
        {
            onlyOrders[i] = DataSource.OrderArr[i];
        }
        return onlyOrders;
    }
}
    

