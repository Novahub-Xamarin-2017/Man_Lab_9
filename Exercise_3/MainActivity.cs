using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Exercise_3
{
    [Activity(Label = "Exercise_3", MainLauncher = true)]
    public class MainActivity : Activity
    {
        [InjectView(Resource.Id.edFirstCofficient)] private EditText edFirstCofficient;
        [InjectView(Resource.Id.edSecondCofficient)] private EditText edSecondCofficient;
        [InjectView(Resource.Id.edThirdCofficient)] private EditText edThirdCofficient;
        [InjectView(Resource.Id.tvResultTitle)] private TextView tvResultTitle;
        [InjectView(Resource.Id.tvResult)] private TextView tvResult;
        [InjectOnClick(Resource.Id.btnSolve)]
        private void Solve(object sender, EventArgs e)
        {
            tvResultTitle.Visibility = ViewStates.Invisible;
            tvResult.Text = "";
            if (edFirstCofficient.Text.Equals("") ||
                edSecondCofficient.Text.Equals("") ||
                edThirdCofficient.Text.Equals(""))
            {
                Toast.MakeText(this, "Vui lòng nhập đầy đủ dữ liệu!", ToastLength.Short).Show();
            }
            else
            {
                try
                {
                    var a = Convert.ToDouble(edFirstCofficient.Text);
                    var b = Convert.ToDouble(edSecondCofficient.Text);
                    var c = Convert.ToDouble(edThirdCofficient.Text);
                    tvResultTitle.Visibility = ViewStates.Visible;
                    if (a == 0)
                    {
                        if (b == 0)
                        {
                            if (c == 0)
                            {
                                tvResult.Text = "Phương trình có vô số nghiệm.";
                            }
                            else
                            {
                                tvResult.Text = "Phương trình đã cho vô nghiệm.";
                            }
                        }
                        else
                        {
                            tvResult.Text = $"Phương trình đã cho có một nghiệm:\n\t\tx = {-c / b}";
                        }
                    }
                    else
                    {
                        var delta = b * b - 4 * a * c;
                        if (delta < 0)
                        {
                            tvResult.Text = "Phương trình đã cho vô nghiệm.";
                        }
                        else if (delta == 0)
                        {
                            tvResult.Text = $"Phương trình đã cho có nghiệm kép:\n\t\tx1 = x2 = {-b / (2 * a)}";
                        }
                        else
                        {
                            var x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                            var x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                            tvResult.Text = $"Phương trình đã cho có hai nghiệm:\n\t\tx1 = {x1}\n\t\tx2 = {x2}";
                        }
                    }   
                }
                catch (Exception)
                {
                    Toast.MakeText(this, "Vui lòng nhập đúng định dạng dữ liệu.", ToastLength.Short).Show();
                }
            }
        }

        [InjectOnClick(Resource.Id.btnReset)]
        private void Reset(object sender, EventArgs e)
        {
            edFirstCofficient.Text = "";
            edSecondCofficient.Text = "";
            edThirdCofficient.Text = "";
            tvResult.Text = "";
            tvResultTitle.Visibility = ViewStates.Invisible;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Cheeseknife.Inject(this);
        }
    }
}

