using System;
using AutoMapper;
using OpenCI.Business.Models;
using OpenCI.Contracts.Business;
using OpenCI.Data.Contracts;
using System.Threading.Tasks;

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

        public async Task<ProjectModel> GetProject(Guid guid)
        {
            var entity = await _projectData.GetProject(guid).ConfigureAwait(false);

            if (entity == null) return null;

            var model = _mapper.Map<ProjectModel>(entity);

            return model;
        }
    }
}
