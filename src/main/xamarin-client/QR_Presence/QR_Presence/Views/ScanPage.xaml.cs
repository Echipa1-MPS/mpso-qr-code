using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace QR_Presence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ZXingScannerPage
    {

        int isScanned = 0;
        public ScanPage()
        {
            InitializeComponent();
        }

        private void ZXingScannerPage_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (isScanned == 0)
                {
                    string[] vars = result.Text.Split('/');
                    isScanned = 1;
                }
               // await DisplayAlert("Scanned Barcode", $"{result.Text} (type: {result.BarcodeFormat.ToString()})", "OK");
                await Navigation.PopAsync();
            });
        }
    }
}