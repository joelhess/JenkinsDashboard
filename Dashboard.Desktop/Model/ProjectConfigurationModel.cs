using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Desktop.Model
{
    internal class ProjectConfigurationModel
    {
        private List<ServerConfigurationViewModel> _servers;
        public List<ServerConfigurationViewModel> Servers
        {
            get
            {
                if (_servers == null)
                    _servers = new List<ServerConfigurationViewModel>();

                return _servers;
            }
            set
            {
                _servers = value;
            }
        }

        private List<JenkinsClient.BuildJob> _buildJobs;
        public List<JenkinsClient.BuildJob> BuildJobs
        {
            get
            {
                if (_buildJobs == null)
                    _buildJobs = new List<JenkinsClient.BuildJob>();

                return _buildJobs;
            }
            set { _buildJobs = value; }
        }
    }
}
