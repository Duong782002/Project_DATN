using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK.Core.Business.Model
{
    public class WishListDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal RetailPrice { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
    }
}
