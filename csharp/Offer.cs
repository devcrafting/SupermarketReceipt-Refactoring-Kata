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
            if (this._offerType == SpecialOfferType.TenPercentDiscount)
            {
                return new Discount(product, this._argument + "% off", quantity * unitPrice * this._argument / 100.0);
            }

            return null;
        }
    }
}

