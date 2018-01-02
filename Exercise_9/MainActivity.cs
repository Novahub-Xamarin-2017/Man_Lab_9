using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Exercise_9.MyCustomControls;

namespace Exercise_9
{
    [Activity(Label = "Exercise_9", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var myValueBar = (ValueBar) FindViewById(Resource.Id.myValueBar);
            myValueBar.Values = new List<double> {45, 100, 75};
        }
    }
}

