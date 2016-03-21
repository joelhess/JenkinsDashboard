using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JenkinsClient;

namespace Dashboard.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnConfigure_Click(object sender, RoutedEventArgs e)
        {
            var jenkinsServer = new JenkinsServerInfo();
            jenkinsServer.JenkinsServer = "https://ci.jenkins-ci.org/";
            Model.ProjectConfigurationModel projectconfig = new Model.ProjectConfigurationModel();
            
            Model.ServerConfigurationViewModel model = new Model.ServerConfigurationViewModel() { FriendlyName="Jenkins CI", ServerInfo = jenkinsServer };
            projectconfig.Servers.Add(model);

            Views.ProjectsConfiguration projectConfigView = new ProjectsConfiguration(projectconfig);
            projectConfigView.Owner = this;
            var result = projectConfigView.ShowDialog();
        }

    }
}