using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace Exercise_67
{
    [Activity(Label = "Login", Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {
        [InjectOnClick(Resource.Id.btnRegister)]
        private void StartRegisterActivity(object sender, EventArgs e)
        {
            StartActivity(typeof(Register));
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

