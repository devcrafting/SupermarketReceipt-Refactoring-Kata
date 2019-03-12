using System.Collections.Generic;

namespace supermarket
{
    public class SingleProductOffer : IOffer
    {
        private readonly IDiscountOneProduct _discounter;
        private readonly Product _product;

        public SingleProductOffer(Product product, IDiscountOneProduct discounter)
        {
            _discounter = discounter;
            _product = product;
        }

        public virtual Discount GetDiscount(IDictionary<Product, double> quantitiesByProduct, SupermarketCatalog catalog)
        {
            if (!quantitiesByProduct.ContainsKey(_product)) return null;
            var unitPrice = catalog.GetUnitPrice(_product);
            var quantity = quantitiesByProduct[_product];
            return _discounter.GetDiscount(_product, quantity, unitPrice);
        }
    }
}

