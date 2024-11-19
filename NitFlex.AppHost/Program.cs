//Tilføj Aspire apphost project, højreklik på microservicesprojeker og add aspire orchestration support eller hvad det hedder

var builder = DistributedApplication.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore("statestore");
var pubsub = builder.AddDaprPubSub("pubsub");

builder.AddProject<Projects.MovieMicroservice>("moviemicroservice")
  .WithDaprSidecar()
  .WithReference(pubsub);


builder.AddProject<Projects.RentalMicroservice>("rentalmicroservice")
  .WithDaprSidecar()
  .WithReference(pubsub);

builder.AddProject<Projects.IdentityMicroservice>("identitymicroservice")
  .WithDaprSidecar()
  .WithReference(pubsub);

builder.Build().Run();
