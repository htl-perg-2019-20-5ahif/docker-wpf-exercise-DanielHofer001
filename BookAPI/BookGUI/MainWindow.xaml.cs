using BookAPI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace BookGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        private DateTime SelectedDateValue = DateTime.MinValue;
        private void OnPropertyChanged(string propertyName) =>
PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime SelectedDate
        {
            get => SelectedDateValue;
            set
            {
                SelectedDateValue = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        private DateTime SelectedBookDateValue = DateTime.MinValue;

        public DateTime SelectedBookDate
        {
            get => SelectedBookDateValue;
            set
            {
                SelectedBookDateValue = value;
                OnPropertyChanged(nameof(SelectedBookDate));
            }
        }
        private readonly HttpClient httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };


        private Car SelectedCarValue;
        public Car SelectedCar
        {
            get => SelectedCarValue;
            set
            {
                SelectedCarValue = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

        }

        private async void GetAllCarsAsync(object sender, RoutedEventArgs e)
        {
            var response = await httpClient.GetAsync("/car");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var cars = JsonSerializer.Deserialize<Car[]>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Cars.Clear();
            foreach (var c in cars)
            {
                Cars.Add(c);
            }
        }
        private async void BookCar(object sender, RoutedEventArgs e)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(SelectedBookDate), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("/book/" + SelectedCar.Id, content);

            response.EnsureSuccessStatusCode();
            
        }
        private async void GetFreeCars(object sender, RoutedEventArgs e)
        {
            var response = await httpClient.GetAsync("/car/free/"+SelectedDate.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
            try {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var cars = JsonSerializer.Deserialize<Car[]>(responseBody);
                Cars.Clear();

                foreach (var c in cars)
                {
                    Cars.Add(c);
                }
            }
            catch (Exception ex)
            {
                Cars.Clear();

            }
          
        }
    }
}
