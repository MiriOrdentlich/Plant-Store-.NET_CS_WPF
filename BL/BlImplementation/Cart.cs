using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Cart : ICart
{

    public void AddItem(BO.Cart cart, int productId)
    {
        throw new NotImplementedException();
    }

    public void UpdateItemAmount(BO.Cart cart, int productId, int amount)
    {

        throw new NotImplementedException();
    }

    public void ConfirmCart(BO.Cart cart, string name, string email, string adress)
    {
        throw new NotImplementedException();
    }
}
