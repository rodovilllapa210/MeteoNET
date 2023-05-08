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
    public partial class UnidadesPage : ContentPage
    {
        public UnidadesPage()
        {
            InitializeComponent();
        }

        private void OnUnidadesSelectedIndexChanged(object sender, EventArgs e)
        {
            var unidadesSeleccionadas = unidadesPicker.SelectedItem.ToString();
            switch (unidadesSeleccionadas)
            {
                case "Imperial":
                    unidadesSeleccionadas = "imperial";
                    break;
                case "Estándar":
                    unidadesSeleccionadas = "standard";
                    break;
                default:
                    unidadesSeleccionadas = "metric";
                    break;
            }

            Preferences.Set("Unidades", unidadesSeleccionadas);

            Navigation.PopAsync();
        }
    }
}