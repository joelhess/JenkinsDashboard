using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Desktop.Model
{
    public class MainWindowModel
    {
        public ObservableCollection<ServerModel> Servers { get; set; }
    }
}
