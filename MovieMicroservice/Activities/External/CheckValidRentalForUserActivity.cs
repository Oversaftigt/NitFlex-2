using Dapr.Client;
using Dapr.Workflow;
using MovieMicroservice.Models.DTOs;

namespace MovieMicroservice.Activities.External
{
    public class CheckValidRentalForUserActivity : WorkflowActivity<RentalValidationRequest, bool>
    {
        private readonly DaprClient _daprClient;

        public CheckValidRentalForUserActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public override async Task<bool> RunAsync(WorkflowActivityContext context, RentalValidationRequest input)
        {
            try
            {
                var isValidrental = await _daprClient.InvokeMethodAsync<RentalValidationRequest, bool>(
                    "rentalmicroservice",
                    "api/rental/validate",
                    input
                    );

                return isValidrental;
            }
            catch (Exception ex)
            {

                throw new Exception("Error validating rental", ex);
            }
        }
    }
}
