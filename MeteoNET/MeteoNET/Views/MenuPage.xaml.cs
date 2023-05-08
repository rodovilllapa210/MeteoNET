using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoNET.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OnUnidadesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UnidadesPage());
        }

        private async void OnIdiomaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IdiomasPage());
        }

        private async void OnFavoritosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavoritosPage());
        }

        
    }
}