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
using Android.Graphics;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySportsBook
{
    class InvoiceUser_ItemAdapter : BaseAdapter<Player>
    {

        Activity context;
        IList<Player> _items;
        bool ViewCourtFirstClick = true;
        private LinearLayout progress;
        private CommonDetails commonDetails;
        Helper helper = new Helper();

        public InvoiceUser_ItemAdapter(Activity context, IList<Player> items, LinearLayout progressbar, CommonDetails details) : base()
        {
            this.context = context;
            this._items = items;
            progress = progressbar;
            commonDetails = details;
        }

        public override Player this[int position]
        {
            get { return _items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            progress.Visibility = Android.Views.ViewStates.Gone;
            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");

            //getting the layout view
            var view = convertView ?? context.LayoutInflater.Inflate(
            Resource.Layout.invoiceuser_item, parent, false);

            var txtInvoiceUserName = view.FindViewById<TextView>(Resource.Id.txtInvoiceUserName);
            var txtInvoiceUserMobile = view.FindViewById<TextView>(Resource.Id.txtInvoiceUserMobile);
            var txtInvoiceUserBatch = view.FindViewById<TextView>(Resource.Id.txtInvoiceUserBatch);
            var txtInvoiceUserAmount = view.FindViewById<TextView>(Resource.Id.txtInvoiceUserAmount);
            var llInvoiceUserContainer = view.FindViewById<RelativeLayout>(Resource.Id.llInvoiceUserContainer);
            var btnViewHistory = view.FindViewById<Button>(Resource.Id.btnView);

            txtInvoiceUserName.Text = _items[position].FirstName+" "+ _items[position].LastName;
            txtInvoiceUserMobile.Text = _items[position].Mobile;
            txtInvoiceUserBatch.Text = "Batch: " + _items[position].BatchName;
            txtInvoiceUserAmount.Text = _items[position].Mobile;

            txtInvoiceUserName.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtInvoiceUserMobile.SetTypeface(faceRegular, TypefaceStyle.Normal);
            txtInvoiceUserBatch.SetTypeface(faceRegular, TypefaceStyle.Bold);
            txtInvoiceUserAmount.SetTypeface(faceRegular, TypefaceStyle.Normal);


            //var rlCourtItemMainContainer = (LinearLayout)view.FindViewById(Resource.Id.llCourt);
            llInvoiceUserContainer.Click += delegate
            {

                LoadEnquiryUserDetails(_items[position].PlayerId, commonDetails);
            };

            btnViewHistory.Click += delegate {
                LoadViewInvoiceHistory(_items[position].PlayerId, commonDetails);
            };

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get { return _items.Count; }
        }

        public void LoadEnquiryUserDetails(int playerId, CommonDetails details)
        {
            Player player = new Player();

            player = _items.Where(x => x.PlayerId == playerId).FirstOrDefault();

            if (helper.CheckInternetConnection(context))
            {
                var intent = new Intent(context, typeof(InvoiceCollectionFormActivity));
                intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                intent.PutExtra("invoicePlayer", JsonConvert.SerializeObject(player));
                context.StartActivity(intent);
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", context);
            }
        }

        public void LoadViewInvoiceHistory(int playerId, CommonDetails details) {
            Player player = new Player();

            player = _items.Where(x => x.PlayerId == playerId).FirstOrDefault();

            if (helper.CheckInternetConnection(context))
            {
                var intent = new Intent(context, typeof(InvoiceUserHistoryActivity));
                intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                intent.PutExtra("invoicePlayer", JsonConvert.SerializeObject(player));
                context.StartActivity(intent);
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", context);
            }
        }

    }
}