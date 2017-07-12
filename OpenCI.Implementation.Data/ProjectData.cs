using System;
using Dapper;
using OpenCI.Data.Contracts;
using OpenCI.Data.Entities;

namespace OpenCI.Data.Implementation
{
    public class ProjectData : IProjectData
    {
        private IConnectionHelper _connectionHelper;

        public ProjectData(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public Project GetProjectById(int id)
        {
            // db call
            var entity = new Project()
            {
                Id = id,
                Name = "Spme Project"
            };

            return entity;
        }
    }
}
