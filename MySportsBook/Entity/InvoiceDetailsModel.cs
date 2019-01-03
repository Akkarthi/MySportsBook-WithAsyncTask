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
    public class InvoiceDetailsModel
    {
        public string InvoicePeriod { get; set; }
        public double Fee { get; set; }
        public int BatchId { get; set; }
        public object BatchCode { get; set; }
        public object BatchName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayer { get; set; }
        public bool IsAttendanceRequired { get; set; }
        public int CourtId { get; set; }
        public object CourtCode { get; set; }
        public object CourtName { get; set; }
        public int SportId { get; set; }
        public object SportCode { get; set; }
        public string SportName { get; set; }
        public int VenueId { get; set; }
        public object VenueCode { get; set; }
        public object VenueName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public object ModifiedBy { get; set; }
        public object ModifiedDate { get; set; }
    }
}