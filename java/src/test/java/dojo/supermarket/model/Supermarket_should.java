package dojo.supermarket.model;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import static org.junit.Assert.assertEquals;

public class Supermarket_should {
    private static final double NEVERMIND = -1000;
    private static SupermarketCatalog catalog = new FakeCatalog();
    private static Product toothbrush = new Product("toothbrush", ProductUnit.Each);
    private static Product toothpaste = new Product("toothpaste", ProductUnit.Each);
    private static Product apples = new Product("apples", ProductUnit.Kilo);
    private static Teller teller = new Teller(catalog);

    @BeforeAll
    public static void setup() {
        catalog.addProduct(toothbrush, 0.99);
        catalog.addProduct(toothpaste, 1.49);
        catalog.addProduct(apples, 1.99);
    }

    @Test
    public void apply_no_offer() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(apples, 2.5);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(4.975, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_10_percent_offer() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(apples, 2.5);
        teller.addSpecialOffer(SpecialOfferType.TenPercentDiscount, apples, 10);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(4.4775, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_three_for_two_offer() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 3);
        teller.addSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, NEVERMIND);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(1.98, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_three_for_two_offer_with_four_items() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 4);
        teller.addSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, NEVERMIND);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(2.97, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_three_for_two_offer_with_six_items() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 6);
        teller.addSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, NEVERMIND);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(3.96, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_five_for_amount_offer() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 5);
        teller.addSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, 5);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(5, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_five_for_amount_offer_with_six_items() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 6);
        teller.addSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, 5);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(5.99, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_five_for_amount_offer_with_ten_items() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 10);
        teller.addSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, 5);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(10, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_three_for_amount_offer() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 2);
        teller.addSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, 1.5);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(1.5, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_three_for_amount_offer_with_three_items_bug_suspected() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 3);
        teller.addSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, 1.5);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(2.49, receipt.getTotalPrice(), 0.01);
    }

    @Test
    public void apply_three_for_amount_offer_with_four_items() {
        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 2);
        teller.addSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, 1.5);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(1.5, receipt.getTotalPrice(), 0.01);
    }
}
