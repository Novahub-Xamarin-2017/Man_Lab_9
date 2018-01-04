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
        private DateTime time;
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
            
            paint.AntiAlias = true;
            paint.Color = Color.White; 
            DrawClock(canvas);
            DrawText(canvas);

            time = DateTime.Now;
            var percentHour = (time.Hour % 12 * 60 + time.Minute) / (12 * 60f);
            var angleAtHour = percentHour * 2 * Math.PI;
            DrawHand(canvas, Color.Black, angleAtHour, 80);

            var percentMinute = (time.Minute * 60 + time.Second) / (60 * 60f);
            var angleAtMinute = percentMinute * 2 * Math.PI;
            DrawHand(canvas, Color.Green, angleAtMinute, 60);

            var percentSecond = time.Second / 60f;
            var angleAtSecond = percentSecond * 2 * Math.PI;
            DrawHand(canvas, Color.Red, angleAtSecond, 40);

            paint.SetStyle(Paint.Style.Fill);
            paint.Color = Color.White;
            canvas.DrawCircle(canvas.Width / 2f, canvas.Height / 2f, 10, paint);
            base.Draw(canvas);
        }

        private void DrawHand(Canvas canvas, Color color, double angle, int spacingToBorder)
        {
            paint.Color = color;
            var radius = Math.Min(canvas.Width, canvas.Height) / 2f - 60;
            var halfWidth = canvas.Width / 2f;
            var halfHeight = canvas.Height / 2f;
            var xEnd = halfWidth + (radius - spacingToBorder) * Math.Sin(angle);
            var yEnd = halfHeight - (radius - spacingToBorder) * Math.Cos(angle);
            canvas.DrawLine(halfWidth, halfHeight, (float) xEnd, (float) yEnd, paint);
        }

        private void DrawClock(Canvas canvas)
        {
            var halfHeight = canvas.Height / 2f;
            var halfWidth = canvas.Width / 2f;

            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 20;
            canvas.DrawCircle(halfWidth, halfHeight, Math.Min(canvas.Width, canvas.Height) / 2f - 20, paint);

            paint.StrokeWidth = 10;
            var radius = Math.Min(canvas.Width, canvas.Height) / 2f - 60;
            canvas.DrawCircle(halfWidth, halfHeight, radius, paint);

            for (var i = 0; i < 60; i++)
            {
                paint.TextAlign = Paint.Align.Center;
                paint.StrokeWidth = (6 * i % 30 == 0) ? 10 : 5;
                var angle = 6 * i / 180f * Math.PI;
                var xStart = halfWidth + (radius - 15) * Math.Sin(angle);
                var yStart = halfHeight - (radius - 15) * Math.Cos(angle);
                var xEnd = halfWidth + (radius - 15 - 20) * Math.Sin(angle);
                var yEnd = halfHeight - (radius - 15 - 20) * Math.Cos(angle);
                canvas.DrawLine((float)xStart, (float)yStart, (float)xEnd, (float)yEnd, paint);
            }
        }

        private void DrawText(Canvas canvas)
        {
            var halfHeight = canvas.Height / 2f;
            var halfWidth = canvas.Width / 2f;
            var radius = Math.Min(canvas.Width, canvas.Height) / 2f - 60;
            for (var i = 1; i <= 12; i++)
            {
                paint.StrokeWidth = 5;
                paint.TextSize = 30;
                paint.TextAlign = Paint.Align.Center;
                paint.SetStyle(Paint.Style.Fill);
                var alpha = 30 * i / 180f * Math.PI;
                var textBound = new Rect();
                paint.GetTextBounds(i.ToString(), 0, i.ToString().Length, textBound);
                var xText = halfWidth + (radius - 15 - 50) * Math.Sin(alpha);
                var yText = halfHeight - (radius - 15 - 50) * Math.Cos(alpha) + textBound.Height() / 2f;
                canvas.DrawText(i.ToString(), (float)xText, (float)yText, paint);
            }
        }
    }
}