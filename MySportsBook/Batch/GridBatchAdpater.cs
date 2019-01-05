using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using static Android.App.ActionBar;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MySportsBook
{
    class GridBatchAdpater : BaseAdapter<BatchCountModel>
    {
        Activity context;
        IList<BatchCountModel> _items;
        bool ViewBatchFirstClick = true;
        private LinearLayout progress;
        private CommonDetails commonDetails;
        Helper helper=new Helper();

        public GridBatchAdpater(Activity context, IList<BatchCountModel> items, LinearLayout progressbar, CommonDetails details) :base()
        {
            this.context = context;
            this._items = items;
            progress = progressbar;
            commonDetails = details;
        }

        public override BatchCountModel this[int position]
        {
            get { return _items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            RelativeLayout rlBatchItemMainContainer;
            //for regular text getting Montserrat-Light.otf
            Typeface face = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/zekton rg.ttf");
            Typeface faceRegular = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Montserrat-Regular.otf");

            //getting the layout view
            var view = convertView ?? context.LayoutInflater.Inflate(
                           Resource.Layout.batch, parent, false);
            
            var lblBatchName = view.FindViewById<TextView>(Resource.Id.lblBatchName);
            var lblBatchCount = view.FindViewById<TextView>(Resource.Id.lblBatchCount);
            rlBatchItemMainContainer = view.FindViewById<RelativeLayout>(Resource.Id.rlBatchItemMainContainer);
            
            lblBatchName.Text = _items[position].BatchName;
            lblBatchCount.Text = _items[position].PlayerCount.ToString();

            if (Convert.ToInt32(_items[position].PlayerCount) < 4)
                lblBatchCount.SetTextColor(Color.ParseColor("#008B8B"));
            else if (Convert.ToInt32(_items[position].PlayerCount) < 7)
                lblBatchCount.SetTextColor(Color.ParseColor("#FF8C00"));
            else
                lblBatchCount.SetTextColor(Color.ParseColor("#8B0000"));

            lblBatchName.SetTypeface(faceRegular, TypefaceStyle.Normal);
            lblBatchCount.SetTypeface(faceRegular, TypefaceStyle.Bold);

            rlBatchItemMainContainer.Click += delegate
            {
                LoadPlayer(position, commonDetails);
            };

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get { return _items.Count; }
        }

        public void LoadPlayer(int position, CommonDetails details)
        {
            if (helper.CheckInternetConnection(context))
            {
                details.BatchId = _items[position].BatchId.ToString();
                var intent = new Intent(context, typeof(BatchPlayer));
                intent.PutExtra("details", JsonConvert.SerializeObject(commonDetails));
                context.StartActivity(intent);
                ViewBatchFirstClick = false;
            }
            else
            {
                helper.AlertPopUp("Warning", "Please enable mobile data", context);
            }
        }
    }
    
}