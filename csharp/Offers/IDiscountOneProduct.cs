namespace supermarket
{
    public interface IDiscountOneProduct
    {
        Discount GetDiscount(Product product, double quantity, double unitPrice);
    }
}