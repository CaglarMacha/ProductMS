using PSM.Domain.Categories;
using PSM.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;


namespace PSM.Domain.Products
{
    public class Product : AuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public int StockQuantity { get; set; } 
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        protected Product(Guid id) : base(id)
        {
        }
        public Product()
        {
                
        }
        internal Product(
            Guid id,
            [NotNull] string title,
        Guid category,
             string description = null)
            : base(id)
        {
            SetTitle(title);
            CategoryId = category;
            Description = description;
        }
        private void SetTitle([NotNull] string title)
        {

            if (!string.IsNullOrWhiteSpace(title) && title.Length<= ProductConsts.MaxTitleLength) 
            {
                Title = title;
            };
        }
    }
}
