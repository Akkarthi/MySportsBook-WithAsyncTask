using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using Newtonsoft.Json;

namespace MySportsBook
{
    public class AysncTaskClass : AsyncTask<string, string, string>
    {
        private string text1;
        private string text2;
        ServiceHelper serviceHelper = new ServiceHelper();
        private string response;
        private LoginInterface loginInterface;
        private VenueInterface venueInterface;
        private SportInterface sportInterface;
        private BatchInterface batchInterface;
        private BatchPlayerInterface batchPlayerInterface;
        private AttendanceAddPlayerAsyncInterface attendanceAddPlayerAsyncInterface;
        private InvoiceUserInterface invoiceUserInterface;
        private EnquiryListInterface enquiryListInterface;
        private InvoiceUserByPlayerInterface invoiceUserByPlayerInterface;
        private EnquirySaveInterface enquirySaveInterface;
        private AttendanceSaveInterface attendanceSaveInterface;
        private InvoiceSaveInterface invoiceSaveInterface;
        private LoginLandingInterface loginLandingInterface;
        private Activity context;
        private LinearLayout linearProgress;
        private string page = string.Empty;
        private CommonDetails details;
        private string batchplayerSelectedDate = string.Empty;
        private Player invoiceSelectedPlayer=new Player();
        private EnquiryModel enquiryModel = new EnquiryModel();
        private List<Attendance> attendances = new List<Attendance>();
        private InvoiceModel invoiceModel = new InvoiceModel();
        private List<VenueSport> venueSports = new List<VenueSport>();

        public AysncTaskClass(string text1, string text2, LoginActivity loginActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.text1 = text1;
            this.text2 = text2;
            context = loginActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, LoginActivity loginActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.details = commonDetails;
            context = loginActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, VenueActivity venueActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.details = commonDetails;
            context = venueActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, SportActivity sportActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.details = commonDetails;
            context = sportActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, BatchesActivity batchesActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.details = commonDetails;
            context = batchesActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, BatchPlayer batchPlayerActivity, LinearLayout linearProgressBar, string pageName,string selectedDate)
        {
            this.details = commonDetails;
            context = batchPlayerActivity;
            linearProgress = linearProgressBar;
            page = pageName;
            batchplayerSelectedDate = selectedDate;
        }

        public AysncTaskClass(CommonDetails commonDetails, AttendanceAddPlayerActivity attendanceAddPlayerActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.details = commonDetails;
            context = attendanceAddPlayerActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, InvoiceUserActivity invoiceUserActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.details = commonDetails;
            context = invoiceUserActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, EnquiryUserActivity enquiryUserActivity, LinearLayout linearProgressBar, string pageName)
        {
            this.details = commonDetails;
            context = enquiryUserActivity;
            linearProgress = linearProgressBar;
            page = pageName;
        }

        public AysncTaskClass(CommonDetails commonDetails, InvoiceCollectionFormActivity invoiceCollectionFormActivity, LinearLayout linearProgressBar, string pageName,Player selectedPlayer)
        {
            this.details = commonDetails;
            context = invoiceCollectionFormActivity;
            linearProgress = linearProgressBar;
            page = pageName;
            invoiceSelectedPlayer = selectedPlayer;
        }

        public AysncTaskClass(CommonDetails commonDetails, InvoiceCollectionFormActivity invoiceCollectionFormActivity, LinearLayout linearProgressBar, string pageName, InvoiceModel invoiceModelData,Player selectedPlayer)
        {
            this.details = commonDetails;
            context = invoiceCollectionFormActivity;
            linearProgress = linearProgressBar;
            page = pageName;
            invoiceModel = invoiceModelData;
            invoiceSelectedPlayer = selectedPlayer;
        }

        public AysncTaskClass(CommonDetails commonDetails, EnquiryFormActivity enquiryFormActivity, LinearLayout linearProgressBar, string pageName, EnquiryModel enquiryModelData)
        {
            this.details = commonDetails;
            context = enquiryFormActivity;
            linearProgress = linearProgressBar;
            page = pageName;
            enquiryModel = enquiryModelData;
        }

        public AysncTaskClass(CommonDetails commonDetails, BatchPlayer batchPlayerActivity, LinearLayout linearProgressBar, string pageName, List<Attendance> attendancesList)
        {
            this.details = commonDetails;
            context = batchPlayerActivity;
            linearProgress = linearProgressBar;
            page = pageName;
            attendances = attendancesList;
        }

        protected override void OnPreExecute()
        {
            //base.OnPreExecute();
            linearProgress.Visibility = ViewStates.Visible;
        }

        protected override string RunInBackground(params string[] @params)
        {
            if (@params[0] == "Login")
            {
                response = serviceHelper.GetLoginResponse(text1, text2);
            }
            if (@params[0] == "LoginCheckLanding")
            {
                response = serviceHelper.GetLoginLandingResponse(details.access_token);
            }
            else if (@params[0] == "Venue")
            {
                response = serviceHelper.GetVenueResponse(details.access_token);
            }
            else if (@params[0] == "Sport")
            {
                response = serviceHelper.GetSportsResponse(details.access_token, details.VenueId);
            }
            else if (@params[0] == "Batches")
            {
                response = serviceHelper.GetBatchResponse(details.access_token, details.VenueId, details.SportId, "0");
            }
            else if (@params[0] == "BatchPlayer")
            {
                if (details.isAttendance)
                {
                    response = serviceHelper.GetPlayerForAttendanceResponse(details.access_token, details.VenueId, details.SportId,
                        details.CourtId, details.BatchId, "0", batchplayerSelectedDate);
                }
                else
                {
                    response = serviceHelper.GetPlayerResponse(details.access_token, details.VenueId, details.SportId,
                       details.CourtId, details.BatchId);
                }
            }
            else if (@params[0] == "AttendanceAddPlayer")
            {
                response = serviceHelper.GetPlayerForAddingToAttendanceResponse(details.access_token, details.VenueId, details.SportId);
            }
            else if (@params[0] == "AttendanceSave")
            {
                response = serviceHelper.AttendanceSubmitResponse(details.access_token, attendances);
            }
            else if (@params[0] == "InvoiceUser")
            {
                response = serviceHelper.GetInvoiceUserResponse(details.access_token, details.VenueId, details.SportId);
            }
            else if (@params[0] == "EnquiryUserList")
            {
                response = serviceHelper.GetEnquiryResponse(details.access_token);
            }
            else if (@params[0] == "EnquirySave")
            {
                response = serviceHelper.AddEnquiryResponse(details.access_token, enquiryModel);
            }
            else if (@params[0] == "InvoiceUserByPlayer")
            {
                response = serviceHelper.GetInvoiceUserDetailsByPlayerIdResponse(details.access_token, details.VenueId, invoiceSelectedPlayer);
            }
            else if (@params[0] == "InvoiceSave")
            {
                response = serviceHelper.SaveInvoiceUserDetailsByPlayerIdResponse(details.access_token, details.VenueId, invoiceSelectedPlayer, invoiceModel);
            }
            return response;
        }

        protected override void OnPostExecute(Java.Lang.Object result)
        {
            if (page == "Login")
            {                
                Login login = new Login();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    try
                    {
                        login = JsonConvert.DeserializeObject<Login>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        login = null;
                    }
                }
                else {
                    login = null;
                }
                loginInterface = (LoginActivity)context;
                loginInterface.LoginInterface(login);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "LoginCheckLanding")
            {
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    try
                    {
                        venueSports = JsonConvert.DeserializeObject<List<VenueSport>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        venueSports = null;
                    }
                }
                else
                {
                    venueSports = null;
                }
                loginLandingInterface = (LoginActivity)context;
                loginLandingInterface.LoginLandingInterface(venueSports);
                linearProgress.Visibility = ViewStates.Gone;
            }            
            else if (page == "Venue")
            {
                List<Venue> venueList = new List<Venue>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    try
                    {
                        venueList = JsonConvert.DeserializeObject<List<Venue>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        venueList = null;
                    }                    
                }
                else
                {
                    venueList = null;
                }
                venueInterface = (VenueActivity)context;
                venueInterface.VenueInterface(venueList);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "Sport")
            {
                List<Sport> sportList = new List<Sport>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))                    
                {
                    try
                    {
                        sportList = JsonConvert.DeserializeObject<List<Sport>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        sportList = null;
                    }
                }
                else
                {
                    sportList = null;
                }
                sportInterface = (SportActivity)context;
                sportInterface.SportInterface(sportList);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "Batches")
            {
                List<BatchCountModel> batchList = new List<BatchCountModel>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    try
                    {
                        batchList = JsonConvert.DeserializeObject<List<BatchCountModel>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        batchList = null;
                    }
                }
                else
                {
                    batchList = null;
                }
                batchInterface = (BatchesActivity)context;
                batchInterface.BatchInterface(batchList);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "BatchPlayer")
            {
                List<Player> batchPlayerList = new List<Player>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))                    
                {
                    try
                    {
                        batchPlayerList = JsonConvert.DeserializeObject<List<Player>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        batchPlayerList = null;
                    }
                }
                else
                {
                    batchPlayerList = null;
                }
                batchPlayerInterface = (BatchPlayer)context;
                batchPlayerInterface.BatchPlayerInterface(batchPlayerList);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "AttendanceAddPlayer")
            {
                List<Player> attendancePlayerList = new List<Player>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))                    
                {
                    try
                    {
                        attendancePlayerList = JsonConvert.DeserializeObject<List<Player>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        attendancePlayerList = null;
                    }
                }
                else
                {
                    attendancePlayerList = null;
                }
                attendanceAddPlayerAsyncInterface = (AttendanceAddPlayerActivity)context;
                attendanceAddPlayerAsyncInterface.AttendanceAddPlayerAsyncInterface(attendancePlayerList);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "AttendanceSave")
            {
                bool resultResponse = false;
                if (result.ToString() == "true")
                    resultResponse = true;
                else
                    resultResponse = false;
                attendanceSaveInterface = (BatchPlayer)context;
                attendanceSaveInterface.AttendanceSaveInterface(resultResponse);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "InvoiceUser")
            {
                List<Player> invoiceUserList = new List<Player>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    try
                    {
                        invoiceUserList = JsonConvert.DeserializeObject<List<Player>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        invoiceUserList = null;
                    }
                }
                else
                {
                    invoiceUserList = null;
                }
                invoiceUserInterface = (InvoiceUserActivity)context;
                invoiceUserInterface.InvoiceUserInterface(invoiceUserList);
                linearProgress.Visibility = ViewStates.Gone;
            }
            else if (page == "EnquiryUserList")
            {
                List<EnquiryModel> enquiryModelsList = new List<EnquiryModel>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    try
                    {
                        enquiryModelsList = JsonConvert.DeserializeObject<List<EnquiryModel>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        enquiryModelsList = null;
                    }
                }
                else
                {
                    enquiryModelsList = null;
                }
                enquiryListInterface = (EnquiryUserActivity)context;
                enquiryListInterface.EnquiryListInterface(enquiryModelsList);
                linearProgress.Visibility = ViewStates.Gone;
            }

            else if (page == "EnquirySave")
            {
                bool resultResponse = false;
                if (result.ToString() == "true")
                    resultResponse = true;
                else
                    resultResponse = false;
                enquirySaveInterface = (EnquiryFormActivity)context;
                enquirySaveInterface.EnquirySaveInterface(resultResponse);
                linearProgress.Visibility = ViewStates.Gone;
            }

            else if (page == "InvoiceUserByPlayer")
            {
                List<InvoiceDetailsModel> invoiceDetailsModelsList = new List<InvoiceDetailsModel>();
                if (result != null && !string.IsNullOrEmpty(result.ToString()))                    
                {
                    try
                    {
                        invoiceDetailsModelsList = JsonConvert.DeserializeObject<List<InvoiceDetailsModel>>(result.ToString());
                    }
                    catch (System.Exception)
                    {
                        invoiceDetailsModelsList = null;
                    }
                }
                else
                {
                    invoiceDetailsModelsList = null;
                }
                invoiceUserByPlayerInterface = (InvoiceCollectionFormActivity)context;
                invoiceUserByPlayerInterface.InvoiceUserByPlayerInterface(invoiceDetailsModelsList);
                linearProgress.Visibility = ViewStates.Gone;
            }

            else if (page == "InvoiceSave")
            {
                bool resultResponse = false;
                if (result.ToString() == "true")
                    resultResponse = true;
                else
                    resultResponse = false;
                invoiceSaveInterface = (InvoiceCollectionFormActivity)context;
                invoiceSaveInterface.InvoiceSaveInterface(resultResponse);
                linearProgress.Visibility = ViewStates.Gone;
            }
        }
    }
}