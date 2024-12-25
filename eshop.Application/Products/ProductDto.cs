namespace eshop.Application
{

    namespace eshop.Application.Products
    {
        public record ProductDto(int Id, string Name, string Description, decimal Price, int CategoryId);
    }
    
}
