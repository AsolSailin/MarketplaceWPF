using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonkeyShop.Pages.UserPages.EmployeePages
{
    /// <summary>
    /// Логика взаимодействия для IssuancePage.xaml
    /// </summary>
    public partial class IssuancePage : Page
    {
        public IssuancePage()
        {
            InitializeComponent();
        }

        private void CreateQRCode_Click(object sender, RoutedEventArgs e)
        {
            string soucer_xl = "https://www.ozon.ru/search/?from_global=true&text="; 
            //https://www.ozon.ru/search/?from_global=true&text=саше+лаванды+и+хвои
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData data = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            Bitmap bitmap = code.GetGraphic(100);

            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                imageQr.Source = bitmapimage;
            }
        }

        private void GetOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
