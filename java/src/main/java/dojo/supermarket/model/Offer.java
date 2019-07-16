package dojo.supermarket.model;

import java.util.Map;

public class Offer {
    SpecialOfferType offerType;
    private final Product product;
    double argument;

    public Offer(SpecialOfferType offerType, Product product, double argument) {
        this.offerType = offerType;
        this.argument = argument;
        this.product = product;
    }

    Product getProduct() {
        return this.product;
    }

    public boolean canApplyTo(Map<Product, Double> productQuantities) {
        return productQuantities.containsKey(product);
    }

    public Discount getDiscount(Map<Product, Double> productQuantities, SupermarketCatalog catalog) {
        return getDiscount(productQuantities.get(product), catalog.getUnitPrice(product));
    }

    public Discount getDiscount(double quantity, double unitPrice) {
        int quantityAsInt = (int) quantity;
        if (offerType == SpecialOfferType.TwoForAmount) {
            return getXForAmountDiscount(2, quantity, argument, unitPrice, quantityAsInt);
        } else if (offerType == SpecialOfferType.ThreeForTwo) {
            return  getThreeForTwo(quantity, unitPrice, quantityAsInt);
        } else if (offerType == SpecialOfferType.TenPercentDiscount) {
            return new Discount(product, argument + "% off", quantity * unitPrice * argument / 100.0);
        } else if (offerType == SpecialOfferType.FiveForAmount) {
            return getXForAmountDiscount(5, quantity, argument, unitPrice, quantityAsInt);
        } else {
            return null;
        }
    }

    private Discount getXForAmountDiscount(int nbPerPackage, double quantity, double packagePrice, double unitPrice, int quantityAsInt) {
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
        return new Discount(product, nbPerPackage + " for " + packagePrice, discountTotal);
    }

    private Discount getThreeForTwo(double quantity, double unitPrice, int quantityAsInt) {
        if (quantityAsInt <= 2) {
            return null;
        }
        int numberOfXs = quantityAsInt / 3;
        double discountAmount = quantity * unitPrice - ((numberOfXs * 2 * unitPrice) + quantityAsInt % 3 * unitPrice);
        return new Discount(product, "3 for 2", discountAmount);
    }
}
