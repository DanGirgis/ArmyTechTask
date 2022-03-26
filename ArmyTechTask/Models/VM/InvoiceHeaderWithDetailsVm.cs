using System;
using System.Linq;
using System.Web;

namespace ArmyTechTask.Models.VM
{
    public class InvoiceHeaderWithDetailsVm
    {
        public InvoiceHeader InvoiceHeader { get; set; }
        public decimal TotalPrice { get; set; }
        public string itemsNames { get; set; }

    }
}