using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Threading;

namespace Kursovaya.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public DispatcherTimer dispatcherRevers = new DispatcherTimer();
        public float full_second = 10;
        public bool start_stopwatch = false;

        public MainWindow mainWindow;
        public Login(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            dispatcherTimer.Tick += TimerSecond;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }
        int count = 0;

        public void TimerSecond(object sender, EventArgs e)
        {
            full_second--;

            float hours = (int)(full_second / 60 / 60);
            float minuts = (int)(full_second / 60);
            float seconds = full_second - (hours * 60 * 60) - (minuts * 60);

            string s_seconds = seconds.ToString();
            if (seconds < 10) s_seconds = "0" + seconds;
            time.Text = s_seconds;
            if (s_seconds == "00")
            {
                dispatcherTimer.Stop();
                Enter.Visibility = Visibility.Visible;
                Guest.Visibility = Visibility.Visible;
                time.Text = "";
                full_second = 10;
            }
        }

        private void Enter_click(object sender, RoutedEventArgs e)
        {
            var rule = mainWindow.workingDB.Login(Pass.Password, Log.Text);
            mainWindow.helpclass.Uid = mainWindow.workingDB.FindId(Pass.Password, Log.Text);
            if (rule == null)
            {
                Log.Clear();
                Pass.Clear();
                count += 1;
                MessageBox.Show("Логин или пароль неверен");
                if (text == enter_captcha.Text)
                {
                    MessageBox.Show("Введите пароль и логин снова!");
                    count = 0;
                    captcha.Visibility = Visibility.Hidden;
                    enter_captcha.Visibility = Visibility.Hidden;
                }
                if (count > 0)
                {
                    captcha.Visibility = Visibility.Visible;
                    enter_captcha.Visibility = Visibility.Visible;
                    CreateImage(Convert.ToInt32(captcha.Width), Convert.ToInt32(captcha.Height));
                }
                if (count > 1)
                {
                    dispatcherTimer.Start();
                    Enter.Visibility = Visibility.Hidden;
                    Guest.Visibility = Visibility.Hidden;
                }
                enter_captcha.Clear();
            }
            else
            {
                if (text == enter_captcha.Text)
                {
                    count = 0;
                    captcha.Visibility = Visibility.Hidden;
                    enter_captcha.Visibility = Visibility.Hidden;
                    if (rule == "Клиент")
                    {
                        mainWindow.helpclass.consroleuser = "Клиент";
                        mainWindow.OpenPages(MainWindow.pages.main);
                    }
                    if (rule == "Менеджер")
                    {
                        mainWindow.helpclass.consroleuser = "Менеджер";
                        mainWindow.OpenPages(MainWindow.pages.main);
                    }
                    if (rule == "Администратор")
                    {
                        mainWindow.helpclass.consroleuser = "Администратор";
                        mainWindow.OpenPages(MainWindow.pages.main);
                    }
                }
                if (count > 0)
                {
                    captcha.Visibility = Visibility.Visible;
                    enter_captcha.Visibility = Visibility.Visible;
                    count += 1;
                    CreateImage(Convert.ToInt32(captcha.Width), Convert.ToInt32(captcha.Height));
                }
                if (count > 1)
                {
                    dispatcherTimer.Start();
                    Enter.Visibility = Visibility.Hidden;
                    Guest.Visibility = Visibility.Hidden;
                }
                if (count == 0)
                {
                    enter_captcha.Clear();
                    if (rule == "Клиент")
                    {
                        mainWindow.helpclass.consroleuser = "Клиент";
                        mainWindow.OpenPages(MainWindow.pages.main);
                    }
                    if (rule == "Менеджер")
                    {
                        mainWindow.helpclass.consroleuser = "Менеджер";
                        mainWindow.OpenPages(MainWindow.pages.main);
                    }
                    if (rule == "Администратор")
                    {
                        mainWindow.helpclass.consroleuser = "Администратор";
                        mainWindow.OpenPages(MainWindow.pages.main);
                    }
                }
                enter_captcha.Clear();
            }
        }
        private void Admin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                mainWindow.helpclass.consroleuser = "Администратор";
                mainWindow.OpenPages(MainWindow.pages.main);
            }
        }

        public string text = " ";

        public void CreateImage(int Width, int Height)
        {
            Random rnd = new Random(); // Создаём рандом

            Bitmap result = new Bitmap(Width, Height); // Картинка

            int Xpos = rnd.Next(0, Width-200); // начальная позиция по иксу
            int Ypos = rnd.Next(15, Height-50); // начальная позиция по игреку

            System.Drawing.Brush[] colors = { System.Drawing.Brushes.Black,
                     System.Drawing.Brushes.Red,
                     System.Drawing.Brushes.RoyalBlue,
                     System.Drawing.Brushes.Green }; // коллекция цветов

            Graphics g = Graphics.FromImage((System.Drawing.Image)result); // для отрисовки капчи

            g.Clear(System.Drawing.Color.Gray); 

            text = String.Empty; // текст капчи
            string ALF = "123456789QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm"; // коллекция символов
            for (int i = 0; i < 4; ++i)
                text += ALF[rnd.Next(ALF.Length)]; // генерация капчи

            foreach (char c in text)
            {
                g.DrawString(c.ToString(),
                             new Font("Arial", 35),
                             colors[rnd.Next(colors.Length)],
                             new PointF(Xpos, Ypos));
                Xpos += 50;
                Ypos -= 10;
            } // добавление символов со смещением и разными цветами

            g.DrawLine(Pens.Black,
                       new System.Drawing.Point(0, 0),
                       new System.Drawing.Point(Width - 1, Height - 1)); // линии для ухудшения видимости

            g.DrawLine(Pens.Black,
                       new System.Drawing.Point(0, Height - 1),
                       new System.Drawing.Point(Width - 1, 0)); // линии для ухудшения видимости

            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, System.Drawing.Color.White); // точки для ухудшения видимости

            captcha.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                result.GetHbitmap(),// создание картинки
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(Width, Height) // настройки по координатам
                );
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.helpclass.Uid = 0;
            mainWindow.helpclass.consroleuser = "Гость";
            if (count == 0)
            {
                MessageBox.Show("Перед авторизацией в систему как гость пожалуйста введите символы с картинки");
            }
            if (count >= 0)
            {
                if (text == enter_captcha.Text)
                {
                    mainWindow.OpenPages(MainWindow.pages.main);
                }
                captcha.Visibility = Visibility.Visible;
                enter_captcha.Visibility = Visibility.Visible;
                count += 1;
                CreateImage(Convert.ToInt32(captcha.Width), Convert.ToInt32(captcha.Height));
            }
            if (count > 1)
            {
                if (text == enter_captcha.Text)
                {
                    mainWindow.OpenPages(MainWindow.pages.main);
                }
                dispatcherTimer.Start();
                Enter.Visibility = Visibility.Hidden;
                Guest.Visibility = Visibility.Hidden;
                count += 1;
            }
            enter_captcha.Clear();
        }
    }
}

