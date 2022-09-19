using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppEcom.Shared
{
    public class Category
    {
        public string CategoryName { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public int CategoryId { get; set; }
    }
}
