// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/weather.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace BlazorApp2 {
  public static partial class WeatherForecast
  {
    static readonly string __ServiceName = "WeatherForecast.WeatherForecast";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::BlazorApp2.WeatherRequest> __Marshaller_WeatherForecast_WeatherRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::BlazorApp2.WeatherRequest.Parser));
    static readonly grpc::Marshaller<global::BlazorApp2.WeatherReply> __Marshaller_WeatherForecast_WeatherReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::BlazorApp2.WeatherReply.Parser));

    static readonly grpc::Method<global::BlazorApp2.WeatherRequest, global::BlazorApp2.WeatherReply> __Method_GetWeather = new grpc::Method<global::BlazorApp2.WeatherRequest, global::BlazorApp2.WeatherReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetWeather",
        __Marshaller_WeatherForecast_WeatherRequest,
        __Marshaller_WeatherForecast_WeatherReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::BlazorApp2.WeatherReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of WeatherForecast</summary>
    [grpc::BindServiceMethod(typeof(WeatherForecast), "BindService")]
    public abstract partial class WeatherForecastBase
    {
      public virtual global::System.Threading.Tasks.Task<global::BlazorApp2.WeatherReply> GetWeather(global::BlazorApp2.WeatherRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for WeatherForecast</summary>
    public partial class WeatherForecastClient : grpc::ClientBase<WeatherForecastClient>
    {
      /// <summary>Creates a new client for WeatherForecast</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public WeatherForecastClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for WeatherForecast that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public WeatherForecastClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected WeatherForecastClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected WeatherForecastClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::BlazorApp2.WeatherReply GetWeather(global::BlazorApp2.WeatherRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetWeather(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::BlazorApp2.WeatherReply GetWeather(global::BlazorApp2.WeatherRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetWeather, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::BlazorApp2.WeatherReply> GetWeatherAsync(global::BlazorApp2.WeatherRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetWeatherAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::BlazorApp2.WeatherReply> GetWeatherAsync(global::BlazorApp2.WeatherRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetWeather, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override WeatherForecastClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new WeatherForecastClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(WeatherForecastBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetWeather, serviceImpl.GetWeather).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, WeatherForecastBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetWeather, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::BlazorApp2.WeatherRequest, global::BlazorApp2.WeatherReply>(serviceImpl.GetWeather));
    }

  }
  public static partial class WeatherForecast2
  {
    static readonly string __ServiceName = "WeatherForecast.WeatherForecast2";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::BlazorApp2.WeatherRequest> __Marshaller_WeatherForecast_WeatherRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::BlazorApp2.WeatherRequest.Parser));
    static readonly grpc::Marshaller<global::BlazorApp2.WeatherReply> __Marshaller_WeatherForecast_WeatherReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::BlazorApp2.WeatherReply.Parser));

    static readonly grpc::Method<global::BlazorApp2.WeatherRequest, global::BlazorApp2.WeatherReply> __Method_GetWeather2 = new grpc::Method<global::BlazorApp2.WeatherRequest, global::BlazorApp2.WeatherReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetWeather2",
        __Marshaller_WeatherForecast_WeatherRequest,
        __Marshaller_WeatherForecast_WeatherReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::BlazorApp2.WeatherReflection.Descriptor.Services[1]; }
    }

    /// <summary>Base class for server-side implementations of WeatherForecast2</summary>
    [grpc::BindServiceMethod(typeof(WeatherForecast2), "BindService")]
    public abstract partial class WeatherForecast2Base
    {
      public virtual global::System.Threading.Tasks.Task<global::BlazorApp2.WeatherReply> GetWeather2(global::BlazorApp2.WeatherRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for WeatherForecast2</summary>
    public partial class WeatherForecast2Client : grpc::ClientBase<WeatherForecast2Client>
    {
      /// <summary>Creates a new client for WeatherForecast2</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public WeatherForecast2Client(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for WeatherForecast2 that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public WeatherForecast2Client(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected WeatherForecast2Client() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected WeatherForecast2Client(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::BlazorApp2.WeatherReply GetWeather2(global::BlazorApp2.WeatherRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetWeather2(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::BlazorApp2.WeatherReply GetWeather2(global::BlazorApp2.WeatherRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetWeather2, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::BlazorApp2.WeatherReply> GetWeather2Async(global::BlazorApp2.WeatherRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetWeather2Async(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::BlazorApp2.WeatherReply> GetWeather2Async(global::BlazorApp2.WeatherRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetWeather2, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override WeatherForecast2Client NewInstance(ClientBaseConfiguration configuration)
      {
        return new WeatherForecast2Client(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(WeatherForecast2Base serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetWeather2, serviceImpl.GetWeather2).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, WeatherForecast2Base serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetWeather2, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::BlazorApp2.WeatherRequest, global::BlazorApp2.WeatherReply>(serviceImpl.GetWeather2));
    }

  }
}
#endregion
