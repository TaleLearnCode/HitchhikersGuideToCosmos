using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Table
{
	public class Common
	{
		public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
		{
			CloudStorageAccount storageAccount;
			try
			{
				storageAccount = CloudStorageAccount.Parse(storageConnectionString);
			}
			catch (FormatException)
			{
				Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
				throw;
			}
			catch (ArgumentException)
			{
				Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
				Console.ReadLine();
				throw;
			}

			return storageAccount;
		}

		public static async Task<CloudTable> CreateTableAsync(string tableName)
		{
			string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=hitchhiker-eastus2-table;AccountKey=RfKNDRX6akAj1OQpcZLgtwSfcrNNY1EknPOzApwAzGQXQxMpYp3hWAZpXuf0COUrW6hqEDVCNI1EPUlC4wgvTw==;TableEndpoint=https://hitchhiker-eastus2-table.table.cosmos.azure.com:443/;";

			// Retrieve storage account information from connection string.
			CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

			// Create a table client for interacting with the table service
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

			Console.WriteLine("Create a Table for the demo");

			// Create a table client for interacting with the table service 
			CloudTable table = tableClient.GetTableReference(tableName);
			if (await table.CreateIfNotExistsAsync())
			{
				Console.WriteLine("Created Table named: {0}", tableName);
			}
			else
			{
				Console.WriteLine("Table {0} already exists", tableName);
			}

			Console.WriteLine();
			return table;
		}

		public static async Task<CloudTable> CreateTableAsync(CloudTableClient tableClient, string tableName)
		{
			Console.WriteLine("Create a Table for the demo");

			// Create a table client for interacting with the table service 
			CloudTable table = tableClient.GetTableReference(tableName);
			try
			{
				if (await table.CreateIfNotExistsAsync())
				{
					Console.WriteLine("Created Table named: {0}", tableName);
				}
				else
				{
					Console.WriteLine("Table {0} already exists", tableName);
				}
			}
			catch (StorageException)
			{
				Console.WriteLine(
						"If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
				Console.ReadLine();
				throw;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine();
			return table;
		}
	}

}