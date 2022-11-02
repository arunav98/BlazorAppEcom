using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppEcom.Shared
{
    public class ProductSearchResultDTO
    {
        public List<Product> Products { get; set; } =new List<Product>();
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
