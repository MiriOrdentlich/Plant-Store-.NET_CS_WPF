using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    readonly static string s_config = "configuration";
    internal static int GetNextProductId()
    {
        return XmlTools.ToIntNullable(XmlTools.LoadListFromXMLElement(s_config), "NextProductId") ?? throw new DalDoesNotExistIdException(-1, "Product");

    }
    public static int GetNextOrderId()
    {
        return XmlTools.ToIntNullable(XmlTools.LoadListFromXMLElement(s_config), "NextOrderId") ?? throw new DalDoesNotExistIdException(-1, "Order");

    }
    public static int GetNextOrderItemId()
    {
        return XmlTools.ToIntNullable(XmlTools.LoadListFromXMLElement(s_config), "NextOrderItemId") ?? throw new DalDoesNotExistIdException(-1, "OrderItem");

    }
    public static void SetNextProductId(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(s_config);
        root.Element("NextProductId")?.SetValue(id.ToString());
        XmlTools.SaveListToXMLElement(root, s_config);
    }
    public static void SetNextOrderId(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderId")?.SetValue(id.ToString());
        XmlTools.SaveListToXMLElement(root, s_config);
    }
    public static void SetNextOrderItemId(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItemId")?.SetValue(id.ToString());
        XmlTools.SaveListToXMLElement(root, s_config);
    }
}