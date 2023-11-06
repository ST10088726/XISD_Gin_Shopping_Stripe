using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using XISD_Gin_Website_Shopping.Models;

namespace XISD_Gin_Website_Shopping.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            List<ProductEntity> productsList=new List<ProductEntity>();
            productsList = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Product="Tommy Hilfiger",
                    Price=1500,
                    Quantity=2,
                    ImagePath="img/GIN-SOCIETY-ORIGINAL-750ML.jpg"//Edit

                }
            };

            return View(productsList);
        }
        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();

            Session session = service.Get(TempData["Session"].ToString());


            if(session.PaymentStatus == "paid")
            {
                var transaction=session.PaymentIntentId.ToString();

                return View("Success");
            }
            return View("Login"); 
            
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
         List<ProductEntity> productsList = new List<ProductEntity>();
            productsList = new List<ProductEntity>
          {
                new ProductEntity
                {
                    Product="Tommy Hilfiger",
                    Price=1500,
                    Quantity=2,
                    ImagePath="img/GIN-SOCIETY-ORIGINAL-750ML.jpg"//Edit

                }
           };

            var domain = "https://localhost:7139/";

            var options = new SessionCreateOptions
            {
                SuccessUrl=domain + $"CheckOut/OrderConfirmation",
                CancelUrl=domain + "Checkout/Login",
                LineItems=new List<SessionLineItemOptions>(),
                Mode="payment",
                CustomerEmail="mahomedfaraaz@gmail.com"//Edit
            };

            foreach (var item in productsList)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * item.Quantity),
                        Currency = "zar",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.ToString(),
                        }
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionListItem);
            }

            var service=new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }
    }
}
