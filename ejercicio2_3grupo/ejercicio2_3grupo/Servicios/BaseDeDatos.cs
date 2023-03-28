using ejercicio2_3grupo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ejercicio2_3grupo.Servicios
{
    public class BaseDeDatos
    {
        readonly SQLiteAsyncConnection dbase;

        public BaseDeDatos(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);


            dbase.CreateTableAsync<Audios>(); 

        }

        #region OperacionesAudio
        //CREATE
        public Task<int> insertUpdateAudio(Audios Audio)
        {
            if (Audio.Id != 0)
            {
                return dbase.UpdateAsync(Audio);
            }
            else
            {
                return dbase.InsertAsync(Audio);
            }
        }

        //READ
        public Task<List<Audios>> getListAudio()
        {
            return dbase.Table<Audios>().ToListAsync();
        }

        public Task<Audios> getAudio(int id)
        {
            return dbase.Table<Audios>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        //DELETE
        public Task<int> deleteAudio(Audios Audio)
        {
            return dbase.DeleteAsync(Audio);
        }

        #endregion OperacionesAudio

    }
}
