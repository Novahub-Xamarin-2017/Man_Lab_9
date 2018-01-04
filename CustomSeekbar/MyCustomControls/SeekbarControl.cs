using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Exercise_11;

namespace CustomSeekbar.MyCustomControls
{
    class SeekbarControl : LinearLayout
    {
        private int value = 50;
        [InjectView(Resource.Id.txtValue)] private TextView txtValue;
        [InjectView(Resource.Id.mySeekBar)] private SeekBar mySeekBar;

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                Invalidate();
            }
        }

        [InjectOnClick(Resource.Id.btnAdd)]
        private void Increase(object sender, EventArgs e)
        {
            value++;
            Change();
        }
        [InjectOnClick(Resource.Id.btnSub)]
        private void Descrease(object sender, EventArgs e)
        {
            value--;
            Change();
        }

        [InjectOnClick(Resource.Id.btnUpdate)]
        private void Update(object sender, EventArgs e)
        {
            Change();
        }

        private void Change()
        {
            txtValue.Text = value.ToString();
            mySeekBar.SetProgress(value, true);
        }

        protected SeekbarControl(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public SeekbarControl(Context context) : this(context, null)
        {
        }

        public SeekbarControl(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public SeekbarControl(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs, defStyleAttr, 0)
        {
        }

        public SeekbarControl(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            var inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.my_seekbar, this);
            Cheeseknife.Inject(this, view);
            mySeekBar.SetProgress(value, true);
            mySeekBar.ProgressChanged += (sender, e) => {
                if (!e.FromUser) return;
                value = e.Progress;
                Change();
            };
        }
    }
}