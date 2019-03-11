namespace supermarket
{
    public abstract class Offer
    {
        protected Offer(Product product)
        {
            Product = product;
        }

        public Product Product { get; }

        public abstract Discount GetDiscount(Product product, double quantity, double unitPrice);
    }
}

