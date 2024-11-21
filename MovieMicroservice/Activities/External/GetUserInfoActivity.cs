using Dapr.Client;
using Dapr.Workflow;

namespace MovieMicroservice.Activities.External
{
    public class GetUserInfoActivity : WorkflowActivity<object, Guid>
    {
        private readonly DaprClient _daprClient;

        public GetUserInfoActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public override async Task<Guid> RunAsync(WorkflowActivityContext context, object input)
        {
            try
            {
                var userId = await _daprClient.InvokeMethodAsync<Guid>(
                    "identitymicroservice",
                    "api/account/userid"
                    );

                return userId;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error getting userId", ex);
            }
        }
    }
}
