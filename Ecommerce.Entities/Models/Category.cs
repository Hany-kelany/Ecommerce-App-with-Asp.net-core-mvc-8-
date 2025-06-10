namespace Ecommerce.Entities.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
