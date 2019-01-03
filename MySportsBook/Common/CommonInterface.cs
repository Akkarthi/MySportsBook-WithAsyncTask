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
    public interface LoginInterface
    {
        void LoginInterface(Login login);
    }

    public interface VenueInterface
    {
        void VenueInterface(List<Venue> venue);
    }

    public interface VenueSportInterface
    {
        void VenueSportInterface(List<Sport> sport);
    }

    public interface SportInterface
    {
        void SportInterface(List<Sport> sportList);
    }

    public interface BatchInterface
    {
        void BatchInterface(List<BatchCountModel> batchList);
    }

    public interface BatchPlayerInterface
    {
        void BatchPlayerInterface(List<Player> batchPlayerList);
    }

    public interface AttendanceAddPlayerAsyncInterface
    {
        void AttendanceAddPlayerAsyncInterface(List<Player> attendanceAddPlayerList);
    }

    public interface AttendanceSaveInterface
    {
        void AttendanceSaveInterface(bool resultResponse);
    }

    public interface InvoiceUserInterface
    {
        void InvoiceUserInterface(List<Player> invoiceUserList);
    }

    public interface EnquiryListInterface
    {
        void EnquiryListInterface(List<EnquiryModel> enquiryList);
    }

    public interface EnquirySaveInterface
    {
        void EnquirySaveInterface(bool responseResult);
    }

    public interface InvoiceUserByPlayerInterface
    {
        void InvoiceUserByPlayerInterface(List<InvoiceDetailsModel> invoiceDetailsList);
    }

    public interface InvoiceSaveInterface
    {
        void InvoiceSaveInterface(bool responseResult);
    }

}