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
    public class EnquiryUserActivity : MenuActivity,EnquiryListInterface
    {
        ListView enquiryUserListView;
        TextView lblHeader;
        LinearLayout linearProgressBar;
        private CommonDetails commonDetails;
        private string venueCode = string.Empty;
        Helper helper = new Helper();
        List<EnquiryModel> enquiryModelList = new List<EnquiryModel>();
        TextView txtSearchEnquiryUser;
        EditText editTextSearchEnquiryUser;
        EnquiryUser_ItemAdapter enquiryUser_ItemAdapter;
        private Button btnNewEnquiry;
        private ImageButton imgSearch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            commonDetails = JsonConvert.DeserializeObject<CommonDetails>(Intent.GetStringExtra("details"));

            enquiryUserListView = FindViewById<ListView>(Resource.Id.lstEnquiryUser);
            lblHeader = FindViewById<TextView>(Resource.Id.lblheader);
            linearProgressBar = FindViewById<LinearLayout>(Resource.Id.linearProgressBar);
            txtSearchEnquiryUser  = FindViewById<TextView>(Resource.Id.txtSearchEnquiryUser);
            editTextSearchEnquiryUser = FindViewById<EditText>(Resource.Id.editTextSearchEnquiryUser);
            btnNewEnquiry = FindViewById<Button>(Resource.Id.btnNewEnquiry);
            imgSearch = FindViewById<ImageButton>(Resource.Id.imgSearch);
            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");

            lblHeader.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtSearchEnquiryUser.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextSearchEnquiryUser.SetTypeface(faceRegular, TypefaceStyle.Normal);
            btnNewEnquiry.SetTypeface(faceRegular, TypefaceStyle.Normal);

            btnNewEnquiry.SetAllCaps(false);

            editTextSearchEnquiryUser.TextChanged += EditTextSearchEnquiryUser_TextChanged;
            imgSearch.Click += ImgSearch_Click;

            btnNewEnquiry.Click += BtnNewEnquiry_Click;

            LoadEnquiryUser(commonDetails);
        }

        private void ImgSearch_Click(object sender, EventArgs e)
        {
            imgSearch.Visibility = ViewStates.Gone;
            txtSearchEnquiryUser.Visibility = ViewStates.Visible;
            editTextSearchEnquiryUser.Visibility = ViewStates.Visible;
        }

        private void BtnNewEnquiry_Click(object sender, EventArgs e)
        {
            commonDetails.isAttendance = false;
            Intent intent = new Intent(this, typeof(EnquiryFormActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            intent.PutExtra("isNewEnquiry", "0");
            intent.PutExtra("enquiryDetail", "");
            //close all the other intent
            StartActivity(intent);
        }

        private void EditTextSearchEnquiryUser_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<EnquiryModel> searchEnquiryModelList = new List<EnquiryModel>();
            searchEnquiryModelList = enquiryModelList.Where(x => x.Name.ToLower().Contains(editTextSearchEnquiryUser.Text.ToLower()) || x.Mobile.ToLower().Contains(editTextSearchEnquiryUser.Text.ToLower())).ToList();
            enquiryUser_ItemAdapter =
                                   new EnquiryUser_ItemAdapter(this, searchEnquiryModelList, linearProgressBar, commonDetails);

            enquiryUserListView.Adapter = enquiryUser_ItemAdapter;

        }

        private void LoadEnquiryUser(CommonDetails details)
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
            return Resource.Layout.EnquiryUser;
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

        private void LoadEnquiryList(CommonDetails details)
        {
            ServiceHelper serviceHelper = new ServiceHelper();
            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    AysncTaskClass aysncTaskClass = new AysncTaskClass(commonDetails, this, linearProgressBar, "EnquiryUserList");
                    aysncTaskClass.Execute("EnquiryUserList");

                    //enquiryModelList = serviceHelper.GetEnquiry(details.access_token);

                    //if (enquiryModelList != null && enquiryModelList.Count > 0)
                    //{
                    //    enquiryUserListView.SetAdapter(new EnquiryUser_ItemAdapter(this, enquiryModelList, linearProgressBar, details));

                    //    enquiryUser_ItemAdapter =
                    //    new EnquiryUser_ItemAdapter(this, enquiryModelList, linearProgressBar, details);

                    //    enquiryUserListView.Adapter = enquiryUser_ItemAdapter;

                    //}
                    //else
                    //{
                    //    helper.AlertPopUp("Warning", "There is no data available", this);
                    //}

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

        public void EnquiryListInterface(List<EnquiryModel> enquiryList)
        {
            enquiryModelList = enquiryList;
            if (enquiryModelList != null && enquiryModelList.Count > 0)
            {
                enquiryUserListView.SetAdapter(new EnquiryUser_ItemAdapter(this, enquiryModelList, linearProgressBar, commonDetails));

                enquiryUser_ItemAdapter =
                new EnquiryUser_ItemAdapter(this, enquiryModelList, linearProgressBar, commonDetails);

                enquiryUserListView.Adapter = enquiryUser_ItemAdapter;

            }
            else
            {
                helper.AlertPopUp("Warning", "There is no data available", this);
            }
        }
    }
}