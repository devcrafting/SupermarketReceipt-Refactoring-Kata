namespace supermarket
{
    public class PackForPrice : Offer
    {
        private readonly double _nbInPack;
        private readonly double _price;

        public PackForPrice(Product product, double nbInPack, double price) : base(product)
        {
            _nbInPack = nbInPack;
            _price = price;
        }

        public override Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            if (quantity < _nbInPack) return null;

            var nbPacks = (int) (quantity / _nbInPack);
            var nbOutOfPack = (int)quantity % _nbInPack;
            var discountTotal = unitPrice * quantity - (_price * nbPacks + nbOutOfPack * unitPrice);
            return new Discount(product, _nbInPack + " for " + _price, discountTotal);
        }
    }
}
