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
    public class EnquiryModel
    {
        public string Name { get; set; }

        public string Mobile { get; set; }
        public string Game { get; set; }
        public string Comment { get; set; }

        public int VenueId { get; set; }

        public int EnquiryId { get; set; }

        public string Slot { get; set; }

        public List<string> Comments { get; set; }


    }
}