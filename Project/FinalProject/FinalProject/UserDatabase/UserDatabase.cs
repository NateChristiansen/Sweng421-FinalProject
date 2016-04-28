using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class UserDatabase : DbContext
    {
        public UserDatabase() : base("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\UserDatabase\\UserDatabase.mdf;Integrated Security=True")
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
