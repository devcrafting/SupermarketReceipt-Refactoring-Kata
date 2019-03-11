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
            return null;
        }
    }
}

