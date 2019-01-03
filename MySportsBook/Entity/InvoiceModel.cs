using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MySportsBook
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public int VenueId { get; set; }
        public int PlayerId { get; set; }
        public int PaymentId { get; set; }
        public double TotalFee { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalOtherAmount { get; set; }
        public double TotalPaidAmount { get; set; }
        public string Comments { get; set; }
        public List<InvoiceDetailsModel> InvoiceDetails { get; set; }
    }
}