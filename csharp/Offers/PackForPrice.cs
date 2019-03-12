namespace supermarket
{
    public class PackForPrice : IDiscountOneProduct
    {
        private readonly double _nbInPack;
        private readonly double _price;

        public PackForPrice(double nbInPack, double price)
        {
            _nbInPack = nbInPack;
            _price = price;
        }

        public Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            if (quantity < _nbInPack) return null;

            var nbPacks = (int) (quantity / _nbInPack);
            var nbOutOfPack = (int)quantity % _nbInPack;
            var discountTotal = unitPrice * quantity - (_price * nbPacks + nbOutOfPack * unitPrice);
            return new Discount(product, _nbInPack + " for " + _price, discountTotal);
        }
    }
}
