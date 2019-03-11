namespace supermarket
{
    public enum SpecialOfferType
    {
        ThreeForTwo, TenPercentDiscount, TwoForAmount, FiveForAmount
    }

    public class Offer
    {
        private readonly SpecialOfferType _offerType;
        private readonly double _argument;

        public Product Product { get; }

        public Offer(SpecialOfferType offerType, Product product, double argument)
        {
            this._offerType = offerType;
            this._argument = argument;
            this.Product = product;
        }

        public virtual Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            var quantityAsInt = (int)quantity;
            var x = 1;
            if (this._offerType == SpecialOfferType.TwoForAmount)
            {
                x = 2;
                if (quantityAsInt >= 2)
                {
                    double total = this._argument * quantityAsInt / x + quantityAsInt % 2 * unitPrice;
                    double discountN = unitPrice * quantity - total;
                    return new Discount(product, "2 for " + this._argument, discountN);
                }

            }
            if (this._offerType == SpecialOfferType.FiveForAmount)
            {
                x = 5;
            }
            var numberOfXs = quantityAsInt / x;
            if (this._offerType == SpecialOfferType.TenPercentDiscount)
            {
                return new Discount(product, this._argument + "% off", quantity * unitPrice * this._argument / 100.0);
            }
            if (this._offerType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
            {
                var discountTotal = unitPrice * quantity - (this._argument * numberOfXs + quantityAsInt % 5 * unitPrice);
                return new Discount(product, x + " for " + this._argument, discountTotal);
            }

            return null;
        }
    }
}

