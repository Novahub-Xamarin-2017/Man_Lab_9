using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Exercise_5
{
    [Activity(MainLauncher = true)]
    public class MainActivity : Activity
    {
        [InjectView(Resource.Id.spinner1)] private Spinner spinner1;
        [InjectView(Resource.Id.spinner2)] private Spinner spinner2;
        [InjectView(Resource.Id.toolbar)] private Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Cheeseknife.Inject(this);
            SetActionBar(toolbar);
            InitSpinners();
        }

        void OnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

        }

        private void InitSpinners()
        {
            spinner1.ItemSelected += OnItemSelected;
            var adapter1 = ArrayAdapter.CreateFromResource(this, Resource.Array.firstArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner1.Adapter = adapter1;

            spinner2.ItemSelected += OnItemSelected;
            var adapter2 = ArrayAdapter.CreateFromResource(this, Resource.Array.secondArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}

