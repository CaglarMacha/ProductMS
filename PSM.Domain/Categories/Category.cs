using PSM.Domain.Entities;
using PSM.Domain.Products;


namespace PSM.Domain.Categories
{
    public class Category : AuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
        public Category(Guid id) : base(id)
        {
        }
        public Category() { }
    }
}
