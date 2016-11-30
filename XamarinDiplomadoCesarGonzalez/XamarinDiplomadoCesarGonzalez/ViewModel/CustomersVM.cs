using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinDiplomadoCesarGonzalez.Model.Entities;
using XamarinDiplomadoCesarGonzalez.Model.Services;

namespace XamarinDiplomadoCesarGonzalez.ViewModel
{
    public class CustomersVM : ObservableBaseObject
    {
        public ObservableCollection<Customer> Customers { get; set; }
        private AzureServiceClient _client;
        public Command RefreshCommand { get; set; }
        public Command AddCustomer { get; set; }
        public Command CleanLocalDataCommand { get; set; }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }


        public CustomersVM()
        {
            RefreshCommand = new Command(() => Load());
            AddCustomer = new Command(() => addCustomer());
            CleanLocalDataCommand = new Command(() => cleanLocalData());
            Customers = new ObservableCollection<Customer>();
            _client = new AzureServiceClient();

        }

        private async void cleanLocalData()
        {
            await _client.CleanCustomersData();
        }

        async void addCustomer()
        {

        }

        async void generateCustomers()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            string[] names = { "José Luis", "Miguel Ángel", "José Francisco", "Jesús Antonio", "Jorge", "Alberto",
                                "Sofía", "Camila", "Valentina", "Isabella", "Ximena", "Ana"};
            string[] lastNames = { "Hernández", "García", "Martínez", "López", "González", "Méndez", "Castillo", "Corona", "Cruz" };

            Random rdn = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; i++)
            {
                var Customer = new Customer() { Name = $"{names[rdn.Next(0, 12)]} {lastNames[rdn.Next(0, 8)]}" };
                _client.AddCustomer(Customer);
            }

            IsBusy = false;
        }


        public async void Load()
        {
            IsBusy = true;
            var result = await _client.GetCustomers();

            Customers.Clear();

            foreach (var item in result)
            {
                Customers.Add(item);
            }
            IsBusy = false;
        }
    }
}
