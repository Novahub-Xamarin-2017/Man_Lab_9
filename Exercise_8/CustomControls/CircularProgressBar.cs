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

        public CircularProgressBar(Context context) : this(context, null)
        {
        }

        public CircularProgressBar(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
            
        }

        public CircularProgressBar(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs, defStyleAttr, 0)
        {
        }

        public CircularProgressBar(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            TypedArray typeArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.progress, 0, 0);
            Value = typeArray.GetInteger(Resource.Styleable.progress_value, 0);
            StrokeWeight = typeArray.GetInteger(Resource.Styleable.progress_strokeWeight, 20);
            HighLightColor = typeArray.GetColor(Resource.Styleable.progress_highlightColor, Color.Green);
            NormalColor = typeArray.GetColor(Resource.Styleable.progress_normalColor, Color.White);
        }

        public override void Draw(Canvas canvas)
        {
            DrawArc(canvas, 100, highlightColor);
            DrawArc(canvas, value, normalColor);
            DrawText(canvas, highlightColor);
        }

        private void DrawArc(Canvas canvas, int value, Color color)
        {
            var size = Math.Min(canvas.Width, canvas.Height) - 2 * MarginValue;
            var x = (canvas.Width > canvas.Height ? canvas.Width - canvas.Height : 0) / 2+ MarginValue;
            var y = (canvas.Width > canvas.Height ? 0 : canvas.Height - canvas.Width) / 2+ MarginValue;
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = StrokeWeight;
            paint.Color = color;
            canvas.DrawArc(x, y, x + size, y + size, 270, (float)(value == 100 ? 360 : 3.6 * (100 - value)), false, paint);
        }

        private void DrawText(Canvas canvas, Color color)
        {
            var size = Math.Min(canvas.Width, canvas.Height) - 2 * MarginValue;
            paint.Color = color;
            paint.TextSize = size / 3f;
            var textBound = new Rect();
            paint.SetStyle(Paint.Style.Fill);
            paint.GetTextBounds(Value + "%", 0, (Value + "%").Length, textBound);
            canvas.DrawText(Value + "%", (canvas.Width - textBound.Width()) / 2f, (canvas.Height + textBound.Height()) / 2f, paint);
        }
    }
}