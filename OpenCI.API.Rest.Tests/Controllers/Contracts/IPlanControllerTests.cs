using System.Threading.Tasks;

namespace OpenCI.API.Rest.Tests.Controllers.Contracts
{
    public interface IPlanControllerTests
    {
        Task GetPlan_ShouldReturnTheCorrectPlan();
        Task GetPlan_ShouldReturnBadRequestIfTheEntityDoesNotExist();
        Task GetAllPlans_ShouldReturnTheCorrectListOfProject();
        Task CreatePlan_ShouldReturnTheCreatedPlan();
        Task DeletePlan_ShouldReturnSuccessIfTheDeleteIsSuccessfull();
        Task DeletePlan_ShouldReturnBadRequestIfTheDeleteIsUnsuccessfull();
        Task UpdatePlan_ShouldReturnTheUpdatedProject();
        Task UpdatePlan_ShouldReturnBadRequestIfTheEntityDoesNotExist();
    }
}
