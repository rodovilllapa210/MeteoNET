using MeteoNET.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MeteoNET.Helper
{
    public class DatabaseHelper
    {
        private SQLiteConnection database;

        public DatabaseHelper()
        {
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Localizaciones.db3"));
            database.CreateTable<Localizacion>();
        }

        public List<Localizacion> ObtenerLocalizaciones()
        {
            return database.Table<Localizacion>().OrderByDescending(l => l.FechaConsulta).ToList();
        }

        public void AgregarLocalizacion(Localizacion localizacion)
        {
            database.Insert(localizacion);
        }

        public void BorrarLocalizaciones()
        {
            database.Execute("DELETE FROM Localizacion");
        }
    }
}
