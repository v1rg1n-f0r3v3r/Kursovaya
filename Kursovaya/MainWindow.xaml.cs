using System.Windows;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;

namespace Kursovaya
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddNull();
            LoadNames();
            OpenPages(pages.login);
        }

        public void AddNull()
        {
            Clasess.Cart newCart = new Clasess.Cart();
            newCart.id = 0;
            newCart.idP = 0;
            newCart.article = null;
            newCart.name = null;
            newCart.measure_unit = null;
            newCart.price = 0;
            newCart.manufacturer = null;
            newCart.discount = 0;
            newCart.description = null;
            newCart.img_src = null;
            newCart.count = 0;
            carts.Add(newCart);
        }
        public void OpenPages(pages _pages)
        {
            if (_pages == pages.login)
                frame.Navigate(new Pages.Login(this));
            else  if (_pages == pages.main)
                frame.Navigate(new Pages.Main(this));
            else if (_pages == pages.add)
                frame.Navigate(new Pages.Add(this));
            else if (_pages == pages.cart)
                frame.Navigate(new Pages.Cart(this));
            else if (_pages == pages.mo)
                frame.Navigate(new Pages.MakeOrder(this));
        }
        public enum pages
        {
            login,
            main,
            add,
            cart,
            mo
        }

        public void LoadNames()
        {
            var orders = Context.Order.ToList();
            Clasess.ApplicationContext db = new Clasess.ApplicationContext();
            foreach (var o in orders)
            {
                Orderz name = new Orderz();
                name.name = (from users in db.User where users.id == o.Client select users.Full_Name).FirstOrDefault();
                name.id = o.id;
                users_name.Add(name);
            }
        }
        public class ImageConverter
        {
            public BitmapImage LoadImage(byte[] photoSource)
            {
                if (photoSource == null)
                {
                    return null;
                }

                var photo = new BitmapImage();
                MemoryStream stream = new MemoryStream(photoSource);

                photo.BeginInit();
                photo.StreamSource = stream;
                photo.EndInit();

                return photo;
            }

            public byte[] ImageToByteArray(BitmapImage image)
            {
                byte[] pixels;
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    pixels = stream.ToArray();
                }
                return pixels;
            }
        }

        public ImageConverter imageConverter = new ImageConverter();
        public WorkingDB workingDB = new WorkingDB();
        public Clasess.Helpclass helpclass = new Clasess.Helpclass();
        public Clasess.ApplicationContext Context = new Clasess.ApplicationContext();
        public Clasess.Cart cart = new Clasess.Cart();
        public List<Clasess.Cart> carts = new List<Clasess.Cart>();
        public List<Orderz> users_name = new List<Orderz>();
        public Orderz user_name = new Orderz();


        public class Orderz
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public void AddCart(int id)
        {
            bool exist = false;
            Clasess.Cart newCart = new Clasess.Cart();
            for (int i = 0;i < carts.Count; i++)
            {
                if (carts[i].idP == Context.Product.Find(id).id && carts[i].id != 0)
                {
                    carts[i].count += 1;
                    exist = true;
                }
            }
            if (!exist)
            {
                newCart.id = carts.Count + 1;
                newCart.idP = Context.Product.Find(id).id;
                newCart.article = Context.Product.Find(id).article;
                newCart.name = Context.Product.Find(id).name;
                newCart.measure_unit = Context.Product.Find(id).measure_unit;
                newCart.price = Context.Product.Find(id).price;
                newCart.manufacturer = Context.Product.Find(id).manufacturer;
                newCart.discount = Context.Product.Find(id).discount;
                newCart.description = Context.Product.Find(id).description;
                newCart.img_src = Context.Product.Find(id).img_src;
                newCart.count = 1;
                carts.Add(newCart);
            }
        }
    }
}
