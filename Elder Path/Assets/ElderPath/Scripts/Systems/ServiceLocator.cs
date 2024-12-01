using System;
using System.Collections.Generic;

public class ServiceLocator {

    private static ServiceLocator instance;
    public static ServiceLocator Instance => instance ??= new ServiceLocator();

    private readonly Dictionary<Type, object> services;

    private ServiceLocator() => services = new Dictionary<Type, object>();

    public void RegisterService<T>(T service) {
        var type = typeof(T);
        if (!services.ContainsKey(type)) services.Add(type, service);
    }

    public T GetService<T>() {
        var type = typeof(T);
        if (services.TryGetValue(type, out var service)) return (T)service;
        throw new Exception($"Service {type} not found.");
    }

    public void UnregisterService<T>() {
        var type = typeof(T);
        if (services.ContainsKey(type)) {
            services.Remove(type);
            return;
        }
        throw new Exception($"Service {type} is not registered.");
    }
}