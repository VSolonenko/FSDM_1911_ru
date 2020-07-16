using ClientsApp.Wpf.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace ClientsApp.Wpf.ViewModels
{
    class CloseDialogWIndowViewModel: ViewModelBase
    {
        private readonly DisconnectedDbProvider provider;
        private readonly Action onFinish;

        public ICommand SaveCommand { get; }
        public ICommand DiscardCommand { get; }

        public CloseDialogWIndowViewModel(DisconnectedDbProvider provider, Action onFinish)
        {
            this.provider = provider;
            this.onFinish = onFinish;

            SaveCommand = new RelayCommand(OnSave);
            DiscardCommand = new RelayCommand(OnDiscard);
        }

        private void OnDiscard()
        {
            onFinish();
        }

        private void OnSave()
        {
            provider.Apply();
            onFinish();
        }
    }
}
