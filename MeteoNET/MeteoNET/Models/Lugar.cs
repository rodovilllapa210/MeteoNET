using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoNET.Models
{
    public class Localizacion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Ciudad { get; set; }
        public DateTime FechaConsulta { get; set; }
    }
}
