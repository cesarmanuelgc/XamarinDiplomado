using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinDiplomadoCesarGonzalez.Model.Entities;

namespace XamarinDiplomadoCesarGonzalez.Model.Services
{
    public class AzureServiceClient
    {
        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<Customer> _customerTable;
        private const string serviceUri = "http://xamarindiplomadocesargonzalez.azurewebsites.net";
        const string localDbPath = "storeLocalDb";
        public AzureServiceClient()
        {
            _client = new MobileServiceClient(serviceUri);
            var store = new MobileServiceSQLiteStore(localDbPath);
            store.DefineTable<Customer>();
            _client.SyncContext.InitializeAsync(store);
            _customerTable = _client.GetSyncTable<Customer>();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var empty = new Customer[0];
            try
            {
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    await SyncAsync();

                return await _customerTable.ToEnumerableAsync();
            }
            catch (Exception ex)
            {
                return empty;
            }
        }

        public async void AddCustomer(Customer Customer)
        {
            await _customerTable.InsertAsync(Customer);

        }

        public async Task SyncAsync()
        {
            IReadOnlyCollection<MobileServiceTableOperationError> syncError = null;
            try
            {
                await _client.SyncContext.PushAsync();
                await _customerTable.PullAsync("allCustomers", _customerTable.CreateQuery());
            }
            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncError = pushEx.PushResult.Errors;
            }
        }

        public async Task CleanCustomersData()
        {
            await _customerTable.PurgeAsync("allCustomers", _customerTable.CreateQuery(), new System.Threading.CancellationToken());
        }
    }
}
