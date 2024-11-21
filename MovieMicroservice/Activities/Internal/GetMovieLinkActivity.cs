using Dapr.Workflow;
using MovieMicroservice.Services;

namespace MovieMicroservice.Activities.Internal
{
    public class GetMovieLinkActivity : WorkflowActivity<Guid, string>
    {
        private readonly IMovieService _movieService;

        public GetMovieLinkActivity(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public override async Task<string> RunAsync(WorkflowActivityContext context, Guid input)
        {
            var movieLink = _movieService.GetMovieLinkById(input);
            return movieLink;
        }
    }
}
