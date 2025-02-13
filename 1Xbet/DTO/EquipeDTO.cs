using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Xbet.DTO
{
    public class EquipeDTO
    {
        public int Id { get; set; }
        public string? NomEquipe { get; set; }
        public string? Description { get; set; }
        public int NbreMatchJoue { get; set; }
        public int NbreButsMarque { get; set; }
        public int NbreButsEncaisse { get; set; }
        public int NbrePoints { get; set; }
    }
}
