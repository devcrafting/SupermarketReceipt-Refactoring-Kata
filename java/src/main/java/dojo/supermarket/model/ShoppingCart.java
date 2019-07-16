package dojo.supermarket.model;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ShoppingCart {

    private final List<ProductQuantity> items = new ArrayList<>();
    Map<Product, Double> productQuantities = new HashMap<>();


    List<ProductQuantity> getItems() {
        return new ArrayList<>(items);
    }

    void addItem(Product product) {
        this.addItemQuantity(product, 1.0);
    }

    Map<Product, Double> productQuantities() {
        return productQuantities;
    }


    public void addItemQuantity(Product product, double quantity) {
        items.add(new ProductQuantity(product, quantity));
        if (productQuantities.containsKey(product)) {
            productQuantities.put(product, productQuantities.get(product) + quantity);
        } else {
            productQuantities.put(product, quantity);
        }
    }

    void handleOffers(Receipt receipt, Map<Product, Offer> offers, SupermarketCatalog catalog) {
        for (Product p : productQuantities().keySet()) {
            double quantity = productQuantities.get(p);
            if (offers.containsKey(p)) {
                Offer offer = offers.get(p);
                double unitPrice = catalog.getUnitPrice(p);
                int quantityAsInt = (int) quantity;
                Discount discount = null;
                if (offer.offerType == SpecialOfferType.TwoForAmount) {
                    discount = getXForAmountDiscount(2, p, quantity, offer.argument, unitPrice, quantityAsInt);
                } else if (offer.offerType == SpecialOfferType.ThreeForTwo) {
                    discount = getThreeForTwo(p, quantity, unitPrice, quantityAsInt);
                } else if (offer.offerType == SpecialOfferType.TenPercentDiscount) {
                    discount = new Discount(p, offer.argument + "% off", quantity * unitPrice * offer.argument / 100.0);
                } else if (offer.offerType == SpecialOfferType.FiveForAmount) {
                    discount = getXForAmountDiscount(5, p, quantity, offer.argument, unitPrice, quantityAsInt);
                }
                if (discount != null)
                    receipt.addDiscount(discount);
            }
        }
    }

    private Discount getXForAmountDiscount(int nbPerPackage, Product p, double quantity, double packagePrice, double unitPrice, int quantityAsInt) {
        if (quantityAsInt < nbPerPackage) {
            return null;
        }
        int nbOfPackages = quantityAsInt / nbPerPackage;
        double normalPrice = unitPrice * quantity;
        int nbItemsOutOfPackage = quantityAsInt % nbPerPackage;
        double discountedPrice =
                packagePrice * nbOfPackages
                + nbItemsOutOfPackage * unitPrice;
        double discountTotal = normalPrice - discountedPrice;
        return new Discount(p, nbPerPackage + " for " + packagePrice, discountTotal);
    }

    private Discount getThreeForTwo(Product p, double quantity, double unitPrice, int quantityAsInt) {
        if (quantityAsInt <= 2) {
            return null;
        }
        int numberOfXs = quantityAsInt / 3;
        double discountAmount = quantity * unitPrice - ((numberOfXs * 2 * unitPrice) + quantityAsInt % 3 * unitPrice);
        return new Discount(p, "3 for 2", discountAmount);
    }
}
