using Dapr.Client;
using Dapr.Workflow;
using MovieMicroservice.Activities.External;
using MovieMicroservice.Activities.Internal;
using MovieMicroservice.Models.DTOs;

namespace MovieMicroservice.Workflows
{
    public class FetchMovieWorkflow : Workflow<RentalValidationRequest,string>
    {
        public FetchMovieWorkflow()
        {

        }

        public override async Task<string> RunAsync(WorkflowContext context, RentalValidationRequest input)//input = movieid
        {
            //1. Check for valid rental between user and movie
            var isValidRental = await context.CallActivityAsync<bool>(nameof(CheckValidRentalForUserActivity), input);

            if (isValidRental is false) 
            {
                throw new Exception("User does not have a rental for this movie! " +
                                    "Report them to the police immediately for trying to steal!");
            }

            //2. Fetch movielink
            var movieLink = await context.CallActivityAsync<string>(nameof(GetMovieLinkActivity),input.MovieId);

            return movieLink;
        }
    }
}
