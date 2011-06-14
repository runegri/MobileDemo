using System;
using System.Threading;
using Applicable.Location;

namespace DemoLib
{
    public class DebugLocationService : ILocationService
    {
        private Timer _timer;
        private LocationData _locationData;

        public DebugLocationService()
        {
            _locationData = new LocationData(63.42573, 10.44499, 0, 1, DateTime.Now);
        }

        private void UpdateLocation(object state)
        {
            _locationData = new LocationData(
                _locationData.Latitude + 0.001,
                _locationData.Longtitude + 0.001, 
                0, 1,DateTime.Now);

            var locationChanged = LocationChanged;
            if (locationChanged != null)
            {
                locationChanged(_locationData);
            }
        }

        public void Start()
        {
            _timer = new Timer(UpdateLocation, null, 1000, 1000);
        }

        public void Stop()
        {
            _timer.Change(Int32.MaxValue, Int32.MaxValue);
            _timer = null;
        }

        public Action<LocationData> LocationChanged
        {
            get;
            set;
        }
    }
}
