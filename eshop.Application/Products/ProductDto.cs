namespace eshop.Application
{

    namespace eshop.Application.Products
    {
        public record ProductDto(int id, string name, string description, decimal price, int categoryId);
    }
    
}
