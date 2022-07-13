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
using Microsoft.EntityFrameworkCore;
using System.IO;



namespace Kursovaya.Pages
{
    public partial class Main : Page
    {
        public MainWindow mainWindow;
        public Main(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            CreatedProducts();
            OpenOrder.Visibility = Visibility.Hidden;

            try
            {
                if (mainWindow.helpclass.consroleuser == "Гость")
                {
                    FIO.Content += "Гость";
                }
                else { FIO.Content += mainWindow.Context.User.Find(mainWindow.helpclass.Uid).Full_Name; }
            }
            catch { }
            if (mainWindow.helpclass.consroleuser == "Администратор")
            {
            }
            if (mainWindow.helpclass.consroleuser == "Клиент")
            {
                Category.Visibility = Visibility.Hidden;
                Users.Visibility = Visibility.Hidden;
                Point.Visibility = Visibility.Hidden;
                AddButO.Visibility = Visibility.Hidden;
                AddButPr.Visibility = Visibility.Hidden;
            }
            if (mainWindow.helpclass.consroleuser == "Гость")
            {
                Category.Visibility = Visibility.Hidden;
                Users.Visibility = Visibility.Hidden;
                Point.Visibility = Visibility.Hidden;
                Orders.Visibility = Visibility.Hidden;
                AddButPr.Visibility = Visibility.Hidden;
            }
            if (mainWindow.helpclass.consroleuser == "Модератор")
            {
            }
            int count = 0;
            for (int i = 1; i < mainWindow.carts.Count; i++)
            {
                if (mainWindow.carts[i].id != 0)
                {
                    count += 1;
                }
            }
            if (count != 0)
            {
                OpenOrder.Visibility = Visibility.Visible;
            }
        }

        public void CreatedUser()
        {
            var users = mainWindow.Context.User.ToList();
            foreach (var u in users)
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = 200;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                Userz.Children.Add(parent_grid);

                System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                name.Content = "ФИО: " + u.Full_Name;
                name.Margin = new Thickness(10, 45, 10, 0);
                name.FontWeight = FontWeights.Bold;
                name.FontSize = 20;
                name.Foreground = Brushes.Black;
                name.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(name);

                System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                about.Content = "Логин: " + u.Login;
                about.Margin = new Thickness(10, 115, 10, 0);
                about.VerticalAlignment = VerticalAlignment.Top;
                about.Foreground = Brushes.Black;
                about.FontSize = 20;
                parent_grid.Children.Add(about);

                System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                type.Content = "Пароль: " + u.Password;
                type.Margin = new Thickness(10, 80, 10, 0);
                type.VerticalAlignment = VerticalAlignment.Top;
                type.Foreground = Brushes.Black;
                type.FontSize = 20;
                parent_grid.Children.Add(type);

                System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                video.Content = "Роль: " + u.Role;
                video.Margin = new Thickness(10, 150, 10, 0);
                video.VerticalAlignment = VerticalAlignment.Top;
                video.Foreground = Brushes.Black;
                video.FontSize = 20;
                parent_grid.Children.Add(video);

                System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                buy.Content = "Изменить";
                buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                buy.Width = 100;
                buy.Height = 50;
                buy.Background = Brushes.White;
                buy.VerticalAlignment = VerticalAlignment.Bottom;
                buy.Margin = new Thickness(0, 0, 10, 40);
                buy.Tag = u.id.ToString();
                buy.FontSize = 20;
                buy.FontFamily = new FontFamily("Comic Sans MS");
                buy.Foreground = Brushes.White;
                buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.Click += delegate
                {
                    int id = int.Parse(buy.Tag.ToString());
                    mainWindow.helpclass.id = id;
                    mainWindow.helpclass.add_edit = "Change User";
                    mainWindow.OpenPages(MainWindow.pages.add);
                };
                Grid.SetColumn(buy, 1);
                parent_grid.Children.Add(buy);
                if (mainWindow.helpclass.consroleuser == "Администратор")
                {
                    System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                    del.Content = "Удалить";
                    del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    del.VerticalAlignment = VerticalAlignment.Top;
                    del.Width = 100;
                    del.Height = 50;
                    del.Background = Brushes.White;
                    del.Margin = new Thickness(0, 40, 10, 0);
                    del.Tag = u.id.ToString();
                    del.FontSize = 20;
                    del.FontFamily = new FontFamily("Comic Sans MS");
                    del.Foreground = Brushes.White;
                    del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.Click += delegate
                    {
                        int id = int.Parse(del.Tag.ToString());
                        Clasess.Users user1 = mainWindow.Context.User.Find(id);
                        mainWindow.Context.User.Remove(user1);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные удалены!");
                    };
                    Grid.SetColumn(del, 2);
                    parent_grid.Children.Add(del);
                }
            }
        }

        public void CreatedPoint()
        {
            var points = mainWindow.Context.Point.ToList();
            foreach (var p in points)
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = 200;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                Pointz.Children.Add(parent_grid);

                System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                name.Content = "Почтовый индекс: " + p.post_code;
                name.Margin = new Thickness(10, 45, 10, 0);
                name.FontWeight = FontWeights.Bold;
                name.FontSize = 20;
                name.Foreground = Brushes.Black;
                name.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(name);

                System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                about.Content = "Город: " + p.city;
                about.Margin = new Thickness(10, 115, 10, 0);
                about.VerticalAlignment = VerticalAlignment.Top;
                about.Foreground = Brushes.Black;
                about.FontSize = 20;
                parent_grid.Children.Add(about);

                System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                type.Content = "Улица: " + p.street;
                type.Margin = new Thickness(10, 80, 10, 0);
                type.VerticalAlignment = VerticalAlignment.Top;
                type.Foreground = Brushes.Black;
                type.FontSize = 20;
                parent_grid.Children.Add(type);

                System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                video.Content = "Дом: " + p.house;
                video.Margin = new Thickness(10, 150, 10, 0);
                video.VerticalAlignment = VerticalAlignment.Top;
                video.Foreground = Brushes.Black;
                video.FontSize = 20;
                parent_grid.Children.Add(video);

                System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                buy.Content = "Изменить";
                buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                buy.Width = 100;
                buy.Height = 50;
                buy.Background = Brushes.White;
                buy.VerticalAlignment = VerticalAlignment.Bottom;
                buy.Margin = new Thickness(0, 0, 10, 40);
                buy.Tag = p.id.ToString();
                buy.FontSize = 20;
                buy.FontFamily = new FontFamily("Comic Sans MS");
                buy.Foreground = Brushes.White;
                buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.Click += delegate
                {
                    int id = int.Parse(buy.Tag.ToString());
                    mainWindow.helpclass.id = id;
                    mainWindow.helpclass.add_edit = "Change Point";
                    mainWindow.OpenPages(MainWindow.pages.add);
                };
                Grid.SetColumn(buy, 1);
                parent_grid.Children.Add(buy);

                if (mainWindow.helpclass.consroleuser == "Администратор")
                {
                    System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                    del.Content = "Удалить";
                    del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    del.VerticalAlignment = VerticalAlignment.Top;
                    del.Width = 100;
                    del.Height = 50;
                    del.Background = Brushes.White;
                    del.Margin = new Thickness(0, 40, 10, 0);
                    del.Tag = p.id.ToString();
                    del.FontSize = 20;
                    del.FontFamily = new FontFamily("Comic Sans MS");
                    del.Foreground = Brushes.White;
                    del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.Click += delegate
                    {
                        int id = int.Parse(del.Tag.ToString());
                        Clasess.Points point1 = mainWindow.Context.Point.Find(id);
                        mainWindow.Context.Point.Remove(point1);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные удалены!");
                    };
                    Grid.SetColumn(del, 2);
                    parent_grid.Children.Add(del);
                }
            }
        }

        public void CreatedCategory()
        {
            var categorys = mainWindow.Context.Category.ToList();
            foreach (var c in categorys)
            {

                Grid parent_grid = new Grid();
                parent_grid.Height = 200;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                Categoryz.Children.Add(parent_grid);

                System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                name.Content = "Название : " + c.name;
                name.Margin = new Thickness(10, 45, 10, 0);
                name.FontWeight = FontWeights.Bold;
                name.FontSize = 20;
                name.Foreground = Brushes.Black;
                name.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(name);

                System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                buy.Content = "Изменить";
                buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                buy.Width = 100;
                buy.Height = 50;
                buy.Background = Brushes.White;
                buy.VerticalAlignment = VerticalAlignment.Bottom;
                buy.Margin = new Thickness(0, 0, 10, 40);
                buy.Tag = c.id.ToString();
                buy.FontSize = 20;
                buy.FontFamily = new FontFamily("Comic Sans MS");
                buy.Foreground = Brushes.White;
                buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.Click += delegate
                {
                    int id = int.Parse(buy.Tag.ToString());
                    mainWindow.helpclass.id = id;
                    mainWindow.helpclass.add_edit = "Change Category";
                    mainWindow.OpenPages(MainWindow.pages.add);
                };
                Grid.SetColumn(buy, 1);
                parent_grid.Children.Add(buy);
                if (mainWindow.helpclass.consroleuser == "Администратор")
                {
                    System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                    del.Content = "Удалить";
                    del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    del.VerticalAlignment = VerticalAlignment.Top;
                    del.Width = 100;
                    del.Height = 50;
                    del.Background = Brushes.White;
                    del.Margin = new Thickness(0, 40, 10, 0);
                    del.Tag = c.id.ToString();
                    del.FontSize = 20;
                    del.FontFamily = new FontFamily("Comic Sans MS");
                    del.Foreground = Brushes.White;
                    del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.Click += delegate
                    {
                        int id = int.Parse(del.Tag.ToString());
                        Clasess.Categorys categoryz = mainWindow.Context.Category.Find(id);
                        mainWindow.Context.Category.Remove(categoryz);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные удалены!");
                    };
                    Grid.SetColumn(del, 2);
                    parent_grid.Children.Add(del);
                }
            }
        }

        public void CreatedOrder()
        {
            if (mainWindow.helpclass.consroleuser == "Клиент")
            {
                AddButO.Visibility = Visibility.Hidden;
                var orders = mainWindow.Context.Order.ToList();
                int i = -1;
                foreach (var o in orders)
                {
                    if (mainWindow.helpclass.Uid == o.Client)
                    {
                        i++;
                        Grid parent_grid = new Grid();
                        parent_grid.Height = 220;
                        parent_grid.Margin = new Thickness(20, 20, 20, 20);
                        Orderz.Children.Add(parent_grid);

                        System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                        name.Content = "Код: " + o.Code;
                        name.Margin = new Thickness(10, 45, 10, 0);
                        name.FontWeight = FontWeights.Bold;
                        name.FontSize = 20;
                        name.Foreground = Brushes.Black;
                        name.FontFamily = new FontFamily("Comic Sans MS");
                        parent_grid.Children.Add(name);

                        System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                        about.Content = "Дата заказа: " + o.Order_Date;
                        about.Margin = new Thickness(10, 115, 10, 0);
                        about.VerticalAlignment = VerticalAlignment.Top;
                        about.Foreground = Brushes.Black;
                        about.FontSize = 20;
                        parent_grid.Children.Add(about);

                        System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                        type.Content = "Дата выдачи: " + o.Date_of_issue;
                        type.Margin = new Thickness(10, 80, 10, 0);
                        type.VerticalAlignment = VerticalAlignment.Top;
                        type.Foreground = Brushes.Black;
                        type.FontSize = 20;
                        parent_grid.Children.Add(type);

                        System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                        video.Content = "Клиент: текущий пользователь";
                        video.Margin = new Thickness(10, 150, 10, 0);
                        video.VerticalAlignment = VerticalAlignment.Top;
                        video.Foreground = Brushes.Black;
                        video.FontSize = 20;
                        parent_grid.Children.Add(video);

                        System.Windows.Controls.Label video1 = new System.Windows.Controls.Label();
                        video1.Content = "Статус: " + o.Status;
                        video1.Margin = new Thickness(10, 185, 10, 0);
                        video1.VerticalAlignment = VerticalAlignment.Top;
                        video1.Foreground = Brushes.Black;
                        video1.FontSize = 20;
                        parent_grid.Children.Add(video1);

                        System.Windows.Controls.Label video2 = new System.Windows.Controls.Label();
                        video2.Content = "Точка выдачи: " + o.Point;
                        video2.Margin = new Thickness(10, 210, 10, 0);
                        video2.VerticalAlignment = VerticalAlignment.Top;
                        video2.Foreground = Brushes.Black;
                        video2.FontSize = 20;
                        parent_grid.Children.Add(video2);

                        if (mainWindow.helpclass.consroleuser == "Администратор")
                        {
                            System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                            del.Content = "Отменить";
                            del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                            del.VerticalAlignment = VerticalAlignment.Top;
                            del.Width = 100;
                            del.Height = 50;
                            del.Background = Brushes.White;
                            del.Margin = new Thickness(0, 40, 10, 0);
                            del.Tag = o.id.ToString();
                            del.FontSize = 20;
                            del.FontFamily = new FontFamily("Comic Sans MS");
                            del.Foreground = Brushes.White;
                            del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.Click += delegate
                            {
                                int id = int.Parse(del.Tag.ToString());
                                Clasess.Orders orderz = mainWindow.Context.Order.Find(id);
                                mainWindow.Context.Order.Remove(orderz);
                                mainWindow.Context.SaveChanges();
                                MessageBox.Show("Данные удалены!");
                            };
                            Grid.SetColumn(del, 2);
                            parent_grid.Children.Add(del);
                        }
                        }
                    }
            }
            else
            {
                int i = -1;
                var orders = mainWindow.Context.Order.ToList();
                foreach (var o in orders)
                {
                    i++;
                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Orderz.Children.Add(parent_grid);

                    System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                    name.Content = "Код: " + o.Code;
                    name.Margin = new Thickness(10, 45, 10, 0);
                    name.FontWeight = FontWeights.Bold;
                    name.FontSize = 20;
                    name.Foreground = Brushes.Black;
                    name.FontFamily = new FontFamily("Comic Sans MS");
                    parent_grid.Children.Add(name);

                    System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                    about.Content = "Дата заказа: " + o.Order_Date;
                    about.Margin = new Thickness(10, 115, 10, 0);
                    about.VerticalAlignment = VerticalAlignment.Top;
                    about.Foreground = Brushes.Black;
                    about.FontSize = 20;
                    parent_grid.Children.Add(about);

                    System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                    type.Content = "Дата выдачи: " + o.Date_of_issue;
                    type.Margin = new Thickness(10, 80, 10, 0);
                    type.VerticalAlignment = VerticalAlignment.Top;
                    type.Foreground = Brushes.Black;
                    type.FontSize = 20;
                    parent_grid.Children.Add(type);

                    System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                    video.Content = "Клиент: " + mainWindow.users_name[i].name;
                    video.Margin = new Thickness(10, 150, 10, 0);
                    video.VerticalAlignment = VerticalAlignment.Top;
                    video.Foreground = Brushes.Black;
                    video.FontSize = 20;
                    parent_grid.Children.Add(video);

                    System.Windows.Controls.Label video1 = new System.Windows.Controls.Label();
                    video1.Content = "Статус: " + o.Status;
                    video1.Margin = new Thickness(10, 185, 10, 0);
                    video1.VerticalAlignment = VerticalAlignment.Top;
                    video1.Foreground = Brushes.Black;
                    video1.FontSize = 20;
                    parent_grid.Children.Add(video1);

                    System.Windows.Controls.Label video2 = new System.Windows.Controls.Label();
                    video2.Content = "Точка выдачи: " + o.Point;
                    video2.Margin = new Thickness(10, 210, 10, 0);
                    video2.VerticalAlignment = VerticalAlignment.Top;
                    video2.Foreground = Brushes.Black;
                    video2.FontSize = 20;
                    parent_grid.Children.Add(video2);

                    System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                    buy.Content = "Изменить";
                    buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    buy.Width = 100;
                    buy.Height = 50;
                    buy.Background = Brushes.White;
                    buy.VerticalAlignment = VerticalAlignment.Bottom;
                    buy.Margin = new Thickness(0, 0, 10, 40);
                    buy.Tag = o.id.ToString();
                    buy.FontSize = 20;
                    buy.FontFamily = new FontFamily("Comic Sans MS");
                    buy.Foreground = Brushes.White;
                    buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.Click += delegate
                    {
                        int id = int.Parse(buy.Tag.ToString());
                        mainWindow.helpclass.id = id;
                        mainWindow.helpclass.add_edit = "Change Order";
                        mainWindow.OpenPages(MainWindow.pages.add);
                    };
                    Grid.SetColumn(buy, 1);
                    parent_grid.Children.Add(buy);

                    if (mainWindow.helpclass.consroleuser == "Администратор")
                    {
                        System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                        del.Content = "Отменить";
                        del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                        del.VerticalAlignment = VerticalAlignment.Top;
                        del.Width = 100;
                        del.Height = 50;
                        del.Background = Brushes.White;
                        del.Margin = new Thickness(0, 40, 10, 0);
                        del.Tag = o.id.ToString();
                        del.FontSize = 20;
                        del.FontFamily = new FontFamily("Comic Sans MS");
                        del.Foreground = Brushes.White;
                        del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.Click += delegate
                        {
                            int id = int.Parse(del.Tag.ToString());
                            Clasess.Orders orderz = mainWindow.Context.Order.Find(id);
                            mainWindow.Context.Order.Remove(orderz);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные удалены!");
                        };
                        Grid.SetColumn(del, 2);
                        parent_grid.Children.Add(del);
                    }
                }
            }
        }

        public void CreatedProducts()
        {
            if (SearchPr.Text != "")
            {
                Search_ChangePr(null, null);
            }
            else
            {
            int count = 0;
            Productz.Children.Clear();
            if (Convert.ToBoolean(up.IsChecked))
            {
                var products = mainWindow.Context.Product.OrderBy(p => p.price);
                foreach (Clasess.Products p in products)
                {
                    if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                    {

                    }
                    else
                    {
                        if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked))
                        {
                            if (14.99 <= p.discount) continue;
                        }
                        else
                        {
                            if (Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                            {
                                if (10 >= p.discount) continue;
                            }
                            else
                            {
                                if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                                {
                                    if (9.99 <= p.discount & 15 >= p.discount) continue;
                                }
                                else
                                {
                                    if (Convert.ToBoolean(zero.IsChecked))
                                    {
                                        if (9.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(ten.IsChecked))
                                    {
                                        if (10 >= p.discount) continue;
                                        if (14.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(fifthy.IsChecked))
                                    {
                                        if (15 >= p.discount) continue;
                                    }
                                }
                            }
                        }
                    }
                    count++;

                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Productz.Children.Add(parent_grid);


                    Image image = new Image();
                    image.Width = 150;
                    image.Height = 150;
                    image.Margin = new Thickness(10, 10, 10, 10);
                    image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    if (mainWindow.imageConverter.LoadImage(p.img_src) == null)
                    {
                        image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + (@"\picture.png")));
                    }
                    else
                    {
                        image.Source = mainWindow.imageConverter.LoadImage(p.img_src);
                    }
                    Grid.SetColumn(image, 1);
                    parent_grid.Children.Add(image);

                    System.Windows.Controls.TextBlock name = new System.Windows.Controls.TextBlock();
                    name.Text = p.name.ToString();
                    name.Margin = new Thickness(10, 45, 10, 0);
                    name.FontWeight = FontWeights.Bold;
                    name.Width = 200;
                    name.FontSize = 15;
                    name.Foreground = Brushes.Black;
                    name.FontFamily = new FontFamily("Comic Sans MS");
                    Grid.SetColumn(name, 2);
                    parent_grid.Children.Add(name);

                    System.Windows.Controls.TextBlock about = new System.Windows.Controls.TextBlock();
                    about.Text = p.description.ToString();
                    about.TextWrapping = TextWrapping.Wrap;
                    about.Height = 70;
                    about.Width = 200;
                    about.Margin = new Thickness(10, 115, 10, 0);
                    about.VerticalAlignment = VerticalAlignment.Top;
                    about.Foreground = Brushes.Black;
                    about.FontSize = 15;
                    Grid.SetColumn(about, 2);
                    parent_grid.Children.Add(about);

                    System.Windows.Controls.TextBlock type = new System.Windows.Controls.TextBlock();
                    type.Text = "Производитель: " + p.manufacturer;
                    type.Margin = new Thickness(10, 80, 10, 0);
                    type.VerticalAlignment = VerticalAlignment.Top;
                    type.Width = 200;
                    type.Foreground = Brushes.Black;
                    type.FontSize = 15;
                    Grid.SetColumn(type, 2);
                    parent_grid.Children.Add(type);

                    System.Windows.Controls.TextBlock video = new System.Windows.Controls.TextBlock();
                    video.Text = p.price.ToString();
                    video.Margin = new Thickness(90, 190, 10, 0);
                    video.Width = 200;
                    video.VerticalAlignment = VerticalAlignment.Top;
                    video.TextDecorations = TextDecorations.Strikethrough;
                    video.Foreground = Brushes.Black;
                    video.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video);

                    System.Windows.Controls.TextBlock video3 = new System.Windows.Controls.TextBlock();
                    video3.Text = "Цена: ";
                    video3.Margin = new Thickness(10, 190, 10, 0);
                    video3.Width = 200;
                    video3.VerticalAlignment = VerticalAlignment.Top;
                    video3.Foreground = Brushes.Black;
                    video3.FontSize = 15;
                    Grid.SetColumn(video3, 2);
                    parent_grid.Children.Add(video3);

                    System.Windows.Controls.TextBlock video2 = new System.Windows.Controls.TextBlock();
                    video2.Text = ((Convert.ToDouble(p.price) - (Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01))).ToString());
                    video2.Margin = new Thickness(180, 190, 10, 0);
                    video2.VerticalAlignment = VerticalAlignment.Top;
                    video2.Foreground = Brushes.Black;
                    video2.Width = 200;
                    video2.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video2);

                    System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                    buy.Content = "Добавить в заказ";
                    buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    buy.Width = 150;
                    buy.Height = 50;
                    buy.Background = Brushes.White;
                    buy.VerticalAlignment = VerticalAlignment.Bottom;
                    buy.Tag = p.id.ToString();
                    buy.FontSize = 15;
                    buy.FontFamily = new FontFamily("Comic Sans MS");
                    buy.Foreground = Brushes.White;
                    buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.Click += delegate
                    {
                        int id = int.Parse(buy.Tag.ToString());
                        mainWindow.AddCart(id);
                        OpenOrder.Visibility = Visibility.Visible;
                    };
                    Grid.SetColumn(buy, 3);
                    parent_grid.Children.Add(buy);

                    if (mainWindow.helpclass.consroleuser == "Администратор")
                    {
                        System.Windows.Controls.Button chan = new System.Windows.Controls.Button();
                        chan.Content = "Изменить";
                        chan.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        chan.Width = 100;
                        chan.Height = 50;
                        chan.Background = Brushes.White;
                        chan.VerticalAlignment = VerticalAlignment.Top;
                        chan.Tag = p.id.ToString();
                        chan.FontSize = 15;
                        chan.FontFamily = new FontFamily("Comic Sans MS");
                        chan.Foreground = Brushes.White;
                        chan.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.Click += delegate
                        {
                            int id = int.Parse(chan.Tag.ToString());
                            mainWindow.helpclass.id = id;
                            mainWindow.helpclass.add_edit = "Change Product";
                            mainWindow.OpenPages(MainWindow.pages.add);
                        };
                        Grid.SetColumn(chan, 2);
                        parent_grid.Children.Add(chan);

                        System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                        del.Content = "Удалить";
                        del.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        del.Margin = new Thickness(120,0,0,0);
                        del.Width = 100;
                        del.Height = 50;
                        del.Background = Brushes.White;
                        del.VerticalAlignment = VerticalAlignment.Top;
                        del.Tag = p.id.ToString();
                        del.FontSize = 15;
                        del.FontFamily = new FontFamily("Comic Sans MS");
                        del.Foreground = Brushes.White;
                        del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.Click += delegate
                        {
                            int id = int.Parse(del.Tag.ToString());
                            Clasess.Products products1 = mainWindow.Context.Product.Find(id);
                            mainWindow.Context.Product.Remove(products1);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные удалены!");
                        };
                        Grid.SetColumn(del, 2);
                        parent_grid.Children.Add(del);
                    }


                    System.Windows.Controls.TextBlock video5 = new System.Windows.Controls.TextBlock();
                    video5.Text = ((Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01)).ToString());
                    video5.Margin = new Thickness(10, 10, 10, 10);
                    video5.VerticalAlignment = VerticalAlignment.Center;
                    video5.HorizontalAlignment = HorizontalAlignment.Right;
                    video5.Foreground = Brushes.Black;
                    if (p.discount > 15)
                    {
                        video5.Background = (Brush)(new BrushConverter().ConvertFrom("#7fff00"));
                    }
                    video5.FontSize = 15;
                    Grid.SetColumn(video5, 3);
                    parent_grid.Children.Add(video5);
                }
            }
            else
            if (Convert.ToBoolean(down.IsChecked))
            {
                var products = mainWindow.Context.Product.OrderByDescending(p => p.price);
                foreach (Clasess.Products p in products)
                {
                    if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                    {

                    }
                    else
                    {
                        if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked))
                        {
                            if (14.99 <= p.discount) continue;
                        }
                        else
                        {
                            if (Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                            {
                                if (10 >= p.discount) continue;
                            }
                            else
                            {
                                if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                                {
                                    if (9.99 <= p.discount & 15 >= p.discount) continue;
                                }
                                else
                                {
                                    if (Convert.ToBoolean(zero.IsChecked))
                                    {
                                        if (9.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(ten.IsChecked))
                                    {
                                        if (10 >= p.discount) continue;
                                        if (14.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(fifthy.IsChecked))
                                    {
                                        if (15 >= p.discount) continue;
                                    }
                                }
                            }
                        }
                    }
                    count++;
                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Productz.Children.Add(parent_grid);


                    Image image = new Image();
                    image.Width = 150;
                    image.Height = 150;
                    image.Margin = new Thickness(10, 10, 10, 10);
                    image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    if (mainWindow.imageConverter.LoadImage(p.img_src) == null)
                    {
                        image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + (@"\picture.png")));
                    }
                    else
                    {
                        image.Source = mainWindow.imageConverter.LoadImage(p.img_src);
                    }
                    Grid.SetColumn(image, 1);
                    parent_grid.Children.Add(image);

                    System.Windows.Controls.TextBlock name = new System.Windows.Controls.TextBlock();
                    name.Text = p.name.ToString();
                    name.Margin = new Thickness(10, 45, 10, 0);
                    name.FontWeight = FontWeights.Bold;
                    name.Width = 200;
                    name.FontSize = 15;
                    name.Foreground = Brushes.Black;
                    name.FontFamily = new FontFamily("Comic Sans MS");
                    Grid.SetColumn(name, 2);
                    parent_grid.Children.Add(name);

                    System.Windows.Controls.TextBlock about = new System.Windows.Controls.TextBlock();
                    about.Text = p.description.ToString();
                    about.TextWrapping = TextWrapping.Wrap;
                    about.Height = 70;
                    about.Width = 200;
                    about.Margin = new Thickness(10, 115, 10, 0);
                    about.VerticalAlignment = VerticalAlignment.Top;
                    about.Foreground = Brushes.Black;
                    about.FontSize = 15;
                    Grid.SetColumn(about, 2);
                    parent_grid.Children.Add(about);

                    System.Windows.Controls.TextBlock type = new System.Windows.Controls.TextBlock();
                    type.Text = "Производитель: " + p.manufacturer;
                    type.Margin = new Thickness(10, 80, 10, 0);
                    type.VerticalAlignment = VerticalAlignment.Top;
                    type.Width = 200;
                    type.Foreground = Brushes.Black;
                    type.FontSize = 15;
                    Grid.SetColumn(type, 2);
                    parent_grid.Children.Add(type);

                    System.Windows.Controls.TextBlock video = new System.Windows.Controls.TextBlock();
                    video.Text = p.price.ToString();
                    video.Margin = new Thickness(90, 190, 10, 0);
                    video.Width = 200;
                    video.VerticalAlignment = VerticalAlignment.Top;
                    video.TextDecorations = TextDecorations.Strikethrough;
                    video.Foreground = Brushes.Black;
                    video.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video);

                    System.Windows.Controls.TextBlock video3 = new System.Windows.Controls.TextBlock();
                    video3.Text = "Цена: ";
                    video3.Margin = new Thickness(10, 190, 10, 0);
                    video3.Width = 200;
                    video3.VerticalAlignment = VerticalAlignment.Top;
                    video3.Foreground = Brushes.Black;
                    video3.FontSize = 15;
                    Grid.SetColumn(video3, 2);
                    parent_grid.Children.Add(video3);

                    System.Windows.Controls.TextBlock video2 = new System.Windows.Controls.TextBlock();
                    video2.Text = ((Convert.ToDouble(p.price) - (Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01))).ToString());
                    video2.Margin = new Thickness(180, 190, 10, 0);
                    video2.VerticalAlignment = VerticalAlignment.Top;
                    video2.Foreground = Brushes.Black;
                    video2.Width = 200;
                    video2.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video2);

                    System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                    buy.Content = "Добавить в заказ";
                    buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    buy.Width = 150;
                    buy.Height = 50;
                    buy.Background = Brushes.White;
                    buy.VerticalAlignment = VerticalAlignment.Bottom;
                    buy.Tag = p.id.ToString();
                    buy.FontSize = 15;
                    buy.FontFamily = new FontFamily("Comic Sans MS");
                    buy.Foreground = Brushes.White;
                    buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.Click += delegate
                    {
                        int id = int.Parse(buy.Tag.ToString());
                        mainWindow.AddCart(id);
                        OpenOrder.Visibility = Visibility.Visible;
                    };
                    Grid.SetColumn(buy, 3);
                    parent_grid.Children.Add(buy);

                    if (mainWindow.helpclass.consroleuser == "Администратор")
                    {
                        System.Windows.Controls.Button chan = new System.Windows.Controls.Button();
                        chan.Content = "Изменить";
                        chan.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        chan.Width = 100;
                        chan.Height = 50;
                        chan.Background = Brushes.White;
                        chan.VerticalAlignment = VerticalAlignment.Top;
                        chan.Tag = p.id.ToString();
                        chan.FontSize = 15;
                        chan.FontFamily = new FontFamily("Comic Sans MS");
                        chan.Foreground = Brushes.White;
                        chan.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.Click += delegate
                        {
                            int id = int.Parse(chan.Tag.ToString());
                            mainWindow.helpclass.id = id;
                            mainWindow.helpclass.add_edit = "Change Product";
                            mainWindow.OpenPages(MainWindow.pages.add);
                        };
                        Grid.SetColumn(chan, 2);
                        parent_grid.Children.Add(chan);

                        System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                        del.Content = "Удалить";
                        del.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        del.Margin = new Thickness(120, 0, 0, 0);
                        del.Width = 100;
                        del.Height = 50;
                        del.Background = Brushes.White;
                        del.VerticalAlignment = VerticalAlignment.Top;
                        del.Tag = p.id.ToString();
                        del.FontSize = 15;
                        del.FontFamily = new FontFamily("Comic Sans MS");
                        del.Foreground = Brushes.White;
                        del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.Click += delegate
                        {
                            int id = int.Parse(del.Tag.ToString());
                            Clasess.Products products1 = mainWindow.Context.Product.Find(id);
                            mainWindow.Context.Product.Remove(products1);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные удалены!");
                        };
                        Grid.SetColumn(del, 2);
                        parent_grid.Children.Add(del);
                    }


                    System.Windows.Controls.TextBlock video5 = new System.Windows.Controls.TextBlock();
                    video5.Text = ((Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01)).ToString());
                    video5.Margin = new Thickness(10, 10, 10, 10);
                    video5.VerticalAlignment = VerticalAlignment.Center;
                    video5.HorizontalAlignment = HorizontalAlignment.Right;
                    video5.Foreground = Brushes.Black;
                    if (p.discount > 15)
                    {
                        video5.Background = (Brush)(new BrushConverter().ConvertFrom("#7fff00"));
                    }
                    video5.FontSize = 15;
                    Grid.SetColumn(video5, 3);
                    parent_grid.Children.Add(video5);
                }
            }
            else
            {
                var products = mainWindow.Context.Product.ToList();
                    foreach (var pr in products)
                    {
                        if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                        {

                        }
                        else
                        {
                            if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked))
                            {
                                if (14.99 <= pr.discount) continue;
                            }
                            else
                            {
                                if (Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                                {
                                    if (10 >= pr.discount) continue;
                                }
                                else
                                {
                                    if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                                    {
                                        if (9.99 <= pr.discount & 15 >= pr.discount) continue;
                                    }
                                    else
                                    {
                                        if (Convert.ToBoolean(zero.IsChecked))
                                        {
                                            if (9.99 <= pr.discount) continue;
                                        }

                                        if (Convert.ToBoolean(ten.IsChecked))
                                        {
                                            if (10 >= pr.discount) continue;
                                            if (14.99 <= pr.discount) continue;
                                        }

                                        if (Convert.ToBoolean(fifthy.IsChecked))
                                        {
                                            if (15 >= pr.discount) continue;
                                        }
                                    }
                                }
                            }
                        }
                        count++;
                        Grid parent_grid = new Grid();
                        parent_grid.Height = 220;
                        parent_grid.Margin = new Thickness(20, 20, 20, 20);
                        Productz.Children.Add(parent_grid);


                        Image image = new Image();
                        image.Width = 150;
                        image.Height = 150;
                        image.Margin = new Thickness(10, 10, 10, 10);
                        image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        image.VerticalAlignment = VerticalAlignment.Center;
                        if (mainWindow.imageConverter.LoadImage(pr.img_src) == null)
                        {
                            image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + (@"\picture.png")));
                        }
                        else
                        {
                            image.Source = mainWindow.imageConverter.LoadImage(pr.img_src);
                        }
                        Grid.SetColumn(image, 1);
                        parent_grid.Children.Add(image);

                        System.Windows.Controls.TextBlock name = new System.Windows.Controls.TextBlock();
                        name.Text = pr.name.ToString();
                        name.Margin = new Thickness(10, 45, 10, 0);
                        name.FontWeight = FontWeights.Bold;
                        name.Width = 200;
                        name.FontSize = 15;
                        name.Foreground = Brushes.Black;
                        name.FontFamily = new FontFamily("Comic Sans MS");
                        Grid.SetColumn(name, 2);
                        parent_grid.Children.Add(name);

                        System.Windows.Controls.TextBlock about = new System.Windows.Controls.TextBlock();
                        about.Text = pr.description.ToString();
                        about.TextWrapping = TextWrapping.Wrap;
                        about.Height = 70;
                        about.Width = 200;
                        about.Margin = new Thickness(10, 115, 10, 0);
                        about.VerticalAlignment = VerticalAlignment.Top;
                        about.Foreground = Brushes.Black;
                        about.FontSize = 15;
                        Grid.SetColumn(about, 2);
                        parent_grid.Children.Add(about);

                        System.Windows.Controls.TextBlock type = new System.Windows.Controls.TextBlock();
                        type.Text = "Производитель: " + pr.manufacturer;
                        type.Margin = new Thickness(10, 80, 10, 0);
                        type.VerticalAlignment = VerticalAlignment.Top;
                        type.Width = 200;
                        type.Foreground = Brushes.Black;
                        type.FontSize = 15;
                        Grid.SetColumn(type, 2);
                        parent_grid.Children.Add(type);

                        System.Windows.Controls.TextBlock video = new System.Windows.Controls.TextBlock();
                        video.Text = pr.price.ToString();
                        video.Margin = new Thickness(90, 190, 10, 0);
                        video.Width = 200;
                        video.VerticalAlignment = VerticalAlignment.Top;
                        video.TextDecorations = TextDecorations.Strikethrough;
                        video.Foreground = Brushes.Black;
                        video.FontSize = 15;
                        Grid.SetColumn(video, 2);
                        parent_grid.Children.Add(video);

                        System.Windows.Controls.TextBlock video3 = new System.Windows.Controls.TextBlock();
                        video3.Text = "Цена: ";
                        video3.Margin = new Thickness(10, 190, 10, 0);
                        video3.Width = 200;
                        video3.VerticalAlignment = VerticalAlignment.Top;
                        video3.Foreground = Brushes.Black;
                        video3.FontSize = 15;
                        Grid.SetColumn(video3, 2);
                        parent_grid.Children.Add(video3);

                        System.Windows.Controls.TextBlock video2 = new System.Windows.Controls.TextBlock();
                        video2.Text = ((Convert.ToDouble(pr.price) - (Convert.ToDouble(pr.price) * (Convert.ToDouble(pr.discount) * 0.01))).ToString());
                        video2.Margin = new Thickness(180, 190, 10, 0);
                        video2.VerticalAlignment = VerticalAlignment.Top;
                        video2.Foreground = Brushes.Black;
                        video2.Width = 200;
                        video2.FontSize = 15;
                        Grid.SetColumn(video, 2);
                        parent_grid.Children.Add(video2);

                        System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                        buy.Content = "Добавить в заказ";
                        buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                        buy.Width = 150;
                        buy.Height = 50;
                        buy.Background = Brushes.White;
                        buy.VerticalAlignment = VerticalAlignment.Bottom;
                        buy.Tag = pr.id.ToString();
                        buy.FontSize = 15;
                        buy.FontFamily = new FontFamily("Comic Sans MS");
                        buy.Foreground = Brushes.White;
                        buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        buy.Click += delegate
                        {
                            int id = int.Parse(buy.Tag.ToString());
                            mainWindow.AddCart(id);
                            OpenOrder.Visibility = Visibility.Visible;
                        };
                        Grid.SetColumn(buy, 3);
                        parent_grid.Children.Add(buy);

                        if (mainWindow.helpclass.consroleuser == "Администратор")
                        {
                            System.Windows.Controls.Button chan = new System.Windows.Controls.Button();
                            chan.Content = "Изменить";
                            chan.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            chan.Width = 100;
                            chan.Height = 50;
                            chan.Background = Brushes.White;
                            chan.VerticalAlignment = VerticalAlignment.Top;
                            chan.Tag = pr.id.ToString();
                            chan.FontSize = 15;
                            chan.FontFamily = new FontFamily("Comic Sans MS");
                            chan.Foreground = Brushes.White;
                            chan.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            chan.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            chan.Click += delegate
                            {
                                int id = int.Parse(chan.Tag.ToString());
                                mainWindow.helpclass.id = id;
                                mainWindow.helpclass.add_edit = "Change Product";
                                mainWindow.OpenPages(MainWindow.pages.add);
                            };
                            Grid.SetColumn(chan, 2);
                            parent_grid.Children.Add(chan);

                            System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                            del.Content = "Удалить";
                            del.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            del.Margin = new Thickness(120, 0, 0, 0);
                            del.Width = 100;
                            del.Height = 50;
                            del.Background = Brushes.White;
                            del.VerticalAlignment = VerticalAlignment.Top;
                            del.Tag = pr.id.ToString();
                            del.FontSize = 15;
                            del.FontFamily = new FontFamily("Comic Sans MS");
                            del.Foreground = Brushes.White;
                            del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.Click += delegate
                            {
                                int id = int.Parse(del.Tag.ToString());
                                Clasess.Products products1 = mainWindow.Context.Product.Find(id);
                                mainWindow.Context.Product.Remove(products1);
                                mainWindow.Context.SaveChanges();
                                MessageBox.Show("Данные удалены!");
                            };
                            Grid.SetColumn(del, 2);
                            parent_grid.Children.Add(del);
                        }


                        System.Windows.Controls.TextBlock video5 = new System.Windows.Controls.TextBlock();
                        video5.Text = ((Convert.ToDouble(pr.price) * (Convert.ToDouble(pr.discount) * 0.01)).ToString());
                        video5.Margin = new Thickness(10, 10, 10, 10);
                        video5.VerticalAlignment = VerticalAlignment.Center;
                        video5.HorizontalAlignment = HorizontalAlignment.Right;
                        video5.Foreground = Brushes.Black;
                        if (pr.discount > 15)
                        {
                            video5.Background = (Brush)(new BrushConverter().ConvertFrom("#7fff00"));
                        }
                        video5.FontSize = 15;
                        Grid.SetColumn(video5, 3);
                        parent_grid.Children.Add(video5);
                    }
                }
                status.Content = count + " из " + mainWindow.Context.Product.Count();
            }
        }



        private void Search_Change(object sender, TextChangedEventArgs e)
        {
            var users = mainWindow.Context.User.FromSqlRaw(" SELECT * FROM User WHERE Full_Name LIKE '" + Search.Text + "%'").ToList();
            Userz.Children.Clear();
            foreach (var user in users)
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = 200;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                Userz.Children.Add(parent_grid);

                System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                name.Content = "ФИО: " + user.Full_Name;
                name.Margin = new Thickness(10, 45, 10, 0);
                name.FontWeight = FontWeights.Bold;
                name.FontSize = 20;
                name.Foreground = Brushes.Black;
                name.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(name);

                System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                about.Content = "Логин: " + user.Login;
                about.Margin = new Thickness(10, 115, 10, 0);
                about.VerticalAlignment = VerticalAlignment.Top;
                about.Foreground = Brushes.Black;
                about.FontSize = 20;
                parent_grid.Children.Add(about);

                System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                type.Content = "Пароль: " + user.Password;
                type.Margin = new Thickness(10, 80, 10, 0);
                type.VerticalAlignment = VerticalAlignment.Top;
                type.Foreground = Brushes.Black;
                type.FontSize = 20;
                parent_grid.Children.Add(type);

                System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                video.Content = "Роль: " + user.Role;
                video.Margin = new Thickness(10, 150, 10, 0);
                video.VerticalAlignment = VerticalAlignment.Top;
                video.Foreground = Brushes.Black;
                video.FontSize = 20;
                parent_grid.Children.Add(video);

                System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                buy.Content = "Изменить";
                buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                buy.VerticalAlignment = VerticalAlignment.Bottom;
                buy.Width = 100;
                buy.Height = 50;
                buy.Background = Brushes.White;
                buy.Margin = new Thickness(0, 0, 10, 40);
                buy.Tag = user.id.ToString();
                buy.FontSize = 20;
                buy.FontFamily = new FontFamily("Comic Sans MS");
                buy.Foreground = Brushes.White;
                buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.Click += delegate
                {
                    int id = int.Parse(buy.Tag.ToString());
                    mainWindow.helpclass.id = id;
                    mainWindow.helpclass.add_edit = "Change User";
                    mainWindow.OpenPages(MainWindow.pages.add);
                };
                Grid.SetColumn(buy, 1);
                parent_grid.Children.Add(buy);

                if (mainWindow.helpclass.consroleuser == "Администратор")
                {
                    System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                    del.Content = "Удалить";
                    del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    del.VerticalAlignment = VerticalAlignment.Top;
                    del.Width = 100;
                    del.Height = 50;
                    del.Background = Brushes.White;
                    del.Margin = new Thickness(0, 40, 10, 0);
                    del.Tag = user.id.ToString();
                    del.FontSize = 20;
                    del.FontFamily = new FontFamily("Comic Sans MS");
                    del.Foreground = Brushes.White;
                    del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.Click += delegate
                    {
                        int id = int.Parse(buy.Tag.ToString());
                        Clasess.Users user1 = mainWindow.Context.User.Find(id);
                        mainWindow.Context.User.Remove(user1);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные удалены!");
                    };
                    Grid.SetColumn(del, 1);
                    parent_grid.Children.Add(del);
                }
            }
        }

        private void Search_ChangeO(object sender, TextChangedEventArgs e)
        {
            var orders = mainWindow.Context.Order.FromSqlRaw("SELECT * FROM `Order` WHERE Code LIKE '" + SearchO.Text + "%'").ToList();
            Orderz.Children.Clear();
            foreach (var order in orders)
            {
                if (mainWindow.helpclass.consroleuser == "Клиент")
                {
                    if (mainWindow.helpclass.Uid == order.Client)
                    {
                        Grid parent_grid = new Grid();
                        parent_grid.Height = 220;
                        parent_grid.Margin = new Thickness(20, 20, 20, 20);
                        Orderz.Children.Add(parent_grid);

                        System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                        name.Content = "Код: " + order.Code;
                        name.Margin = new Thickness(10, 45, 10, 0);
                        name.FontWeight = FontWeights.Bold;
                        name.FontSize = 20;
                        name.Foreground = Brushes.Black;
                        name.FontFamily = new FontFamily("Comic Sans MS");
                        parent_grid.Children.Add(name);

                        System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                        about.Content = "Дата заказа: " + order.Order_Date;
                        about.Margin = new Thickness(10, 115, 10, 0);
                        about.VerticalAlignment = VerticalAlignment.Top;
                        about.Foreground = Brushes.Black;
                        about.FontSize = 20;
                        parent_grid.Children.Add(about);

                        System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                        type.Content = "Дата выдачи: " + order.Date_of_issue;
                        type.Margin = new Thickness(10, 80, 10, 0);
                        type.VerticalAlignment = VerticalAlignment.Top;
                        type.Foreground = Brushes.Black;
                        type.FontSize = 20;
                        parent_grid.Children.Add(type);

                        System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                        video.Content = "Клиент: текущий пользователь";
                        video.Margin = new Thickness(10, 150, 10, 0);
                        video.VerticalAlignment = VerticalAlignment.Top;
                        video.Foreground = Brushes.Black;
                        video.FontSize = 20;
                        parent_grid.Children.Add(video);

                        System.Windows.Controls.Label video1 = new System.Windows.Controls.Label();
                        video1.Content = "Статус: " + order.Status;
                        video1.Margin = new Thickness(10, 185, 10, 0);
                        video1.VerticalAlignment = VerticalAlignment.Top;
                        video1.Foreground = Brushes.Black;
                        video1.FontSize = 20;
                        parent_grid.Children.Add(video1);

                        System.Windows.Controls.Label video2 = new System.Windows.Controls.Label();
                        video2.Content = "Точка выдачи: " + order.Point;
                        video2.Margin = new Thickness(10, 210, 10, 0);
                        video2.VerticalAlignment = VerticalAlignment.Top;
                        video2.Foreground = Brushes.Black;
                        video2.FontSize = 20;
                        parent_grid.Children.Add(video2);

                        if (mainWindow.helpclass.consroleuser == "Администратор")
                        {
                            System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                            del.Content = "Удалить";
                            del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                            del.VerticalAlignment = VerticalAlignment.Top;
                            del.Width = 100;
                            del.Height = 50;
                            del.Background = Brushes.White;
                            del.Margin = new Thickness(0, 40, 10, 0);
                            del.Tag = order.id.ToString();
                            del.FontSize = 20;
                            del.FontFamily = new FontFamily("Comic Sans MS");
                            del.Foreground = Brushes.White;
                            del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.Click += delegate
                            {
                                int id = int.Parse(del.Tag.ToString());
                                Clasess.Orders orderz = mainWindow.Context.Order.Find(id);
                                mainWindow.Context.Order.Remove(orderz);
                                mainWindow.Context.SaveChanges();
                                MessageBox.Show("Данные удалены!");
                            };
                            Grid.SetColumn(del, 2);
                            parent_grid.Children.Add(del);
                        }
                    }
                }
                else
                {
                    {
                        Grid parent_grid = new Grid();
                        parent_grid.Height = 220;
                        parent_grid.Margin = new Thickness(20, 20, 20, 20);
                        Orderz.Children.Add(parent_grid);

                        System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                        name.Content = "Код: " + order.Code;
                        name.Margin = new Thickness(10, 45, 10, 0);
                        name.FontWeight = FontWeights.Bold;
                        name.FontSize = 20;
                        name.Foreground = Brushes.Black;
                        name.FontFamily = new FontFamily("Comic Sans MS");
                        parent_grid.Children.Add(name);

                        System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                        about.Content = "Дата заказа: " + order.Order_Date;
                        about.Margin = new Thickness(10, 115, 10, 0);
                        about.VerticalAlignment = VerticalAlignment.Top;
                        about.Foreground = Brushes.Black;
                        about.FontSize = 20;
                        parent_grid.Children.Add(about);

                        System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                        type.Content = "Дата выдачи: " + order.Date_of_issue;
                        type.Margin = new Thickness(10, 80, 10, 0);
                        type.VerticalAlignment = VerticalAlignment.Top;
                        type.Foreground = Brushes.Black;
                        type.FontSize = 20;
                        parent_grid.Children.Add(type);

                        System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                        for (int j = 0; j < mainWindow.users_name.Count(); j++)
                        {
                            if (mainWindow.users_name[j].id == order.id)
                            {
                                video.Content = "Клиент: " + mainWindow.users_name[j].name;
                            }
                        }
                        video.Margin = new Thickness(10, 150, 10, 0);
                        video.VerticalAlignment = VerticalAlignment.Top;
                        video.Foreground = Brushes.Black;
                        video.FontSize = 20;
                        parent_grid.Children.Add(video);

                        System.Windows.Controls.Label video1 = new System.Windows.Controls.Label();
                        video1.Content = "Статус: " + order.Status;
                        video1.Margin = new Thickness(10, 185, 10, 0);
                        video1.VerticalAlignment = VerticalAlignment.Top;
                        video1.Foreground = Brushes.Black;
                        video1.FontSize = 20;
                        parent_grid.Children.Add(video1);

                        System.Windows.Controls.Label video2 = new System.Windows.Controls.Label();
                        video2.Content = "Точка выдачи: " + order.Point;
                        video2.Margin = new Thickness(10, 210, 10, 0);
                        video2.VerticalAlignment = VerticalAlignment.Top;
                        video2.Foreground = Brushes.Black;
                        video2.FontSize = 20;
                        parent_grid.Children.Add(video2);

                        System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                        buy.Content = "Изменить";
                        buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                        buy.Width = 100;
                        buy.Height = 50;
                        buy.Background = Brushes.White;
                        buy.VerticalAlignment = VerticalAlignment.Bottom;
                        buy.Margin = new Thickness(0, 0, 10, 40);
                        buy.Tag = order.id.ToString();
                        buy.FontSize = 20;
                        buy.FontFamily = new FontFamily("Comic Sans MS");
                        buy.Foreground = Brushes.White;
                        buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        buy.Click += delegate
                        {
                            int id = int.Parse(buy.Tag.ToString());
                            mainWindow.helpclass.id = id;
                            mainWindow.helpclass.add_edit = "Change Order";
                            mainWindow.OpenPages(MainWindow.pages.add);
                        };
                        Grid.SetColumn(buy, 1);
                        parent_grid.Children.Add(buy);
                        if (mainWindow.helpclass.consroleuser == "Администратор")
                        {
                            System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                            del.Content = "Удалить";
                            del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                            del.VerticalAlignment = VerticalAlignment.Top;
                            del.Width = 100;
                            del.Height = 50;
                            del.Background = Brushes.White;
                            del.Margin = new Thickness(0, 40, 10, 0);
                            del.Tag = order.id.ToString();
                            del.FontSize = 20;
                            del.FontFamily = new FontFamily("Comic Sans MS");
                            del.Foreground = Brushes.White;
                            del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                            del.Click += delegate
                            {
                                int id = int.Parse(del.Tag.ToString());
                                Clasess.Orders orderz = mainWindow.Context.Order.Find(id);
                                mainWindow.Context.Order.Remove(orderz);
                                mainWindow.Context.SaveChanges();
                                MessageBox.Show("Данные удалены!");
                            };
                            Grid.SetColumn(del, 2);
                            parent_grid.Children.Add(del);
                        }
                    }
                }
            }
        }

        private void Search_ChangeC(object sender, TextChangedEventArgs e)
        {
            var Category = mainWindow.Context.Category.FromSqlRaw(" SELECT * FROM Category WHERE name LIKE '" + SearchC.Text + "%'").ToList();
            Categoryz.Children.Clear();
            foreach (var category in Category)
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = 200;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                Categoryz.Children.Add(parent_grid);

                System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                name.Content = "Заказ: " + category.name;
                name.Margin = new Thickness(10, 45, 10, 0);
                name.FontWeight = FontWeights.Bold;
                name.FontSize = 20;
                name.Foreground = Brushes.Black;
                name.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(name);

                System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                buy.Content = "Изменить";
                buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                buy.Width = 100;
                buy.Height = 50;
                buy.Background = Brushes.White;
                buy.VerticalAlignment = VerticalAlignment.Bottom;
                buy.Margin = new Thickness(0, 0, 10, 40);
                buy.Tag = category.id.ToString();
                buy.FontSize = 20;
                buy.FontFamily = new FontFamily("Comic Sans MS");
                buy.Foreground = Brushes.White;
                buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.Click += delegate
                {
                    int id = int.Parse(buy.Tag.ToString());
                    mainWindow.helpclass.id = id;
                    mainWindow.helpclass.add_edit = "Change Category";
                    mainWindow.OpenPages(MainWindow.pages.add);
                };
                Grid.SetColumn(buy, 1);
                parent_grid.Children.Add(buy);
                if (mainWindow.helpclass.consroleuser == "Администратор")
                {
                    System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                    del.Content = "Удалить";
                    del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    del.VerticalAlignment = VerticalAlignment.Top;
                    del.Width = 100;
                    del.Height = 50;
                    del.Background = Brushes.White;
                    del.Margin = new Thickness(0, 40, 10, 0);
                    del.Tag = category.id.ToString();
                    del.FontSize = 20;
                    del.FontFamily = new FontFamily("Comic Sans MS");
                    del.Foreground = Brushes.White;
                    del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.Click += delegate
                    {
                        int id = int.Parse(del.Tag.ToString());
                        Clasess.Categorys categorys = mainWindow.Context.Category.Find(id);
                        mainWindow.Context.Category.Remove(categorys);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные удалены!");
                    };
                    Grid.SetColumn(del, 2);
                    parent_grid.Children.Add(del);
                }
            }
        }

        private void Search_ChangePr(object sender, TextChangedEventArgs e)
        {
            int count = 0;
            Productz.Children.Clear();
            if (Convert.ToBoolean(up.IsChecked))
            {
                var products = mainWindow.Context.Product.FromSqlRaw(" SELECT * FROM Product WHERE name LIKE '" + SearchPr.Text + "%'").OrderBy(p => p.price);
                foreach (Clasess.Products p in products)
                {
                    if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                    {

                    }
                    else
                    {
                        if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked))
                        {
                            if (14.99 <= p.discount) continue;
                        }
                        else
                        {
                            if (Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                            {
                                if (10 >= p.discount) continue;
                            }
                            else
                            {
                                if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                                {
                                    if (9.99 <= p.discount & 15 >= p.discount) continue;
                                }
                                else
                                {
                                    if (Convert.ToBoolean(zero.IsChecked))
                                    {
                                        if (9.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(ten.IsChecked))
                                    {
                                        if (10 >= p.discount) continue;
                                        if (14.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(fifthy.IsChecked))
                                    {
                                        if (15 >= p.discount) continue;
                                    }
                                }
                            }
                        }
                    }

                    count++;

                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Productz.Children.Add(parent_grid);


                    Image image = new Image();
                    image.Width = 150;
                    image.Height = 150;
                    image.Margin = new Thickness(10, 10, 10, 10);
                    image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    if (mainWindow.imageConverter.LoadImage(p.img_src) == null)
                    {
                        image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + (@"\picture.png")));
                    }
                    else
                    {
                        image.Source = mainWindow.imageConverter.LoadImage(p.img_src);
                    }
                    Grid.SetColumn(image, 1);
                    parent_grid.Children.Add(image);

                    System.Windows.Controls.TextBlock name = new System.Windows.Controls.TextBlock();
                    name.Text = p.name.ToString();
                    name.Margin = new Thickness(10, 45, 10, 0);
                    name.FontWeight = FontWeights.Bold;
                    name.Width = 200;
                    name.FontSize = 15;
                    name.Foreground = Brushes.Black;
                    name.FontFamily = new FontFamily("Comic Sans MS");
                    Grid.SetColumn(name, 2);
                    parent_grid.Children.Add(name);

                    System.Windows.Controls.TextBlock about = new System.Windows.Controls.TextBlock();
                    about.Text = p.description.ToString();
                    about.TextWrapping = TextWrapping.Wrap;
                    about.Height = 70;
                    about.Width = 200;
                    about.Margin = new Thickness(10, 115, 10, 0);
                    about.VerticalAlignment = VerticalAlignment.Top;
                    about.Foreground = Brushes.Black;
                    about.FontSize = 15;
                    Grid.SetColumn(about, 2);
                    parent_grid.Children.Add(about);

                    System.Windows.Controls.TextBlock type = new System.Windows.Controls.TextBlock();
                    type.Text = "Производитель: " + p.manufacturer;
                    type.Margin = new Thickness(10, 80, 10, 0);
                    type.VerticalAlignment = VerticalAlignment.Top;
                    type.Width = 200;
                    type.Foreground = Brushes.Black;
                    type.FontSize = 15;
                    Grid.SetColumn(type, 2);
                    parent_grid.Children.Add(type);

                    System.Windows.Controls.TextBlock video = new System.Windows.Controls.TextBlock();
                    video.Text = p.price.ToString();
                    video.Margin = new Thickness(90, 190, 10, 0);
                    video.Width = 200;
                    video.VerticalAlignment = VerticalAlignment.Top;
                    video.TextDecorations = TextDecorations.Strikethrough;
                    video.Foreground = Brushes.Black;
                    video.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video);

                    System.Windows.Controls.TextBlock video3 = new System.Windows.Controls.TextBlock();
                    video3.Text = "Цена: ";
                    video3.Margin = new Thickness(10, 190, 10, 0);
                    video3.Width = 200;
                    video3.VerticalAlignment = VerticalAlignment.Top;
                    video3.Foreground = Brushes.Black;
                    video3.FontSize = 15;
                    Grid.SetColumn(video3, 2);
                    parent_grid.Children.Add(video3);

                    System.Windows.Controls.TextBlock video2 = new System.Windows.Controls.TextBlock();
                    video2.Text = ((Convert.ToDouble(p.price) - (Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01))).ToString());
                    video2.Margin = new Thickness(180, 190, 10, 0);
                    video2.VerticalAlignment = VerticalAlignment.Top;
                    video2.Foreground = Brushes.Black;
                    video2.Width = 200;
                    video2.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video2);

                    System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                    buy.Content = "Добавить в заказ";
                    buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    buy.Width = 150;
                    buy.Height = 50;
                    buy.Background = Brushes.White;
                    buy.VerticalAlignment = VerticalAlignment.Bottom;
                    buy.Tag = p.id.ToString();
                    buy.FontSize = 15;
                    buy.FontFamily = new FontFamily("Comic Sans MS");
                    buy.Foreground = Brushes.White;
                    buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.Click += delegate
                    {
                        int id = int.Parse(buy.Tag.ToString());
                        mainWindow.AddCart(id);
                        OpenOrder.Visibility = Visibility.Visible;
                    };
                    Grid.SetColumn(buy, 3);
                    parent_grid.Children.Add(buy);

                    if (mainWindow.helpclass.consroleuser == "Администратор")
                    {
                        System.Windows.Controls.Button chan = new System.Windows.Controls.Button();
                        chan.Content = "Изменить";
                        chan.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        chan.Width = 100;
                        chan.Height = 50;
                        chan.Background = Brushes.White;
                        chan.VerticalAlignment = VerticalAlignment.Top;
                        chan.Tag = p.id.ToString();
                        chan.FontSize = 15;
                        chan.FontFamily = new FontFamily("Comic Sans MS");
                        chan.Foreground = Brushes.White;
                        chan.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.Click += delegate
                        {
                            int id = int.Parse(chan.Tag.ToString());
                            mainWindow.helpclass.id = id;
                            mainWindow.helpclass.add_edit = "Change Product";
                            mainWindow.OpenPages(MainWindow.pages.add);
                        };
                        Grid.SetColumn(chan, 2);
                        parent_grid.Children.Add(chan);

                        System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                        del.Content = "Удалить";
                        del.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        del.Margin = new Thickness(120, 0, 0, 0);
                        del.Width = 100;
                        del.Height = 50;
                        del.Background = Brushes.White;
                        del.VerticalAlignment = VerticalAlignment.Top;
                        del.Tag = p.id.ToString();
                        del.FontSize = 15;
                        del.FontFamily = new FontFamily("Comic Sans MS");
                        del.Foreground = Brushes.White;
                        del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.Click += delegate
                        {
                            int id = int.Parse(del.Tag.ToString());
                            Clasess.Products products1 = mainWindow.Context.Product.Find(id);
                            mainWindow.Context.Product.Remove(products1);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные удалены!");
                        };
                        Grid.SetColumn(del, 2);
                        parent_grid.Children.Add(del);
                    }


                    System.Windows.Controls.TextBlock video5 = new System.Windows.Controls.TextBlock();
                    video5.Text = ((Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01)).ToString());
                    video5.Margin = new Thickness(10, 10, 10, 10);
                    video5.VerticalAlignment = VerticalAlignment.Center;
                    video5.HorizontalAlignment = HorizontalAlignment.Right;
                    video5.Foreground = Brushes.Black;
                    if (p.discount > 15)
                    {
                        video5.Background = (Brush)(new BrushConverter().ConvertFrom("#7fff00"));
                    }
                    video5.FontSize = 15;
                    Grid.SetColumn(video5, 3);
                    parent_grid.Children.Add(video5);
                }
            }
            else
            if (Convert.ToBoolean(down.IsChecked))
            {
                var products = mainWindow.Context.Product.FromSqlRaw(" SELECT * FROM Product WHERE name LIKE '" + SearchPr.Text + "%'").OrderByDescending(p => p.price);
                foreach (Clasess.Products p in products)
                {
                    if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                    {

                    }
                    else
                    {
                        if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked))
                        {
                            if (14.99 <= p.discount) continue;
                        }
                        else
                        {
                            if (Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                            {
                                if (10 >= p.discount) continue;
                            }
                            else
                            {
                                if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                                {
                                    if (9.99 <= p.discount & 15 >= p.discount) continue;
                                }
                                else
                                {
                                    if (Convert.ToBoolean(zero.IsChecked))
                                    {
                                        if (9.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(ten.IsChecked))
                                    {
                                        if (10 >= p.discount) continue;
                                        if (14.99 <= p.discount) continue;
                                    }

                                    if (Convert.ToBoolean(fifthy.IsChecked))
                                    {
                                        if (15 >= p.discount) continue;
                                    }
                                }
                            }
                        }
                    }
                    count++;
                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Productz.Children.Add(parent_grid);


                    Image image = new Image();
                    image.Width = 150;
                    image.Height = 150;
                    image.Margin = new Thickness(10, 10, 10, 10);
                    image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    if (mainWindow.imageConverter.LoadImage(p.img_src) == null)
                    {
                        image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + (@"\picture.png")));
                    }
                    else
                    {
                        image.Source = mainWindow.imageConverter.LoadImage(p.img_src);
                    }
                    Grid.SetColumn(image, 1);
                    parent_grid.Children.Add(image);

                    System.Windows.Controls.TextBlock name = new System.Windows.Controls.TextBlock();
                    name.Text = p.name.ToString();
                    name.Margin = new Thickness(10, 45, 10, 0);
                    name.FontWeight = FontWeights.Bold;
                    name.Width = 200;
                    name.FontSize = 15;
                    name.Foreground = Brushes.Black;
                    name.FontFamily = new FontFamily("Comic Sans MS");
                    Grid.SetColumn(name, 2);
                    parent_grid.Children.Add(name);

                    System.Windows.Controls.TextBlock about = new System.Windows.Controls.TextBlock();
                    about.Text = p.description.ToString();
                    about.TextWrapping = TextWrapping.Wrap;
                    about.Height = 70;
                    about.Width = 200;
                    about.Margin = new Thickness(10, 115, 10, 0);
                    about.VerticalAlignment = VerticalAlignment.Top;
                    about.Foreground = Brushes.Black;
                    about.FontSize = 15;
                    Grid.SetColumn(about, 2);
                    parent_grid.Children.Add(about);

                    System.Windows.Controls.TextBlock type = new System.Windows.Controls.TextBlock();
                    type.Text = "Производитель: " + p.manufacturer;
                    type.Margin = new Thickness(10, 80, 10, 0);
                    type.VerticalAlignment = VerticalAlignment.Top;
                    type.Width = 200;
                    type.Foreground = Brushes.Black;
                    type.FontSize = 15;
                    Grid.SetColumn(type, 2);
                    parent_grid.Children.Add(type);

                    System.Windows.Controls.TextBlock video = new System.Windows.Controls.TextBlock();
                    video.Text = p.price.ToString();
                    video.Margin = new Thickness(90, 190, 10, 0);
                    video.Width = 200;
                    video.VerticalAlignment = VerticalAlignment.Top;
                    video.TextDecorations = TextDecorations.Strikethrough;
                    video.Foreground = Brushes.Black;
                    video.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video);

                    System.Windows.Controls.TextBlock video3 = new System.Windows.Controls.TextBlock();
                    video3.Text = "Цена: ";
                    video3.Margin = new Thickness(10, 190, 10, 0);
                    video3.Width = 200;
                    video3.VerticalAlignment = VerticalAlignment.Top;
                    video3.Foreground = Brushes.Black;
                    video3.FontSize = 15;
                    Grid.SetColumn(video3, 2);
                    parent_grid.Children.Add(video3);

                    System.Windows.Controls.TextBlock video2 = new System.Windows.Controls.TextBlock();
                    video2.Text = ((Convert.ToDouble(p.price) - (Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01))).ToString());
                    video2.Margin = new Thickness(180, 190, 10, 0);
                    video2.VerticalAlignment = VerticalAlignment.Top;
                    video2.Foreground = Brushes.Black;
                    video2.Width = 200;
                    video2.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video2);

                    System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                    buy.Content = "Добавить в заказ";
                    buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    buy.Width = 150;
                    buy.Height = 50;
                    buy.Background = Brushes.White;
                    buy.VerticalAlignment = VerticalAlignment.Bottom;
                    buy.Tag = p.id.ToString();
                    buy.FontSize = 15;
                    buy.FontFamily = new FontFamily("Comic Sans MS");
                    buy.Foreground = Brushes.White;
                    buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.Click += delegate
                    {
                        int id = int.Parse(buy.Tag.ToString());
                        mainWindow.AddCart(id);
                        OpenOrder.Visibility = Visibility.Visible;
                    };
                    Grid.SetColumn(buy, 3);
                    parent_grid.Children.Add(buy);

                    if (mainWindow.helpclass.consroleuser == "Администратор")
                    {
                        System.Windows.Controls.Button chan = new System.Windows.Controls.Button();
                        chan.Content = "Изменить";
                        chan.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        chan.Width = 100;
                        chan.Height = 50;
                        chan.Background = Brushes.White;
                        chan.VerticalAlignment = VerticalAlignment.Top;
                        chan.Tag = p.id.ToString();
                        chan.FontSize = 15;
                        chan.FontFamily = new FontFamily("Comic Sans MS");
                        chan.Foreground = Brushes.White;
                        chan.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.Click += delegate
                        {
                            int id = int.Parse(chan.Tag.ToString());
                            mainWindow.helpclass.id = id;
                            mainWindow.helpclass.add_edit = "Change Product";
                            mainWindow.OpenPages(MainWindow.pages.add);
                        };
                        Grid.SetColumn(chan, 2);
                        parent_grid.Children.Add(chan);

                        System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                        del.Content = "Удалить";
                        del.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        del.Margin = new Thickness(120, 0, 0, 0);
                        del.Width = 100;
                        del.Height = 50;
                        del.Background = Brushes.White;
                        del.VerticalAlignment = VerticalAlignment.Top;
                        del.Tag = p.id.ToString();
                        del.FontSize = 15;
                        del.FontFamily = new FontFamily("Comic Sans MS");
                        del.Foreground = Brushes.White;
                        del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.Click += delegate
                        {
                            int id = int.Parse(del.Tag.ToString());
                            Clasess.Products products1 = mainWindow.Context.Product.Find(id);
                            mainWindow.Context.Product.Remove(products1);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные удалены!");
                        };
                        Grid.SetColumn(del, 2);
                        parent_grid.Children.Add(del);
                    }


                    System.Windows.Controls.TextBlock video5 = new System.Windows.Controls.TextBlock();
                    video5.Text = ((Convert.ToDouble(p.price) * (Convert.ToDouble(p.discount) * 0.01)).ToString());
                    video5.Margin = new Thickness(10, 10, 10, 10);
                    video5.VerticalAlignment = VerticalAlignment.Center;
                    video5.HorizontalAlignment = HorizontalAlignment.Right;
                    video5.Foreground = Brushes.Black;
                    if (p.discount > 15)
                    {
                        video5.Background = (Brush)(new BrushConverter().ConvertFrom("#7fff00"));
                    }
                    video5.FontSize = 15;
                    Grid.SetColumn(video5, 3);
                    parent_grid.Children.Add(video5);
                }
            }
            else
            {
                var products = mainWindow.Context.Product.FromSqlRaw(" SELECT * FROM Product WHERE name LIKE '" + SearchPr.Text + "%'").ToList();
                foreach (var pr in products)
                {
                    if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                    {

                    }
                    else
                    {
                        if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(ten.IsChecked))
                        {
                            if (14.99 <= pr.discount) continue;
                        }
                        else
                        {
                            if (Convert.ToBoolean(ten.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                            {
                                if (10 >= pr.discount) continue;
                            }
                            else
                            {
                                if (Convert.ToBoolean(zero.IsChecked) & Convert.ToBoolean(fifthy.IsChecked))
                                {
                                    if (9.99 <= pr.discount & 15 >= pr.discount) continue;
                                }
                                else
                                {
                                    if (Convert.ToBoolean(zero.IsChecked))
                                    {
                                        if (9.99 <= pr.discount) continue;
                                    }

                                    if (Convert.ToBoolean(ten.IsChecked))
                                    {
                                        if (10 >= pr.discount) continue;
                                        if (14.99 <= pr.discount) continue;
                                    }

                                    if (Convert.ToBoolean(fifthy.IsChecked))
                                    {
                                        if (15 >= pr.discount) continue;
                                    }
                                }
                            }
                        }
                    }

                    count++;
                    Grid parent_grid = new Grid();
                    parent_grid.Height = 220;
                    parent_grid.Margin = new Thickness(20, 20, 20, 20);
                    Productz.Children.Add(parent_grid);


                    Image image = new Image();
                    image.Width = 150;
                    image.Height = 150;
                    image.Margin = new Thickness(10, 10, 10, 10);
                    image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    if (mainWindow.imageConverter.LoadImage(pr.img_src) == null)
                    {
                        image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + (@"\picture.png")));
                    }
                    else
                    {
                        image.Source = mainWindow.imageConverter.LoadImage(pr.img_src);
                    }
                    Grid.SetColumn(image, 1);
                    parent_grid.Children.Add(image);

                    System.Windows.Controls.TextBlock name = new System.Windows.Controls.TextBlock();
                    name.Text = pr.name.ToString();
                    name.Margin = new Thickness(10, 45, 10, 0);
                    name.FontWeight = FontWeights.Bold;
                    name.Width = 200;
                    name.FontSize = 15;
                    name.Foreground = Brushes.Black;
                    name.FontFamily = new FontFamily("Comic Sans MS");
                    Grid.SetColumn(name, 2);
                    parent_grid.Children.Add(name);

                    System.Windows.Controls.TextBlock about = new System.Windows.Controls.TextBlock();
                    about.Text = pr.description.ToString();
                    about.TextWrapping = TextWrapping.Wrap;
                    about.Height = 70;
                    about.Width = 200;
                    about.Margin = new Thickness(10, 115, 10, 0);
                    about.VerticalAlignment = VerticalAlignment.Top;
                    about.Foreground = Brushes.Black;
                    about.FontSize = 15;
                    Grid.SetColumn(about, 2);
                    parent_grid.Children.Add(about);

                    System.Windows.Controls.TextBlock type = new System.Windows.Controls.TextBlock();
                    type.Text = "Производитель: " + pr.manufacturer;
                    type.Margin = new Thickness(10, 80, 10, 0);
                    type.VerticalAlignment = VerticalAlignment.Top;
                    type.Width = 200;
                    type.Foreground = Brushes.Black;
                    type.FontSize = 15;
                    Grid.SetColumn(type, 2);
                    parent_grid.Children.Add(type);

                    System.Windows.Controls.TextBlock video = new System.Windows.Controls.TextBlock();
                    video.Text = pr.price.ToString();
                    video.Margin = new Thickness(90, 190, 10, 0);
                    video.Width = 200;
                    video.VerticalAlignment = VerticalAlignment.Top;
                    video.TextDecorations = TextDecorations.Strikethrough;
                    video.Foreground = Brushes.Black;
                    video.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video);

                    System.Windows.Controls.TextBlock video3 = new System.Windows.Controls.TextBlock();
                    video3.Text = "Цена: ";
                    video3.Margin = new Thickness(10, 190, 10, 0);
                    video3.Width = 200;
                    video3.VerticalAlignment = VerticalAlignment.Top;
                    video3.Foreground = Brushes.Black;
                    video3.FontSize = 15;
                    Grid.SetColumn(video3, 2);
                    parent_grid.Children.Add(video3);

                    System.Windows.Controls.TextBlock video2 = new System.Windows.Controls.TextBlock();
                    video2.Text = ((Convert.ToDouble(pr.price) - (Convert.ToDouble(pr.price) * (Convert.ToDouble(pr.discount) * 0.01))).ToString());
                    video2.Margin = new Thickness(180, 190, 10, 0);
                    video2.VerticalAlignment = VerticalAlignment.Top;
                    video2.Foreground = Brushes.Black;
                    video2.Width = 200;
                    video2.FontSize = 15;
                    Grid.SetColumn(video, 2);
                    parent_grid.Children.Add(video2);

                    System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                    buy.Content = "Добавить в заказ";
                    buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    buy.Width = 150;
                    buy.Height = 50;
                    buy.Background = Brushes.White;
                    buy.VerticalAlignment = VerticalAlignment.Bottom;
                    buy.Tag = pr.id.ToString();
                    buy.FontSize = 15;
                    buy.FontFamily = new FontFamily("Comic Sans MS");
                    buy.Foreground = Brushes.White;
                    buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    buy.Click += delegate
                    {
                        int id = int.Parse(buy.Tag.ToString());
                        mainWindow.AddCart(id);
                        OpenOrder.Visibility = Visibility.Visible;
                    };
                    Grid.SetColumn(buy, 3);
                    parent_grid.Children.Add(buy);

                    if (mainWindow.helpclass.consroleuser == "Администратор")
                    {
                        System.Windows.Controls.Button chan = new System.Windows.Controls.Button();
                        chan.Content = "Изменить";
                        chan.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        chan.Width = 100;
                        chan.Height = 50;
                        chan.Background = Brushes.White;
                        chan.VerticalAlignment = VerticalAlignment.Top;
                        chan.Tag = pr.id.ToString();
                        chan.FontSize = 15;
                        chan.FontFamily = new FontFamily("Comic Sans MS");
                        chan.Foreground = Brushes.White;
                        chan.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        chan.Click += delegate
                        {
                            int id = int.Parse(chan.Tag.ToString());
                            mainWindow.helpclass.id = id;
                            mainWindow.helpclass.add_edit = "Change Product";
                            mainWindow.OpenPages(MainWindow.pages.add);
                        };
                        Grid.SetColumn(chan, 2);
                        parent_grid.Children.Add(chan);

                        System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                        del.Content = "Удалить";
                        del.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        del.Margin = new Thickness(120, 0, 0, 0);
                        del.Width = 100;
                        del.Height = 50;
                        del.Background = Brushes.White;
                        del.VerticalAlignment = VerticalAlignment.Top;
                        del.Tag = pr.id.ToString();
                        del.FontSize = 15;
                        del.FontFamily = new FontFamily("Comic Sans MS");
                        del.Foreground = Brushes.White;
                        del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                        del.Click += delegate
                        {
                            int id = int.Parse(del.Tag.ToString());
                            Clasess.Products products1 = mainWindow.Context.Product.Find(id);
                            mainWindow.Context.Product.Remove(products1);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные удалены!");
                        };
                        Grid.SetColumn(del, 2);
                        parent_grid.Children.Add(del);
                    }


                    System.Windows.Controls.TextBlock video5 = new System.Windows.Controls.TextBlock();
                    video5.Text = ((Convert.ToDouble(pr.price) * (Convert.ToDouble(pr.discount) * 0.01)).ToString());
                    video5.Margin = new Thickness(10, 10, 10, 10);
                    video5.VerticalAlignment = VerticalAlignment.Center;
                    video5.HorizontalAlignment = HorizontalAlignment.Right;
                    video5.Foreground = Brushes.Black;
                    if (pr.discount > 15)
                    {
                        video5.Background = (Brush)(new BrushConverter().ConvertFrom("#7fff00"));
                    }
                    video5.FontSize = 15;
                    Grid.SetColumn(video5, 3);
                    parent_grid.Children.Add(video5);

                }
            }
            status.Content = count + " из " + mainWindow.Context.Product.Count();
        }

          private void Search_ChangeP(object sender, TextChangedEventArgs e)
            {
            var Point = mainWindow.Context.Point.FromSqlRaw(" SELECT * FROM Point WHERE post_code LIKE '" + SearchP.Text + "%'").ToList();
            Pointz.Children.Clear();
            foreach (var point in Point)
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = 200;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                Pointz.Children.Add(parent_grid);

                System.Windows.Controls.Label name = new System.Windows.Controls.Label();
                name.Content = "Почтовый индекс: " + point.post_code;
                name.Margin = new Thickness(10, 45, 10, 0);
                name.FontWeight = FontWeights.Bold;
                name.FontSize = 20;
                name.Foreground = Brushes.Black;
                name.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(name);

                System.Windows.Controls.Label about = new System.Windows.Controls.Label();
                about.Content = "Город: " + point.city;
                about.Margin = new Thickness(10, 115, 10, 0);
                about.VerticalAlignment = VerticalAlignment.Top;
                about.Foreground = Brushes.Black;
                about.FontSize = 20;
                parent_grid.Children.Add(about);

                System.Windows.Controls.Label type = new System.Windows.Controls.Label();
                type.Content = "Улица: " + point.street;
                type.Margin = new Thickness(10, 80, 10, 0);
                type.VerticalAlignment = VerticalAlignment.Top;
                type.Foreground = Brushes.Black;
                type.FontSize = 20;
                parent_grid.Children.Add(type);

                System.Windows.Controls.Label video = new System.Windows.Controls.Label();
                video.Content = "Дом: " + point.house;
                video.Margin = new Thickness(10, 150, 10, 0);
                video.VerticalAlignment = VerticalAlignment.Top;
                video.Foreground = Brushes.Black;
                video.FontSize = 20;
                parent_grid.Children.Add(video);

                System.Windows.Controls.Button buy = new System.Windows.Controls.Button();
                buy.Content = "Изменить";
                buy.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                buy.Width = 100;
                buy.Height = 50;
                buy.Background = Brushes.White;
                buy.VerticalAlignment = VerticalAlignment.Bottom;
                buy.Margin = new Thickness(0, 0, 10, 40);
                buy.Tag = point.id.ToString();
                buy.FontSize = 20;
                buy.FontFamily = new FontFamily("Comic Sans MS");
                buy.Foreground = Brushes.White;
                buy.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                buy.Click += delegate
                {
                    int id = int.Parse(buy.Tag.ToString());
                    mainWindow.helpclass.id = id;
                    mainWindow.helpclass.add_edit = "Change Point";
                    mainWindow.OpenPages(MainWindow.pages.add);
                };
                Grid.SetColumn(buy, 1);
                parent_grid.Children.Add(buy);


                if (mainWindow.helpclass.consroleuser == "Администратор")
                {
                    System.Windows.Controls.Button del = new System.Windows.Controls.Button();
                    del.Content = "Удалить";
                    del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    del.VerticalAlignment = VerticalAlignment.Top;
                    del.Width = 100;
                    del.Height = 50;
                    del.Background = Brushes.White;
                    del.Margin = new Thickness(0, 40, 10, 0);
                    del.Tag = point.id.ToString();
                    del.FontSize = 20;
                    del.FontFamily = new FontFamily("Comic Sans MS");
                    del.Foreground = Brushes.White;
                    del.Background = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#498c51"));
                    del.Click += delegate
                    {
                        int id = int.Parse(del.Tag.ToString());
                        Clasess.Points point1 = mainWindow.Context.Point.Find(id);
                        mainWindow.Context.Point.Remove(point1);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные удалены!");
                    };
                    Grid.SetColumn(del, 2);
                    parent_grid.Children.Add(del);
                }
            }
        }
        public bool P = false;
        private void Point_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (P == false)
            {
            P = true;
            CreatedPoint();
            }
        }
        public bool U = false;
        private void Users_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (U == false)
            {
                U = true;
                CreatedUser();
            }
        }

        public bool C = false;

        private void Category_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (C == false)
            {
                C = true;
                CreatedCategory();
            }
        }

        public bool O = false;
        private void Order_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (O == false)
            {
                O = true;
                CreatedOrder();
                if (mainWindow.helpclass.consroleuser == "Клиент")
                {
                    AddButO.Visibility = Visibility.Hidden;
                }
            }
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.helpclass.add_edit = "Add User";
            mainWindow.OpenPages(MainWindow.pages.add);
        }

        private void AddButP_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.helpclass.add_edit = "Add Point";
            mainWindow.OpenPages(MainWindow.pages.add);
        }

        private void AddButC_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.helpclass.add_edit = "Add Category";
            mainWindow.OpenPages(MainWindow.pages.add);
        }

        private void AddButO_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.helpclass.add_edit = "Add Order";
            mainWindow.OpenPages(MainWindow.pages.add);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.login);
            mainWindow.carts.Clear();
            mainWindow.AddNull();
        }

        private void OpenOrder_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.cart);
        }

        private void down_Checked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
            if (up.IsChecked == true)
            {
                up.IsChecked = false;
            }
        }

        private void up_Checked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
            if (down.IsChecked == true)
            {
                down.IsChecked = false;
            }
        }

        private void fifthy_Checked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void ten_Checked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void zero_Checked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void zero_Unchecked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void ten_Unchecked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void fifthy_Unchecked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void up_Unchecked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void down_Unchecked(object sender, RoutedEventArgs e)
        {
            CreatedProducts();
        }

        private void AddButPr_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.helpclass.add_edit = "Add Product";
            mainWindow.OpenPages(MainWindow.pages.add);
        }
    }
}




































































//VOLK
//VOLK
//VOLK
//VOLK
//VOLK
//VOLK//VOLK//VOLK//VOLK//VOLK//VOLK//VOLK//VOLK












//VOLK//VOLK

//VOLK
//VOLK
//VOLK
//VOLK//VOLK

//VOLK//VOLK
//VOLK//VOLK
//VOLK//VOLK
//VOLK
//VOLK
//VOLK
//VOLK