namespace eshop.Client.Models.Products
{
    public record ProductResponse(int Id, string Name, string Description, decimal Price, int CategoryId);
}
