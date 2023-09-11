namespace Ecommerce_Api.Models.Entityies
{
    public class PaginatedResult<TEntity>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<TEntity> Items { get; set; }
    }
}
