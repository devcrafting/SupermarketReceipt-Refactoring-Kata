using System;
using System.Collections.Generic;

namespace supermarket
{
    public class ShoppingCart
    {

        private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
        private readonly Dictionary<Product, double> _productQuantities = new Dictionary<Product, double>();


        public List<ProductQuantity> GetItems()
        {
            return new List<ProductQuantity>(_items);
        }

        public void AddItem(Product product)
        {
            this.AddItemQuantity(product, 1.0);
        }


        public void AddItemQuantity(Product product, double quantity)
        {
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                var newAmount = _productQuantities[product] + quantity;
                _productQuantities[product] = newAmount;
            }
            else
            {
                _productQuantities.Add(product, quantity);
            }
        }

        public void HandleOffers(Receipt receipt, IEnumerable<IOffer> offers, SupermarketCatalog catalog)
        {
            foreach (var offer in offers)
            {
                var discount = offer.GetDiscount(_productQuantities, catalog);
                if (discount != null) receipt.AddDiscount(discount);
            }
        }
    }

}
