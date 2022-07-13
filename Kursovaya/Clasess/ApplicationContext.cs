using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Kursovaya.Clasess
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Users> User { get; set; } //Таблица Users
        public DbSet<Points> Point { get; set; } //Таблица Points
        public DbSet<Categorys> Category { get; set; } //Таблица Points
        public DbSet<Orders> Order { get; set; } //Таблица Orders
        public DbSet<Products> Product { get; set; } //Таблица Products
        public DbSet<OrderContents> OrderContent { get; set; } //Таблица Products


        public ApplicationContext()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch 
            { 
                MessageBox.Show("Сервера в данный момент не доступны, попробуйте зайти позже");
            }
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=beeqgbaotkd2lw7t0vtv-mysql.services.clever-cloud.com;port=3306;database=beeqgbaotkd2lw7t0vtv;uid=uhzx8bfxloal4j1t;pwd=FA821DC0F47dm4rjJqAQ; SSL Mode=None;");
            //optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=kurs;uid=root;pwd=Asdfg123;");
        }
    }
}
