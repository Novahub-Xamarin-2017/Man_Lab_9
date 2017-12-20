using Android.App;
using Android.Widget;
using Android.OS;
using Exercise_8.CustomControls;

namespace Exercise_8
{
    [Activity(Label = "Exercise_8", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var progessBar = FindViewById<CircularProgressBar>(Resource.Id.progressControl);
        }
    }
}

