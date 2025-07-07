namespace catalogApi.Exceptions
{
    public class ProductsServiceExceptions : Exception
    {
        public ProductsServiceExceptions() : base("")
        { }
        public ProductsServiceExceptions(string msg) : base(msg)
        { }
    }
}
