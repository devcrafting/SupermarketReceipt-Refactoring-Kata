namespace supermarket
{
    public class GetSomeForFree : IDiscountOneProduct
    {
        private readonly double _nbToApply;
        private readonly double _nbPerOffer;

        public GetSomeForFree(double nbToApply, double nbFree)
        {
            _nbToApply = nbToApply;
            _nbPerOffer = nbToApply + nbFree;
        }

        public Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            if (quantity <= _nbToApply) return null;
            var nbOfOffer = (int) (quantity / _nbPerOffer);
            var nbProductOutOfOffer = (int) quantity % _nbPerOffer;
            var discountAmount = quantity * unitPrice - (nbOfOffer * _nbToApply * unitPrice + nbProductOutOfOffer * unitPrice);
            return new Discount(product, $"{_nbPerOffer} for {_nbToApply}", discountAmount);
        }
    }
}