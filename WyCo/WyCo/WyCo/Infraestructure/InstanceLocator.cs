using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WyCo.ViewModels;

namespace WyCo.Infraestructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
