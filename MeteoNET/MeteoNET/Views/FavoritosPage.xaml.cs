using MeteoNET.Helper;
using MeteoNET.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoNET.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritosPage : ContentPage
    {
        public ObservableCollection<Localizacion> Items { get; set; }

        public FavoritosPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<Localizacion>();
            var databaseHelper = new DatabaseHelper();
            var localizaciones = databaseHelper.ObtenerLocalizaciones();
            foreach (var localizacion in localizaciones)
            {
                Items.Add(localizacion);
            }
            // Asigna la ObservableCollection como la fuente de datos para la ListView
            listaLocalizaciones.ItemsSource = Items;
        }


        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var localizacionSeleccionada = (Localizacion)e.Item;

            // Pasar la localización seleccionada a la página principal
            var paginaPrincipal = Navigation.NavigationStack.FirstOrDefault(p => p is CurrentWeatherPage) as CurrentWeatherPage;
            if (paginaPrincipal != null)
            {
                paginaPrincipal.Location = localizacionSeleccionada.Ciudad;
            }

            // Navegar de vuelta
            _ = Navigation.PopAsync();
        }

        private void BorrarTodo_Clicked(object sender, EventArgs e)
        {
            var databaseHelper = new DatabaseHelper();
            databaseHelper.BorrarLocalizaciones();
            Items.Clear();
        }



    }


}
