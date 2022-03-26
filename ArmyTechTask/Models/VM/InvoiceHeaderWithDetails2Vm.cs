using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArmyTechTask.Models.VM
{
    public class InvoiceHeaderWithDetails2Vm
    {
        public InvoiceHeader Invoiceheader { get; set; }
        public List<InvoiceDetails> ItemsDetails { get; set; }
        public decimal TotalPrice { get; set; }
       

        public InvoiceHeaderWithDetails2Vm()
        {
            this.ItemsDetails = new List<InvoiceDetails>();
        }

    }
}