using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Exercise_9.MyCustomControls
{
    public class ValueBar : View
    {
        private List<double> values = new List<double>();
        public List<double> Values
        {
            get => values;
            set
            {
                this.values = value;
                this.Invalidate();
            }
        }

        private readonly Paint pen = new Paint();
        private int margin = 30;
        private int bar_margin = 5;

        protected ValueBar(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public ValueBar(Context context) : this(context, null)
        {
        }

        public ValueBar(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public ValueBar(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs, defStyleAttr, 0)
        {
        }

        public ValueBar(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            var widthPerUnit = (canvas.Width - 2 * margin) / 100f;
            var heightPerBar = (canvas.Height - 2f * margin) / values.Count;
            pen.SetStyle(Paint.Style.Fill);
            var random = new Random();
            for (var i = 0; i < values.Count; i++)
            {
                pen.Color = Color.Rgb(random.Next(255), random.Next(255), random.Next(255));
                canvas.DrawRect(margin, heightPerBar * i + bar_margin, (float) values[i] * widthPerUnit + margin, heightPerBar * i + heightPerBar - bar_margin, pen);
            }
        }
    }
}