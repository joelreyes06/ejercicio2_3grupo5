using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ejercicio2_3grupo.Models
{
    public class Audios
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Path { get; set; }
        public DateTime Fecha { get; set; }
    }
}
