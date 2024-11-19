var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MovieMicroservice>("moviemicroservice");

builder.AddProject<Projects.RentalMicroservice>("rentalmicroservice");

builder.AddProject<Projects.IdentityMicroservice>("identitymicroservice");

builder.Build().Run();
