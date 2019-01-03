﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Android.Graphics.Drawables;
using Android.Graphics;
using Newtonsoft.Json;

namespace MySportsBook
{
    [Activity(Label = "")]
    public abstract class MenuActivity : Activity
    {
        #region "Variable declarations & Instantiations"

        private DrawerLayout mDrawerLayout;
        private ActionBarDrawerToggle mDrawerToggle;
        private RelativeLayout mLeftDrawer;
        private RelativeLayout closeButton;
        private TextView txtLogOut;
        private TextView txtBatchAvailability;
        private ImageButton imgVenueLocation;
        private TextView txtActionBarVenueCode;
        private TextView txtActionBarAppName;
        private RelativeLayout rrLeftMenuAttendanceContainer;
        private TextView txtAttendanceMenu;
        CommonDetails commonDetails=new CommonDetails();
        Helper helper=new Helper();
        private RelativeLayout rrLeftMenuBatchContainer;
        private TextView txtBatchMenu;
        private RelativeLayout rrLeftMenuEnquiryContainer;
        private TextView txtEnquiryMenu;
        private RelativeLayout rrLeftMenuEnquiryUserContainer;
        private TextView txtEnquiryUserMenu;
        private RelativeLayout rrLeftMenuBookingMessageContainer;
        private TextView txtBookingMenu;
        private RelativeLayout rrLeftMenuInvoiceContainer;
        private TextView txtInvoiceCollection;
        #endregion

        #region "Abstract Methods"

        public abstract int GetResourceLayout();

        public abstract string GetSessionId();

        public abstract string GetUserId();

        public abstract CommonDetails GetDetails();

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // this.SetTheme(Resource.Style.MyTheme);

            SetContentView(GetResourceLayout());
            
            //Leftmenu
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myMenuDrawer);
            mLeftDrawer = FindViewById<RelativeLayout>(Resource.Id.left_fragment_container);
            

            //passing image and string to bind slidder menu
            mDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout, false, Resource.Drawable.menu, Resource.String.Open_Drawer, Resource.String.Close_Drawer);

            mDrawerLayout.SetDrawerListener(mDrawerToggle);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetHomeAsUpIndicator(Resources.GetDrawable(Resource.Drawable.menu));
            ActionBar.SetDisplayShowHomeEnabled(true);

            ////Hide the Application icon
            ActionBar.SetIcon(new Android.Graphics.Drawables.ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));

            ////settingtopbar icon 
            ActionBar.SetBackgroundDrawable(new ColorDrawable(Resources.GetColor(Resource.Color.actionbarbackground)));

            ActionBar.LayoutParams navBarParams = new ActionBar.LayoutParams(
                                                         ActionBar.LayoutParams.WrapContent,
                                                         ActionBar.LayoutParams.WrapContent,
                                                         GravityFlags.Center);

            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;

            View customView = inflater.Inflate(Resource.Layout.CustomActionBar, null);

            imgVenueLocation = customView.FindViewById<ImageButton>(Resource.Id.imgActionBarLocation);
            txtActionBarVenueCode= customView.FindViewById<TextView>(Resource.Id.txtActionBarVenueCode);
            txtActionBarAppName= customView.FindViewById<TextView>(Resource.Id.txtActionBarAppName);
            ActionBar.SetCustomView(customView, navBarParams);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("VenueCodeDetail", FileCreationMode.Private);
            //pref.GetString("venueCode", string.Empty);

            txtActionBarVenueCode.Text = pref.GetString("venueCode", string.Empty); 

            ActionBar.SetDisplayShowCustomEnabled(true);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window window = this.Window;

                // clear FLAG_TRANSLUCENT_STATUS flag:
                window.ClearFlags(WindowManagerFlags.TranslucentStatus);

                // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
                window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

                Window.SetStatusBarColor(Android.Graphics.Color.Black);
            }

            //bringing mLeftDrawer to front
            mLeftDrawer.BringToFront();

            imgVenueLocation.Click += delegate
            {
                LoadVenue();
            };



            //Side menu click events for navigation
            BindSideMenuClickEvents(GetSessionId());

            FontStyle();
        }

        #region "Drawer methods"

        #region Font Style
        public void FontStyle()
        {
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");
            Typeface faceBold = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Bold.otf");

            txtBatchAvailability.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtLogOut.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtActionBarVenueCode.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtActionBarAppName.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtAttendanceMenu.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtBatchMenu.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtEnquiryMenu.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtEnquiryUserMenu.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtBookingMenu.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceCollection.SetTypeface(faceRegular, TypefaceStyle.Normal);

        }
        #endregion


        public void SetDrawerEnabled(bool enabled)
        {
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myMenuDrawer);

            int lockMode = enabled ? DrawerLayout.LockModeUnlocked :
                                     DrawerLayout.LockModeLockedClosed;

            mDrawerLayout.SetDrawerLockMode(lockMode);
        }

        //for slidding menu
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        //for slidding menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (mDrawerToggle.OnOptionsItemSelected(item))
                return true;
            return
                base.OnOptionsItemSelected(item);
        }

        #endregion  

        #region[Side Menu ClickEvents]
        /// <summary>
        /// Bind the side menu click events
        /// </summary>
        /// <param name="sessionId"></param>
        private void BindSideMenuClickEvents(string sessionId)
        {
            txtBatchAvailability = (TextView)FindViewById(Resource.Id.txtBatchAvailability);
            txtBatchAvailability.Click += delegate
            {
                BatchAvailability();
            };




            txtLogOut = (TextView)FindViewById(Resource.Id.txtLogOut);
            txtLogOut.Click += delegate
            {
                LogOut();
            };

            rrLeftMenuAttendanceContainer = (RelativeLayout) FindViewById(Resource.Id.rrLeftMenuAttendanceContainer);
            txtAttendanceMenu = (TextView)FindViewById(Resource.Id.txtAttendanceMenu);
            rrLeftMenuAttendanceContainer.Click += delegate { Attendance(); };

            rrLeftMenuBatchContainer = (RelativeLayout)FindViewById(Resource.Id.rrLeftMenuBatchContainer);
            txtBatchMenu= (TextView)FindViewById(Resource.Id.txtBatchMenu);
            rrLeftMenuBatchContainer.Click += delegate { LoadBatches(); };

            rrLeftMenuEnquiryContainer = (RelativeLayout)FindViewById(Resource.Id.rrLeftMenuEnquiryContainer);
            txtEnquiryMenu = (TextView)FindViewById(Resource.Id.txtEnquiryMenu);
            rrLeftMenuEnquiryContainer.Click += delegate { LoadEnquiry(); };

            rrLeftMenuEnquiryUserContainer = (RelativeLayout)FindViewById(Resource.Id.rrLeftMenuEnquiryUserContainer);
            txtEnquiryUserMenu = (TextView)FindViewById(Resource.Id.txtEnquiryUserMenu);
            rrLeftMenuEnquiryUserContainer.Click += delegate { LoadEnquiryUser(); };

            rrLeftMenuBookingMessageContainer = (RelativeLayout)FindViewById(Resource.Id.rrLeftMenuBookingMessageContainer);
            txtBookingMenu = (TextView)FindViewById(Resource.Id.txtBookingMenu);
            rrLeftMenuBookingMessageContainer.Click += delegate { Booking(); };

            rrLeftMenuInvoiceContainer = (RelativeLayout)FindViewById(Resource.Id.rrLeftMenuInvoiceContainer);
            txtInvoiceCollection = (TextView)FindViewById(Resource.Id.txtInvoiceCollection);
            rrLeftMenuInvoiceContainer.Click += delegate { LoadInvoiceCollection(); };

        }
        #endregion

        /// <summary>
        /// sign out functionallity
        /// </summary>
        private void LogOut()
        {
            Context mContext = Android.App.Application.Context;

            StorageHelper oStorageHelper = new StorageHelper(mContext);

            oStorageHelper.SaveBoolInStorage("isLoggedIn", false);

            Intent intent = new Intent(this, typeof(LoginActivity));
            //close all the other intent
            intent.AddFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        private void BatchAvailability()
        {
            //Intent intent = new Intent(this, typeof(CourtActivity));
            ////close all the other intent
            //StartActivity(intent);
        }

        public void LoadVenue()
        {
            commonDetails = GetDetails();
            commonDetails.isAttendance = false;
            Intent intent = new Intent(this, typeof(VenueActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            //close all the other intent
            StartActivity(intent);
        }

        public void LoadEnquiry()
        {
            commonDetails = GetDetails();
            commonDetails.isAttendance = false;
            Intent intent = new Intent(this, typeof(EnquiryFormActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            intent.PutExtra("isNewEnquiry", "0");
            intent.PutExtra("enquiryDetail", "");
            //close all the other intent
            StartActivity(intent);
        }

        public void LoadEnquiryUser()
        {
            commonDetails = GetDetails();
            commonDetails.isAttendance = false;
            Intent intent = new Intent(this, typeof(EnquiryUserActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            //close all the other intent
            StartActivity(intent);
        }

        public void Attendance()
        {
            commonDetails = GetDetails();
            commonDetails.isAttendance = true;
            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    //CheckLandingPage(commonDetails);
                    Intent intent = new Intent(this, typeof(SportActivity));
                    intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                    //close all the other intent
                    StartActivity(intent);
                }
                catch (Exception e)
                {
                    helper.AlertPopUp("Error", "Unable to retrieve data from the server", this);
                }
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", this);
            }

            //commonDetails = GetDetails();
            //Intent intent = new Intent(this, typeof(SportActivity));
            //intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            ////close all the other intent
            //StartActivity(intent);
        }

        public void LoadBatches()
        {
            commonDetails = GetDetails();
            commonDetails.isAttendance = false;
            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    //CheckLandingPage(commonDetails);
                    Intent intent = new Intent(this, typeof(SportActivity));
                    intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                    //close all the other intent
                    StartActivity(intent);
                }
                catch (Exception e)
                {
                    helper.AlertPopUp("Error", "Unable to retrieve data from the server", this);
                }
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", this);
            }

            //commonDetails = GetDetails();
            //Intent intent = new Intent(this, typeof(SportActivity));
            //intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            ////close all the other intent
            //StartActivity(intent);
        }

        public void Booking()
        {
            commonDetails = GetDetails();
            Intent intent = new Intent(this, typeof(BookingActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            //close all the other intent
            StartActivity(intent);
        }

        public void LoadInvoiceCollection()
        {
            commonDetails = GetDetails();
            commonDetails.isAttendance = false;
            Intent intent = new Intent(this, typeof(InvoiceUserActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            //close all the other intent
            StartActivity(intent);
        }

        public void CheckLandingPage(CommonDetails details)
        {
            ServiceHelper serviceHelper;
            serviceHelper = new ServiceHelper();

            List<Sport> sportList = new List<Sport>();
            List<Court> courtList = new List<Court>();
            List<BatchCountModel> batchList = new List<BatchCountModel>();

            sportList = serviceHelper.GetSports(details.access_token, details.VenueId);


            if (sportList != null && sportList.Count > 0)
            {
                if (sportList.Count > 1)
                {
                    var sportIntent = new Intent(this, typeof(SportActivity));
                    sportIntent.PutExtra("details", JsonConvert.SerializeObject(details));
                    this.StartActivity(sportIntent);
                    this.Finish();
                }
                else
                {
                    serviceHelper = new ServiceHelper();

                    details.SportId = sportList[0].SportId.ToString();

                    courtList = serviceHelper.GetCourt(details.access_token, details.VenueId, details.SportId);
                    if (courtList != null && courtList.Count > 0)
                    {
                        if (courtList.Count > 1)
                        {
                            var courtIntent = new Intent(this, typeof(CourtActivity));
                            courtIntent.PutExtra("details", JsonConvert.SerializeObject(details));
                            this.StartActivity(courtIntent);
                            this.Finish();
                        }
                        else
                        {
                            serviceHelper = new ServiceHelper();

                            details.CourtId = courtList[0].CourtId.ToString();

                            batchList = serviceHelper.GetBatch(details.access_token, details.VenueId, details.SportId, details.CourtId);

                            var batchesIntent = new Intent(this, typeof(BatchesActivity));
                            batchesIntent.PutExtra("details", JsonConvert.SerializeObject(details));
                            this.StartActivity(batchesIntent);
                            this.Finish();
                        }
                    }
                    else
                    {
                        helper.AlertPopUp("Warning", "There are no court available", this);
                    }

                }
            }
            else
            {
                helper.AlertPopUp("Warning", "There are no sports available", this);
            }

        }
    }
}