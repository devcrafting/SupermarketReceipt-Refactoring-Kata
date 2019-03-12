using System.Collections.Generic;
using System.Linq;

namespace supermarket
{
    public class Bundle : IOffer
    {
        private readonly IEnumerable<Product> _products;
        private readonly double _percentDiscount;

        public Bundle(IEnumerable<Product> products, double percentDiscount)
        {
            _products = products;
            _percentDiscount = percentDiscount;
        }

        public Discount GetDiscount(IDictionary<Product, double> quantitiesByProduct, SupermarketCatalog catalog)
        {
            var nbBundles =
                _products.Select(product =>
                {
                    var found = quantitiesByProduct.TryGetValue(product, out var quantity);
                    return found ? quantity : 0;
                }).Max();
            var discountedAmount = _products.Select(catalog.GetUnitPrice).Sum() * _percentDiscount / 100 * nbBundles;
            return new Discount(new Product("bundle", ProductUnit.Each), "10% off", discountedAmount);
        }
    }
}