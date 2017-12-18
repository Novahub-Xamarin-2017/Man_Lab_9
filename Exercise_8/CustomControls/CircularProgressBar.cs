using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Exercise_8.CustomControls
{
    public class CircularProgressBar : View
    {
        private int value;
        private int strokeWeight;
        private Color highlightColor;
        private Color normalColor;
        private const int MarginValue = 20;
        private readonly Paint paint = new Paint();

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                this.Invalidate();
            }
        }

        public int StrokeWeight
        {
            get => strokeWeight;
            set
            {
                this.strokeWeight = value;
                this.Invalidate();
            }
        }

        public Color HighLightColor
        {
            get => highlightColor;
            set
            {
                this.highlightColor = value;
                this.Invalidate();
            }
            
        }

        public Color NormalColor
        {
            get => normalColor;
            set
            {
                this.normalColor = value;
                this.Invalidate();
            }

        }

        protected CircularProgressBar(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public CircularProgressBar(Context context) : base(context, null)
        {
        }

        public CircularProgressBar(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
            TypedArray typeArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.SetProgressProperties, 0, 0);
            Value = typeArray.GetInteger(Resource.Styleable.SetProgressProperties_value, 0);
            StrokeWeight = typeArray.GetInteger(Resource.Styleable.SetProgressProperties_strokeWeight, 20);
            HighLightColor = typeArray.GetColor(Resource.Styleable.SetProgressProperties_highlightColor, Color.Green);
            NormalColor = typeArray.GetColor(Resource.Styleable.SetProgressProperties_normalColor, Color.White);

        }

        public CircularProgressBar(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs, defStyleAttr, 0)
        {
        }

        public CircularProgressBar(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public override void Draw(Canvas canvas)
        {
            var halfWidth = (float)canvas.Width / 2;
            var halfHeight = (float)canvas.Height / 2;
            base.Draw(canvas);

            paint.Color = highlightColor;
            paint.Flags = PaintFlags.AntiAlias;
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = StrokeWeight;
            canvas.DrawCircle(halfWidth, halfHeight, Math.Min(halfWidth, halfHeight) - MarginValue, paint);

            paint.Color = normalColor;

            var size = Math.Min(canvas.Width, canvas.Height) - 2 * MarginValue;
            var x = (halfWidth > halfHeight ? halfWidth - halfHeight : 0) + MarginValue;
            var y = (halfWidth > halfHeight ? 0 : halfHeight - halfWidth) + MarginValue;
            canvas.DrawArc(x, y, x + size, y + size, 270, (float)(360 * (100 - value) / 100.0), false, paint);

            paint.Color = highlightColor;
            paint.TextSize = size / 3f;
            var textBound = new Rect();
            paint.SetStyle(Paint.Style.Fill);
            paint.GetTextBounds(Value + "%", 0, (Value + "%").Length, textBound);
            canvas.DrawText(Value + "%", halfWidth - textBound.Width() / 2f, halfHeight + textBound.Height() / 2f, paint);
        }
    }
}