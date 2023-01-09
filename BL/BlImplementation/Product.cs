using BO;
using DO;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;


    /// <summary>
    /// GetListedProducts shows a list of products to the manager and for the client Catalogue
    /// </summary>
    /// <returns></returns> the new List products (type ProductForList)
    /// <exception cref="NullReferenceException"></exception>

    //For Manager:
    public IEnumerable<BO.ProductForList?> GetListedProducts(Func<ProductForList?, bool>? filter)
    {
        var productForList = from doProduct in dal.Product.GetAll() // get a list of products and scan it
               select new BO.ProductForList //build a new List products (type ProductForList) 
               {
                   Id = doProduct?.Id ?? 0,
                   Name = doProduct?.Name ?? "",
                   Category = (BO.Category)doProduct?.Category!,
                   Price = doProduct?.Price ?? 0,
                   ImageRelativeName = @"\pics\IMG" + doProduct?.Name + ".jpg"
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
    /// Get products by their id for the manager
    /// </summary>
    /// <param name="productId"></param> gets a product id
    /// <returns></returns> the Product object
    /// <exception cref="Exception"></exception>
    public BO.Product GetByIdM(int productId)
    {
        try
        {

            DO.Product doProduct = dal.Product.Get(x => x?.Id == productId); //find the wanted product by its id and copy it to a new one

            if (doProduct.Id > 0)
            {
                return new BO.Product() //create a new Product (type BO) and return it with the wanted values
                {
                    //copy the details

                    Id = doProduct.Id,
                    Category = (BO.Category)doProduct.Category!, //take the doProduct Category, turn it into BO.Category
                    Price = doProduct.Price,
                    Name = doProduct.Name,
                    InStock = doProduct.InStock,
                    ImageRelativeName = @"\pics\IMG" + doProduct.Name + ".jpg"
                };
            }
            else
                throw new BlInvalidEntityException(doProduct.Id, doProduct.Name! , 0);
        }

        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException("Data exception:", ex);
        }
    }

    /// <summary>
    /// add a new product
    /// </summary>
    /// <param name="productId">the product of the id</param>
    /// <param name="productName">the product's name</param>
    /// <param name="category">it's category</param>
    /// <param name="price">it's price</param>
    /// <param name="amount">it's amount</param>
    /// <exception cref="BO.BlInvalidEntityException">throw if the input isn't valid</exception> 
    /// <exception cref="BO.BlAlreadyExistEntityException">throw if the product already exists</exception>
    public void AddProduct(int productId, string productName, BO.Category category, double price, int amount)
    {
        try
        {
            if (productId <= 100000)
                throw new BO.BlInvalidEntityException("product Id", 1);
            if (productName is null) 
                throw new BO.BlInvalidEntityException("product Name", 1); //will put EntityChoice = 4 and print - Name is null ;
            if (price < 0)
                throw new BO.BlInvalidEntityException("product price", 0);
            if (amount < 0)
                throw new BO.BlInvalidEntityException("product amount", 0);

            DO.Product newDoProduct = new DO.Product() //create a new data layer product
            {
                //copy the fields
                Id = productId,
                Name = productName,
                Category = (DO.Category)category,  //take the newDoProduct Category, turn it into DO.Category
                Price = price,
                InStock = amount,
                ImageRelativeName = @"\pics\IMG" + productName + ".jpg"
            };
            int newId = dal.Product.Add(newDoProduct); //add the product (DO type), and dal.Product.Add(newDoProduct) returns int type

        }
        catch (DO.DalAlreadyExistsIdException ex)
        {
            throw new BO.BlAlreadyExistEntityException("Data exception:", ex);
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
            if (product.Id < 0)
                throw new BO.BlInvalidEntityException("product Id", 0);
            if (product.Name is null)
                throw new BO.BlInvalidEntityException("product Name", 1); //will put EntityChoice = 1 and print - Name is null ;
            if (product.Price < 0)
                throw new BO.BlInvalidEntityException("product price", 0);
            if (product.InStock < 0)
                throw new BO.BlInvalidEntityException("product amount", 0);
            DO.Product newDoProduct = new DO.Product() //create a new data layer product
            {
                //copy the fields
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                InStock = product.InStock,
                Category = (DO.Category)product.Category!, //take the newDoProduct Category, turn it into DO.Category
                ImageRelativeName = @"\pics\IMG" + product.Name + ".jpg"
            };
            dal.Product.Update(newDoProduct); //update product in data layer
        }

        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException("Data exception:", ex);
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
            throw new BO.BlMissingEntityException("Data exception:", ex);
        }
    }
    
    //For Client:

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

            if (doProduct.Id > 0)
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
                    ImageRelativeName = @"\pics\IMG" + doProduct.Name + ".jpg"
                };
            }
            else
                throw new BlInvalidEntityException("Id", 0);
        }
        catch (DO.DalDoesNotExistIdException ex) 
        {
            throw new BO.BlMissingEntityException("Data exception:", ex);
        }

    }

    public IEnumerable<BO.ProductItem?> GetListedProductItems(BO.Cart cart, Func<ProductItem?, bool>? filter)
    {
        var productItemsList = from doProduct in dal.Product.GetAll() // get a list of products and scan it
                               select GetByIdC(doProduct?.Id ?? 0, cart);//build a new products list (type ProductItem) 

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