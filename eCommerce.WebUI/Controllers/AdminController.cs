﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Contracts.Repositories;
using eCommerce.Model;

namespace eCommerce.WebUI.Controllers
{
    public class AdminController : Controller
    {
        //Inject repository into the controller rather than instantiate within actionresult
        public IRepositoryBase<Customer> customers;
        public IRepositoryBase<Product> products;

        //use constructor that takes in interface
        public AdminController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products)
        {
            this.customers = customers; //customers passed in = customers up the top
            this.products = products;
        }
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var model = products.GetAll();

            return View(model);
        }

        public ActionResult CreateProduct()
        {
            var model = new Product();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            products.Insert(product);
            products.Commit();
            return RedirectToAction("ProductList");
        }

        public ActionResult EditProduct(int id)
        {
            var product = products.GetById(id);

            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            products.Update(product);
            products.Commit();
            return RedirectToAction("ProductList");
        }
    }
}