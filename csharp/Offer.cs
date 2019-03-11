using System.Collections.Generic;

namespace supermarket
{
    public abstract class Offer
    {
        protected Offer(Product product)
        {
            Product = product;
        }

        public Product Product { get; }

        public abstract Discount GetDiscount(Product product, double quantity, double unitPrice);

        public virtual Discount GetDiscount(IDictionary<Product, double> quantitiesByProduct, SupermarketCatalog catalog)
        {
            if (!quantitiesByProduct.ContainsKey(Product)) return null;
            var unitPrice = catalog.GetUnitPrice(Product);
            var quantity = quantitiesByProduct[Product];
            return GetDiscount(Product, quantity, unitPrice);
        }
    }
}

