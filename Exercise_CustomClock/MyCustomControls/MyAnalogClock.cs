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

namespace Exercise_CustomClock.MyCustomControls
{
    class MyAnalogClock : View
    {
        private DateTime time = DateTime.Now;
        private readonly Paint paint = new Paint();
        protected MyAnalogClock(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MyAnalogClock(Context context) : this(context, null)
        {
        }

        public MyAnalogClock(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public MyAnalogClock(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs, defStyleAttr, 0)
        {
        }

        public MyAnalogClock(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {

        }

        public override void Draw(Canvas canvas)
        {
            var halfHeight = canvas.Height / 2f;
            var halfWidth = canvas.Width / 2f;
            paint.Color = Color.White;
            paint.SetStyle(Paint.Style.Fill);
            canvas.DrawCircle(halfWidth, halfHeight, 10, paint);
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 20;
            canvas.DrawCircle(halfWidth, halfHeight, Math.Min(canvas.Width, canvas.Height) / 2f - 20, paint);
            var padding = 15;
            paint.StrokeWidth = 10;
            var radius = Math.Min(canvas.Width, canvas.Height) / 2f - 60;
            canvas.DrawCircle(halfWidth, halfHeight, radius, paint);
            for (var i = 0; i < 30; i++)
            {
                paint.StrokeWidth = (6 * i % 30 == 0) ? 10 : 5;
                var alpha = 6 * i / 180f * Math.PI;
                var itemLength = 20;
                var xStart1 = halfWidth + (radius - padding) * Math.Sin(alpha);
                var yStart1 = halfHeight + (radius - padding) * Math.Cos(alpha);
                var xEnd1 = halfWidth + (radius - padding - itemLength) * Math.Sin(alpha);
                var yEnd1 = halfHeight + (radius - padding - itemLength)* Math.Cos(alpha);
                canvas.DrawLine((float)xStart1, (float)yStart1, (float)xEnd1, (float)yEnd1, paint);
                var xStart2 = halfWidth - (radius - padding) * Math.Sin(alpha);
                var yStart2 = halfHeight - (radius - padding) * Math.Cos(alpha);
                var xEnd2 = halfWidth - (radius - padding - itemLength) * Math.Sin(alpha);
                var yEnd2 = halfHeight - (radius - padding - itemLength) * Math.Cos(alpha);
                canvas.DrawLine((float)xStart2, (float)yStart2, (float)xEnd2, (float)yEnd2, paint);
            }
            base.Draw(canvas);
        }
    }
}