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
    public class BookingActivity : MenuActivity
    {
        private CommonDetails commonDetails;
        private Button btnSubmit;
        TextView lblHeader;
        LinearLayout linearProgressBar;
        Helper helper = new Helper();
        private EditText editTextBookingMessage;
        private TextView txtBookingMessage;
        private Button btnPaste;

        public override CommonDetails GetDetails()
        {
            return commonDetails;
        }

        public override int GetResourceLayout()
        {
            return Resource.Layout.AddBooking;
        }

        public override string GetSessionId()
        {
            return null;
        }

        public override string GetUserId()
        {
            return null;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            commonDetails = JsonConvert.DeserializeObject<CommonDetails>(Intent.GetStringExtra("details"));

            lblHeader = FindViewById<TextView>(Resource.Id.lblheader);
            linearProgressBar = FindViewById<LinearLayout>(Resource.Id.linearProgressBar);
            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            btnPaste = FindViewById<Button>(Resource.Id.btnPaste);
            editTextBookingMessage = FindViewById<EditText>(Resource.Id.editTextBookingMessage);

            txtBookingMessage = FindViewById<TextView>(Resource.Id.txtBookingHeading);

            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");

            lblHeader.SetTypeface(faceRegular, TypefaceStyle.Bold);
            btnSubmit.SetTypeface(faceRegular, TypefaceStyle.Normal);

            editTextBookingMessage.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtBookingMessage.SetTypeface(faceRegular, TypefaceStyle.Normal);

            btnSubmit.SetAllCaps(false);

            btnSubmit.Click += btnSubmit_Click;
            btnPaste.Click += btnPaste_Click;
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                ClipboardManager clipboardManager;
                clipboardManager = (ClipboardManager)GetSystemService(ClipboardService);

                ClipData clipData = clipboardManager.PrimaryClip;
                ClipData.Item item = clipData.GetItemAt(0);
                editTextBookingMessage.Text = item.Text.ToString();
            }
            catch (Exception exception)
            {
                editTextBookingMessage.Text = "";
            }
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(editTextBookingMessage.Text))
            {
                linearProgressBar.Visibility = Android.Views.ViewStates.Visible;
                if (helper.CheckInternetConnection(this))
                {
                    ServiceHelper serviceHelper = new ServiceHelper();
                    try
                    {
                        new Thread(new ThreadStart(delegate
                        {
                            RunOnUiThread(async () =>
                            {
                                await AddBookingMessage(commonDetails.access_token, editTextBookingMessage.Text,commonDetails.VenueId);
                                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                            });
                        })).Start();
                    }
                    catch (Exception ex)
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
            else
            {
                helper.AlertPopUp("Error", "Please Enter Message", this);
                linearProgressBar.Visibility = ViewStates.Gone;
            }


        }

        public async Task AddBookingMessage(string token, string bookingMessage,string venueId)
        {
            bool result = false;
            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    ServiceHelper serviceHelper = new ServiceHelper();
                    result = serviceHelper.AddBookingMessage(token, bookingMessage, venueId);
                    linearProgressBar.Visibility = Android.Views.ViewStates.Gone;

                    if (result)
                    {
                        helper.AlertPopUp("Message", "Data has been updated successfully", this);

                        editTextBookingMessage.Text = string.Empty;
                    }

                    else
                    {
                        helper.AlertPopUp("Error", "Unable to submit the data to server", this);
                    }
                }
                catch (Exception ex)
                {
                    helper.AlertPopUp("Error", "Unable to submit the data to server", this);
                    linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                }
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", this);

                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
            }
        }

    }
}