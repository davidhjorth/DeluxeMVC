using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DavidHjorthDeluxeMobler.Models;

namespace DavidHjorthDeluxeMobler.Controllers
{
    public class CartController : Controller
    {

        public List<Furniture> furniturelist = Furniture.GetData();
        public UserData userdata;
        // GET: Cart
        public ActionResult Index()
        {
                // Det görs en extra kontroll att man är inloggad annars skickas man tillbaka till inloggningen. Efter kontrollen så hämtar man det usern har klickat buy på och skickar det till carten för att få en summa av allt
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            ViewModel VM = ViewModel.viewmodel(furniturelist, userdata);

            return View("Index", VM);
        }
    }
}