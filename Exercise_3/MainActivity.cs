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
            if (String.IsNullOrEmpty(edFirstCofficient.Text) ||
                String.IsNullOrEmpty(edSecondCofficient.Text) ||
                String.IsNullOrEmpty(edThirdCofficient.Text))
            {
                Toast.MakeText(this, Resource.String.InputData, ToastLength.Short).Show();
            }
            else
            {
                try
                {
                    var a = Convert.ToDouble(edFirstCofficient.Text);
                    var b = Convert.ToDouble(edSecondCofficient.Text);
                    var c = Convert.ToDouble(edThirdCofficient.Text);
                    tvResultTitle.Visibility = ViewStates.Visible;
                    tvResult.Text = SolveEquation(a, b, c);
                }
                catch (Exception)
                {
                    Toast.MakeText(this, Resource.String.InputFloat, ToastLength.Short).Show();
                }
            }
        }

        private string SolveEquation(double a, double b, double c)
        {
            if (a.Equals(0))
            {
                return b.Equals(0)
                    ? (c.Equals(0)
                        ? GetString(Resource.String.InfiniteSolution)
                        : GetString(Resource.String.NoSolution))
                    : GetString(Resource.String.OneRoot) + $"\n\t\tx = {-c / b}";
            }
            var delta = b * b - 4 * a * c;
            return delta < 0 ? GetString(Resource.String.NoSolution) : (delta > 0
                ? GetString(Resource.String.TwoRoots) + $"\n\t\tx1 = {(-b + Math.Sqrt(delta)) / (2 * a)}\n\t\tx2 = {(-b - Math.Sqrt(delta)) / (2 * a)}"
                : GetString(Resource.String.DupRoot) + $"\n\t\tx1 = x2 = {-b / (2 * a)}");
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

