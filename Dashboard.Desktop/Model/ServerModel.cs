using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Desktop.Model
{
    public class ServerModel
    {
        public string FriendlyName { get; set; }
        public JenkinsClient.JenkinsServerInfo ServerInfo { get; set; }
        public int RefreshInterval { get; set; }

        public ObservableCollection<ProjectModel> Projects { get; set; }

        public override string ToString()
        {
            return FriendlyName;
        }
    }
}
