using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TestBLE.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _labelText = "Hello this is a demo";

        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                RaisePropertyChanged();
            }
        }
        public MainViewModel()
        {
        }
        private Command changeTextCommand { get; set; }
        public Command ChangeTextCommand
        {
            get
            {
                return changeTextCommand ?? (changeTextCommand = new Command(() => { LabelText = "GoddBye"; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
