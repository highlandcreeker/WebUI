using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FBPortal.Domain.Entities;

namespace FBPortal.WebUI.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string ReturnUrl { get; set; }

    }
}