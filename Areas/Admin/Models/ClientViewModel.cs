using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPortal.Domain.Entities;

namespace FBPortal.WebUI.Models
{
    public class ClientViewModel
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Balance { get; set; }

        public IEnumerable<Invoice> Invoices { get; set; }
    }
}