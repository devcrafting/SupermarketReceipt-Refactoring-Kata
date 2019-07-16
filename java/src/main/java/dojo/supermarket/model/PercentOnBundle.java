package dojo.supermarket.model;

import java.util.Arrays;
import java.util.Map;

public class PercentOnBundle extends Offer {
    private double percentReduction;
    private Product[] products;

    public PercentOnBundle(double percentReduction, Product... products) {
        super();
        this.percentReduction = percentReduction;
        this.products = products;
    }

    @Override
    public boolean canApplyTo(Map<Product, Double> productQuantities) {
        return Arrays.stream(products)
                .allMatch(product -> productQuantities.containsKey(product));
    }

    @Override
    public Discount getDiscount(Map<Product, Double> productQuantities, SupermarketCatalog catalog) {
        double discount =
            Arrays.stream(products)
                .mapToDouble(product ->
                    productQuantities.get(product) * catalog.getUnitPrice(product) * percentReduction / 100.)
                .sum();
        return new Discount(null, percentReduction + "% on bundle", discount);
    }
}
