using System;
using System.Collections.Generic;
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
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya.Pages
{
    /// <summary>
    /// Логика взаимодействия для MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Page
    {
        public MainWindow mainWindow;
        double summa = 0;
        double summa2 = 0;
        double sumDd = 0;
        public MakeOrder(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            for (int j = 1; j < mainWindow.carts.Count(); j++)
            {
                if (mainWindow.carts[j].id == 0)
                {
                }
                else
                {
                    sumDd += (mainWindow.carts[j].price * mainWindow.carts[j].discount * 0.01) * mainWindow.carts[j].count;
                    summa += (mainWindow.carts[j].price - (mainWindow.carts[j].price * mainWindow.carts[j].discount * 0.01)) * mainWindow.carts[j].count;
                    summa2 += (mainWindow.carts[j].price) * mainWindow.carts[j].count;
                }
            }
            sum.Content = "Общая сумма заказа: " + summa.ToString();
            CreateSchema();
        }

        public void CreateSchema()
        {
            for (int i = 1; i < mainWindow.carts.Count(); i++)
            {
                if (mainWindow.carts[i].id == 0)
                {
                }
                else
                {
                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Productz.Children.Add(parent_grid);

                    System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                    name.Content = "Кол-во: " + mainWindow.carts[i].count;
                    name.Margin = new Thickness(10, 45, 10, 0);
                    name.FontWeight = FontWeights.Bold;
                    name.FontSize = 20;
                    name.Foreground = Brushes.Black;
                    name.FontFamily = new FontFamily("Comic Sans MS");
                    parent_grid.Children.Add(name);

                    System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                    about.Content = "Название: " + mainWindow.carts[i].name;
                    about.Margin = new Thickness(10, 115, 10, 0);
                    about.VerticalAlignment = VerticalAlignment.Top;
                    about.Foreground = Brushes.Black;
                    about.FontSize = 20;
                    parent_grid.Children.Add(about);

                    System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                    type.Content = "Производитель: " + mainWindow.carts[i].manufacturer;
                    type.Margin = new Thickness(10, 80, 10, 0);
                    type.VerticalAlignment = VerticalAlignment.Top;
                    type.Foreground = Brushes.Black;
                    type.FontSize = 20;
                    parent_grid.Children.Add(type);

                    System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                    video.Content = "Цена с учётом скидки: " + (mainWindow.carts[i].price - (mainWindow.carts[i].price * mainWindow.carts[i].discount * 0.01)).ToString();
                    video.Margin = new Thickness(10, 150, 10, 0);
                    video.VerticalAlignment = VerticalAlignment.Top;
                    video.Foreground = Brushes.Black;
                    video.FontSize = 20;
                    parent_grid.Children.Add(video);

                    System.Windows.Controls.Label video1 = new System.Windows.Controls.Label();
                    video1.Content = "Описание: " + mainWindow.carts[i].description;
                    video1.Margin = new Thickness(10, 185, 10, 0);
                    video1.VerticalAlignment = VerticalAlignment.Top;
                    video1.Foreground = Brushes.Black;
                    video1.FontSize = 20;
                    parent_grid.Children.Add(video1);
                }
            }
        }

        private void ConOrder_Click(object sender, RoutedEventArgs e)
        {

            if (!Convert.ToBoolean(Conf.IsChecked))
            {
                MessageBox.Show("Подтвердите согласие на персональную обработку данных!");
            }
            else
            {
                Random rand = new Random();
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("MS Comic Sans", 12);
                XFont fontX = new XFont("MS Comic Sans", 20, XFontStyle.Bold);
                double Width = 0;
                double Height = 0;
                double sum = 0;
                double sumD = 0;
                gfx.DrawString("Состав заказа: ", font, XBrushes.Black, new XRect(5, 5, Width, Height), XStringFormats.CenterLeft);
                for (int i = 1; i < mainWindow.carts.Count(); i++)
                {
                    Width += 40;
                    Height += 40;
                    sum += (mainWindow.carts[i].price - (mainWindow.carts[i].price * mainWindow.carts[i].discount * 0.01));
                    sumD += (mainWindow.carts[i].price * mainWindow.carts[i].discount * 0.01);
                    gfx.DrawString(mainWindow.carts[i].name +" | "+mainWindow.carts[i].article + " | "+ mainWindow.carts[i].count + "* | " + mainWindow.carts[i].price, font, XBrushes.Black, new XRect(5, 5, Width, Height), XStringFormats.CenterLeft);
                }
                Width += 40;
                Height += 40;
                gfx.DrawString("Дата заказа: " + DateTime.Now.Date.ToString(), font, XBrushes.Black, new XRect(5, 5, Width, Height), XStringFormats.CenterLeft);
                Width += 40;
                Height += 40;
                gfx.DrawString("Номер заказа: " + mainWindow.Context.Order.Count(), font, XBrushes.Black, new XRect(5, 5, Width, Height), XStringFormats.CenterLeft);
                Width += 40;
                Height += 40;
                gfx.DrawString("Сумма заказа: " + summa.ToString(), font, XBrushes.Black, new XRect(5, 5, Width, Height), XStringFormats.CenterLeft);
                Width += 40;
                Height += 40;
                gfx.DrawString("Сумма заказа без скидки: " + summa2.ToString(), font, XBrushes.Black, new XRect(5, 5, Width, Height), XStringFormats.CenterLeft);
                Width += 40;
                Height += 40;
                gfx.DrawString("Сумма скидки: " + sumDd.ToString(), font, XBrushes.Black, new XRect(5, 5, Width, Height), XStringFormats.CenterLeft);
                Width += 40;
                Height += 40;
                Width += 40;
                Height += 40;
                Width += 40;
                Height += 40;
                gfx.DrawString("Код получения: " + rand.Next(1,1000).ToString(), fontX, XBrushes.Black, new XRect(0, 0, Width, Height), XStringFormats.CenterLeft);
                string filename = "Чек "+mainWindow.Context.Order.Count()+".pdf";
                Clasess.Orders orders = new Clasess.Orders();
                {
                    orders.Order_Date = DateTime.Today.ToString("dd.MM.yyyy");
                    orders.Date_of_issue = DateTime.Today.AddDays(6).ToString("dd.MM.yyyy");
                    orders.Point = mainWindow.cart.p;
                    orders.Client = mainWindow.helpclass.Uid;
                    Clasess.ApplicationContext db = new Clasess.ApplicationContext();
                    orders.Code = (211 + mainWindow.Context.Order.Count()).ToString();
                    orders.Status = "Новый";
                };
                Clasess.OrderContents content = new Clasess.OrderContents();
                {
                    content.order_id = mainWindow.Context.OrderContent.Count() + 1;
                    for (int i = 0; i < mainWindow.carts.Count(); i++)
                    { content.article += " " + mainWindow.carts[i].article; }
                    content.count = mainWindow.carts.Count();
                }
                try
                {
                    mainWindow.Context.OrderContent.Add(content);
                    mainWindow.Context.Order.Add(orders);
                    mainWindow.Context.SaveChanges();
                    document.Save(filename);
                    Process.Start(filename);
                    MessageBox.Show("Заказ успешно оформлен!");
                    mainWindow.carts.Clear();
                    mainWindow.AddNull();
                    mainWindow.OpenPages(MainWindow.pages.main);
                }
                catch { MessageBox.Show("Что-то пошло не так, попробуйте позже"); }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.cart);
        }
    }
}
