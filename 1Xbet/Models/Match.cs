using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Xbet.Models
{
    public class Match
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string EquipeExt { get; set; }
        public string EquipeDom { get; set; }
        public int ButEquipeExt { get; set; }
        public int ButEquipeDom { get; set; }
        public string Date { get; set; }

    }
}
