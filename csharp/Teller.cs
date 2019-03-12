using System.Collections.Generic;

namespace supermarket
{

    public class Teller
    {

        private readonly SupermarketCatalog _catalog;
        private readonly List<IOffer> _offers = new List<IOffer>();

        public Teller(SupermarketCatalog catalog)
        {
            this._catalog = catalog;
        }

        public void AddSpecialOffer(SingleProductOffer singleProductOffer)
        {
            this._offers.Add(singleProductOffer);
        }

        public Receipt ChecksOutArticlesFrom(ShoppingCart theCart)
        {
            var receipt = new Receipt();
            var productQuantities = theCart.GetItems();
            foreach (ProductQuantity pq in productQuantities) {
                var p = pq.Product;
                var quantity = pq.Quantity;
                var unitPrice = this._catalog.GetUnitPrice(p);
                var price = quantity * unitPrice;
                receipt.AddProduct(p, quantity, unitPrice, price);
            }
            theCart.HandleOffers(receipt, this._offers, this._catalog);

            return receipt;
        }

    }
}
