using MonkeyShop.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        private Order CurrentOrder { get; set; }

        public IssuancePage()
        {
            InitializeComponent();
        }

        private void CreateQRCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderNumber = int.Parse(tboxNumber.Text);
                CurrentOrder = App.Connection.Order.Where(x => x.Id == orderNumber).FirstOrDefault();

                if (CurrentOrder != null)
                {
                    string soucer_xl = "https://www.ozon.ru/search/?from_global=true&text=" + tboxNumber.Text;
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
                else
                {
                    MessageBox.Show("Неверный номер заказа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch 
            {
                MessageBox.Show("Неверные данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetOrder_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentOrder != null)
            {
                CurrentOrder.Status_Id = 5;

                App.Connection.Order.AddOrUpdate(CurrentOrder);
                App.Connection.SaveChanges();
                MessageBox.Show("Заказ выдан!");
                tboxNumber.Text = "";
                imageQr.Source = null;
            }
            else
            {
                MessageBox.Show("Невозможно оформить заказ, так как его не существует!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
