﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Xbet.Models
{
    public class Equipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? NomEquipe { get; set; }
        public string? Description { get; set; }
    }
}
