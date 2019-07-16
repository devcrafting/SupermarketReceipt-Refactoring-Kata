package dojo.supermarket.model;

import java.util.Map;

public interface Offer {
    boolean canApplyTo(Map<Product, Double> productQuantities);

    Discount getDiscount(Map<Product, Double> productQuantities, SupermarketCatalog catalog);
}
