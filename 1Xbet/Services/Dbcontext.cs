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
    public class Dbcontext
    {
        private readonly SQLiteConnection _database;

        public Dbcontext(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Equipe>();
        }

        public ObservableCollection<Equipe> GetTeams()
        {
            var teams = _database.Table<Equipe>().OrderByDescending(t => t.NbrePoints).ToList();
            return new ObservableCollection<Equipe>(teams);
        }

        public void AddTeam(Equipe equipe)
        {
            _database.Insert(equipe);
        }
    }
}
