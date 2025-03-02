﻿namespace MyAspNetCoreApp3.Web.Models {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string? Color { get; set; }
        public int? Height { get; set; }
        //public int? Width { get; set; }
        public string? Barcode { get; set; }

        public bool IsPublish { get; set; }

        public int Expire { get; set; }

        public DateTime? PublishDate { get; set; }

    }
}
