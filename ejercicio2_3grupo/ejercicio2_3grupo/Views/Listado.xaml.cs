using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ejercicio2_3grupo.Models;
using ejercicio2_3grupo.Servicios;
using ejercicio2_3grupo.Views;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ejercicio2_3grupo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listado : ContentPage
    {
        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        public Listado()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            list.ItemsSource = await new Service().GetAudios();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            try
            {
                var item = sender as SwipeItem;

                var audio = item.BindingContext as Audios;

                audioPlayer.Play(audio.Path);
            }
            catch (Exception)
            {

                await DisplayAlert("Aviso", "No se pudo reproducir el audio", "OK");
            }

        }

        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {

            try
            {
                var item = sender as SwipeItem;

                var audio = item.BindingContext as Audios;

                if (await new Service().DeleteAudio(audio))
                {
                    await DisplayAlert("Aviso", "Audio eliminado correctamente !!!", "OK");

                    list.ItemsSource = await new Service().GetAudios();
                }
                else await DisplayAlert("Aviso", "No se pudo eliminar el audio", "OK");

            }
            catch (Exception)
            {

                await DisplayAlert("Aviso", "No se pudo eliminar el audio", "OK");
            }

        }

        private void btnVolver_Clicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }
    }
}