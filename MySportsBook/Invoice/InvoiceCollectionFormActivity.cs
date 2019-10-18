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
using Android.Text;

namespace MySportsBook
{
    [Activity(Label = "", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.StateHidden)]
    public class InvoiceCollectionFormActivity : MenuActivity,InvoiceUserByPlayerInterface,InvoiceSaveInterface
    {
        private CommonDetails commonDetails;
        private Button btnSubmit;
        private Button btnCancel;
        TextView lblHeader;
        LinearLayout linearProgressBar;
        Helper helper = new Helper();
        private Spinner spinnerInvoiceMonth;
        private Spinner spinnerInvoiceModeOfPayment;
        private EditText editTextInvoicePayingAmount;
        private EditText editTextInvoiceRemarks;
        private EditText editTextInvoiceOthersAmount;
        private EditText editTextInvoiceDiscountName;
        private EditText editTextInvoiceTotalAmount;
        private EditText editTextInvoiceTotalPayingAmount;


        private TextView txtViewInvoiceName;
        private TextView txtViewInvoiceMobile;
        private TextView txtViewInvoiceBatch;
        private TextView txtInvoiceTotalAmount;
        private TextView txtInvoicePayingAmount;
        private TextView txtInvoiceMonth;
        private TextView txtInvoiceRemarks;
        private TextView txtInvoiceLateAmount;
        private TextView txtInvoiceDiscountAmount;
        private TextView txtInvoiceModeOfPayment;
        private TextView txtInvoiceTotalPayingAmount;
        private string selectedInvoicePeriod = string.Empty;
        private int selectedPaymentId = 0;

        Player selectedPlayer = new Player();

        private List<InvoiceDetailsModel> invoiceDetailsModelsList;

        private List<PaymentMode> paymentModesList;

        public override CommonDetails GetDetails()
        {
            return commonDetails;
        }

        public override int GetResourceLayout()
        {
            return Resource.Layout.InvoiceCollectionForm;
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
            selectedPlayer = JsonConvert.DeserializeObject<Player>(Intent.GetStringExtra("invoicePlayer"));

            lblHeader = FindViewById<TextView>(Resource.Id.lblheader);
            linearProgressBar = FindViewById<LinearLayout>(Resource.Id.linearProgressBar);
            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            spinnerInvoiceMonth = FindViewById<Spinner>(Resource.Id.spinnerInvoiceMonth);
            spinnerInvoiceModeOfPayment = FindViewById<Spinner>(Resource.Id.spinnerInvoiceModeOfPayment);

            editTextInvoicePayingAmount = FindViewById<EditText>(Resource.Id.editTextInvoicePayingAmount);
            editTextInvoiceRemarks = FindViewById<EditText>(Resource.Id.editTextInvoiceRemarks);
            editTextInvoiceOthersAmount = FindViewById<EditText>(Resource.Id.editTextInvoiceOthersAmount);
            editTextInvoiceDiscountName = FindViewById<EditText>(Resource.Id.editTextInvoiceDiscountName);
            editTextInvoiceTotalAmount = FindViewById<EditText>(Resource.Id.editTextInvoiceTotalAmount);
            editTextInvoiceTotalPayingAmount = FindViewById<EditText>(Resource.Id.editTextInvoiceTotalPayingAmount);

            txtViewInvoiceName = FindViewById<TextView>(Resource.Id.txtViewInvoiceName);
            txtViewInvoiceMobile = FindViewById<TextView>(Resource.Id.txtViewInvoiceMobile);
            txtViewInvoiceBatch = FindViewById<TextView>(Resource.Id.txtViewInvoiceBatch);
            txtInvoiceTotalAmount = FindViewById<TextView>(Resource.Id.txtInvoiceTotalAmount);
            txtInvoicePayingAmount = FindViewById<TextView>(Resource.Id.txtInvoicePayingAmount);
            txtInvoiceMonth = FindViewById<TextView>(Resource.Id.txtInvoiceMonth);
            txtInvoiceRemarks = FindViewById<TextView>(Resource.Id.txtInvoiceRemarks);
            txtInvoiceLateAmount = FindViewById<TextView>(Resource.Id.txtInvoiceLateAmount);
            txtInvoiceDiscountAmount = FindViewById<TextView>(Resource.Id.txtInvoiceDiscountAmount);
            txtInvoiceModeOfPayment = FindViewById<TextView>(Resource.Id.txtInvoiceModeOfPayment);
            txtInvoiceTotalPayingAmount = FindViewById<TextView>(Resource.Id.txtInvoiceTotalPayingAmount);

            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");

            lblHeader.SetTypeface(faceRegular, TypefaceStyle.Bold);
            btnSubmit.SetTypeface(faceRegular, TypefaceStyle.Normal);
            btnCancel.SetTypeface(faceRegular, TypefaceStyle.Normal);


            editTextInvoicePayingAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextInvoiceRemarks.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextInvoiceOthersAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextInvoiceDiscountName.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextInvoiceTotalAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);
            editTextInvoiceTotalPayingAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);

            txtViewInvoiceName.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtViewInvoiceMobile.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtViewInvoiceBatch.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtInvoiceTotalAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoicePayingAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceMonth.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceRemarks.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceLateAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceDiscountAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceModeOfPayment.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceTotalPayingAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);

            editTextInvoiceDiscountName.TextChanged += editTextInvoiceDiscountName_TextChanged;
            editTextInvoiceOthersAmount.TextChanged += editTextInvoiceOthersAmount_TextChanged;


            btnSubmit.SetAllCaps(false);
            btnCancel.SetAllCaps(false);

            btnSubmit.Click += btnSubmit_Click;
            btnCancel.Click += btnCancel_Click;

            editTextInvoiceTotalAmount.Enabled = false;
            editTextInvoiceTotalPayingAmount.Enabled = false;

            txtViewInvoiceName.Text = selectedPlayer.FirstName + " " + selectedPlayer.LastName;
            txtViewInvoiceMobile.Text = selectedPlayer.Mobile;
            txtViewInvoiceBatch.Text = "Batch: " + selectedPlayer.BatchName;

            linearProgressBar.Visibility = Android.Views.ViewStates.Visible;
            
            LoadInvoiceUserDetail(commonDetails);


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

        private void editTextInvoiceDiscountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32 invoiceTotalAmount = 0;
            Int32 invoiceDiscountAmount = 0;
            Int32 invoiceOtherAmount = 0;

            if (string.IsNullOrEmpty(editTextInvoiceTotalAmount.Text))
            {
                invoiceTotalAmount = 0;
            }
            else
                invoiceTotalAmount = Convert.ToInt32(editTextInvoiceTotalAmount.Text);
            if (string.IsNullOrEmpty(editTextInvoiceDiscountName.Text))
            {
                invoiceDiscountAmount = 0;
            }
            else
                invoiceDiscountAmount = Convert.ToInt32(editTextInvoiceDiscountName.Text);
            if (string.IsNullOrEmpty(editTextInvoiceOthersAmount.Text))
            {
                invoiceOtherAmount = 0;
            }
            else
                invoiceOtherAmount = Convert.ToInt32(editTextInvoiceOthersAmount.Text);

            editTextInvoiceTotalPayingAmount.Text = Convert.ToString((invoiceTotalAmount + invoiceOtherAmount) - invoiceDiscountAmount);

        }

        private void editTextInvoiceOthersAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32 invoiceTotalAmount = 0;
            Int32 invoiceDiscountAmount = 0;
            Int32 invoiceOtherAmount = 0;

            if (string.IsNullOrEmpty(editTextInvoiceTotalAmount.Text))
            {
                invoiceTotalAmount = 0;
            }
            else
                invoiceTotalAmount = Convert.ToInt32(editTextInvoiceTotalAmount.Text);
            if (string.IsNullOrEmpty(editTextInvoiceDiscountName.Text))
            {
                invoiceDiscountAmount = 0;
            }
            else
                invoiceDiscountAmount = Convert.ToInt32(editTextInvoiceDiscountName.Text);
            if (string.IsNullOrEmpty(editTextInvoiceOthersAmount.Text))
            {
                invoiceOtherAmount = 0;
            }
            else
                invoiceOtherAmount = Convert.ToInt32(editTextInvoiceOthersAmount.Text);

            editTextInvoiceTotalPayingAmount.Text = Convert.ToString((invoiceTotalAmount + invoiceOtherAmount) - invoiceDiscountAmount);
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

        public void LoadInvoiceUserDetail(CommonDetails details)
        {
            if (helper.CheckInternetConnection(this))
            {

                //invoiceDetailsModelsList = new List<InvoiceDetailsModel>();
                try
                {

                    AysncTaskClass aysncTaskClass = new AysncTaskClass(details, this, linearProgressBar, "InvoiceUserByPlayer",selectedPlayer);
                    aysncTaskClass.Execute("InvoiceUserByPlayer");

                    //ServiceHelper serviceHelper = new ServiceHelper();


                    //invoiceDetailsModelsList = serviceHelper.GetInvoiceUserDetailsByPlayerId(details.access_token, details.VenueId, selectedPlayer);

                    //if (invoiceDetailsModelsList != null && invoiceDetailsModelsList.Count > 0)
                    //{
                    //    List<String> invoicePeriodList = new List<String>();
                    //    invoicePeriodList.Add("Select");
                    //    foreach (var name in invoiceDetailsModelsList)
                    //    {
                    //        invoicePeriodList.Add(name.InvoicePeriod);

                    //    }

                    //    ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this,
                    //        Android.Resource.Layout.SimpleSpinnerDropDownItem, invoicePeriodList);
                    //    dataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    //    spinnerInvoiceMonth.Adapter = dataAdapter;
                    //    spinnerInvoiceMonth.ItemSelected +=
                    //        new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

                    //    LoadPaymentMethod(details);
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

        public void LoadPaymentMethod(CommonDetails details)
        {
            if (helper.CheckInternetConnection(this))
            {

                paymentModesList = new List<PaymentMode>();
                try
                {
                    ServiceHelper serviceHelper = new ServiceHelper();


                    paymentModesList = serviceHelper.GetPaymentMethods(details.access_token, details.VenueId);

                    if (paymentModesList != null && paymentModesList.Count > 0)
                    {
                        List<String> paymentList = new List<String>();
                        paymentList.Add("Select");
                        foreach (var name in paymentModesList)
                        {
                            paymentList.Add(name.PaymentName);

                        }

                        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this,
                            Android.Resource.Layout.SimpleSpinnerDropDownItem, paymentList);
                        dataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                        spinnerInvoiceModeOfPayment.Adapter = dataAdapter;
                        spinnerInvoiceModeOfPayment.ItemSelected +=
                            new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerInvoiceModeOfPayment_ItemSelected);

                    }
                    else
                    {
                        helper.AlertPopUp("Warning", "There is no data available", this);
                    }

                    linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
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


        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            View v = spinner.SelectedView;
            ((TextView)v).SetTextColor(Color.ParseColor("#000000"));
            selectedInvoicePeriod = string.Empty;

            string selectedText = spinner.GetItemAtPosition(e.Position).ToString();

            selectedInvoicePeriod = selectedText;

            try
            {
                var totalAmount = invoiceDetailsModelsList.Where(x => x.InvoicePeriod == selectedText.Split(" - ")[0])
                    .Select(x => x.Fee).FirstOrDefault();

                editTextInvoiceTotalAmount.Text = totalAmount.ToString();
                editTextInvoiceTotalPayingAmount.Text = totalAmount.ToString();
                editTextInvoiceOthersAmount.Text = "0";
                editTextInvoiceDiscountName.Text = "0";
                editTextInvoicePayingAmount.Text = "0";
            }
            catch (Exception)
            {
                editTextInvoiceTotalAmount.Text = "0";
                editTextInvoiceTotalPayingAmount.Text = "0";
                editTextInvoiceOthersAmount.Text = "0";
                editTextInvoiceDiscountName.Text = "0";
                editTextInvoicePayingAmount.Text = "0";
            }


        }

        private void spinnerInvoiceModeOfPayment_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            View v = spinner.SelectedView;
            ((TextView)v).SetTextColor(Color.ParseColor("#000000"));

            string selectedText = spinner.GetItemAtPosition(e.Position).ToString();

            try
            {
                selectedPaymentId = paymentModesList.Where(x => x.PaymentName == selectedText)
                    .Select(x => x.PayementId).FirstOrDefault();

            }
            catch (Exception)
            {
                selectedPaymentId = 0;
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(editTextInvoiceTotalAmount.Text))
            {
                editTextInvoiceTotalAmount.Text = "0";
            }
            if (string.IsNullOrEmpty(editTextInvoiceDiscountName.Text))
            {
                editTextInvoiceDiscountName.Text = "0";
            }
            if (string.IsNullOrEmpty(editTextInvoiceOthersAmount.Text))
            {
                editTextInvoiceOthersAmount.Text = "0";
            }
            if (string.IsNullOrEmpty(editTextInvoicePayingAmount.Text))
            {
                editTextInvoicePayingAmount.Text = "0";
            }

            List<InvoiceDetailsModel> selectedinvoiceDetailsModel = new List<InvoiceDetailsModel>();

            selectedinvoiceDetailsModel = invoiceDetailsModelsList.Where(x => x.InvoicePeriod == selectedInvoicePeriod.Split(" - ")[0] && x.SportName== selectedInvoicePeriod.Split(" - ")[1]).ToList();

            InvoiceModel invoiceModel = new InvoiceModel();

            invoiceModel.InvoiceId = 0;
            invoiceModel.VenueId = Convert.ToInt32(commonDetails.VenueId);
            invoiceModel.PlayerId = Convert.ToInt32(selectedPlayer.PlayerId);
            invoiceModel.PaymentId = selectedPaymentId;
            invoiceModel.TotalFee = Convert.ToDouble(editTextInvoiceTotalAmount.Text);
            invoiceModel.TotalDiscount = Convert.ToDouble(editTextInvoiceDiscountName.Text);
            invoiceModel.TotalOtherAmount = Convert.ToDouble(editTextInvoiceOthersAmount.Text);
            invoiceModel.TotalPaidAmount = Convert.ToDouble(editTextInvoicePayingAmount.Text);
            invoiceModel.Comments = editTextInvoiceRemarks.Text;

            invoiceModel.InvoiceDetails = selectedinvoiceDetailsModel;
            //invoiceModel.InvoiceDate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
            invoiceModel.InvoiceDate = Convert.ToDateTime(DateTime.Now);

            linearProgressBar.Visibility = Android.Views.ViewStates.Visible;

            if (Convert.ToDouble(invoiceModel.TotalFee) != 0 && Convert.ToDouble(invoiceModel.TotalDiscount) >= 0 &&
                Convert.ToDouble(invoiceModel.TotalOtherAmount) >= 0
                && Convert.ToDouble(invoiceModel.TotalPaidAmount) >= 0)
            {
                double amountSum = 0;
                //Comment to sort out Total Fee 500 Discount 500 then shown alert
                //amountSum = (Convert.ToDouble(invoiceModel.TotalPaidAmount) + Convert.ToDouble(invoiceModel.TotalOtherAmount)) -
                //            Convert.ToDouble(invoiceModel.TotalDiscount);
                if (invoiceModel.TotalPaidAmount != 0)
                    amountSum = Convert.ToDouble(invoiceModel.TotalPaidAmount);
                else
                {
                    amountSum = (Convert.ToDouble(invoiceModel.TotalPaidAmount) + Convert.ToDouble(invoiceModel.TotalOtherAmount)) -
                                Convert.ToDouble(invoiceModel.TotalDiscount);
                }
                if (amountSum != 0)
                {
                    if (amountSum <= Convert.ToDouble(invoiceModel.TotalFee))
                    {
                        new Thread(new ThreadStart(delegate
                        {
                            RunOnUiThread(async () =>
                            {
                                await SaveInvoiceForPlayer(commonDetails.access_token, commonDetails.VenueId,
                                    invoiceModel);
                                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                            });
                        })).Start();
                    }
                    else if (amountSum > Convert.ToDouble(invoiceModel.TotalFee))
                    {
                        if (!string.IsNullOrEmpty(editTextInvoiceRemarks.Text))
                        {
                            new Thread(new ThreadStart(delegate
                            {
                                RunOnUiThread(async () =>
                                {
                                    await SaveInvoiceForPlayer(commonDetails.access_token, commonDetails.VenueId,
                                        invoiceModel);
                                    linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                                });
                            })).Start();
                        }
                        else
                        {
                            helper.AlertPopUp("Error", "Please enter the remarks", this);
                        }
                    }

                    //else
                    //{
                    //    helper.AlertPopUp("Error", "Please check the amount should be equal to total amount", this);
                    //    linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
                    //}
                }
                else
                {
                    helper.AlertPopUp("Error", "Please enter the amount", this);
                }

                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
            }
            else
            {
                helper.AlertPopUp("Error", "Please selected the invoice period", this);
                linearProgressBar.Visibility = Android.Views.ViewStates.Gone;
            }
        }


        public async Task SaveInvoiceForPlayer(string token, string VenueId, InvoiceModel invoiceModel)
        {
            bool result = false;
            if (helper.CheckInternetConnection(this))
            {
                invoiceDetailsModelsList = new List<InvoiceDetailsModel>();
                try
                {

                    AysncTaskClass aysncTaskClass = new AysncTaskClass(commonDetails, this, linearProgressBar, "InvoiceSave",invoiceModel, selectedPlayer);
                    aysncTaskClass.Execute("InvoiceSave");

                    //ServiceHelper serviceHelper = new ServiceHelper();

                    //result =
                    //    serviceHelper.SaveInvoiceUserDetailsByPlayerId(token, VenueId,
                    //        selectedPlayer, invoiceModel);

                    //if (result)
                    //{
                    //    helper.AlertPopUp("Message", "Data has been updated successfully", this);
                    //    Intent intent = new Intent(this, typeof(InvoiceUserActivity));
                    //    intent.AddFlags(ActivityFlags.ClearTop);
                    //    intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                    //    StartActivity(intent);
                    //}
                    //else
                    //{
                    //    helper.AlertPopUp("Error", "Unable to submit the data to server", this);
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



        private void btnCancel_Click(object sender, EventArgs e)
        {
            commonDetails.isAttendance = false;
            Intent intent = new Intent(this, typeof(EnquiryUserActivity));
            intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
            //close all the other intent
            StartActivity(intent);
        }

        public void InvoiceUserByPlayerInterface(List<InvoiceDetailsModel> invoiceDetailsList)
        {
            invoiceDetailsModelsList = new List<InvoiceDetailsModel>();
            invoiceDetailsModelsList = invoiceDetailsList;

            if (invoiceDetailsModelsList != null && invoiceDetailsModelsList.Count > 0)
            {
                List<String> invoicePeriodList = new List<String>();
                invoicePeriodList.Add("Select");
                foreach (var name in invoiceDetailsModelsList)
                {
                    invoicePeriodList.Add(name.InvoicePeriod + " - " + name.SportName);

                }

                ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this,
                    Android.Resource.Layout.SimpleSpinnerDropDownItem, invoicePeriodList);
                dataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerInvoiceMonth.Adapter = dataAdapter;
                spinnerInvoiceMonth.ItemSelected +=
                    new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

                LoadPaymentMethod(commonDetails);
            }
            else
            {
                helper.AlertPopUp("Warning", "There is no data available", this);
            }
        }

        public void InvoiceSaveInterface(bool responseResult)
        {
            if (responseResult)
            {
                helper.AlertPopUp("Message", "Data has been updated successfully", this);
                Intent intent = new Intent(this, typeof(InvoiceUserActivity));
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