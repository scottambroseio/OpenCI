using OpenCI.Contracts.Data;
using Dapper;

namespace OpenCI.Implementation.Data
{
    public class ProjectData : IProjectData
    {
        private IConnectionHelper _connectionHelper;

        public ProjectData(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }
    }
}
