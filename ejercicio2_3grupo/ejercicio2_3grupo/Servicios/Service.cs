using ejercicio2_3grupo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ejercicio2_3grupo.Servicios
{
    public class Service
    {
        public async Task<bool> saveAudios(Audios Audio)
        {
            var result = await App.DBase.insertUpdateAudio(Audio);

            return (result > 0);

        }


        public async Task<List<Audios>> GetAudios()
        {
            return await App.DBase.getListAudio();
        }

        public async Task<bool> DeleteAudio(Audios audio)
        {
            var result = await App.DBase.deleteAudio(audio);
            return (result > 0);
        }
    }
}

