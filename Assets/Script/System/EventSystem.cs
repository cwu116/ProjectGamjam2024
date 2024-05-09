using System;
using System.Collections.Generic;

namespace Game.System
{
    public interface IEvent { }

    public class EventSystem
    {
        public static EventSystem Instance;

        ~EventSystem()
        {
            _registeredEvents.Clear();
        }

        public interface IRegisterActions
        {
        }

        private static readonly Dictionary<Type, IRegisterActions> _registeredEvents = new();

        public class Registrations<T> : IRegisterActions
        {
            public Action<T> OnEvent = e => { };
        }

        public static void Send<T>() where T : IEvent, new()
        {
            Send(new T());
        }

        public static void Send<T>(T e) where T : IEvent
        {
            var type = typeof(T);
            if (_registeredEvents.TryGetValue(type, out var registrations))
            {
                (registrations as Registrations<T>)?.OnEvent(e);
            }
        }

        public static void Register<T>(Action<T> onEvent) where T : IEvent
        {
            var type = typeof(T);
            if (!_registeredEvents.TryGetValue(type, out var registrations))
            {
                registrations = new Registrations<T>();
                _registeredEvents.Add(type, registrations);
            }

            if (registrations is Registrations<T> { OnEvent: { } } e)
            {
                e.OnEvent += onEvent;
            }
        }

        public static void UnRegister<T>(Action<T> onEvent) where T : IEvent
        {
            var type = typeof(T);
            if (!_registeredEvents.TryGetValue(type, out var registrations)) return;
            if (registrations is Registrations<T> { OnEvent: { } } e) e.OnEvent -= onEvent;
        }

        public static void ClearAllRegister()
        {
            _registeredEvents.Clear();
        }
    }
}