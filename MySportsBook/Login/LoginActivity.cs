using Android.App;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using System;
using Android.Content;
using Android.Views;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Generic;

namespace MySportsBook
{
    [Activity(Label = "My SportsBook", MainLauncher = true,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class LoginActivity : Activity, LoginInterface,LoginLandingInterface
    {

        #region Declaration

        EditText userName;
        EditText password;
        Button btnLoginButton;
        TextView txtName;
        Helper helper = new Helper();
        CommonDetails commonDetails = new CommonDetails();
        private Dialog dialog;
        private LinearLayout linearProgressBar;
        private bool isInternetConnection = false;
        private ProgressDialog pdialog;
        private TextView lblRememberMe;
        CheckBox chkRememberme;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            Context mContext = Android.App.Application.Context;
            StorageHelper oStorageHelper = new StorageHelper(mContext);

            // Get the username/password EditBox, label and button resources:
            userName = FindViewById<EditText>(Resource.Id.txtUsername);
            password = FindViewById<EditText>(Resource.Id.txtPassword);
            btnLoginButton = FindViewById<Button>(Resource.Id.btnLogin);
            txtName = FindViewById<TextView>(Resource.Id.txtName);
            lblRememberMe = FindViewById<TextView>(Resource.Id.lblRememberMe);
            linearProgressBar = FindViewById<LinearLayout>(Resource.Id.linearProgressBar);
            chkRememberme = FindViewById<CheckBox>(Resource.Id.saveLoginCheckBox);

            btnLoginButton = FindViewById<Button>(Resource.Id.btnLogin);

            // setting the action bar region background color
            ActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#E91402")));

            //to hide the default app icon setting false
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayHomeAsUpEnabled(false);
            ActionBar.SetHomeButtonEnabled(false);

            ActionBar.Hide();

            #region[Login]
            //login button will check the credentaial and navigate to home(MyEvents screen)
            //btnLoginButton.Click += async (sender, e) =>
            //{
            //    helper.ProgressDialogShow(this);
            //    await LoginEvent();

            //};

            btnLoginButton.Click += delegate
            {
                LoginEvent();
            };

            #endregion[Login]

            FontStyle();

            bool isRemebered = oStorageHelper.GetBoolFromStorage("rememberme");
            bool isLoggedIn = oStorageHelper.GetBoolFromStorage("isLoggedIn");

            if (isRemebered)
            {
                userName.Text = oStorageHelper.GetStringFromStorage("username");
                password.Text = oStorageHelper.GetStringFromStorage("password");
                chkRememberme.Checked = isRemebered;
            }

            if (isLoggedIn)
            {
                userName.Text = oStorageHelper.GetStringFromStorage("username");
                password.Text = oStorageHelper.GetStringFromStorage("password");
                //linearProgressBar.Visibility = ViewStates.Visible;
                LoginEvent();
            }


        }

        private void LoginEvent()
        {
            isInternetConnection = false;
            Context mContext = Android.App.Application.Context;

            StorageHelper oStorageHelper = new StorageHelper(mContext);

            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    ServiceHelper serviceHelper = new ServiceHelper();
                    Login login = new Login();

                    if (userName.Text != "" && password.Text != "")
                    {
                        AysncTaskClass aysncTaskClass = new AysncTaskClass(userName.Text, password.Text, this,
                            linearProgressBar, "Login");
                        aysncTaskClass.Execute("Login");
                        //login = serviceHelper.GetLogin(userName.Text, password.Text);

                        //if (login != null && login.access_token != null)
                        //{

                        //    oStorageHelper.SaveStringInStorage("username", userName.Text);
                        //    oStorageHelper.SaveStringInStorage("password", password.Text);
                        //    oStorageHelper.SaveBoolInStorage("rememberme", chkRememberme.Checked);
                        //    oStorageHelper.SaveBoolInStorage("isLoggedIn", true);

                        //    commonDetails.access_token = login.access_token;
                        //    commonDetails.ExpireTime = login.expires_in;
                        //    commonDetails.refreshToken = login.refresh_token;

                        //    ISharedPreferences pref = Application.Context.GetSharedPreferences("LoggedUserDetails", FileCreationMode.Private);
                        //    ISharedPreferencesEditor edit = pref.Edit();
                        //    edit.PutString("access_token", login.access_token);
                        //    edit.PutString("refresh_token", login.refresh_token);
                        //    edit.PutString("expires_in", login.expires_in);
                        //    edit.Apply();

                        //    CheckLandingPage(commonDetails);

                        //    //var intent = new Intent(this, typeof(VenueActivity));
                        //    //intent.PutExtra("access_token", login.access_token);
                        //    //intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                        //    //StartActivity(intent);
                        //    //Finish();
                        //    linearProgressBar.Visibility = ViewStates.Gone;
                        //}
                        //else
                        //{
                        //    helper.AlertPopUp("Error", "Unable to login. Please check credential", this);
                        //    linearProgressBar.Visibility = ViewStates.Gone;
                        //}
                    }
                    else
                    {
                        helper.AlertPopUp("Warning", "Please enter the credentail", this);
                        linearProgressBar.Visibility = ViewStates.Gone;
                    }


                }
                catch (Exception ex)
                {
                    helper.AlertPopUp("Error", "Unable to login. Please try again", this);
                    linearProgressBar.Visibility = ViewStates.Gone;
                }
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", this);
                linearProgressBar.Visibility = ViewStates.Gone;
            }


        }

        public void CheckLandingPage(CommonDetails details)
        {
            try
            {
                AysncTaskClass aysncTaskClass = new AysncTaskClass(details, this,
                            linearProgressBar, "LoginCheckLanding");
                aysncTaskClass.Execute("LoginCheckLanding");
            }
            catch (Exception)
            {
                helper.AlertPopUp("Warning", "There are no venue available", this);
            }

            //ServiceHelper serviceHelper;
            //serviceHelper = new ServiceHelper();
            ////Check More than one Venue
            //List<Venue> venueList = new List<Venue>();
            //List<Sport> sportList = new List<Sport>();
            //List<Court> courtList = new List<Court>();
            //List<BatchCountModel> batchList = new List<BatchCountModel>();

            //venueList = serviceHelper.GetVenue(details.access_token);

            //if (venueList != null && venueList.Count > 0)
            //{
            //    if (venueList.Count > 1)
            //    {
            //        var venueIntent = new Intent(this, typeof(VenueActivity));
            //        venueIntent.PutExtra("isLogin", "1");
            //        venueIntent.PutExtra("details", JsonConvert.SerializeObject(details));
            //        StartActivity(venueIntent);
            //        Finish();
            //    }
            //    else
            //    {
            //        serviceHelper = new ServiceHelper();

            //        details.VenueId = venueList[0].VenueId.ToString();
            //        details.VenueCode = venueList[0].VenueCode.ToString();



            //        sportList = serviceHelper.GetSports(details.access_token, details.VenueId);


            //        if (sportList != null && sportList.Count > 0)
            //        {
            //            if (sportList.Count > 1)
            //            {
            //                var sportIntent = new Intent(this, typeof(SportActivity));
            //                sportIntent.PutExtra("details", JsonConvert.SerializeObject(details));
            //                StartActivity(sportIntent);
            //                Finish();
            //            }
            //            else
            //            {
            //                serviceHelper = new ServiceHelper();

            //                details.SportId = sportList[0].SportId.ToString();

            //                courtList = serviceHelper.GetCourt(details.access_token, details.VenueId, details.SportId);
            //                if (courtList != null && courtList.Count > 0)
            //                {
            //                    //if (courtList.Count > 1)
            //                    //{
            //                    //    var courtIntent = new Intent(this, typeof(CourtActivity));
            //                    //    courtIntent.PutExtra("details", JsonConvert.SerializeObject(details));
            //                    //    StartActivity(courtIntent);
            //                    //    Finish();
            //                    //}
            //                    //else
            //                    //{
            //                    //    serviceHelper = new ServiceHelper();

            //                    //    details.CourtId = courtList[0].CourtId.ToString();

            //                    //    batchList = serviceHelper.GetBatch(details.access_token, details.VenueId, details.SportId, details.CourtId);
            //                    details.CourtId = "0";
            //                    var batchesIntent = new Intent(this, typeof(BatchesActivity));
            //                    batchesIntent.PutExtra("details", JsonConvert.SerializeObject(details));
            //                    StartActivity(batchesIntent);
            //                    Finish();
            //                    //}
            //                }
            //                else
            //                {
            //                    helper.AlertPopUp("Warning", "There are no court available", this);
            //                }

            //            }
            //        }
            //        else
            //        {
            //            helper.AlertPopUp("Warning", "There are no sport available", this);

            //        }
            //    }

            //}
            //else
            //{
            //    helper.AlertPopUp("Warning", "There are no venue available", this);
            //}

        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(Intent.ActionMain);
            intent.AddCategory(Intent.CategoryHome);
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        #region[Font Style]

        private void FontStyle()
        {
            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");
            Typeface faceBold = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Bold.otf");


            userName.SetTypeface(faceRegular, TypefaceStyle.Normal);
            userName.TextSize = 15;
            password.TextSize = 15;

            txtName.SetTypeface(face, TypefaceStyle.Bold);

            //setting normal face
            password.SetTypeface(faceRegular, TypefaceStyle.Normal);

            //setting bold face
            btnLoginButton.SetTypeface(faceRegular, TypefaceStyle.Normal);
            lblRememberMe.SetTypeface(faceRegular, TypefaceStyle.Normal);

        }

        public void LoginInterface(Login login)
        {
            LoginResponse(login);
        }

        #endregion[End Font Style]

        public void LoginResponse(Login login)
        {
            Context mContext = Android.App.Application.Context;
            StorageHelper oStorageHelper = new StorageHelper(mContext);
            if (login != null && login.access_token != null)
            {

                oStorageHelper.SaveStringInStorage("username", userName.Text);
                oStorageHelper.SaveStringInStorage("password", password.Text);
                oStorageHelper.SaveBoolInStorage("rememberme", chkRememberme.Checked);
                oStorageHelper.SaveBoolInStorage("isLoggedIn", true);

                commonDetails.access_token = login.access_token;
                commonDetails.ExpireTime = login.expires_in;
                commonDetails.refreshToken = login.refresh_token;

                ISharedPreferences pref =
                    Application.Context.GetSharedPreferences("LoggedUserDetails", FileCreationMode.Private);
                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("access_token", login.access_token);
                edit.PutString("refresh_token", login.refresh_token);
                edit.PutString("expires_in", login.expires_in);
                edit.Apply();

                CheckLandingPage(commonDetails);

                //var intent = new Intent(this, typeof(VenueActivity));
                //intent.PutExtra("access_token", login.access_token);
                //intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                //StartActivity(intent);
                //Finish();
                linearProgressBar.Visibility = ViewStates.Gone;
            }
            else
            {
                helper.AlertPopUp("Error", "Unable to login. Please check credential", this);
                linearProgressBar.Visibility = ViewStates.Gone;
            }
        }

        public void LoginLandingInterface(List<VenueSport> venueSports)
        {
            if (venueSports != null && venueSports.Count > 0)
            {
                if (venueSports.Count == 1)
                {
                    commonDetails.VenueId = venueSports[0].VenueId.ToString();
                    commonDetails.VenueCode = venueSports[0].VenueCode.ToString();
                    if (venueSports.Count == 1)
                    {
                        commonDetails.SportId = "0";//venueSports[0].SportModels[0].SportId.ToString();
                        commonDetails.CourtId = "0";
                        var batchesIntent = new Intent(this, typeof(BatchesActivity));
                        batchesIntent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                        StartActivity(batchesIntent);
                        Finish();
                    }
                    else
                    {
                        var sportIntent = new Intent(this, typeof(SportActivity));
                        sportIntent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                        StartActivity(sportIntent);
                        Finish();
                    }
                }
                else {
                    var venueIntent = new Intent(this, typeof(VenueActivity));
                    venueIntent.PutExtra("isLogin", "1");
                    venueIntent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                    StartActivity(venueIntent);
                    Finish();
                }
            }
            else {
                helper.AlertPopUp("Warning", "There are no venue available", this);
            }
        }
    }

   
}