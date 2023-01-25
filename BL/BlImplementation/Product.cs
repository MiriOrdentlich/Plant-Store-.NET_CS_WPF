using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;

    //Get a list of 8 most popular items 
    public IEnumerable<ProductForList?> GetListedPopularItems()
    {
        var tmp = dal!.OrderItem.GetAll();
        var Pop = from doOrderItem in tmp
                  group doOrderItem by doOrderItem?.ProductID into orderItemGroup
                  select new { Id = orderItemGroup.Key, Items = orderItemGroup };

        //popular is decided by the number of orders that have the product
        Pop = Pop.OrderByDescending(x => x.Items.Count()).Take(8);

        try
        {
            var tmp1= from item in Pop // get a list of products and scan it
                   let doProduct = dal.Product.GetById(item?.Id ?? throw new Exception("sds"))
                   select new BO.ProductForList //build a new List products (type ProductForList) 
                   {
                       Id = doProduct.Id,
                       Name = doProduct.Name,
                       Category = (BO.Category)doProduct.Category!,
                       Price = doProduct.Price,
                       ImageRelativeName = @"\pics\" + doProduct.Name + ".jpeg"
                   };
            return tmp1;
        }
        catch(DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing Product", ex, 2);
        }
    }



    /// <summary>
    /// GetListedProducts shows a list of products to the manager and for the client Catalogue
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>the new List products (type ProductForList)</returns>
    public IEnumerable<BO.ProductForList?> GetListedProducts(Func<ProductForList?, bool>? filter)
    {
        var productForList = from doProduct in dal.Product.GetAll() // get a list of products and scan it
               select new BO.ProductForList //build a new List products (type ProductForList) 
               {
                   Id = doProduct?.Id ?? -1,
                   Name = doProduct?.Name ?? "",
                   Category = (BO.Category)doProduct?.Category!,
                   Price = doProduct?.Price ?? 0,
                   ImageRelativeName = @"\pics\" + doProduct?.Name + ".jpeg"
               };
        if (filter == null)
        {
            return productForList;
        }
        else
        {
            return from x in productForList
                   where filter(x)
                   select x;
        }
    }

    /// <summary>
    /// MANAGER
    /// base on the given Product code, build a Product object
    /// that match the needed product type
    /// </summary>
    /// <param name="productId"></param> gets a product id
    /// <returns></returns> the Product object
    /// <exception cref="Exception"></exception>
    public BO.Product GetByIdM(int productId)
    {
        try
        {
            DO.Product doProduct = dal.Product.Get(x => x?.Id == productId); //find the wanted product by its id and copy it to a new one
            if (doProduct.Id >= 100000)
            {
                return new BO.Product() //create a new Product (type BO) and return it with the wanted values
                {
                    //copy the details
                    Id = doProduct.Id,
                    Category = (BO.Category)doProduct.Category!, //take the doProduct Category, turn it into BO.Category
                    Price = doProduct.Price,
                    Name = doProduct.Name,
                    InStock = doProduct.InStock,
                    ImageRelativeName = @"\pics\" + doProduct.Name + ".jpeg"
                };
            }
            else
                throw new BlInvalidEntityException("Product name" , 1);
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    /// <summary>
    /// get product item details, create a product and add a new product to dataSource
    /// </summary>
    /// <param name="productId">the product of the id</param>
    /// <param name="productName">the product's name</param>
    /// <param name="category">it's category</param>
    /// <param name="price">it's price</param>
    /// <param name="amount">it's amount</param>
    /// <exception cref="BO.BlInvalidEntityException">throw if the input isn't valid</exception> 
    /// <exception cref="BO.BlAlreadyExistEntityException">throw if the product already exists</exception>
    public void AddProduct(int productId, string productName, BO.Category? category, double price, int amount)
    {
        try
        {
            //if (productId < 100000)
            //    throw new BO.BlInvalidEntityException("Product ID", 1);
            if (productName == "") 
                throw new BO.BlInvalidEntityException("product Name", 1); //will put EntityChoice = 4 and print - Name is null ;
            if (price < 0)
                throw new BO.BlInvalidEntityException("Product price", 0);
            if (amount < 0)
                throw new BO.BlInvalidEntityException("Product amount", 0);

            //try to add the product (DO type):
            int newId = dal.Product.Add(new DO.Product() //create a new data layer product
            {
                //copy the fields
                Id = productId,
                Name = productName,
                Category = (DO.Category)category!,  //take the newDoProduct Category, turn it into DO.Category
                Price = price,
                InStock = amount,
                ImageRelativeName = @"\pics\" + productName + ".jpeg"
            });
        }
        catch (DO.DalAlreadyExistsIdException ex)
        {
            throw new BO.BlAlreadyExistEntityException(ex.EntityName, ex.EntityId, 1);
        }
    }

    /// <summary>
    /// update an existing product
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.BlInvalidEntityException">throw if the input isn't valid</exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public void UpdateProduct(BO.Product product) //update product details
    {
        try
        {
            //product.InStock.AmountIsNegative();
            if (product.Id < 100000)
                throw new BO.BlInvalidEntityException("Product ID", 1);
            if (product.Name == "")
                throw new BO.BlInvalidEntityException("Product name", 1); //will put EntityChoice = 1 and print - Name is null
            if (product.Price < 0)
                throw new BO.BlInvalidEntityException("Product price", 0);
            if (product.InStock < 0)
                throw new BO.BlInvalidEntityException("Product amount", 0);
            DO.Product newDoProduct = new DO.Product() //create a new data layer product
            {
                //copy the fields:
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                InStock = product.InStock,
                Category = (DO.Category)product.Category!, //take the newDoProduct Category, turn it into DO.Category
                ImageRelativeName = @"\pics\" + product.Name + ".jpeg"
            };
            dal.Product.Update(newDoProduct); //update product in data layer
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Delete a product from the list
    /// </summary>
    /// <param name="productId">the product id</param>
    /// <exception cref="BO.BlAlreadyExistEntityException">throw </exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public void DeleteProduct(int productId) //delete a product by its id
    {
        try
        {
            var tmp = dal.Product.Get(x => x?.Id == productId); //if product doesn't exist get exception from data layer 

            // get a list of orders in order to check if the wanted product is there,
            //search which product.id is equal to the given product id:
            var list = dal.OrderItem.GetAll(x => x?.ProductID == productId);
            if (list.Any()) //if there is a product.id that match the wanted one
                throw new BO.BlAlreadyExistEntityException("Product", productId, -1); //Exception: Product exists in an order
            dal.Product.Delete(productId); //delete the product
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }
    
    /// <summary>
    /// Get a product Item by its id for the client (convert DO.Product to BO.ProductItem)
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    /// <exception cref="BlInvalidEntityException">if the id is negative</exception>
    /// <exception cref="BO.BlMissingEntityException">if the id doesnt exist</exception>
    public BO.ProductItem GetByIdC(int productId, BO.Cart cart)
    {
        try
        {
            DO.Product doProduct = dal.Product.Get(x => x?.Id == productId); //find the wanted product by its id and copy it to a new one

            if (doProduct.Id > 99999)
            {
                return new BO.ProductItem()//create a new Product (type BO) and return it withthe wanted values   
                {
                    Id = doProduct.Id,
                    Category = (BO.Category)doProduct.Category!, //take the doProduct Category, turn it into BO.Category
                    Price = doProduct.Price,
                    Name = doProduct.Name,
                    InStock = doProduct.InStock > 0,
                    Amount = (from item in cart.Items
                             where item.ProductID == doProduct.Id
                             select item.Amount).Sum(),
                    ImageRelativeName = @"\pics\" + doProduct.Name + ".jpeg"
                };
            }
            else
                throw new BlInvalidEntityException("Id", 1);
        }
        catch (DO.DalDoesNotExistIdException ex) 
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }

    }

    /// <summary>
    /// make a list of product items according to the filter (if there is one) by converting DO.Product 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="filter"></param>
    /// <returns>list of product items</returns>
    public IEnumerable<BO.ProductItem?> GetListedProductItems(BO.Cart cart, Func<ProductItem?, bool>? filter)
    {
        var productItemsList = from doProduct in dal.Product.GetAll() // get a list of products and scan it
                               select GetByIdC(doProduct?.Id ?? -1, cart);//build a new products list (type ProductItem) 

        if (filter == null)
        {
            return productItemsList;
        }
        else
        {
            return from x in productItemsList
                   where filter(x)
                   select x;
        }
    }
}