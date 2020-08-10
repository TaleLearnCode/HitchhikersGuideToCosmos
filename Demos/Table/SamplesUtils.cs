using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;


namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Table
{
	class SamplesUtils
	{
		public static async Task<Presentation> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
		{
			try
			{
				TableOperation retrieveOperation = TableOperation.Retrieve<Presentation>(partitionKey, rowKey);
				TableResult result = await table.ExecuteAsync(retrieveOperation);
				Presentation presentation = result.Result as Presentation;
				if (presentation != null)
				{
					Console.WriteLine("\t{0}\t{1}\t{2}", presentation.PartitionKey, presentation.RowKey, presentation.Title);
				}

				if (result.RequestCharge.HasValue)
				{
					Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
				}

				return presentation;
			}
			catch (StorageException e)
			{
				Console.WriteLine(e.Message);
				Console.ReadLine();
				throw;
			}
		}

		public static async Task<Presentation> InsertOrMergeEntityAsync(CloudTable table, Presentation entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			try
			{
				// Create the InsertOrReplace table operation
				TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

				// Execute the operation.
				TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
				Presentation insertedCustomer = result.Result as Presentation;

				if (result.RequestCharge.HasValue)
				{
					Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
				}

				return insertedCustomer;
			}
			catch (StorageException e)
			{
				Console.WriteLine(e.Message);
				Console.ReadLine();
				throw;
			}
		}

		public static async Task DeleteEntityAsync(CloudTable table, Presentation deleteEntity)
		{
			try
			{
				if (deleteEntity == null)
				{
					throw new ArgumentNullException("deleteEntity");
				}

				TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
				TableResult result = await table.ExecuteAsync(deleteOperation);

				if (result.RequestCharge.HasValue)
				{
					Console.WriteLine("Request Charge of Delete Operation: " + result.RequestCharge);
				}

			}
			catch (StorageException e)
			{
				Console.WriteLine(e.Message);
				Console.ReadLine();
				throw;
			}
		}

	}
}
