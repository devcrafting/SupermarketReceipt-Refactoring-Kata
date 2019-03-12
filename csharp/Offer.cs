using System.Collections.Generic;

namespace supermarket
{
    public class Offer
    {
        private readonly IDiscountOneProduct _discounter;

        public Offer(Product product, IDiscountOneProduct discounter)
        {
            _discounter = discounter;
            Product = product;
        }

        public Product Product { get; }

        public virtual Discount GetDiscount(IDictionary<Product, double> quantitiesByProduct, SupermarketCatalog catalog)
        {
            if (!quantitiesByProduct.ContainsKey(Product)) return null;
            var unitPrice = catalog.GetUnitPrice(Product);
            var quantity = quantitiesByProduct[Product];
            return _discounter.GetDiscount(Product, quantity, unitPrice);
        }
    }
}

