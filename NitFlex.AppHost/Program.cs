//Tilf�j Aspire apphost project, h�jreklik p� microservicesprojeker og add aspire orchestration support eller hvad det hedder

var builder = DistributedApplication.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore("statestore");
var pubsub = builder.AddDaprPubSub("pubsub");



builder.AddProject<Projects.MovieMicroservice>("moviemicroservice")
  .WithDaprSidecar()
  .WithReference(pubsub)
  .WithReference(stateStore);


builder.AddProject<Projects.RentalMicroservice>("rentalmicroservice")
  .WithDaprSidecar()
  .WithReference(pubsub)
  .WithReference(stateStore);


builder.AddProject<Projects.IdentityMicroservice>("identitymicroservice")
  .WithDaprSidecar()
  .WithReference(pubsub)
  .WithReference(stateStore);


builder.Build().Run();
