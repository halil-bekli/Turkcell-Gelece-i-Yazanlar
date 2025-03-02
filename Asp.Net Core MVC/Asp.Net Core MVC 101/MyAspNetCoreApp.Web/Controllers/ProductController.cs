﻿using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers {
    public class ProductController : Controller {

        private readonly ProductRepository _productRepository;

        public ProductController() {
            _productRepository = new ProductRepository();

            if (!_productRepository.GetProducts().Any()) {

            }
        }

        public IActionResult Index() {
            var products = _productRepository.GetProducts();

            return View(products);
        }

        public IActionResult Remove(int id) {

            _productRepository.RemoveProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult Add() {

            return View();
        }

        public IActionResult Update(int id) {
            return View();
        }
    }
}
