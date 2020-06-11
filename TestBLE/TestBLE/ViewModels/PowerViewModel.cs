using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TestBLE.ViewModels
{
    public class PowerViewModel
    {

        private string _statusText = "OFF";


        public string Status
        {
            get => _statusText;
            set
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }
        public Command DisplayStatus
        {
            get
            {
                return new Command(() =>
                {
                    Status = "On";
                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
