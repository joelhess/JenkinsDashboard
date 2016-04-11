using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dashboard.Desktop.ViewModels
{
    internal class ProjectsConfigurationViewModel : BindableBase, IInteractionRequestAware
    {
        public ICommand SelectItemCommand { get; private set; }

        public ProjectsConfigurationViewModel()
        {
            this.SelectItemCommand = new DelegateCommand(this.AcceptSelectedItem);
        }
         
        public void AcceptSelectedItem()
        {

        }


        //public MainWindowModel MainModel { get; set; }
        public Action FinishInteraction
        {
             get; set; 

        }

        public INotification Notification
        {
            get;set;
        }
    }
}
