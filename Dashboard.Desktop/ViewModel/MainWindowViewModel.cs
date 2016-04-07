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
using System.Windows.Threading;

namespace Dashboard.Desktop.ViewModel
{
    internal class MainWindowViewModel : BindableBase
    {
        public string PersistanceFileName { get { return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jenkins.DesktopClient\\JenkinsData.xml"); } }

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
                        await Task.Run(async () =>
                        {
                            JenkinsClient.JenkinsDataLoader jdl = new JenkinsClient.JenkinsDataLoader(server.ServerInfo);
                            project.BuildJob = await jdl.GetProjectData(project.ProjectUri.ToString());
                            project.FriendlyName = project.BuildJob.displayName;

                            var lastBuildInfo = await jdl.GetBuildInformation(project.BuildJob.lastBuild.url);

                            project.LastProjectStatus = lastBuildInfo.building ? "Building" : "Idle";

                            project.LastChecked = DateTime.Now;
                            project.LastBuildStatus = (JenkinsClient.BuildStatus)Enum.Parse(typeof(JenkinsClient.BuildStatus), lastBuildInfo.result, true);
                        }
                        );
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }

            MainWindowModel.Save(PersistanceFileName);
        }

        public MainWindowViewModel(string path)
        {
            MainWindowModel = new Model.MainWindowModel();
            if (System.IO.File.Exists(PersistanceFileName))
            {
                MainWindowModel.Load(PersistanceFileName);
            }
            else
            {

                MainWindowModel.Servers = new List<Model.ServerModel>()
            {
                new Model.ServerModel() {FriendlyName= "Jenkins.SpokVDev",
                    ServerInfo = new JenkinsClient.JenkinsServerInfo() {JenkinsServer="http://jenkins.spokvdev.com/", UserName = "joel.hess", ApiToken = "1e76a66f0565b3dbf3f741922b0f9435"},
                    Projects = new List<Model.ProjectModel>()
                    {
                        new Model.ProjectModel() {ProjectUri = "http://jenkins.spokvdev.com/job/Spok.Mobile.Build-4.4/" },
                        new Model.ProjectModel() {ProjectUri = "http://jenkins.spokvdev.com/job/Spok.Mobile.Installer-4.4/" },
                        new Model.ProjectModel() {ProjectUri = "http://jenkins.spokvdev.com/job/Spok.Mobile.Tests-4.4/" }
                    }
                }

                //new Model.ServerModel() {FriendlyName= "Franklin",
                //    ServerInfo = new JenkinsClient.JenkinsServerInfo() {JenkinsServer="https://ci.jenkins-ci.org/" },
                //    Projects = new ObservableCollection<Model.ProjectModel>()
                //    {new Model.ProjectModel() {ProjectUri = new Uri("https://ci.jenkins-ci.org/job/jenkins_main_trunk/") } } 
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
            StartDispatchTimer();
        }

        private void StartDispatchTimer()
        {
            DispatcherTimer tmr = new DispatcherTimer(DispatcherPriority.Background);
            tmr.Interval = new TimeSpan(0, 0, 30);
            tmr.Tick += Tmr_Tick;
            tmr.Start();
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            RefreshData(null);
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
