using System;
using System.Windows;
using System.Windows.Threading;

namespace DemoLib
{
    public class Wp7Dispatcher : IDispatcher
    {
        private readonly Dispatcher _dispatcher;

        public Wp7Dispatcher()
        {
            _dispatcher = Application.Current.RootVisual.Dispatcher;
        }

        public void Invoke(Action action)
        {
            if (_dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                _dispatcher.BeginInvoke(action);
            }
        }
    }
}
