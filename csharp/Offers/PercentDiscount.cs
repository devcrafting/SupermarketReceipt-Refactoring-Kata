namespace supermarket
{
    public class PercentDiscount : IDiscountOneProduct
    {
        private readonly double _percentToDiscount;

        public PercentDiscount(double percentToDiscount)
        {
            _percentToDiscount = percentToDiscount;
        }

        public Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            return new Discount(product, _percentToDiscount + "% off", quantity * unitPrice * _percentToDiscount / 100.0);
        }
    }
}