using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Kursovaya.Pages
{
    /// <summary>
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        public MainWindow mainWindow;
        public Cart(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            CreateSchema();

            var points = mainWindow.Context.Point.ToList();
            foreach (var p in points)
            {
                selectz.Items.Add(p.city + "|" + p.street + "|" + p.house);
            }
        }

        private void CreateSchema()
        {
            for (int i = 1; i < mainWindow.carts.Count(); i++)
            {
                if (mainWindow.carts[i].id == 0)
                {
                }
                else
                {
                    double summa = 0;
                    for (int j = 1; j < mainWindow.carts.Count(); j++)
                    {
                        if (mainWindow.carts[j].id == 0)
                        {
                        }
                        else
                        {
                            summa += (mainWindow.carts[j].price - (mainWindow.carts[j].price * mainWindow.carts[j].discount * 0.01)) * mainWindow.carts[j].count;
                        }
                    }
                    sum.Content = "Общая сумма заказа: " + summa.ToString();

                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Productz.Children.Add(parent_grid);

                    Image image = new Image();
                    image.Width = 50;
                    image.Height = 50;
                    image.Margin = new Thickness(10, 10, 10, 10);
                    image.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    image.VerticalAlignment = VerticalAlignment.Top;
                    if (mainWindow.imageConverter.LoadImage(mainWindow.carts[i].img_src) == null)
                    {
                        image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + (@"\picture.png")));
                    }
                    else
                    {
                        image.Source = mainWindow.imageConverter.LoadImage(mainWindow.carts[i].img_src);
                    }
                    Grid.SetColumn(image, 1);
                    parent_grid.Children.Add(image);

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

                    System.Windows.Controls.Button plus = new System.Windows.Controls.Button();
                    plus.Content = "+1";
                    plus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    plus.Width = 50;
                    plus.Height = 50;
                    plus.Background = Brushes.White;
                    plus.VerticalAlignment = VerticalAlignment.Bottom;
                    plus.Margin = new Thickness(0, 40, 10, 40);
                    plus.Tag = mainWindow.carts[i].id.ToString();
                    plus.FontSize = 20;
                    plus.FontFamily = new FontFamily("Comic Sans MS");
                    plus.Foreground = Brushes.White;
                    plus.Background = (Brush)(new BrushConverter().ConvertFrom("#76E383"));
                    plus.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#76E383"));
                    plus.Click += delegate
                    {
                        int id = int.Parse(plus.Tag.ToString());
                        id -= 1;
                        mainWindow.carts[id].count += 1;
                        Productz.Children.Clear();
                        CreateSchema();
                    };
                    Grid.SetColumn(plus, 1);
                    parent_grid.Children.Add(plus);

                    System.Windows.Controls.Button minus = new System.Windows.Controls.Button();
                    minus.Content = "-1";
                    minus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    minus.Width = 50;
                    minus.Height = 50;
                    minus.Background = Brushes.White;
                    minus.VerticalAlignment = VerticalAlignment.Bottom;
                    minus.Margin = new Thickness(0, 90, 90, 40);
                    minus.Tag = mainWindow.carts[i].id.ToString();
                    minus.FontSize = 20;
                    minus.FontFamily = new FontFamily("Comic Sans MS");
                    minus.Foreground = Brushes.White;
                    minus.Background = (Brush)(new BrushConverter().ConvertFrom("#76E383"));
                    minus.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#76E383"));
                    minus.Click += delegate
                    {
                        int id = int.Parse(minus.Tag.ToString());
                        id -= 1;
                        mainWindow.carts[id].count -= 1;
                        if
                        (mainWindow.carts[id].count == 0)
                        {
                            mainWindow.carts.Remove(mainWindow.carts[id]);
                        }
                        Productz.Children.Clear();
                        CreateSchema();
                    };
                    Grid.SetColumn(minus, 1);
                    parent_grid.Children.Add(minus);

                    System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                    del.Content = "Удалить";
                    del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    del.VerticalAlignment = VerticalAlignment.Top;
                    del.Width = 110;
                    del.Height = 50;
                    del.Background = Brushes.White;
                    del.Margin = new Thickness(0, 80, 20, 0);
                    del.Tag = mainWindow.carts[i].id.ToString();
                    del.FontSize = 20;
                    del.FontFamily = new FontFamily("Comic Sans MS");
                    del.Foreground = Brushes.White;
                    del.Background = (Brush)(new BrushConverter().ConvertFrom("#76E383"));
                    del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#76E383"));
                    del.Click += delegate
                    {
                        int id = int.Parse(del.Tag.ToString());
                        id -= 1;
                        mainWindow.carts[id].id = 0;
                        MessageBox.Show(mainWindow.carts[id].name + " убрано из заказа!");
                        Productz.Children.Clear();
                        CreateSchema();
                    };
                    Grid.SetColumn(del, 2);
                    parent_grid.Children.Add(del);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.main);
        }

        private void ConOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectz.Text != "")
            {
                mainWindow.OpenPages(MainWindow.pages.mo);
                mainWindow.cart.p = 1;
                string[] S = selectz.Text.Split('|');
                Clasess.ApplicationContext db = new Clasess.ApplicationContext();
                mainWindow.cart.p = (from point in db.Point
                                     where point.street == S[1] && point.city == S[0] && point.house == S[2]
                                     select point.id).First();
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите точку доставки (в выпадающем списке в правом верхнем углу).");
            }
        }
    }
}
