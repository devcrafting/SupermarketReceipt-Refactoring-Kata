package dojo.supermarket.model;

import java.util.ArrayList;
import java.util.List;

public class Teller {

    private final SupermarketCatalog catalog;
    private List<Offer> offers = new ArrayList<>();

    public Teller(SupermarketCatalog catalog) {
        this.catalog = catalog;
    }

    public void addSpecialOffer(SpecialOfferType offerType, Product product, double argument) {
        this.offers.add(new SingleProductOffer(offerType, product, argument));
    }

    public void addSpecialOffer(Offer offer) {
        this.offers.add(offer);
    }

    public Receipt checksOutArticlesFrom(ShoppingCart theCart) {
        Receipt receipt = new Receipt();
        List<ProductQuantity> productQuantities = theCart.getItems();
        for (ProductQuantity pq: productQuantities) {
            Product p = pq.getProduct();
            double quantity = pq.getQuantity();
            double unitPrice = this.catalog.getUnitPrice(p);
            double price = quantity * unitPrice;
            receipt.addProduct(p, quantity, unitPrice, price);
        }

        handleOffers(theCart, receipt);

        return receipt;
    }

    private void handleOffers(ShoppingCart theCart, Receipt receipt) {
        for (Offer offer : this.offers) {
            if (offer.canApplyTo(theCart.productQuantities())) {
                receipt.addDiscount(offer.getDiscount(theCart.productQuantities(), catalog));
            }
        }
    }
}
