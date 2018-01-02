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

namespace Exercise_10.CustomControls
{
    class CircularImageView : ImageView
    {
        private readonly Paint paint = new Paint();
        protected CircularImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public CircularImageView(Context context) : this(context, null)
        {
        }

        public CircularImageView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public CircularImageView(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs, defStyleAttr, 0)
        {
        }

        public CircularImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            paint.AntiAlias = true;
            var backgroudStroke = Math.Sqrt(canvas.Width * canvas.Width + canvas.Height * canvas.Height) / 2f -
                                  Math.Min(canvas.Width, canvas.Height) / 2f * 0.9f;

            DrawCirle(canvas, (float) backgroudStroke, Color.Black);
            SetLayerType(LayerType.Software, paint);
            paint.SetShadowLayer(16f, 0f, 4f, Color.Black);
            DrawCirle(canvas, 20, Color.White);
        }

        private void DrawCirle(Canvas canvas, float strokeWidth, Color color)
        {
            paint.SetStyle(Paint.Style.Stroke);
            paint.Color = color;
            paint.StrokeWidth = strokeWidth;
            var x = canvas.Width / 2f;
            var y = canvas.Height / 2f;
            var radius = Math.Min(canvas.Width, canvas.Height) / 2f * 0.9f;
            canvas.DrawCircle(x, y, radius + strokeWidth / 2f, paint);
        }
    }
}