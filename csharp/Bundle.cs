using System.Collections.Generic;
using System.Linq;

namespace supermarket
{
    public class Bundle : IOffer
    {
        private readonly IEnumerable<Product> _products;
        private readonly IDiscountOneProduct _discounter;

        public Bundle(IEnumerable<Product> products, IDiscountOneProduct discounter)
        {
            _products = products;
            _discounter = discounter;
        }

        public Discount GetDiscount(IDictionary<Product, double> quantitiesByProduct, SupermarketCatalog catalog)
        {
            var nbBundles =
                _products.Select(product =>
                {
                    var found = quantitiesByProduct.TryGetValue(product, out var quantity);
                    return found ? quantity : 0;
                }).Max();
            var bundlePriceWithoutDiscount = _products.Select(catalog.GetUnitPrice).Sum();
            return _discounter.GetDiscount(new Product("bundle", ProductUnit.Each), nbBundles, bundlePriceWithoutDiscount);
        }
    }
}