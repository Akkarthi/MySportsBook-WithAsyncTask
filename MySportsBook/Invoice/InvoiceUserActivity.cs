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
using Newtonsoft.Json;
using Android.Graphics;
using System.Threading;
using System.Threading.Tasks;

namespace MySportsBook
{
    [Activity(Label = "", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.StateHidden)]
    public class InvoiceUserActivity : MenuActivity,InvoiceUserInterface
    {
        ListView invoiceUserListView;
        TextView lblHeader;
        LinearLayout linearProgressBar;
        private CommonDetails commonDetails;
        private string venueCode = string.Empty;
        Helper helper = new Helper();
        List<Player> invoiceModelList = new List<Player>();
        TextView txtSearchInvoiceUser;
        EditText editTextSearchInvoiceUser;
        InvoiceUser_ItemAdapter invoiceUser_ItemAdapter;
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            commonDetails = JsonConvert.DeserializeObject<CommonDetails>(Intent.GetStringExtra("details"));

            invoiceUserListView = FindViewById<ListView>(Resource.Id.lstInvoiceUser);
            lblHeader = FindViewById<TextView>(Resource.Id.lblheader);
            linearProgressBar = FindViewById<LinearLayout>(Resource.Id.linearProgressBar);
            txtSearchInvoiceUser = FindViewById<TextView>(Resource.Id.txtSearchInvoiceUser);
            editTextSearchInvoiceUser = FindViewById<EditText>(Resource.Id.editTextSearchInvoiceUser);
        
            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");

            lblHeader.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtSearchInvoiceUser.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextSearchInvoiceUser.SetTypeface(faceRegular, TypefaceStyle.Normal);


            editTextSearchInvoiceUser.TextChanged += EditTextSearchInvoiceUser_TextChanged;

           

            linearProgressBar.Visibility = ViewStates.Visible;
            new Thread(new ThreadStart(delegate { RunOnUiThread(async () => { await LoadInvoiceUser(commonDetails); }); }))
                .Start();

        }

        private void EditTextSearchInvoiceUser_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<Player> searchInvoiceModelList = new List<Player>();
            searchInvoiceModelList = invoiceModelList.Where(x => x.FirstName.ToLower().Contains(editTextSearchInvoiceUser.Text.ToLower()) || x.Mobile.ToLower().Contains(editTextSearchInvoiceUser.Text.ToLower())).ToList();
            invoiceUser_ItemAdapter =
                new InvoiceUser_ItemAdapter(this, searchInvoiceModelList, linearProgressBar, commonDetails);

            invoiceUserListView.Adapter = invoiceUser_ItemAdapter;
        }

        private async Task LoadInvoiceUser(CommonDetails details)
        {
            ServiceHelper serviceHelper = new ServiceHelper();
            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    LoadEnquiryList(commonDetails);
                    //linearProgressBar.Visibility = Android.Views.ViewStates.Visible;
                    //new Thread(new ThreadStart(delegate
                    //{
                    //    RunOnUiThread(async () => { await LoadEnquiryList(commonDetails); linearProgressBar.Visibility = Android.Views.ViewStates.Gone; });
                    //})).Start();

                }
                catch (Exception e)
                {
                    helper.AlertPopUp("Error", "Unable to retrieve data from the server", this);
                    linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                }
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", this);
                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
            }




        }

        public override int GetResourceLayout()
        {
            return Resource.Layout.InvoiceUser;
        }

        public override string GetSessionId()
        {
            return null;
        }

        public override string GetUserId()
        {
            return null;
        }

        public override CommonDetails GetDetails()
        {
            return JsonConvert.DeserializeObject<CommonDetails>(Intent.GetStringExtra("details"));
        }

        public override void OnBackPressed()
        {
            if (helper.CheckInternetConnection(this))
            {
                Intent intent = new Intent(this, typeof(VenueActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                StartActivity(intent);
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", this);
            }
        }

        private async Task LoadEnquiryList(CommonDetails details)
        {
            ServiceHelper serviceHelper = new ServiceHelper();
            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    AysncTaskClass aysncTaskClass = new AysncTaskClass(commonDetails, this, linearProgressBar, "InvoiceUser");
                    aysncTaskClass.Execute("InvoiceUser");

                    //invoiceModelList = serviceHelper.GetInvoiceUser(details.access_token,details.VenueId,details.SportId);

                    //linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                }
                catch (Exception e)
                {
                    helper.AlertPopUp("Error", "Unable to retrieve data from the server", this);
                    linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                }
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", this);
                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
            }
        }

        public void InvoiceUserInterface(List<Player> invoiceUserList)
        {
            try
            {
                invoiceModelList = invoiceUserList;
            }
            catch (Exception)
            {
                helper.AlertPopUp("Error", "Unable to retrieve data from the server", this);
                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
            }
        }
    }
}