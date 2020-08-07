using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService service)
        {
            _sellerService = service;
        }

        public IActionResult Index()
        {

            var list = _sellerService.FindAll(); // retornar uma lista de Seller
            return View(list);
        }
    }
}
