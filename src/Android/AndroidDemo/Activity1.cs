using Android.App;
using Android.Widget;
using Android.OS;
using AndroidDemoLib;
using Applicable.Location;
using DemoLib;
using TinyIoC;

namespace AndroidDemo
{
    [Activity(Label = "AndroidDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private TinyIoCContainer Container { get { return TinyIoCContainer.Current; }}

        private MainViewModel _viewModel;
        private TextView _output;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetupIocContainer();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _viewModel = Container.Resolve<MainViewModel>();
            _viewModel.PropertyChanged += PropertyChanged;

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.UpdateLocation);
            _output = FindViewById<TextView>(Resource.Id.Output);

            button.Click += delegate { _viewModel.UpdateLocationCommand.Execute(null); };
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _output.Text = _viewModel.Distance;
        }

        private void SetupIocContainer()
        {
            var container = Container;
            container.Register<Activity>(this);
            container.Register<ILocationService, AndroidLocationService>();
            container.Register<IDispatcher, AndroidDispatcher>();
        }
    }
}

