using System.Threading.Tasks;

namespace OpenCI.API.Rest.Tests.Controllers.Contracts
{
    public interface IProjectControllerTest
    {
        Task GetProject_ShouldReturnTheCorrectProject();
        Task GetProject_ShouldReturnBadRequestIfTheEntityDoesNotExist();

        Task GetAllProjects_ShouldReturnTheCorrectListOfProject();

        Task CreateProject_ShouldReturnTheCreatedProject();

        Task DeleteProject_ShouldReturnSuccessIfTheDeleteIsSuccessfull();
        Task DeleteProject_ShouldReturnBadRequestIfTheDeleteIsUnsuccessfull();

        Task UpdateProject_ShouldReturnTheUpdatedProject();
        Task UpdateProject_ShouldReturnBadRequestIfTheEntityDoesNotExist();
    }
}
