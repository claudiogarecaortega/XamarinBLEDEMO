using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.BLE.Abstractions.Contracts;

namespace TestBLE.ViewModels
{
    public class DeviceViewModel : INotifyPropertyChanged
    {
        private IDevice _nativeDevice;
        public event PropertyChangedEventHandler PropertyChanged;
        public IDevice NativeDevice
        {
            get => _nativeDevice;
            set
            {
                _nativeDevice = value;
                RaisePropertyChanged();
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
