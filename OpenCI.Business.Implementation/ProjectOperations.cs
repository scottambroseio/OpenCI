using AutoMapper;
using OpenCI.Business.Models;
using OpenCI.Contracts.Business;
using OpenCI.Data.Contracts;

namespace OpenCI.Implementation.Business
{
    public class ProjectOperations : IProjectOperations
    {
        private readonly IProjectData _projectData;
        private readonly IMapper _mapper;

        public ProjectOperations(IProjectData projectData, IMapper mapper)
        {
            _projectData = projectData;
            _mapper = mapper;
        }

        public ProjectModel GetProjectById(int id)
        {
            var entity = _projectData.GetProjectById(id);
            var model = _mapper.Map<ProjectModel>(entity);

            return model;
        }
    }
}
