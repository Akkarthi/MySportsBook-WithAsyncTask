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
using Android.Net;
using Newtonsoft.Json;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace MySportsBook
{
    class Helper:Activity
    {
        ProgressDialog progress;

        #region [Check Internet Connection]
        /// <summary>
        /// Checks Internet Connection
        /// </summary>
        /// <param name="activity"> Pass Activity</param>
        /// <returns>Returns either true or false</returns>
        public bool CheckInternetConnection(Activity activity)
        {
            bool result = false;
            try
            {
                var connectivityManager = (ConnectivityManager)activity.GetSystemService(ConnectivityService);
                return connectivityManager.ActiveNetworkInfo == null ? false : connectivityManager.ActiveNetworkInfo.IsConnected;
            }
            catch (System.Exception ex)
            {
            }
            return result;
        }
        #endregion

        #region Alert Popup

        /// <summary>
        /// Alert Pop Up
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="activity"></param>
        /// <param name="postiveAction"></param>
        public void AlertPopUp(string title, string message, Activity activity, Action postiveAction = null)
        {
            try
            {
                ////TODO: Have to return error message from Common library
                //AlertDialog.Builder alert = new AlertDialog.Builder(activity);

                //alert.SetTitle(title);
                //alert.SetMessage(message);


                //alert.SetPositiveButton("OK", (senderAlert, args) =>
                //{
                //    alert.Dispose();

                //    postiveAction?.Invoke();

                //});
                //activity.RunOnUiThread(() => { alert.Show(); });

                TextView txtAlertDialogHeading;
                TextView txtAlertDialogMessage;
                LinearLayout llAlertDialogButton;
                TextView txtAlertDialogButton;

                Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");
                Typeface faceBold = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Bold.otf");


                Dialog dialog=new Dialog(activity);
                dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
                dialog.SetCanceledOnTouchOutside(false);
                dialog.SetContentView(Resource.Layout.Alert_Dialog);
                txtAlertDialogHeading= (TextView)dialog.FindViewById<TextView>(Resource.Id.txtAlertDialogHeading);
                txtAlertDialogMessage = (TextView)dialog.FindViewById<TextView>(Resource.Id.txtAlertDialogMessage);
                txtAlertDialogButton = (TextView)dialog.FindViewById<TextView>(Resource.Id.txtAlertDialogButton);
                //llAlertDialogButton = (LinearLayout)dialog.FindViewById<LinearLayout>(Resource.Id.llAlertDialogButton);

                txtAlertDialogHeading.SetTypeface(faceRegular, TypefaceStyle.Bold);
                txtAlertDialogMessage.SetTypeface(faceRegular, TypefaceStyle.Normal);
                txtAlertDialogButton.SetTypeface(faceRegular, TypefaceStyle.Bold);

                txtAlertDialogHeading.Text = title;
                txtAlertDialogMessage.Text = message;
                txtAlertDialogButton.Click += delegate { dialog.Dismiss(); };
                dialog.Show();

            }
            catch
            {

            }
        }

        #endregion

        #region Progress Dialog 

        public void ProgressDialogShow(Activity activity)
        {
            progress = new ProgressDialog(activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading... Please wait...");
            progress.SetCancelable(false);
            activity.RunOnUiThread(() =>
            {
                progress.Show();
            });
        }

        public void ProgressDialogDismiss()
        {
           if(progress!=null)
                progress.Dismiss();
        }

        #endregion

    

    }
}