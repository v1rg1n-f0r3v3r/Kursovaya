using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public MainWindow mainWindow;
        public Add(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            CreateShemaAdd();
        }
        public byte[] h;
        private void CreateShemaAdd()
        {
            string[] s = mainWindow.helpclass.add_edit.Split(' ');
            if (s[1] == "User")
            {
            Grid parent_grid = new Grid();
            parent_grid.Height = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight - 50; ;
            parent_grid.Margin = new Thickness(20, 20, 20, 20);
            UI.Children.Add(parent_grid);

            Label label = new Label();
            label.Content = "ФИО:";
            label.Margin = new Thickness(10, 0, 10, 0);
            label.FontSize = 20;
            label.Foreground = Brushes.Black;
            label.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(label);

            TextBox name = new TextBox();
            name.Margin = new Thickness(10, 35, 10, 0);
            name.FontWeight = FontWeights.Bold;
            name.Height = 20;
            name.FontSize = 10;
            name.Tag = "name";
            name.VerticalAlignment = VerticalAlignment.Top;
            name.Foreground = Brushes.Black;
            name.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(name);

            Label label2 = new Label();
            label2.Content = "Логин:";
            label2.Margin = new Thickness(10, 50, 10, 0);
            label2.FontSize = 20;
            label2.Foreground = Brushes.Black;
            label2.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(label2);

            TextBox log = new TextBox();
            log.Margin = new Thickness(10, 85, 10, 0);
            log.FontWeight = FontWeights.Bold;
            log.Height = 20;
            log.VerticalAlignment = VerticalAlignment.Top;
            log.FontSize = 10;
            log.Tag = "log";
            log.Foreground = Brushes.Black;
            log.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(log);

            Label label3 = new Label();
            label3.Content = "Пароль:";
            label3.Margin = new Thickness(10, 100, 10, 0);
            label3.FontSize = 20;
            label3.Foreground = Brushes.Black;
            label3.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(label3);

            TextBox pas = new TextBox();
            pas.Margin = new Thickness(10, 135, 10, 0);
            pas.FontWeight = FontWeights.Bold;
            pas.VerticalAlignment = VerticalAlignment.Top;
            pas.Height = 20;
            pas.FontSize = 10;
            pas.Tag = "pas";
            pas.Foreground = Brushes.Black;
            pas.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(pas);

            Label label4 = new Label();
            label4.Content = "Роль:";
            label4.Margin = new Thickness(10, 150, 10, 0);
            label4.FontSize = 20;
            label4.Foreground = Brushes.Black;
            label4.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(label4);

            ComboBox role = new ComboBox();
            role.Margin = new Thickness(10, 185, 10, 0);
            role.FontWeight = FontWeights.Bold;
            role.Height = 20;
            role.FontSize = 10;
            role.Tag = "role";
            role.VerticalAlignment = VerticalAlignment.Top;
            role.Foreground = Brushes.Black;
            role.FontFamily = new FontFamily("Comic Sans MS");
            parent_grid.Children.Add(role);
            role.Items.Add("Клиент");
            role.Items.Add("Администратор");
            role.Items.Add("Менеджер");

            if (s[0] == "Add")
            {
                System.Windows.Controls.Button add = new System.Windows.Controls.Button();
                add.Content = "Добавить";
                add.VerticalAlignment = VerticalAlignment.Bottom;
                add.Height = 40;
                add.Background = Brushes.White;
                add.FontSize = 20;
                add.FontFamily = new FontFamily("Comic Sans MS");
                add.Foreground = Brushes.White;
                add.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                add.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                add.Click += delegate
                {
                    Clasess.Users user = new Clasess.Users();
                    {
                        if (name.Text != null)
                        {
                            if (role.Text != null)
                            {
                                if (log.Text != null)
                                {
                                    if (pas.Text != null)
                                    {
                                        user.Full_Name = name.Text;
                                        user.Role = role.Text;
                                        user.Login = log.Text;
                                        user.Password = pas.Text;
                                    }
                                    else pas.Text = "";
                                }
                                else log.Text = "";
                            }
                            else role.Text = "";
                        }
                        else name.Text = "";
                    };
                    try
                    {
                        mainWindow.Context.User.Add(user);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные внесены!");
                    }
                    catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                    mainWindow.OpenPages(MainWindow.pages.main);
                };
                parent_grid.Children.Add(add);
            }
                if (s[0] == "Change")
                {
                    int idU = mainWindow.helpclass.id;
                    name.Text = mainWindow.Context.User.Find(idU).Full_Name;
                    role.Text = mainWindow.Context.User.Find(idU).Role;
                    log.Text = mainWindow.Context.User.Find(idU).Login;
                    pas.Text = mainWindow.Context.User.Find(idU).Password;

                    System.Windows.Controls.Button change = new System.Windows.Controls.Button();
                    change.Content = "Изменить";
                    change.VerticalAlignment = VerticalAlignment.Bottom;
                    change.Height = 40;
                    change.Background = Brushes.White;
                    change.FontSize = 20;
                    change.FontFamily = new FontFamily("Comic Sans MS");
                    change.Foreground = Brushes.White;
                    change.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.Click += delegate
                    {
                        var dbUser = mainWindow.Context.User.FirstOrDefault(x => x.id.Equals(idU));
                        {
                            if (name.Text != null)
                            {
                                if (role.Text != null)
                                {
                                    if (log.Text != null)
                                    {
                                        if (pas.Text != null)
                                        {
                                            dbUser.Full_Name = name.Text;
                                            dbUser.Role = role.Text;
                                            dbUser.Login = log.Text;
                                            dbUser.Password = pas.Text;
                                        }
                                        else pas.Text = "";
                                    }
                                    else log.Text = "";
                                }
                                else role.Text = "";
                            }
                            else name.Text = "";
                        };
                        try
                        {
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные изменены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(change);
                }
            }
            if (s[1] == "Point")
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight - 50; ;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                UI.Children.Add(parent_grid);

                Label label = new Label();
                label.Content = "Почтовый индекс:";
                label.Margin = new Thickness(10, 0, 10, 0);
                label.FontSize = 20;
                label.Foreground = Brushes.Black;
                label.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label);

                TextBox post_code = new TextBox();
                post_code.Margin = new Thickness(10, 35, 10, 0);
                post_code.FontWeight = FontWeights.Bold;
                post_code.Height = 20;
                post_code.FontSize = 10;
                post_code.Tag = "name";
                post_code.VerticalAlignment = VerticalAlignment.Top;
                post_code.Foreground = Brushes.Black;
                post_code.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(post_code);

                Label label2 = new Label();
                label2.Content = "Город:";
                label2.Margin = new Thickness(10, 50, 10, 0);
                label2.FontSize = 20;
                label2.Foreground = Brushes.Black;
                label2.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label2);

                TextBox city = new TextBox();
                city.Margin = new Thickness(10, 85, 10, 0);
                city.FontWeight = FontWeights.Bold;
                city.Height = 20;
                city.VerticalAlignment = VerticalAlignment.Top;
                city.FontSize = 10;
                city.Tag = "log";
                city.Foreground = Brushes.Black;
                city.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(city);

                Label label3 = new Label();
                label3.Content = "Улица:";
                label3.Margin = new Thickness(10, 100, 10, 0);
                label3.FontSize = 20;
                label3.Foreground = Brushes.Black;
                label3.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label3);

                TextBox street = new TextBox();
                street.Margin = new Thickness(10, 135, 10, 0);
                street.FontWeight = FontWeights.Bold;
                street.VerticalAlignment = VerticalAlignment.Top;
                street.Height = 20;
                street.FontSize = 10;
                street.Tag = "pas";
                street.Foreground = Brushes.Black;
                street.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(street);

                Label label4 = new Label();
                label4.Content = "Дом:";
                label4.Margin = new Thickness(10, 150, 10, 0);
                label4.FontSize = 20;
                label4.Foreground = Brushes.Black;
                label4.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label4);

                TextBox house = new TextBox();
                house.Margin = new Thickness(10, 185, 10, 0);
                house.FontWeight = FontWeights.Bold;
                house.Height = 20;
                house.FontSize = 10;
                house.Tag = "role";
                house.VerticalAlignment = VerticalAlignment.Top;
                house.Foreground = Brushes.Black;
                house.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(house);

                if (s[0] == "Add")
                {
                    System.Windows.Controls.Button add = new System.Windows.Controls.Button();
                    add.Content = "Добавить";
                    add.VerticalAlignment = VerticalAlignment.Bottom;
                    add.Height = 40;
                    add.Background = Brushes.White;
                    add.FontSize = 20;
                    add.FontFamily = new FontFamily("Comic Sans MS");
                    add.Foreground = Brushes.White;
                    add.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.Click += delegate
                    {
                        Clasess.Points point = new Clasess.Points();
                        {
                            if (post_code.Text != null)
                            {
                                if (city.Text != null)
                                {
                                    if (street.Text != null)
                                    {
                                        if (house.Text != null)
                                        {
                                            point.post_code = post_code.Text;
                                            point.city = city.Text;
                                            point.street = street.Text;
                                            point.house = house.Text;
                                        }
                                        else post_code.Text = "";
                                    }
                                    else city.Text = "";
                                }
                                else street.Text = "";
                            }
                            else house.Text = "";
                        };
                        try
                        {
                            mainWindow.Context.Point.Add(point);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные внесены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(add);
                }
                if (s[0] == "Change")
                {
                    int idP = mainWindow.helpclass.id;
                    post_code.Text = mainWindow.Context.Point.Find(idP).post_code;
                    city.Text = mainWindow.Context.Point.Find(idP).city;
                    street.Text = mainWindow.Context.Point.Find(idP).street;
                    house.Text = mainWindow.Context.Point.Find(idP).house;

                    System.Windows.Controls.Button change = new System.Windows.Controls.Button();
                    change.Content = "Изменить";
                    change.VerticalAlignment = VerticalAlignment.Bottom;
                    change.Height = 40;
                    change.Background = Brushes.White;
                    change.FontSize = 20;
                    change.FontFamily = new FontFamily("Comic Sans MS");
                    change.Foreground = Brushes.White;
                    change.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.Click += delegate
                    {
                        var dbPoint = mainWindow.Context.Point.FirstOrDefault(x => x.id.Equals(idP));
                        {
                            if (post_code.Text != null)
                            {
                                if (city.Text != null)
                                {
                                    if (street.Text != null)
                                    {
                                        if (house.Text != null)
                                        {
                                            dbPoint.post_code = post_code.Text;
                                            dbPoint.city = city.Text;
                                            dbPoint.street = street.Text;
                                            dbPoint.house = house.Text;
                                        }
                                        else post_code.Text = "";
                                    }
                                    else city.Text = "";
                                }
                                else street.Text = "";
                            }
                            else house.Text = "";
                        };
                        try
                        {
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные изменены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(change);
                }
            }
            if (s[1] == "Category")
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight - 50; ;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                UI.Children.Add(parent_grid);

                Label label = new Label();
                label.Content = "Название:";
                label.Margin = new Thickness(10, 0, 10, 0);
                label.FontSize = 20;
                label.Foreground = Brushes.Black;
                label.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label);

                TextBox post_code = new TextBox();
                post_code.Margin = new Thickness(10, 35, 10, 0);
                post_code.FontWeight = FontWeights.Bold;
                post_code.Height = 20;
                post_code.FontSize = 10;
                post_code.Tag = "name";
                post_code.VerticalAlignment = VerticalAlignment.Top;
                post_code.Foreground = Brushes.Black;
                post_code.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(post_code);

                if (s[0] == "Add")
                {
                    System.Windows.Controls.Button add = new System.Windows.Controls.Button();
                    add.Content = "Добавить";
                    add.VerticalAlignment = VerticalAlignment.Bottom;
                    add.Height = 40;
                    add.Background = Brushes.White;
                    add.FontSize = 20;
                    add.FontFamily = new FontFamily("Comic Sans MS");
                    add.Foreground = Brushes.White;
                    add.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.Click += delegate
                    {
                        Clasess.Categorys categorys = new Clasess.Categorys();
                        {
                            if (post_code.Text != null)
                            {
                                categorys.name = post_code.Text;
                            }
                            else post_code.Text = "";
                        };
                        try
                        {
                            mainWindow.Context.Category.Add(categorys);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные внесены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(add);
                }
                if (s[0] == "Change")
                {
                    int idC = mainWindow.helpclass.id;
                    post_code.Text = mainWindow.Context.Category.Find(idC).name;

                    System.Windows.Controls.Button change = new System.Windows.Controls.Button();
                    change.Content = "Изменить";
                    change.VerticalAlignment = VerticalAlignment.Bottom;
                    change.Height = 40;
                    change.Background = Brushes.White;
                    change.FontSize = 20;
                    change.FontFamily = new FontFamily("Comic Sans MS");
                    change.Foreground = Brushes.White;
                    change.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.Click += delegate
                    {
                        var dbCategory = mainWindow.Context.Category.FirstOrDefault(x => x.id.Equals(idC));
                        {
                            if (post_code.Text != null)
                            {
                                dbCategory.name = post_code.Text;
                            }
                            else post_code.Text = "";
                        };
                        try
                        {
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные изменены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(change);
                }
            }
            if (s[1] == "Order")
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight - 50; ;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                UI.Children.Add(parent_grid);

                Label label = new Label();
                label.Content = "Дата заказа: ";
                label.Margin = new Thickness(10, 0, 10, 0);
                label.FontSize = 20;
                label.Foreground = Brushes.Black;
                label.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label);

                TextBox Order_date = new TextBox();
                Order_date.Margin = new Thickness(10, 35, 10, 0);
                Order_date.FontWeight = FontWeights.Bold;
                Order_date.Height = 20;
                Order_date.FontSize = 10;
                Order_date.Tag = "name";
                Order_date.VerticalAlignment = VerticalAlignment.Top;
                Order_date.Foreground = Brushes.Black;
                Order_date.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(Order_date);

                Label label2 = new Label();
                label2.Content = "Дата прибытия:";
                label2.Margin = new Thickness(10, 50, 10, 0);
                label2.FontSize = 20;
                label2.Foreground = Brushes.Black;
                label2.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label2);

                TextBox Date_of_issue = new TextBox();
                Date_of_issue.Margin = new Thickness(10, 85, 10, 0);
                Date_of_issue.FontWeight = FontWeights.Bold;
                Date_of_issue.Height = 20;
                Date_of_issue.VerticalAlignment = VerticalAlignment.Top;
                Date_of_issue.FontSize = 10;
                Date_of_issue.Tag = "log";
                Date_of_issue.Foreground = Brushes.Black;
                Date_of_issue.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(Date_of_issue);

                Label label3 = new Label();
                label3.Content = "Точка выдачи:";
                label3.Margin = new Thickness(10, 100, 10, 0);
                label3.FontSize = 20;
                label3.Foreground = Brushes.Black;
                label3.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label3);

                ComboBox point = new ComboBox();
                point.Margin = new Thickness(10, 135, 10, 0);
                point.FontWeight = FontWeights.Bold;
                point.VerticalAlignment = VerticalAlignment.Top;
                point.Height = 20;
                point.FontSize = 10;
                point.Tag = "pas";
                point.Foreground = Brushes.Black;
                point.FontFamily = new FontFamily("Comic Sans MS");
                var points = mainWindow.Context.Point.ToList();
                foreach (var p in points)
                {
                    point.Items.Add(p.city + "|" + p.street + "|" + p.house);
                }
                parent_grid.Children.Add(point);

                Label label4 = new Label();
                label4.Content = "Клиент:";
                label4.Margin = new Thickness(10, 150, 10, 0);
                label4.FontSize = 20;
                label4.Foreground = Brushes.Black;
                label4.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label4);

                ComboBox client = new ComboBox();
                client.Margin = new Thickness(10, 185, 10, 0);
                client.FontWeight = FontWeights.Bold;
                client.Height = 20;
                client.FontSize = 10;
                client.Tag = "role";
                client.VerticalAlignment = VerticalAlignment.Top;
                client.Foreground = Brushes.Black;
                client.FontFamily = new FontFamily("Comic Sans MS");
                var clients = mainWindow.Context.User.ToList();
                foreach (var c in clients)
                {
                    client.Items.Add(c.Full_Name);
                }
                parent_grid.Children.Add(client);

                Label label5 = new Label();
                label5.Content = "Код:";
                label5.Margin = new Thickness(10, 200, 10, 0);
                label5.FontSize = 20;
                label5.Foreground = Brushes.Black;
                label5.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label5);

                TextBox code = new TextBox();
                code.Margin = new Thickness(10, 235, 10, 0);
                code.FontWeight = FontWeights.Bold;
                code.Height = 20;
                code.FontSize = 10;
                code.Tag = "role";
                code.VerticalAlignment = VerticalAlignment.Top;
                code.Foreground = Brushes.Black;
                code.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(code);

                Label label6 = new Label();
                label6.Content = "Статус:";
                label6.Margin = new Thickness(10, 250, 10, 0);
                label6.FontSize = 20;
                label6.Foreground = Brushes.Black;
                label6.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label6);

                TextBox status = new TextBox();
                status.Margin = new Thickness(10, 285, 10, 0);
                status.FontWeight = FontWeights.Bold;
                status.Height = 20;
                status.FontSize = 10;
                status.Tag = "role";
                status.VerticalAlignment = VerticalAlignment.Top;
                status.Foreground = Brushes.Black;
                status.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(status);

                if (s[0] == "Add")
                {
                    Date_of_issue.Text = DateTime.Today.AddDays(6).ToString("dd.MM.yyyy");
                    Order_date.Text = DateTime.Today.ToString("dd.MM.yyyy");
                    code.Text = (mainWindow.Context.Order.Count() + 200).ToString();
                    status.Text = "Новый";
                    System.Windows.Controls.Button add = new System.Windows.Controls.Button();
                    add.Content = "Добавить";
                    add.VerticalAlignment = VerticalAlignment.Bottom;
                    add.Height = 40;
                    add.Background = Brushes.White;
                    add.FontSize = 20;
                    add.FontFamily = new FontFamily("Comic Sans MS");
                    add.Foreground = Brushes.White;
                    add.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.Click += delegate
                    {
                        string[] S = point.Text.Split('|');
                        Clasess.ApplicationContext db = new Clasess.ApplicationContext();

                        Clasess.Orders orders = new Clasess.Orders();
                        {
                            orders.Order_Date = Order_date.Text;
                            orders.Date_of_issue = Date_of_issue.Text;
                            orders.Point= (from pointz in db.Point where pointz.street == S[1] && pointz.city == S[0] && pointz.house == S[2] select pointz.id).FirstOrDefault();
                            orders.Client = (from userz in db.User where userz.Full_Name == client.Text select userz.id).FirstOrDefault();
                            orders.Code = code.Text;
                            orders.Status = status.Text;
                        };
                        try
                        {
                            mainWindow.Context.Order.Add(orders);
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные внесены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(add);
                }
                if (s[0] == "Change")
                {
                    Clasess.ApplicationContext db = new Clasess.ApplicationContext();
                    int idO = mainWindow.helpclass.id;
                    Order_date.Text = mainWindow.Context.Order.Find(idO).Order_Date;
                    Date_of_issue.Text = mainWindow.Context.Order.Find(idO).Date_of_issue;
                    point.Text = (from poinx in db.Point where poinx.id == mainWindow.Context.Order.Find(idO).Point select poinx.city).FirstOrDefault() + "|"
                        + (from poinx in db.Point where poinx.id == mainWindow.Context.Order.Find(idO).Point select poinx.street).FirstOrDefault() + "|"
                        + (from poinx in db.Point where poinx.id == mainWindow.Context.Order.Find(idO).Point select poinx.house).FirstOrDefault();
                    client.Text = (from userx in db.User where userx.id == mainWindow.Context.Order.Find(idO).Client select userx.Full_Name).FirstOrDefault();
                    code.Text = mainWindow.Context.Order.Find(idO).Code;
                    status.Text = mainWindow.Context.Order.Find(idO).Status;

                    System.Windows.Controls.Button change = new System.Windows.Controls.Button();
                    change.Content = "Изменить";
                    change.VerticalAlignment = VerticalAlignment.Bottom;
                    change.Height = 40;
                    change.Background = Brushes.White;
                    change.FontSize = 20;
                    change.FontFamily = new FontFamily("Comic Sans MS");
                    change.Foreground = Brushes.White;
                    change.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.Click += delegate
                    {
                        string[] S = point.Text.Split('|');

                        var dbOrder = mainWindow.Context.Order.FirstOrDefault(x => x.id.Equals(idO));
                        {
                            dbOrder.Date_of_issue = Date_of_issue.Text;
                            dbOrder.Order_Date = Order_date.Text;
                            dbOrder.Code = code.Text;
                            dbOrder.Client = (from userz in db.User where userz.Full_Name == client.Text select userz.id).FirstOrDefault();
                            dbOrder.Status = status.Text;
                            dbOrder.Point = (from pointz in db.Point where pointz.street == S[1] && pointz.city == S[0] && pointz.house == S[2] select pointz.id).FirstOrDefault();
                        };
                        try
                        {
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные изменены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(change);
                }
            }
            if (s[1] == "Product")
            {
                Grid parent_grid = new Grid();
                parent_grid.Height = 700;
                parent_grid.Margin = new Thickness(20, 20, 20, 20);
                UI.Children.Add(parent_grid);

                Label label1 = new Label();
                label1.Content = "Артикул:";
                label1.Margin = new Thickness(10, 0, 10, 0);
                label1.FontSize = 20;
                label1.Foreground = Brushes.Black;
                label1.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label1);

                TextBox article = new TextBox();
                article.Margin = new Thickness(10, 35, 10, 0);
                article.FontWeight = FontWeights.Bold;
                article.Height = 20;
                article.FontSize = 10;
                article.Tag = "name";
                article.VerticalAlignment = VerticalAlignment.Top;
                article.Foreground = Brushes.Black;
                article.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(article);

                Label label2 = new Label();
                label2.Content = "Название:";
                label2.Margin = new Thickness(10, 50, 10, 0);
                label2.FontSize = 20;
                label2.Foreground = Brushes.Black;
                label2.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label2);

                TextBox name = new TextBox();
                name.Margin = new Thickness(10, 85, 10, 0);
                name.FontWeight = FontWeights.Bold;
                name.Height = 20;
                name.VerticalAlignment = VerticalAlignment.Top;
                name.FontSize = 10;
                name.Tag = "log";
                name.Foreground = Brushes.Black;
                name.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(name);

                Label label3 = new Label();
                label3.Content = "В чём продаётся:";
                label3.Margin = new Thickness(10, 100, 10, 0);
                label3.FontSize = 20;
                label3.Foreground = Brushes.Black;
                label3.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label3);

                TextBox measure_unit = new TextBox();
                measure_unit.Margin = new Thickness(10, 135, 10, 0);
                measure_unit.FontWeight = FontWeights.Bold;
                measure_unit.VerticalAlignment = VerticalAlignment.Top;
                measure_unit.Height = 20;
                measure_unit.FontSize = 10;
                measure_unit.Tag = "pas";
                measure_unit.Foreground = Brushes.Black;
                measure_unit.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(measure_unit);

                Label label4 = new Label();
                label4.Content = "Цена: ";
                label4.Margin = new Thickness(10, 150, 10, 0);
                label4.FontSize = 20;
                label4.Foreground = Brushes.Black;
                label4.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label4);

                TextBox price = new TextBox();
                price.Margin = new Thickness(10, 185, 10, 0);
                price.FontWeight = FontWeights.Bold;
                price.Height = 20;
                price.FontSize = 10;
                price.Tag = "role";
                price.VerticalAlignment = VerticalAlignment.Top;
                price.Foreground = Brushes.Black;
                price.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(price);

                Label label5 = new Label();
                label5.Content = "Максимальная скидка:";
                label5.Margin = new Thickness(10, 200, 10, 0);
                label5.FontSize = 20;
                label5.Foreground = Brushes.Black;
                label5.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label5);

                TextBox max_discount = new TextBox();
                max_discount.Margin = new Thickness(10, 235, 10, 0);
                max_discount.FontWeight = FontWeights.Bold;
                max_discount.Height = 20;
                max_discount.FontSize = 10;
                max_discount.Tag = "role";
                max_discount.VerticalAlignment = VerticalAlignment.Top;
                max_discount.Foreground = Brushes.Black;
                max_discount.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(max_discount);

                Label label6 = new Label();
                label6.Content = "Производитель:";
                label6.Margin = new Thickness(10, 250, 10, 0);
                label6.FontSize = 20;
                label6.Foreground = Brushes.Black;
                label6.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label6);

                TextBox manufacturer = new TextBox();
                manufacturer.Margin = new Thickness(10, 285, 10, 0);
                manufacturer.FontWeight = FontWeights.Bold;
                manufacturer.Height = 20;
                manufacturer.FontSize = 10;
                manufacturer.Tag = "role";
                manufacturer.VerticalAlignment = VerticalAlignment.Top;
                manufacturer.Foreground = Brushes.Black;
                manufacturer.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(manufacturer);

                Label label7 = new Label();
                label7.Content = "Поставщик:";
                label7.Margin = new Thickness(10, 300, 10, 0);
                label7.FontSize = 20;
                label7.Foreground = Brushes.Black;
                label7.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label7);

                TextBox supplier = new TextBox();
                supplier.Margin = new Thickness(10, 335, 10, 0);
                supplier.FontWeight = FontWeights.Bold;
                supplier.Height = 20;
                supplier.FontSize = 10;
                supplier.Tag = "role";
                supplier.VerticalAlignment = VerticalAlignment.Top;
                supplier.Foreground = Brushes.Black;
                supplier.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(supplier);

                Label label8 = new Label();
                label8.Content = "Айди категории:";
                label8.Margin = new Thickness(10, 350, 10, 0);
                label8.FontSize = 20;
                label8.Foreground = Brushes.Black;
                label8.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label8);

                ComboBox category_id = new ComboBox();
                category_id.Margin = new Thickness(10, 385, 10, 0);
                category_id.FontWeight = FontWeights.Bold;
                category_id.Height = 20;
                category_id.FontSize = 10;
                category_id.Tag = "role";
                var categorys = mainWindow.Context.Category.ToList();
                foreach (var c in categorys)
                {
                    category_id.Items.Add(c.name);
                }
                category_id.VerticalAlignment = VerticalAlignment.Top;
                category_id.Foreground = Brushes.Black;
                category_id.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(category_id);

                Label label9 = new Label();
                label9.Content = "Скидка:";
                label9.Margin = new Thickness(10, 400, 10, 0);
                label9.FontSize = 20;
                label9.Foreground = Brushes.Black;
                label9.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label9);

                TextBox discount = new TextBox();
                discount.Margin = new Thickness(10, 435, 10, 0);
                discount.FontWeight = FontWeights.Bold;
                discount.Height = 20;
                discount.FontSize = 10;
                discount.Tag = "role";
                discount.VerticalAlignment = VerticalAlignment.Top;
                discount.Foreground = Brushes.Black;
                discount.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(discount);

                Label label10 = new Label();
                label10.Content = "Кол-во:";
                label10.Margin = new Thickness(10, 450, 10, 0);
                label10.FontSize = 20;
                label10.Foreground = Brushes.Black;
                label10.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label10);

                TextBox count = new TextBox();
                count.Margin = new Thickness(10, 485, 10, 0);
                count.FontWeight = FontWeights.Bold;
                count.Height = 20;
                count.FontSize = 10;
                count.Tag = "role";
                count.VerticalAlignment = VerticalAlignment.Top;
                count.Foreground = Brushes.Black;
                count.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(count);

                Label label11 = new Label();
                label11.Content = "Описание:";
                label11.Margin = new Thickness(10, 500, 10, 0);
                label11.FontSize = 20;
                label11.Foreground = Brushes.Black;
                label11.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label11);

                TextBox description = new TextBox();
                description.Margin = new Thickness(10, 535, 10, 0);
                description.FontWeight = FontWeights.Bold;
                description.Height = 20;
                description.FontSize = 10;
                description.Tag = "role";
                description.VerticalAlignment = VerticalAlignment.Top;
                description.Foreground = Brushes.Black;
                description.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(description);

                Label label12 = new Label();
                label12.Content = "Фото:";
                label12.Margin = new Thickness(10, 550, 10, 0);
                label12.FontSize = 20;
                label12.Foreground = Brushes.Black;
                label12.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(label12);

                TextBox img_src = new TextBox();
                img_src.Margin = new Thickness(10, 585, 120, 0);
                img_src.FontWeight = FontWeights.Bold;
                img_src.Height = 20;
                img_src.FontSize = 10;
                img_src.Tag = "role";
                img_src.VerticalAlignment = VerticalAlignment.Top;
                img_src.Foreground = Brushes.Black;
                img_src.FontFamily = new FontFamily("Comic Sans MS");
                parent_grid.Children.Add(img_src);

                System.Windows.Controls.Button src = new System.Windows.Controls.Button();
                src.Content = "Ссылка";
                src.HorizontalAlignment = HorizontalAlignment.Right;
                src.Height = 40;
                src.Width = 100;
                src.Margin = new Thickness(10, 500, 10, 0);
                src.Background = Brushes.White;
                src.FontSize = 20;
                src.FontFamily = new FontFamily("Comic Sans MS");
                src.Foreground = Brushes.White;
                src.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                src.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                src.Click += delegate
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "JPEG (*.jpg)|*.jpg|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.ShowDialog();
                    if (openFileDialog.FileName != "")
                    {
                        img_src.Text = openFileDialog.FileName;
                        BitmapImage z = new BitmapImage(new Uri(openFileDialog.FileName));
                        h = mainWindow.imageConverter.ImageToByteArray(z);
                    }
                };
                parent_grid.Children.Add(src);

                    if (s[0] == "Add")
                {
                    Clasess.ApplicationContext db = new Clasess.ApplicationContext();
                    System.Windows.Controls.Button add = new System.Windows.Controls.Button();
                    add.Content = "Добавить";
                    add.VerticalAlignment = VerticalAlignment.Bottom;
                    add.Height = 40;
                    add.Background = Brushes.White;
                    add.FontSize = 20;
                    add.FontFamily = new FontFamily("Comic Sans MS");
                    add.Foreground = Brushes.White;
                    add.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    add.Click += delegate
                    {
                        Clasess.Products products = new Clasess.Products();
                        {
                            if (Convert.ToInt32(price.Text) > 0)
                            {
                                products.article = article.Text;
                                products.name = name.Text;
                                products.measure_unit = measure_unit.Text;
                                products.price = Convert.ToInt32(price.Text);
                                products.max_discount = Convert.ToInt32(max_discount.Text);
                                products.manufacturer = manufacturer.Text;
                                products.supplier = supplier.Text;
                                products.category_id = (from c in db.Category where c.name == category_id.Text select c.id).FirstOrDefault();
                                products.discount = Convert.ToInt32(discount.Text);
                                products.count = Convert.ToInt32(count.Text);
                                products.description = description.Text;
                                products.img_src = h;
                            }
                            else { MessageBox.Show("Цена не может быть отрицательной"); }
                        };
                        try
                        {
                            mainWindow.Context.Product.Add(products);
                        mainWindow.Context.SaveChanges();
                        MessageBox.Show("Данные внесены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                    mainWindow.OpenPages(MainWindow.pages.main);
                };
                    parent_grid.Children.Add(add);
                }
                if (s[0] == "Change")
                {
                    Clasess.ApplicationContext db = new Clasess.ApplicationContext();
                    int idPr = mainWindow.helpclass.id;
                    article.Text = mainWindow.Context.Product.Find(idPr).article;
                    name.Text = mainWindow.Context.Product.Find(idPr).name;
                    measure_unit.Text = mainWindow.Context.Product.Find(idPr).measure_unit.ToString();
                    price.Text = mainWindow.Context.Product.Find(idPr).price.ToString();
                    max_discount.Text = mainWindow.Context.Product.Find(idPr).max_discount.ToString();
                    manufacturer.Text = mainWindow.Context.Product.Find(idPr).manufacturer;
                    supplier.Text = mainWindow.Context.Product.Find(idPr).supplier;
                    category_id.Text = (from c in db.Category where c.id == mainWindow.Context.Product.Find(idPr).category_id select c.name).FirstOrDefault();
                    discount.Text = mainWindow.Context.Product.Find(idPr).discount.ToString();
                    count.Text = mainWindow.Context.Product.Find(idPr).count.ToString();
                    description.Text = mainWindow.Context.Product.Find(idPr).description;
                    if (mainWindow.Context.Product.Find(idPr).img_src != null)
                    {
                        img_src.Text = mainWindow.Context.Product.Find(idPr).img_src.ToString();
                    }


                    System.Windows.Controls.Button change = new System.Windows.Controls.Button();
                    change.Content = "Изменить";
                    change.VerticalAlignment = VerticalAlignment.Bottom;
                    change.Height = 40;
                    change.Background = Brushes.White;
                    change.FontSize = 20;
                    change.FontFamily = new FontFamily("Comic Sans MS");
                    change.Foreground = Brushes.White;
                    change.Background = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF498C51"));
                    change.Click += delegate
                    {
                        var dbProduct = mainWindow.Context.Product.FirstOrDefault(x => x.id.Equals(idPr));
                        {
                            if (Convert.ToInt32(price.Text) > 0)
                            {
                            dbProduct.article = article.Text;
                            dbProduct.name = name.Text;
                            dbProduct.measure_unit = measure_unit.Text;
                            dbProduct.price = Convert.ToInt32(price.Text);
                            dbProduct.max_discount = Convert.ToInt32(max_discount.Text);
                            dbProduct.manufacturer = manufacturer.Text;
                            dbProduct.supplier = supplier.Text;
                            dbProduct.category_id = (from c in db.Category where c.name == category_id.Text select c.id).FirstOrDefault();
                            dbProduct.discount = Convert.ToInt32(discount.Text);
                            dbProduct.count = Convert.ToInt32(count.Text);
                            dbProduct.description = description.Text;
                            dbProduct.img_src = h;
                            }
                            else { MessageBox.Show("Цена не может быть отрицательной"); }
                        };
                        try
                        {
                            mainWindow.Context.SaveChanges();
                            MessageBox.Show("Данные изменены!");
                        }
                        catch { MessageBox.Show("Что-то пошло не так проверьте правильности ввода!"); }
                        mainWindow.OpenPages(MainWindow.pages.main);
                    };
                    parent_grid.Children.Add(change);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.main);
        }
    }
}
