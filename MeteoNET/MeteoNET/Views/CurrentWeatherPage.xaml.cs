/***************************************************************
 * Ventana principal de la aplicación donde se muestran todos los
 * datos recibidos de la llamada a la API del tiempo presente u
 * de la predicción a 5 días, aunque solo se muestran, por ahora,
 * 4 días por motivos de espacio. Se pretende dejar a futuro la
 * creación de una ScrollView que recoja los datos de predicción
 * a 16 días y que el usuario pueda visualizar a voluntad.
 * *************************************************************/

using MeteoNET.Helper;
using MeteoNET.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoNET.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentWeatherPage : ContentPage
    {

        private async void GetCoordinates()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            if(location != null)
            {
                Location = await GetCity(location);

                GetWeatherInfo();
            }
        }

        private async Task<string> GetCity(Location location)
        {
            var places = await Geocoding.GetPlacemarksAsync(location);
            var currentPlace = places?.FirstOrDefault();

            if(currentPlace !=null)
                return $"{currentPlace.Locality}";

            return null;
        }

        public CurrentWeatherPage()
        {
            InitializeComponent();
            GetCoordinates();

        }
        private string _Location;
        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                if (_Location != value)
                {
                    _Location = value;
                }
            }
        }

        public void OnSearchButtonPressed(object sender, EventArgs e)
        {
            Location = searchBar.Text;

            GetWeatherInfo();
        }

        private string _Unidades = "metric";
        public string Unidades
        {
            get
            {
                return _Unidades;
            }
            set
            {
                if (_Unidades != value)
                {
                    _Unidades = value;
                }
            }
        }

        private void OnPickerUnitsItemSelected(object sender, EventArgs e)
        {

            string units = pickerUnits.SelectedItem as string;
            switch (units)
            {
                case "Imperial":
                    Unidades = "imperial";
                    break;
                case "Estándar":
                    Unidades = "standard";
                    break;
                default:
                    Unidades = "metric";
                    break;
            }

            GetWeatherInfo();

        }

        private string _Language = "sp";
        public string Language
        {
            get
            {
                return _Language;
            }
            set
            {
                if (_Language != value)
                {
                    _Language = value;
                }
            }
        }

        private void OnPickerLanguageItemSelected(object sender, EventArgs e)
        {

            string language = pickerLanguage.SelectedItem as string;
            switch (language)
            {
                case "Inglés":
                    Language = "en";
                    break;
                default:
                    Language = "sp";
                    break;
            }

            GetWeatherInfo();

        }

        private async void GetWeatherInfo()
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={_Location}&lang={_Language}&appid=216b2e8f3ecdb26dfafd546daf7e506f&units={_Unidades}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    dateTxt.Text = DateTime.Now.ToString("dddd, MMM dd").ToUpper();

                    GetForecast();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Información meteorológica", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Información meteorológica", "No se encontró predicción del tiempo", "OK");
            }
        }

        private async void GetForecast()
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={Location}&lang=sp&appid=216b2e8f3ecdb26dfafd546daf7e506f&units=metric";
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();

                    foreach (var list in forcastInfo.list)
                    {
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    dayOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd");
                    dateOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                    iconOneImg.Source = $"w{allList[0].weather[0].icon}";
                    tempOneTxt.Text = allList[0].main.temp.ToString("0");

                    dayTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                    dateTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    iconTwoImg.Source = $"w{allList[1].weather[0].icon}";
                    tempTwoTxt.Text = allList[1].main.temp.ToString("0");

                    dayThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                    dateThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    iconThreeImg.Source = $"w{allList[2].weather[0].icon}";
                    tempThreeTxt.Text = allList[2].main.temp.ToString("0");

                    dayFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                    dateFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    iconFourImg.Source = $"w{allList[3].weather[0].icon}";
                    tempFourTxt.Text = allList[3].main.temp.ToString("0");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Información meteorológica", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Información meteorológica", "No se encontró predicción del tiempo", "OK");
            }
        }
    }
}