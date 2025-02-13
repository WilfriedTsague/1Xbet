using _1Xbet.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Xbet.Services
{
    public static class Dbcontext
    {
        public static SQLiteConnection _database { get; set; }
        private const string dbPath = "1XBet.db3";

        public static void ConfigDB()
        {
            var cheminBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbPath);

            _database = new SQLiteConnection(cheminBD);

            _database.CreateTable<Equipe>();
            _database.CreateTable<Match>();

        }


    }
}
