using System.Collections.Generic;
using System.ComponentModel.Design;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace supermarket
{
    [TestClass]
    public class SupermarketTest
    {
        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void TestSomething()
        {
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 0.99);
            var apples = new Product("apples", ProductUnit.Kilo);
            catalog.AddProduct(apples, 1.99);
            var rice = new Product("rice", ProductUnit.Each);
            catalog.AddProduct(rice, 2.49);
            var toothpaste = new Product("toothpaste", ProductUnit.Each);
            catalog.AddProduct(toothpaste, 1.79);
            var cherryTomato = new Product("cherry tomato", ProductUnit.Each);
            catalog.AddProduct(cherryTomato, 0.69);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(apples, 2.5);
            cart.AddItem(rice);
            cart.AddItemQuantity(toothbrush, 2);
            cart.AddItemQuantity(toothpaste, 6);
            cart.AddItemQuantity(toothbrush, 2);
            cart.AddItemQuantity(cherryTomato, 3);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(new SingleProductOffer(apples, new PercentDiscount(20.0)));
            teller.AddSpecialOffer(new SingleProductOffer(rice, new PercentDiscount(10.0)));
            teller.AddSpecialOffer(new SingleProductOffer(toothbrush, new GetSomeForFree(2, 1)));
            teller.AddSpecialOffer(new SingleProductOffer(toothpaste, new PackForPrice(5, 7.49)));
            teller.AddSpecialOffer(new SingleProductOffer(cherryTomato, new PackForPrice(2, 0.99)));

            var receiptPrinter = new ReceiptPrinter();

            var receipt = teller.ChecksOutArticlesFrom(cart);

            Approvals.Verify(receiptPrinter.PrintReceipt(receipt));
        }

        [TestMethod]
        public void GetSomeForFree_should_return_discount_for_2_free_for_3_bought()
        {
            var product = new Product("test", ProductUnit.Each);
            var getSomeForFree = new GetSomeForFree(3, 2);

            var discount = getSomeForFree.GetDiscount(product, 6, 0.99);

            Check.That(discount.DiscountAmount).IsCloseTo(1.98, 0.001);
        }

        [TestMethod]
        public void Bundle_of_toothpaste_and_toothbrush_should_return_10_percent_discount()
        {
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 0.99);
            var toothpaste = new Product("toothpaste", ProductUnit.Each);
            catalog.AddProduct(toothpaste, 1.79);
            var bundle = new Bundle(new[] { toothpaste, toothbrush }, 10);

            var discount = bundle.GetDiscount(new Dictionary<Product, double> {[toothpaste] = 1, [toothbrush] = 1}, catalog);

            Check.That(discount.DiscountAmount).IsCloseTo(0.278, 0.001);
        }

        [TestMethod]
        public void Multiple_bundles_of_toothpaste_and_toothbrush_should_return_10_percent_discount()
        {
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 0.99);
            var toothpaste = new Product("toothpaste", ProductUnit.Each);
            catalog.AddProduct(toothpaste, 1.79);
            var bundle = new Bundle(new[] { toothpaste, toothbrush }, 10);

            var discount = bundle.GetDiscount(new Dictionary<Product, double> { [toothpaste] = 2, [toothbrush] = 2 }, catalog);

            Check.That(discount.DiscountAmount).IsCloseTo(0.556, 0.001);
        }
    }
}
