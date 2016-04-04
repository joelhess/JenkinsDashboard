using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Desktop.Model
{
    public class ProjectModel :Prism.Mvvm.BindableBase
    {
        private DateTime _lastChecked;
        public DateTime LastChecked { get { return _lastChecked; } set { SetProperty(ref this._lastChecked, value); } }

        private JenkinsClient.BuildStatus _lastStatus;
        public JenkinsClient.BuildStatus LastStatus { get { return _lastStatus; } set { SetProperty(ref this._lastStatus, value); } }
        public bool IsEnabled { get; set; }
        public Uri ProjectUri { get; set; }

        private JenkinsClient.BuildJob _buildJob;
        public JenkinsClient.BuildJob BuildJob { get { return _buildJob; } set { SetProperty(ref _buildJob, value); } }
    }
}
