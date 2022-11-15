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

    //string[] orderArray = new string[100];
    public int Add(Order order) //create
    {
        if (DataSource.Config.indexOrder > 99)
        {
            throw new Exception("There is no more space for new orders\n");
        }
        for (int i = 0; i < DataSource.Config.indexOrder; i++)
        {
            
            if (DataSource.OrderArr[i].Id == order.Id)
            {
                throw new Exception("The identifying number already exists\n");
            }
        }
        DataSource.OrderArr[DataSource.Config.indexOrder] = order;
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
            }
            else
                throw new Exception("The identifying number doesn't exists");

        }

    }

    public void Delete(int id)
    {
        int a = -1;
        for (int i = 0; i < 100; i++)
        {

            if (DataSource.OrderArr[i].Id == id)
            {
                a = i;
            }
        }
        if (a != -1)
        {
            int j = a;
            for (; j < DataSource.Config.indexOrder - 2; j++)
            {
                DataSource.OrderArr[j] = DataSource.OrderArr[j + 1];

            }
            DataSource.Config.indexOrder--;
        }
        else
            throw new Exception("The identifying number doesn't exists");
    }

    //public IEnumerable<Order?> GetAll() //מתודת בקשה\קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים)
    //{
    //    string[] onlyOrders = new string[DataSource.Config.indexOrder];
    //    for (int i = 0; i < onlyOrders.Length; i++)
    //    {
    //        onlyOrders[i] = DataSource.OrderArr[i];
    //    }
    //}

    public Order[] getAll()
    {
        Order[] onlyOrders = new Order[DataSource.Config.indexOrder];
        for (int i = 0; i < onlyOrders.Length; i++)
        {
            onlyOrders[i] = DataSource.OrderArr[i];
        }
        return onlyOrders;
    }
    

