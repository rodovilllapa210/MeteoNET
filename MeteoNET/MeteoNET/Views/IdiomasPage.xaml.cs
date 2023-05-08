using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoNET.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdiomasPage : ContentPage
    {
        public IdiomasPage()
        {
            InitializeComponent();
        }

        private void OnIdiomaSelectedIndexChanged(object sender, EventArgs e)
        {
            var idiomaSeleccionado = idiomaPicker.SelectedItem.ToString();
            switch (idiomaSeleccionado)
            {
                case "Inglés":
                    idiomaSeleccionado = "en";
                    break;
                default:
                    idiomaSeleccionado = "sp";
                    break;
            }

            Preferences.Set("Idioma", idiomaSeleccionado);

            Navigation.PopAsync();
        }
    }
}