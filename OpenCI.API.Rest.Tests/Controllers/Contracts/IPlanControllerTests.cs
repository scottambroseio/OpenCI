using System.Threading.Tasks;

namespace OpenCI.API.Rest.Tests.Controllers.Contracts
{
    public interface IPlanControllerTests
    {
        Task GetPlan_ShouldReturnTheCorrectPlan();
        Task GetPlan_ShouldReturnBadRequestIfTheEntityDoesNotExist();
        Task GetAllPlans_ShouldReturnTheCorrectListOfPlans();
        Task CreatePlan_ShouldReturnTheCreatedPlan();
        Task DeletePlan_ShouldReturnSuccessIfTheDeleteIsSuccessful();
        Task DeletePlan_ShouldReturnBadRequestIfTheDeleteIsUnsuccessful();
        Task UpdatePlan_ShouldReturnTheUpdatedPlan();
        Task UpdatePlan_ShouldReturnBadRequestIfTheEntityDoesNotExist();
    }
}
