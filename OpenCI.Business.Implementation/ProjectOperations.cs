using System;
using AutoMapper;
using OpenCI.Business.Models;
using OpenCI.Data.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenCI.Business.Contracts;
using OpenCI.Exceptions;

namespace OpenCI.Business.Implementation
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

        public async Task<ProjectModel> CreateProject(CreateProjectModel model)
        {
            var entity = await _projectData.CreateProject(model).ConfigureAwait(false);

            var mappedModel = _mapper.Map<ProjectModel>(entity);

            return mappedModel;
        }

        public async Task<bool> DeleteProject(Guid projectGuid)
        {
            return await _projectData.DeleteProject(projectGuid).ConfigureAwait(false);
        }

        public async Task<List<ProjectModel>> GetAllProjects()
        {
            var entities = await _projectData.GetAllProjects().ConfigureAwait(false);

            var mappedModels = _mapper.Map<List<ProjectModel>>(entities);

            return mappedModels;
        }

        public async Task<ProjectModel> GetProject(Guid projectGuid)
        {
            var entity = await _projectData.GetProject(projectGuid).ConfigureAwait(false);

            if (entity == null) throw new EntityNotFoundException($"Unable to find the project with the guid: {projectGuid}");

            var mappedModel = _mapper.Map<ProjectModel>(entity);

            return mappedModel;
        }

        public async Task<ProjectModel> UpdateProject(Guid projectGuid, UpdateProjectModel model)
        {
            var entity = await _projectData.UpdateProject(projectGuid, model).ConfigureAwait(false);

            var mappedModel = _mapper.Map<ProjectModel>(entity);

            return mappedModel;
        }
    }
}
