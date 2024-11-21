using Dapr.Client;
using Dapr.Workflow;
using MovieMicroservice.Activities.External;
using MovieMicroservice.Activities.Internal;
using MovieMicroservice.Models.DTOs;

namespace MovieMicroservice.Workflows
{
    public class FetchMovieWorkflow : Workflow<Guid,string>
    {
        public FetchMovieWorkflow()
        {

        }

        public override async Task<string> RunAsync(WorkflowContext context, Guid input)//input = movieid
        {
            //1. Get userid from identitymicroservice
            var userInfo = await context.CallActivityAsync<Guid>(nameof(GetUserInfoActivity));

            //2. Check for valid rental between user and movie
            RentalValidationRequest rentalValidationRequest = new RentalValidationRequest { MovieId = input, UserId = userInfo};
            var isValidRental = await context.CallActivityAsync<bool>(nameof(CheckValidRentalForUserActivity), rentalValidationRequest);

            if (isValidRental is false) 
            {
                throw new Exception("User does not have a rental for this movie! " +
                                    "Report them to the police immediately for trying to steal!");
            }

            //3. Fetch movielink
            var movieLink = await context.CallActivityAsync<string>(nameof(GetMovieLinkActivity),input);

            return movieLink;
        }
    }
}
