using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Desktop.Model
{
    internal class ServerConfigurationViewModel : IObservable<ServerConfigurationViewModel>
    {
        public string FriendlyName { get; set; }
        public JenkinsClient.JenkinsServerInfo ServerInfo { get; set; }
        public int RefreshInterval { get; set; }

        public List<JenkinsClient.BuildJob> Jobs;

        public IDisposable Subscribe(IObserver<ServerConfigurationViewModel> observer)
        {
            throw new NotImplementedException();
        }
    }
}
