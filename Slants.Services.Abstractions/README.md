# Slants.Services.Abstractions
models and services using gRPC (Code-first) to use by WASM components

## Why abstractions? 
To most, this is going to seem incorrect because some of these models are not interfaces. The reason for this is that the models are not the abstraction, the services are. The models are just a way to share the data contracts between the client and server. The services are the abstraction because they are the ones that are going to be used by the client. The client doesn't care about the models, it cares about the services. The models are just a way to share the data contracts between the client and server.


The overall architecture would benefit from another separation of concerns. Creating separate model classes within the WASM application would help with that. Creating separate data entities as well.
