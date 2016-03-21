using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Desktop.Model
{
    internal class ProjectModel
    {
        public DateTime LastChecked { get; set; }
        public JenkinsClient.BuildStatus LastStatus { get; set; }
        public bool IsEnabled { get; set; }
        public JenkinsClient.BuildJob BuildJob{ get; set; }

    }
}
