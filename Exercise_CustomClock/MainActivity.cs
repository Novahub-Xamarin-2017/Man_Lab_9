
using System.Threading;
using Android.App;
using Android.Widget;
using Android.OS;
using Exercise_CustomClock.MyCustomControls;


namespace Exercise_CustomClock
{
    [Activity(Label = "Exercise_CustomClock", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private MyAnalogClock myClock; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            myClock = FindViewById<MyAnalogClock>(Resource.Id.myClock);
            (new Thread(UpdateClock)).Start();
            myClock.Invalidate();
        }

        private void UpdateClock()
        {
            while (true)
            {
                Thread.Sleep(500);
                RunOnUiThread(myClock.PostInvalidate);
            }
        }

      
    }
}

