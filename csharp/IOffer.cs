using System.Collections.Generic;

namespace supermarket
{
    public interface IOffer
    {
        Discount GetDiscount(IDictionary<Product, double> quantitiesByProduct, SupermarketCatalog catalog);
    }
}