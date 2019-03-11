namespace supermarket
{
    public class PercentDiscount : Offer
    {
        private readonly double _percentToDiscount;

        public PercentDiscount(Product product, double percentToDiscount) : base(product)
        {
            _percentToDiscount = percentToDiscount;
        }

        public override Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            return new Discount(product, _percentToDiscount + "% off", quantity * unitPrice * _percentToDiscount / 100.0);
        }
    }
}