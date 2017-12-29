using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Exercise_10.CustomControls
{
    class CircleImageView : ImageView
    {
        private int borderWidth;
        private int canvasSize;
        private Bitmap image;
        private Paint paint;
        private Paint paintBorder;


        public CircleImageView(Context context)
            : this(context, null)
        {
        }

        public CircleImageView(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public CircleImageView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {

            // init paint
            paint = new Paint {AntiAlias = true};

            paintBorder = new Paint {AntiAlias = true};

            // load the styled attributes and set their properties
            var attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.CircleImageView, defStyle, 0);

            if (attributes.GetBoolean(Resource.Styleable.CircleImageView_border, true))
            {
                var defaultBorderSize = (int)(4 * context.Resources.DisplayMetrics.Density + 0.5f);
                BorderWidth = attributes.GetDimensionPixelOffset(Resource.Styleable.CircleImageView_border_width, defaultBorderSize);
                BorderColor = attributes.GetColor(Resource.Styleable.CircleImageView_border_color, Color.White);
            }

            if (attributes.GetBoolean(Resource.Styleable.CircleImageView_shadow, false))
            {
                AddShadow();
            }
        }
        public void AddShadow()
        {
            SetLayerType(LayerType.Software, paintBorder);
            paintBorder.SetShadowLayer(4.0f, 0.0f, 2.0f, Color.Gray);
        }
        public int BorderWidth
        {
            set
            {
                this.borderWidth = 5;
                this.RequestLayout();
                this.Invalidate();
            }
        }

        public int BorderColor
        {
            set
            {
                if (paintBorder != null)
                {
                    paintBorder.Color = Color.Gray;
                }
                this.Invalidate();
            }
        }
        protected override void OnDraw(Canvas canvas)
        {
            // load the bitmap
            image = DrawableToBitmap(Drawable);

            // init shader
            if (image != null)
            {

                canvasSize = canvas.Width;
                if (canvas.Height < canvasSize)
                {
                    canvasSize = canvas.Height;
                }

                var shader = new BitmapShader(Bitmap.CreateScaledBitmap(image, canvasSize, canvasSize, false), Shader.TileMode.Clamp, Shader.TileMode.Clamp);
                paint.SetShader(shader);

                var circleCenter = (canvasSize - (borderWidth * 2)) / 2;
                canvas.DrawCircle(circleCenter + borderWidth, circleCenter + borderWidth, ((canvasSize - (borderWidth * 2)) / 2) + borderWidth - 4.0f, paintBorder);
                canvas.DrawCircle(circleCenter + borderWidth, circleCenter + borderWidth, ((canvasSize - (borderWidth * 2)) / 2) - 4.0f, paint);
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var width = MeasureWidth(widthMeasureSpec);
            var height = MeasureHeight(heightMeasureSpec);
            SetMeasuredDimension(width, height);
        }

        private int MeasureWidth(int measureSpec)
        {
            var result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            var specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
            {
                // The parent has determined an exact size for the child.
                result = specSize;
            }
            else if (specMode == MeasureSpecMode.AtMost)
            {
                // The child can be as large as it wants up to the specified size.
                result = specSize;
            }
            else
            {
                // The parent has not imposed any constraint on the child.
                result = canvasSize;
            }

            return result;
        }

        private int MeasureHeight(int measureSpecHeight)
        {
            var result = 0;
            var specMode = MeasureSpec.GetMode(measureSpecHeight);
            var specSize = MeasureSpec.GetSize(measureSpecHeight);

            if (specMode == MeasureSpecMode.Exactly)
            {
                // We were told how big to be
                result = specSize;
            }
            else if (specMode == MeasureSpecMode.AtMost)
            {
                // The child can be as large as it wants up to the specified size.
                result = specSize;
            }
            else
            {
                // Measure the text (beware: ascent is a negative number)
                result = canvasSize;
            }

            return (result + 2);
        }

        public virtual Bitmap DrawableToBitmap(Drawable drawable)
        {
            if (drawable == null)
            {
                return null;
            }
            if (drawable is BitmapDrawable)
            {
                return ((BitmapDrawable)drawable).Bitmap;
            }

            var bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            drawable.Draw(canvas);

            return bitmap;
        }
    }
}