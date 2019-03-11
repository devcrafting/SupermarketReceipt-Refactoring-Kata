using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, apples, 20.0);
            teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, rice, 10.0);
            teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, toothpaste, 7.49);
            teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, 0);
            teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, cherryTomato, 0.99);

            var receiptPrinter = new ReceiptPrinter();

            var receipt = teller.ChecksOutArticlesFrom(cart);

            Approvals.Verify(receiptPrinter.PrintReceipt(receipt));
        }
    }
}
