namespace BlazorAppEcom.Client.Services.CategoryServices
{
    public interface ICategoryServices
    {
        List<Category> Categories { get; set; }
        Task GetCategories();
    }
}
