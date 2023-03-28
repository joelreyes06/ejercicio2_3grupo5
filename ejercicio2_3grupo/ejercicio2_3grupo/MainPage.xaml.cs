using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using ejercicio2_3grupo.Models;
using ejercicio2_3grupo.Servicios;
using ejercicio2_3grupo.Views;

namespace ejercicio2_3grupo
{
    public partial class MainPage : ContentPage
    {

        private AudioRecorderService audioRecorderService = new AudioRecorderService()
        {
            StopRecordingOnSilence = false,
            StopRecordingAfterTimeout = false
        };

        private AudioPlayer audioPlayer = new AudioPlayer();

        private bool reproducir = false;

        public MainPage()
        {
            InitializeComponent();
            if (App.DBase != null) { }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // audioPlayer.Pause();
        }

        private async void BtnGrabar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Microphone>();
                var status2 = await Permissions.RequestAsync<Permissions.StorageRead>();
                var status3 = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted & status2 != PermissionStatus.Granted & status3 != PermissionStatus.Granted)
                {
                    return; // si no tiene los permisos no avanza
                }

                if (audioRecorderService.IsRecording)
                {
                    await audioRecorderService.StopRecording();


                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());

                    txtMessage.Text = "NO esta grabando";


                    btnGrabar.Text = "Grabar audio";

                    reproducir = true;
                }
                else
                {
                    await audioRecorderService.StartRecording();


                    txtMessage.Text = "Esta grabando";

                    btnGrabar.Text = "Dejar de Grabar";

                    //reproducir = false;
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "OK");
            }


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (reproducir)
            {
                audioPlayer.Play(audioRecorderService.GetAudioFilePath());
            }
            else
            {
                await DisplayAlert("Alerta", "No ha grabado ningun audio", "OK");
            }

        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (reproducir)
                {

                    if (string.IsNullOrEmpty(txtDescription.Text))
                    {
                        await DisplayAlert("Alerta", "Debe escribir una descripcion", "OK");
                        return;
                    }

                  
                    var folderPath = "/storage/emulated/0/Android/data/com.companyname.ejercicio2_3grupo/files/Audio";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var nameAudio = DateTime.Now.ToString("MMddyyyyhhmmss") + ".wav";
                    var fullPath = folderPath + "/" + nameAudio;



                    var stream = audioRecorderService.GetAudioFileStream();


                    using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }


                    var audio = new Audios()
                    {
                        Id = 0,
                        Descripcion = txtDescription.Text,
                        Path = fullPath,
                        Fecha = DateTime.Now
                    };


                    if (await new Service().saveAudios(audio))
                        await DisplayAlert("Alerta", "Audio guardado correctamente !!!", "OK");
                    else
                        await DisplayAlert("Alerta", "El audio  no se pudo guardar correctamente !!!", "OK");




                    reproducir = false;
                    txtDescription.Text = "";
                }
                else
                {
                    await DisplayAlert("Alerta", "No ha grabado ningun audio", "OK");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "OK");
            }

        }

        private async void BtnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.Listado());
        }
    }
}
