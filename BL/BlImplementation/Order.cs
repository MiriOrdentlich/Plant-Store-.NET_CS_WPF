using BlApi;

namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal dal = new Dal.DalList();

}
