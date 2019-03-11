namespace supermarket
{
    public class GetSomeForFree : Offer
    {
        public GetSomeForFree(Product product, double nbToApply, double nbFree) : base(SpecialOfferType.ThreeForTwo, product, 0)
        {
        }

        public override Discount GetDiscount(Product product, double quantity, double unitPrice)
        {
            if (quantity <= 3) return null;
            var nbOfOffer = (int) quantity / 3;
            var nbProductOutOfOffer = (int) quantity % 3;
            var discountAmount = quantity * unitPrice - ((nbOfOffer * 2 * unitPrice) + nbProductOutOfOffer * unitPrice);
            return new Discount(product, "3 for 2", discountAmount);
        }
    }
}