namespace supermarket
{
    public enum SpecialOfferType
    {
        ThreeForTwo, TenPercentDiscount, TwoForAmount, FiveForAmount
    }

    public class Offer
    {
        public SpecialOfferType OfferType { get; }
        private Product _product;
        public double Argument { get; }

        public Offer(SpecialOfferType offerType, Product product, double argument)
        {
            this.OfferType = offerType;
            this.Argument = argument;
            this._product = product;
        }

        public Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            var quantityAsInt = (int)quantity;
            var x = 1;
            if (this.OfferType == SpecialOfferType.ThreeForTwo)
            {
                x = 3;

            }
            else if (this.OfferType == SpecialOfferType.TwoForAmount)
            {
                x = 2;
                if (quantityAsInt >= 2)
                {
                    double total = this.Argument * quantityAsInt / x + quantityAsInt % 2 * unitPrice;
                    double discountN = unitPrice * quantity - total;
                    return new Discount(product, "2 for " + this.Argument, discountN);
                }

            }
            if (this.OfferType == SpecialOfferType.FiveForAmount)
            {
                x = 5;
            }
            var numberOfXs = quantityAsInt / x;
            if (this.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
            {
                var discountAmount = quantity * unitPrice - ((numberOfXs * 2 * unitPrice) + quantityAsInt % 3 * unitPrice);
                return new Discount(product, "3 for 2", discountAmount);
            }
            if (this.OfferType == SpecialOfferType.TenPercentDiscount)
            {
                return new Discount(product, this.Argument + "% off", quantity * unitPrice * this.Argument / 100.0);
            }
            if (this.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
            {
                var discountTotal = unitPrice * quantity - (this.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
                return new Discount(product, x + " for " + this.Argument, discountTotal);
            }

            return null;
        }
    }
}

