using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DavidHjorthDeluxeMobler.Models;

namespace DavidHjorthDeluxeMobler.Controllers
{
    public class HomeController : Controller
    {
        public List<Furniture> furniturelist = Furniture.GetData();
        public UserData userdata;


        public ActionResult Index()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            ViewModel VM = ViewModel.viewmodel(furniturelist, userdata);

            return View(VM);
        }
        public ActionResult Buy(int id)
        {//lägger till varor i carten
            foreach (Furniture furniture in furniturelist)
            {
                if (furniture.Id == id)
                {
                    furniture.Count--;
                    furniture.BuyCount++;
                    Furniture.SaveData(furniturelist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    if (userdata.CartList == null)
                    {
                        userdata.CartList = new List<UserData.Cart>();
                    }
                    userdata.CartList.Add(new UserData.Cart { Id = furniture.Id, ReturnDate = DateTime.Now.AddDays(30) });
                    UserData.SaveUserData(userdata);
                }
            }
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(furniturelist, userdata);

            return View("Index", VM);
        }//Remove item knappen tar bort från carten
        public ActionResult Return(int id)
        {
            foreach (Furniture furniture in furniturelist)
            {
                if (furniture.Id == id)
                {
                    furniture.Count++;
                    Furniture.SaveData(furniturelist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    var itemToRemove = userdata.CartList.FirstOrDefault(r => r.Id == id);
                    if (itemToRemove != null)
                    {
                        userdata.CartList.Remove(itemToRemove);
                        UserData.SaveUserData(userdata);
                    }
                }
            }

            ViewModel VM = ViewModel.viewmodel(furniturelist, userdata);

            return View("Index", VM);
        }
     
    }
}