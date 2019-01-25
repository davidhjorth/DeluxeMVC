﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DavidHjorthDeluxeMobler.Models;

namespace DavidHjorthDeluxeMobler.Controllers
{
    public class ProductController : Controller
    {
        public List<Furniture> furniturelist = Furniture.GetData();
        // GET: Product
        public ActionResult Details(int id)
        {
            var furniture = furniturelist.FirstOrDefault(f => f.Id == id);
            if (furniture == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(furniture);
        }
    }
}