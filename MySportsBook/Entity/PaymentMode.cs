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
    public class PaymentMode
    {
        public int PayementId { get; set; }
        public string PaymentCode { get; set; }
        public string PaymentName { get; set; }
        public int VenueId { get; set; }
        public object VenueCode { get; set; }
        public object VenueName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
       
    }
}