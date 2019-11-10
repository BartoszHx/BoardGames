// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: BoardGamesGrpc/protos/game_online.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace BoardGamesGrpc.GameOnlines {
  public static partial class GameOnlineService
  {
    static readonly string __ServiceName = "BoardGamesGrpc.GameOnlines.GameOnlineService";

    static readonly grpc::Marshaller<global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest> __Marshaller_BoardGamesGrpc_GameOnlines_SearchOpponentRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons> __Marshaller_BoardGamesGrpc_GameOnlines_SearchOpponentRespons = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest> __Marshaller_BoardGamesGrpc_GameOnlines_CancelSearchOpponentRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons> __Marshaller_BoardGamesGrpc_GameOnlines_CancelSearchOpponentRespons = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::BoardGamesGrpc.GameOnlines.PlayMatchRequest> __Marshaller_BoardGamesGrpc_GameOnlines_PlayMatchRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::BoardGamesGrpc.GameOnlines.PlayMatchRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::BoardGamesGrpc.GameOnlines.GamePlay> __Marshaller_BoardGamesGrpc_GameOnlines_GamePlay = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::BoardGamesGrpc.GameOnlines.GamePlay.Parser.ParseFrom);

    static readonly grpc::Method<global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest, global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons> __Method_SearchOpponent = new grpc::Method<global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest, global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SearchOpponent",
        __Marshaller_BoardGamesGrpc_GameOnlines_SearchOpponentRequest,
        __Marshaller_BoardGamesGrpc_GameOnlines_SearchOpponentRespons);

    static readonly grpc::Method<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest, global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons> __Method_CancelSearchOpponent = new grpc::Method<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest, global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CancelSearchOpponent",
        __Marshaller_BoardGamesGrpc_GameOnlines_CancelSearchOpponentRequest,
        __Marshaller_BoardGamesGrpc_GameOnlines_CancelSearchOpponentRespons);

    static readonly grpc::Method<global::BoardGamesGrpc.GameOnlines.PlayMatchRequest, global::BoardGamesGrpc.GameOnlines.GamePlay> __Method_PlayMatch = new grpc::Method<global::BoardGamesGrpc.GameOnlines.PlayMatchRequest, global::BoardGamesGrpc.GameOnlines.GamePlay>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "PlayMatch",
        __Marshaller_BoardGamesGrpc_GameOnlines_PlayMatchRequest,
        __Marshaller_BoardGamesGrpc_GameOnlines_GamePlay);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::BoardGamesGrpc.GameOnlines.GameOnlineReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GameOnlineService</summary>
    [grpc::BindServiceMethod(typeof(GameOnlineService), "BindService")]
    public abstract partial class GameOnlineServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons> SearchOpponent(global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons> CancelSearchOpponent(global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task PlayMatch(grpc::IAsyncStreamReader<global::BoardGamesGrpc.GameOnlines.PlayMatchRequest> requestStream, grpc::IServerStreamWriter<global::BoardGamesGrpc.GameOnlines.GamePlay> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GameOnlineService</summary>
    public partial class GameOnlineServiceClient : grpc::ClientBase<GameOnlineServiceClient>
    {
      /// <summary>Creates a new client for GameOnlineService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public GameOnlineServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GameOnlineService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public GameOnlineServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected GameOnlineServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected GameOnlineServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons SearchOpponent(global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SearchOpponent(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons SearchOpponent(global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SearchOpponent, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons> SearchOpponentAsync(global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SearchOpponentAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons> SearchOpponentAsync(global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SearchOpponent, null, options, request);
      }
      public virtual global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons CancelSearchOpponent(global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CancelSearchOpponent(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons CancelSearchOpponent(global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CancelSearchOpponent, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons> CancelSearchOpponentAsync(global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CancelSearchOpponentAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons> CancelSearchOpponentAsync(global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CancelSearchOpponent, null, options, request);
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::BoardGamesGrpc.GameOnlines.PlayMatchRequest, global::BoardGamesGrpc.GameOnlines.GamePlay> PlayMatch(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PlayMatch(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::BoardGamesGrpc.GameOnlines.PlayMatchRequest, global::BoardGamesGrpc.GameOnlines.GamePlay> PlayMatch(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_PlayMatch, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override GameOnlineServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GameOnlineServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GameOnlineServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SearchOpponent, serviceImpl.SearchOpponent)
          .AddMethod(__Method_CancelSearchOpponent, serviceImpl.CancelSearchOpponent)
          .AddMethod(__Method_PlayMatch, serviceImpl.PlayMatch).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GameOnlineServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SearchOpponent, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::BoardGamesGrpc.GameOnlines.SearchOpponentRequest, global::BoardGamesGrpc.GameOnlines.SearchOpponentRespons>(serviceImpl.SearchOpponent));
      serviceBinder.AddMethod(__Method_CancelSearchOpponent, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest, global::BoardGamesGrpc.GameOnlines.CancelSearchOpponentRespons>(serviceImpl.CancelSearchOpponent));
      serviceBinder.AddMethod(__Method_PlayMatch, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::BoardGamesGrpc.GameOnlines.PlayMatchRequest, global::BoardGamesGrpc.GameOnlines.GamePlay>(serviceImpl.PlayMatch));
    }

  }
}
#endregion
