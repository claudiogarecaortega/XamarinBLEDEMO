using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestBLE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceView : ContentPage
    {
        readonly IBluetoothLE _bluetoothLe;
        readonly IAdapter _adapter;
        readonly ObservableCollection<IDevice> _deviceList;
        IDevice _device;
        IReadOnlyList<IService> _services;
        IService _service;
        IReadOnlyList<ICharacteristic> _characteristics;
        ICharacteristic _characteristic;
        IDescriptor _descriptor;
        IReadOnlyList<IDescriptor> _descriptors;

        public DeviceView()
        {
            InitializeComponent();
            _bluetoothLe = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
            _deviceList = new ObservableCollection<IDevice>();
            ListViewItems.ItemsSource = _deviceList;
        }

        private void btnStatus_Clicked(object sender, EventArgs e)
        {
            var state = _bluetoothLe.State;

            DisplayAlert("Notice", state.ToString(), "OK !");
            if (state != BluetoothState.Off) return;

            TxtErrorBle.BackgroundColor = Color.Red;
            TxtErrorBle.TextColor = Color.White;
            TxtErrorBle.Text = "Your Bluetooth is off ! Turn it on !";
        }

        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                _deviceList.Clear();
                _adapter.DeviceDiscovered += (s, a) =>
                {
                    _deviceList.Add(a.Device);
                };

                if (!_bluetoothLe.Adapter.IsScanning)
                {
                    await _adapter.StartScanningForDevicesAsync();

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Notice", ex.Message, "Error !");
            }
        }

        private async void btnConnect_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_device == null)
                    throw new Exception();

                await _adapter.ConnectToDeviceAsync(_device);
            }
            catch (DeviceConnectionException ex)
            {
                await DisplayAlert("Notice", ex.Message, "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Notice", "No Device selected !", "OK");
            }
        }

        private async void btnKnowConnect_Clicked(object sender, EventArgs e)
        {
            try
            {
                await _adapter.ConnectToKnownDeviceAsync(new Guid("guid"));

            }
            catch (DeviceConnectionException ex)
            {
                await DisplayAlert("Notice", ex.Message, "OK");
            }
        }

        private async void btnGetServices_Clicked(object sender, EventArgs e)
        {
            _services = await _device.GetServicesAsync();

            _service = await _device.GetServiceAsync(_device.Id);
        }

        private async void btnGetCharacters_Clicked(object sender, EventArgs e)
        {
            var characteristics = await _service.GetCharacteristicsAsync();
            Guid idGuid = Guid.Parse("guid");
            _characteristic = await _service.GetCharacteristicAsync(idGuid);
        }

        private async void btnGetRW_Clicked(object sender, EventArgs e)
        {
            var bytes = await _characteristic.ReadAsync();
            await _characteristic.WriteAsync(bytes);


        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            _characteristic.ValueUpdated += (o, args) =>
            {
                var bytes = args.Characteristic.Value;
            };
            await _characteristic.StartUpdatesAsync();
        }

        private async void btnDescriptors_Clicked(object sender, EventArgs e)
        {
            _descriptors = await _characteristic.GetDescriptorsAsync();
            _descriptor = await _characteristic.GetDescriptorAsync(Guid.Parse("guid"));
        }

        private async void btnDescRW_Clicked(object sender, EventArgs e)
        {
            var bytes = await _descriptor.ReadAsync();
            await _descriptor.WriteAsync(bytes);
        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (ListViewItems.SelectedItem == null)
            {
                return;
            }
            _device = ListViewItems.SelectedItem as IDevice;
        }
    }
}