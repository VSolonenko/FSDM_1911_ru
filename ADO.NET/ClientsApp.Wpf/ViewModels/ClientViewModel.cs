using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Data;
using System.Windows.Input;

namespace ClientsApp.Wpf.ViewModels
{
    class ClientViewModel : ViewModelBase
    {
        private readonly Action onFinish;
        private DataRowView clientRow;

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public ICommand ApplyCommand { get; }
        public ICommand DiscardCommand { get; }

        public ClientViewModel(Action onFinish, DataRowView clientRowView = null)
        {
            this.onFinish = onFinish;
            clientRow = clientRowView;

            ApplyCommand = new RelayCommand(OnApply);
            DiscardCommand = new RelayCommand(OnDiscard);

            if (clientRow != null)
            {
                Name = clientRow.Row.ItemArray[1] as string;
                LastName = clientRow.Row.ItemArray[2] as string;
                Phone = clientRow.Row.ItemArray[3] as string;
            }
        }

        private void OnDiscard()
        {
            onFinish();
        }

        private void OnApply()
        {
            var result = new object[] { clientRow.Row.ItemArray[0], Name, LastName, Phone };
            clientRow.Row.ItemArray = result;
            onFinish();
        }
    }
}
