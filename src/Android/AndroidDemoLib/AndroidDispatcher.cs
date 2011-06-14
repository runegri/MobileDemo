using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DemoLib;

namespace AndroidDemoLib
{
    public class AndroidDispatcher :IDispatcher
    {
        private readonly Activity _activity;

        public AndroidDispatcher(Activity activity)
        {
            _activity = activity;
        }

        public void Invoke(Action action)
        {
            _activity.RunOnUiThread(action);
        }
    }
}