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
using Android.Util;
using Android.Views.InputMethods;
using static Android.App.ActionBar;

namespace MySportsBook
{
    [Activity(Label = "", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,WindowSoftInputMode =SoftInput.StateHidden)]
    public class EnquiryFormActivity : MenuActivity,EnquirySaveInterface
    {
        private CommonDetails commonDetails;
        private Button btnSubmit;
        private Button btnCancel;
        TextView lblHeader;
        LinearLayout linearProgressBar;
        Helper helper = new Helper();
        private Spinner spinnerEnquiryGame;
        private EditText editTextEnquiryGames;
        private EditText editTextEnquiryName;
        private EditText editTextEnquiryMobile;
        private EditText editTextEnquiryComment;
        private TextView txtEnquiryName;
        private TextView txtEnquiryMobile;
        private TextView txtEnquiryGame;
        private TextView txtEnquiryComment;
        private LinearLayout llEnquiryFeedbackContainer;
        TextView txtCommentHeader;
        string isNewEnquiry = string.Empty;
        LinearLayout llCommentContainer;
        EnquiryModel selectedEnquiryModel = new EnquiryModel();
        private LinearLayout llEnquiryViewContainer;
        private TextView txtViewEnquiryName;
        private TextView txtViewEnquiryMobile;

        public override CommonDetails GetDetails()
        {
            return commonDetails;
        }

        public override int GetResourceLayout()
        {
            return Resource.Layout.EnquiryForm;
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
            isNewEnquiry= Intent.Extras.GetString("isNewEnquiry") ?? string.Empty;
            selectedEnquiryModel = JsonConvert.DeserializeObject<EnquiryModel>(Intent.GetStringExtra("enquiryDetail"));

            lblHeader = FindViewById<TextView>(Resource.Id.lblheader);
            linearProgressBar = FindViewById<LinearLayout>(Resource.Id.linearProgressBar);
            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            spinnerEnquiryGame = FindViewById<Spinner>(Resource.Id.spinnerEnquiryGame);
            editTextEnquiryGames = FindViewById<EditText>(Resource.Id.editTextEnquiryGames);
            editTextEnquiryName = FindViewById<EditText>(Resource.Id.editTextEnquiryName);
            editTextEnquiryMobile = FindViewById<EditText>(Resource.Id.editTextEnquiryMobile);
            editTextEnquiryComment = FindViewById<EditText>(Resource.Id.editTextEnquiryComment);

            txtEnquiryName = FindViewById<TextView>(Resource.Id.txtEnquiryName);
            txtEnquiryMobile = FindViewById<TextView>(Resource.Id.txtEnquiryMobile);
            txtEnquiryGame = FindViewById<TextView>(Resource.Id.txtEnquiryGame);
            txtEnquiryComment = FindViewById<TextView>(Resource.Id.txtEnquiryComment);
            llEnquiryFeedbackContainer = FindViewById<LinearLayout>(Resource.Id.llEnquiryFeedbackContainer);
            //llEnquiryFeedback = FindViewById<LinearLayout>(Resource.Id.llEnquiryFeedback);
            txtCommentHeader = FindViewById<TextView>(Resource.Id.txtCommentHeader);
            llCommentContainer= FindViewById<LinearLayout>(Resource.Id.llCommentContainer);
            llEnquiryViewContainer = FindViewById<LinearLayout>(Resource.Id.llEnquiryViewContainer);
            txtViewEnquiryName = FindViewById<TextView>(Resource.Id.txtViewEnquiryName);
            txtViewEnquiryMobile = FindViewById<TextView>(Resource.Id.txtViewEnquiryMobile);

            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");

            lblHeader.SetTypeface(faceRegular, TypefaceStyle.Bold);
            btnSubmit.SetTypeface(faceRegular, TypefaceStyle.Normal);
            btnCancel.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtCommentHeader.SetTypeface(faceRegular, TypefaceStyle.Bold);

            editTextEnquiryName.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextEnquiryMobile.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextEnquiryGames.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextEnquiryComment.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtEnquiryName.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtEnquiryMobile.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtEnquiryGame.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtEnquiryComment.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtViewEnquiryName.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtViewEnquiryMobile.SetTypeface(faceRegular, TypefaceStyle.Bold);

            editTextEnquiryGames.RequestFocus();


            btnSubmit.SetAllCaps(false);
            btnCancel.SetAllCaps(false);

            btnSubmit.Click += btnSubmit_Click;
            btnCancel.Click += btnCancel_Click;
            editTextEnquiryGames.Click += EditTextEnquiryGames_Click;

            if (isNewEnquiry == "1")
            {
                llEnquiryFeedbackContainer.Visibility = ViewStates.Visible;
                llCommentContainer.Visibility = ViewStates.Visible;
                llEnquiryViewContainer.Visibility = ViewStates.Visible;

                txtEnquiryName.Visibility = ViewStates.Gone;
                txtEnquiryMobile.Visibility = ViewStates.Gone;
                editTextEnquiryName.Visibility = ViewStates.Gone;
                editTextEnquiryMobile.Visibility = ViewStates.Gone;

                if (selectedEnquiryModel!= null)
                {
                    txtViewEnquiryName.Text = selectedEnquiryModel.Name;
                    txtViewEnquiryMobile.Text = selectedEnquiryModel.Mobile;
                    editTextEnquiryName.Text = selectedEnquiryModel.Name;
                    editTextEnquiryMobile.Text = selectedEnquiryModel.Mobile;
                    editTextEnquiryGames.Text = selectedEnquiryModel.Game;

                    if (!string.IsNullOrEmpty(selectedEnquiryModel.Comment)) {
                        TextView textview = new TextView(this) { Id = 000, Text = selectedEnquiryModel.Comment.ToString() };
                        LinearLayout.LayoutParams textViewParam = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        textview.Gravity = GravityFlags.Left;
                        textview.TextSize = 16;
                        textview.SetPadding(0, 5, 0, 5);
                        textview.SetTextColor(Color.ParseColor("#000000"));
                        textview.SetTypeface(faceRegular, TypefaceStyle.Normal);


                        llEnquiryFeedbackContainer.AddView(textview, textViewParam);

                        View v = new View(this);
                        LinearLayout.LayoutParams viewParam = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
                        v.SetBackgroundColor(Color.ParseColor("#CCCCCC"));

                        llEnquiryFeedbackContainer.AddView(v, viewParam);
                    }

                    if (selectedEnquiryModel.Comments != null && selectedEnquiryModel.Comments.Count > 0) {
                        for (int i = 0; i < selectedEnquiryModel.Comments.Count; i++)
                        {
                            TextView textview = new TextView(this) { Id = i, Text = selectedEnquiryModel.Comments[i].ToString() };
                            LinearLayout.LayoutParams textViewParam = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                            textview.Gravity = GravityFlags.Left;
                            textview.TextSize = 16;
                            textview.SetPadding(0, 5, 0, 5);
                            textview.SetTextColor(Color.ParseColor("#000000"));
                            textview.SetTypeface(faceRegular, TypefaceStyle.Normal);

                            llEnquiryFeedbackContainer.AddView(textview, textViewParam);

                            if (i < selectedEnquiryModel.Comments.Count)
                            {
                                View v = new View(this);
                                LinearLayout.LayoutParams viewParam = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
                                v.SetBackgroundColor(Color.ParseColor("#CCCCCC"));

                                llEnquiryFeedbackContainer.AddView(v, viewParam);
                            }
                        }
                    }                    
                }
            }
            else
            {
                llEnquiryFeedbackContainer.Visibility = ViewStates.Gone;
                llCommentContainer.Visibility = ViewStates.Gone;
                llEnquiryViewContainer.Visibility = ViewStates.Gone;

                txtEnquiryName.Visibility = ViewStates.Visible;
                txtEnquiryMobile.Visibility = ViewStates.Visible;
                editTextEnquiryName.Visibility = ViewStates.Visible;
                editTextEnquiryMobile.Visibility = ViewStates.Visible;
            }



            //linearProgressBar.Visibility = Android.Views.ViewStates.Visible;

            //new Thread(new ThreadStart(delegate
            //{
            //    RunOnUiThread(async () =>
            //    {
            //        await LoadGames(commonDetails);
            //        linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
            //    });
            //})).Start();
        }

        private void EditTextEnquiryGames_Click(object sender, EventArgs e)
        {
            HideKeyBoard();
            Intent intent = new Intent(this, typeof(GamesActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            StartActivityForResult(intent, 1);
        }

        private void HideKeyBoard()
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

        //public async Task LoadGames(CommonDetails details)
        //{
        //    if (helper.CheckInternetConnection(this))
        //    {
        //        List<Games> gameList = new List<Games>();
        //        try
        //        {
        //            ServiceHelper serviceHelper = new ServiceHelper();


        //            gameList = serviceHelper.GetGames(details.access_token, details.VenueId, details.SportId);

        //            if (gameList != null && gameList.Count > 0)
        //            {
        //                List<String> sportNameList = new List<String>();
        //                sportNameList.Add("Select");
        //                foreach (var name in gameList)
        //                {
        //                    sportNameList.Add(name.SportName);

        //                }

        //                ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this,
        //                    Android.Resource.Layout.SimpleSpinnerDropDownItem, sportNameList);
        //                dataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
        //                spinnerEnquiryGame.Adapter = dataAdapter;
        //                spinnerEnquiryGame.ItemSelected +=
        //                    new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
        //            }

        //            linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
        //        }
        //        catch (Exception e)
        //        {
        //            helper.AlertPopUp("Error", "Unable to retrieve data from the server", this);

        //            linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
        //        }
        //    }
        //    else
        //    {
        //        helper.AlertPopUp("Warning", "Please enable mobile data", this);

        //        linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
        //    }

        //}

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            View v = spinner.SelectedView;
            ((TextView)v).SetTextColor(Color.ParseColor("#000000"));
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //string selectedGame = string.Empty;
            //selectedGame = spinnerEnquiryGame.SelectedItem.ToString();


            if (!string.IsNullOrEmpty(editTextEnquiryName.Text) && !string.IsNullOrEmpty(editTextEnquiryMobile.Text))
            {
                //linearProgressBar.Visibility = Android.Views.ViewStates.Visible;
                if (helper.CheckInternetConnection(this))
                {
                    EnquiryModel enquiryModel = new EnquiryModel();

                    ServiceHelper serviceHelper = new ServiceHelper();

                    enquiryModel.Name = editTextEnquiryName.Text;
                    enquiryModel.Mobile = editTextEnquiryMobile.Text;
                    enquiryModel.Game = editTextEnquiryGames.Text;
                    enquiryModel.Comment = editTextEnquiryComment.Text;
                    enquiryModel.Slot = string.Empty;
                    enquiryModel.VenueId = Convert.ToInt32(commonDetails.VenueId);
                    enquiryModel.EnquiryId = 0;

                    List<string> enquiryCommentsList = new List<string>();

                    if (isNewEnquiry == "1")
                    {
                        enquiryCommentsList.Add(editTextEnquiryComment.Text);
                        enquiryModel.Comments = enquiryCommentsList;
                        enquiryModel.EnquiryId = selectedEnquiryModel.EnquiryId;
                    }

                    //
                    //Enquiry_Comments enquiryComments=new Enquiry_Comments();

                    //enquiryComments.Comments = "";

                    //enquiryCommentsList.Add(enquiryComments);

                    //enquiryModel.Enquiry_Comments = enquiryCommentsList;


                    try
                    {
                        AddEnquiry(commonDetails.access_token, enquiryModel);

                        //new Thread(new ThreadStart(delegate
                        //{
                        //    RunOnUiThread(async () =>
                        //    {
                        //        await AddEnquiry(commonDetails.access_token, enquiryModel);
                        //        linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                        //    });
                        //})).Start();
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
                helper.AlertPopUp("Error", "Please Enter Name and Mobile", this);
                linearProgressBar.Visibility = ViewStates.Gone;
            }


        }

        public async Task AddEnquiry(string token, EnquiryModel enquiryModel)
        {
            //bool result = false;
            if (helper.CheckInternetConnection(this))
            {
                try
                {
                    AysncTaskClass aysncTaskClass = new AysncTaskClass(commonDetails, this, linearProgressBar, "EnquirySave", enquiryModel);
                    aysncTaskClass.Execute("EnquirySave");

                    //ServiceHelper serviceHelper = new ServiceHelper();
                    //result = serviceHelper.AddEnquiry(token, enquiryModel);
                    //linearProgressBar.Visibility = Android.Views.ViewStates.Gone;

                    //if (result)
                    //{
                    //    helper.AlertPopUp("Message", "Data has been updated successfully", this);

                    //    Intent intent = new Intent(this, typeof(EnquiryUserActivity));
                    //    intent.AddFlags(ActivityFlags.ClearTop);
                    //    intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                    //    StartActivity(intent);
                    //}

                    //else
                    //{
                    //    helper.AlertPopUp("Error", "Unable to submit the data to server", this);
                    //}
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            commonDetails.isAttendance = false;
            Intent intent = new Intent(this, typeof(EnquiryUserActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            //close all the other intent
            StartActivity(intent);
        }

        protected override void OnActivityResult(Int32 requestCode, Result resultCode, Intent data)
        {
            string enquiredGames = string.Empty;

            enquiredGames = data.Extras.GetString("selectedGames");

            editTextEnquiryGames.Text = enquiredGames;

        }

        public void EnquirySaveInterface(bool responseResult)
        {
            if (responseResult)
            {
                helper.AlertPopUp("Message", "Data has been updated successfully", this);

                Intent intent = new Intent(this, typeof(EnquiryUserActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                StartActivity(intent);
            }

            else
            {
                helper.AlertPopUp("Error", "Unable to submit the data to server", this);
            }
        }
    }
}