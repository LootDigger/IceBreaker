using System;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.ServiceLocator
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> services = new();

        public static void RegisterService<T>(T service)
        {
            if (services.ContainsKey(typeof(T)))
            {
                Debug.LogWarning("Service already registered " + typeof(T));
                return;
            }
            services[typeof(T)] = service;
        }

        public static T GetService<T>()
        {
            try
            {
                return (T)services[typeof(T)];
            }
            catch (Exception exception)
            {
                throw new NotImplementedException("Can't get service of type " + typeof(T));
            }
        }
    }
}