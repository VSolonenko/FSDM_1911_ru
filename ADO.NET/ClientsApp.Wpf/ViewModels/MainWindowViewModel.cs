using ClientsApp.Wpf.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientsApp.Wpf.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private DisconnectedDbProvider provider;
        private ClientViewModel current;

        public DataView Data => provider?.GetView();

        public ClientViewModel CurrentClient { get => current; set => Set(ref current, value); }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand EditCommand { get; }

        public MainWindowViewModel(DisconnectedDbProvider provider)
        {
            this.provider = provider;

            RemoveCommand = new RelayCommand<DataRowView>(OnRemove);
            EditCommand = new RelayCommand<DataRowView>(OnEdit);
        }

        private void OnEdit(DataRowView rowView)
        {
            CurrentClient = CreateClient(rowView);


        }

        private ClientViewModel CreateClient(DataRowView rowView = null) => new ClientViewModel(ClearClient, rowView);

        private void ClearClient()
        {
            CurrentClient = null;
        }

        private void OnRemove(DataRowView rowView)
        {
            rowView.Row.Delete();
        }
    }
}
