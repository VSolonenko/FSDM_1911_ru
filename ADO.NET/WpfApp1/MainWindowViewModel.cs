using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class MainWindowViewModel
    {
        public ObservableCollection<NameViewModel> Names { get; }

        public MainWindowViewModel()
        {
            Names = new ObservableCollection<NameViewModel>();
        }

        internal void Fill(AsyncSample asyncSample)
        {
            //asyncSample.Fill(Names);
            asyncSample.FillAsync(Names);
        }
    }
}
