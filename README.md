# Slants
A Blazor web application used to discuss some blazor concepts, features, and some of my favorite ways of using it.

## Todo List
- [x] Implement a Blazor WebAssembly project (Slants.WebApp), shared razor library (Slants.Core), and html UI design for basic site
- [x] Implement a service abstraction layer using gRPC code-first. (Slants.Services) Mock these abstractions on the server to provide fulling functional gRPC service layer to WASM client.
- [x] Implement a complex Blazor component that uses javascript interop and test this using BUnit and Xunit unit tests. [MobileAdaptiveLayout](Slants.Core/Layouts/MobileAdaptiveLayout.md)
- [x] Implement a multi-platform Maui Blazor app project using same shared code as web app. (Slants.MPApp)
- [x] Implement form examples. ex. allowing customer to add new slants.
- [ ] Implement Domain and Data layer using Entity Framework, then use these with non-mocked service layer. Move existing mocked services to a mocked service library. (Slants and Slants.Data)
- [ ] Add security for both apps utilizing Duende instance.
    - [ ] Implement new web project for it. (Slants.IdentityServer)
    - [ ] Add a SwitchStartup Visual Studio configurations to help with multi-project startup configuration.
    - [ ] Implement Login/Logout/Signup
- [ ] Show ability to work with some additional frameworks. 
    - [ ] testing: NSubstitute, Moq, BUnit, Xunit
    - [ ] layering and dependencies: Automapper, MediatR
- [ ] Document everything thoroughly.


