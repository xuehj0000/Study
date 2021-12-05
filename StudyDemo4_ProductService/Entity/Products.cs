using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyDemo4_ProductService
{
    public class Products
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public static List<Products> GetList()
        {
            var list = new List<Products>() {
                new Products(){Id=1, ProductName="产品一", Price=1888, Image=""},
                new Products(){Id=2, ProductName="产品二", Price=2888, Image=""},
                new Products(){Id=3, ProductName="产品三", Price=3888, Image=""},
                new Products(){Id=4, ProductName="产品四", Price=4888, Image=""},
                new Products(){Id=5, ProductName="产品五", Price=5888, Image=""},
                new Products(){Id=6, ProductName="产品六", Price=5888, Image=""},
            };
            return list;
        }
    }
}
