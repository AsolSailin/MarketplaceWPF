using System;
using System.Collections.Generic;
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
using System.Drawing;

namespace MonkeyShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для QRPage.xaml
    /// </summary>
    public partial class QRPage : Page
    {
        public QRPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Кнопка создания QR кода
        {
            // Ссылка на XL таблицу
            string soucer_xl = "https://yandex.ru/games/app/197304?utm_campaign=rus_games_general-igra-bezkav_yandex_search_460.new%7C59207487&utm_medium=search&utm_source=yandex&utm_term=игра%20бесплатные%20на%20двоих&utm_content=k50id%7C0100000026526833461_26526833461%7Ccid%7C59207487%7Cgid%7C4460504509%7Caid%7C10267104105%7Cadp%7Cno%7Cpos%7Cpremium1%7Csrc%7Csearch_none%7Cdvc%7Cdesktop%7Cmain#app-id=197304&catalog-session-uid=catalog-5b690f4a-6db0-540a-9a67-f132b2746592-1697641824350-ae36&rtx-reqid=17925755948689185606&pos=%7B%22listType%22%3A%22suggested%22%2C%22tabCategory%22%3A%22common%22%7D&redir-data=%7B%22block%22%3A%22suggested%22%2C%22block_index%22%3A2%2C%22card%22%3A%22adaptive_recommended_new%22%2C%22col%22%3A4%2C%22first_screen%22%3A1%2C%22page%22%3A%22main%22%2C%22rn%22%3A197357445%2C%22row%22%3A0%2C%22rtx_reqid%22%3A%2217925755948689185606%22%2C%22wrapper%22%3A%22grid-list%22%2C%22http_ref%22%3A%22https%253A%252F%252Fyandex.ru%252Fgames%252F%253Fk50id%253D0100000026526833461_26526833461%2526yclid%253D10942574011121663999%22%7D";
            // Создание переменной библиотеки QRCoder
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            // Присваеваем значиения
            QRCoder.QRCodeData data = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
            // переводим в Qr
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            Bitmap bitmap = code.GetGraphic(100);
            /// Создание картинки
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
    }
}
