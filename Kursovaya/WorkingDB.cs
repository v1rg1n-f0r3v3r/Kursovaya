using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Kursovaya
{
    public class WorkingDB
    {
        public string Login( string pass, string log) 
        {
            Clasess.ApplicationContext db = new Clasess.ApplicationContext();
            string role = (from user in db.User
                         where user.Password == pass && user.Login == log
                         select user.Role).SingleOrDefault();
            return role;
        }

        public int FindId(string pass, string log)
        {
            Clasess.ApplicationContext db = new Clasess.ApplicationContext();
            int id = (from user in db.User
                           where user.Password == pass && user.Login == log
                           select user.id).SingleOrDefault();
            return id;
        }
    }
}
