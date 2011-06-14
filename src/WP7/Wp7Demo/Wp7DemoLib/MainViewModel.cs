using System;
using Applicable.Location;

namespace DemoLib
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILocationService _locationService;
        private readonly IDispatcher _dispatcher;
        private bool _gettingLocation;

        public MainViewModel(ILocationService locationService, IDispatcher dispatcher)
        {
            _locationService = locationService;
            _dispatcher = dispatcher;
            _locationService.LocationChanged = LocationChanged;
            HomeLocation = new Location(63.43503, 10.41106);
            UpdateLocationCommand = new DelegateCommand(StartGetLocation, CanGetLocation); 
        }

        public DelegateCommand UpdateLocationCommand
        {
            get; private set;
        }

        private void StartGetLocation(object dummy)
        {
            _gettingLocation = true;
            UpdateLocationCommand.UpdateCanExecute();
            _locationService.Start();
        }

        private bool CanGetLocation(object dummy)
        {
            return !_gettingLocation;
        }

        private Location _location;
        public Location Location
        {
            get { return _location; }
            private set
            {
                _location = value;
                RaisePropertyChanged("Location");
                RaisePropertyChanged("Distance");
            }
        }

        public Location HomeLocation { get; private set; }

        public string Distance
        {
            get            
            {
                if (Location == null)
                {
                    return "Aner ikke hvor du er...";
                }
                var distance =  (int)HomeLocation.DistanceTo(Location);
                return string.Format("Du er {0} meter fra kontoret!", distance);
            }
        }
        
        private void LocationChanged(LocationData locationData)
        {
            if ((locationData.Timestamp - DateTime.Now) < TimeSpan.FromSeconds(10))
            {
                _dispatcher.Invoke(() =>
                {
                    _locationService.Stop();
                    Location = new Location(locationData.Latitude, locationData.Longtitude);
                    UpdateLocationCommand.UpdateCanExecute();
                    RaisePropertyChanged("Location");
                    RaisePropertyChanged("Distance");
                    _gettingLocation = false;
                });
            }
        }

    }
}
