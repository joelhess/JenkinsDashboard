using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dashboard.Desktop.ViewModel
{
    internal class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {

        }

        public ICommand CmdRefreshData
        {
            get { return new DelegateCommand<object>(RefreshData); }
        }

        private async void RefreshData(object obj)
        {
            foreach (var server in MainWindowModel.Servers)
            {
                foreach (var project in server.Projects)
                {
                    try
                    {
                        await Task.Run(async() =>
                        {
                            JenkinsClient.JenkinsDataLoader jdl = new JenkinsClient.JenkinsDataLoader(server.ServerInfo);
                            project.BuildJob = await jdl.GetProjectData(project.ProjectUri.ToString());

                            var lastBuildInfo = await jdl.GetBuildInformation(project.BuildJob.lastBuild.url);

                            project.LastChecked = DateTime.Now;
                            project.LastStatus = (JenkinsClient.BuildStatus)Enum.Parse(typeof(JenkinsClient.BuildStatus), lastBuildInfo.result, true);
                        }
                        );
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
        }

        public MainWindowViewModel(string path)
        {
            MainWindowModel = new Model.MainWindowModel();
            MainWindowModel.Servers = new ObservableCollection<Model.ServerModel>()
            {
                new Model.ServerModel() {FriendlyName= "Franklin",
                    ServerInfo = new JenkinsClient.JenkinsServerInfo() {JenkinsServer="https://ci.jenkins-ci.org/" },
                    Projects = new ObservableCollection<Model.ProjectModel>()
                    {new Model.ProjectModel() {ProjectUri = new Uri("https://ci.jenkins-ci.org/job/jenkins_main_trunk/") } } }
                //new Model.ServerModel() {FriendlyName = "Alfred",
                //    Projects = new ObservableCollection<Model.ProjectModel>()
                //    {new Model.ProjectModel()
                //    { BuildJob = new JenkinsClient.BuildJob() {displayName = "Job 1" },
                //        LastChecked = DateTime.Now,
                //        LastStatus = JenkinsClient.BuildStatus.None
                //    }
                //    }
                //}
            };
        }

        public ICommand CmdShowConfigure
        {
            get { return new DelegateCommand<object>(ShowConfigureWindow); }
        }

        private void ShowConfigureWindow(object context)
        {
            Views.ProjectsConfiguration projectConfigView = new Views.ProjectsConfiguration(null);

            var result = projectConfigView.ShowDialog();

            Debug.WriteLine("Recieved  Command");
        }


        public EventHandler RequestClose;



        public Model.MainWindowModel MainWindowModel { get; set; }

    }
}
