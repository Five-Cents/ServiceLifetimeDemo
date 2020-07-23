# ServiceLifetimeDemo

Simple application that demonstrates the differences between service lifetimes in ASP.NET Core.

## Change the Lifetimes

To change the service lifetime modify the following values in `Host/appsettings.json`

```
  // ...
  "AllowedHosts": "*",
  "IsGuidServiceTransient": true,   // Modify to false if you do not want a transient service lifetime.
  "IsGuidServiceScoped": false,     // Modify to true if you want a scoped lifetime.
  "IsGuidServiceSingleton": false   // Modify to true if you want a singleton lifetime.
}
```

Depending on what you set to true/false the `IGuidService` service registered in the dependency injection container will change its lifetime to what you set. When running the 'Domain' project your browser should automatically open up to the api endpoint and give you two GUID values. If the results confuse you read up on service lifetimes and examine the results.

## Lifetime Notes:

### Transient Services

When a service is registered as transient a new instance of that service is created every time that the service is resolved. In other words, every dependent class that accepts a transient service from the dependency injection container will receive its own unique instance.

This is most useful when the service contains mutable state and is not considered thread-safe. Since each dependent class receives a new instance methods can be called on the service which affect its state without fear of access by other consumers. This can come with a small performance cost because it's likely that the object will need to be created multiple times during the lifetime of the application.

Transient services are the easiest to reason about as the instances are not shared. Therefore, they tend to be the safest choice when registering a lifetime for a service when unsure of which lifetime to use.

### Singleton Services

An application service registered with the singleton lifetime will only be created once during the runtime/lifetime of the application. The same instance of the service will be re-used and shared between all classes that resolve it.

If the service is frequently requested this can have positive performance implications by reducing the amount of clock cycles spent allocating objects and reducing the load on the garbage collector.

If registering with the singleton lifetime you must consider thread safety because the same instance of the singleton service can be used by multiple consumers. A reasonable use-case for singleton lifetime is a memory cache where the state must be shared for the cache to function.

It's also important to consider the frequency of use of the service and the amount of memory it's consuming. A massive amount of memory usage could potentially lead to memory leaks.

### Scoped Services

Scoped services sit in a middle ground between transient and singleton lifetimes, where the instance of the service is created on a per-request basis.

In the context of an HTTP request the controller and any middleware involved will get the same instance of a scoped service because it's on a per-request basis. A new request will produce a different instance of the service.
