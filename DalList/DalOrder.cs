using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace Dal;
public class DalOrder
{

    public int Add(Order order) //create
    {
        // search for order in list:
        if(DataSource.OrdersList.Contains(order)) // if found order -> throw exception
            throw new Exception("Order already exists");
        DataSource.OrdersList.Add(order); // if order isn't in list, add order to list
        return order.Id;
    }
    public Order GetById(int id) //Request
    {
        //search orderList for order that match the given id
        //if order not found throw exception
        Order p = DataSource.OrdersList.Find(x => x?.Id == id) ?? throw new Exception("Id Number doesn't exist");
        return p;
    }
    public void Update(Order order)
    {
        // search for order in list. if didn't find order -> throw exception
        Order p = DataSource.OrdersList.Find(x => x?.Id == order.Id) ?? throw new Exception("Order doesn't exist");
        p = order;
    }
    public void Delete(int id)
    {
        if( DataSource.OrdersList.RemoveAll(x => x?.Id == id) == 0)
            throw new Exception("Order doesn't exist");
    }
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> onlyOrders = new List<Order?>();
        foreach (var item in DataSource.OrdersList)
        {
            onlyOrders.Add(item);
        }
        return onlyOrders;
    }
}
    

