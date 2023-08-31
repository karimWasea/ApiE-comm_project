namespace apistudy.Models.Detos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> ProductNames { get; set; }
        public List<string> ProductTitles { get; set; }
    }

}
