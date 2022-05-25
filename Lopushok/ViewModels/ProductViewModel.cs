using Lopushok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lopushok.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public bool Checked { get; set; }
        public Visibility ThumbVisibility { get; }
        public string Image { get; }
        public string ProductType { get; }
        public string ProductName { get; }
        public int Article { get; }
        public string Materials { get; }
        public decimal Cost { get; }

        public ProductViewModel(Product product,
                                Visibility thumbVisibility,
                                string image,
                                string productType,
                                string productName,
                                int article,
                                string materials,
                                decimal cost)
        {
            Product = product;
            ThumbVisibility = thumbVisibility;
            Image = image;
            ProductType = productType;
            ProductName = productName;
            Article = article;
            Materials = materials;
            Cost = cost;
        }
    }
}
