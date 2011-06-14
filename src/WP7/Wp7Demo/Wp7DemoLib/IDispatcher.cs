using System;

namespace DemoLib
{
    public interface IDispatcher
    {
        void Invoke(Action action);
    }
}