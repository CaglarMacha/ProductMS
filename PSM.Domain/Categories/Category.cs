using PSM.Domain.Entities;
using PSM.Domain.Products;
using System.Diagnostics.CodeAnalysis;


namespace PSM.Domain.Categories
{
    public class Category : AuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameNormalized { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category(Guid id) : base(id)
        {
        }
        public Category() { }
        internal Category(Guid id,[NotNull] string name): base(id)
        {
            SetName(name);
        }
        public void SetName([NotNull] string title)
        {
            if (!string.IsNullOrWhiteSpace(title) && title.Length >= CategoryConsts.MixNameLength)
            {
                Name = title;
                NameNormalized = title.ToLowerInvariant().Normalize();
            };
        }
    }
}
