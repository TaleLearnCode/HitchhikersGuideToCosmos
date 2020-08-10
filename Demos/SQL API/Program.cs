using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Cosmos;
using ChadGreen.HitchhikersGuideToTheCosmos.Demos.SQL.Entities;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.SQL
{
	class Program
	{

		private string EndpointUrl = "https://hitchhiker-eastus2-sql.documents.azure.com:443/";
		private string PrimaryKey = "QuqfoUEUeHHjya5Xm0kNfZkvi8SzGclm1bwC1ZyYxfraahMFNZZ4whpCSnDgVlrPkR0M4heiunNtMNIjU3wfGg==";

		private string databaseId = "SpeakingEngagements";
		private string containerId = "SpeakingEngagements";

		private CosmosClient cosmosClient;
		private Database database;
		private Container container;

		static async Task Main(string[] args)
		{
			try
			{
				Console.WriteLine("Beginning operations...\n");
				Program p = new Program();
				await p.GetStartedDemoAsync();
			}
			catch (CosmosException ce)
			{
				Exception baseException = ce.GetBaseException();
				Console.WriteLine($"{ce.StatusCode} error occurred: {ce}");
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e}");
			}
			finally
			{
				Console.WriteLine("End of demo, press any key to exit.");
				Console.ReadKey();
			}
		}

		/// <summary>
		/// Creates the SpeakingEngagements databse if it does not already exist.
		/// </summary>
		private async Task CreateDatabaseAsync()
		{
			this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
			Console.WriteLine($"Created Database: {this.database.Id}\n");
		}

		/// <summary>
		/// Creates the SpeakingEngagements container if it does not already exist.
		/// </summary>
		/// <returns></returns>
		private async Task CreateContainerAsync()
		{
			this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/OwnerEmailAddress");
			Console.WriteLine($"Created Container: {this.container.Id}\n");
		}

		/// <summary>
		/// Add the specified existing presentation to the database (if it does not already exists.
		/// </summary>
		/// <param name="existingPresentation">Identifier of the existing presentation to be added to the database.</param>
		private async Task AddPresentation(ExistingPresentation existingPresentation)
		{
			Presentation presentation = Generate.Presentation(existingPresentation);

			try
			{
				// Read the item to see if it exists. ReadItemAsync will throw an exception if the item does not exist and return status code 404 (Not found).
				ItemResponse<Presentation> presentationResponse = await this.container.ReadItemAsync<Presentation>(presentation.Id, new PartitionKey(presentation.OwnerEmailAddress));
				Console.WriteLine("Item in database with id: {0} already exists\n", presentationResponse.Resource.Id);
			}
			catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
			{
				// Create an item in the container representing the presentation. Note we provide the value of the partition key for this item, which is "Andersen"
				ItemResponse<Presentation> presentationResponse = await this.container.CreateItemAsync<Presentation>(presentation, new PartitionKey(presentation.OwnerEmailAddress));

				// Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse.
				// We can also access the RequestCharge property to see the amount of RUs consumed on this request.
				Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", presentationResponse.Resource.Id, presentationResponse.RequestCharge);
			}

		}

		/// <summary>
		/// Query the database to get the details about the presentation
		/// </summary>
		private async Task QueryPresentationsAsync()
		{
			var sqlQueryText = "SELECT * FROM c WHERE c.OwnerEmailAddress = 'chadgreen@chadgreen.com'";

			Console.WriteLine("Running query: {0}\n", sqlQueryText);

			QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
			FeedIterator<Presentation> queryResultSetIterator = this.container.GetItemQueryIterator<Presentation>(queryDefinition);

			List<Presentation> presentations = new List<Presentation>();

			while (queryResultSetIterator.HasMoreResults)
			{
				FeedResponse<Presentation> currentResultSet = await queryResultSetIterator.ReadNextAsync();
				foreach (Presentation presentation in currentResultSet)
				{
					presentations.Add(presentation);
					Console.WriteLine($"\t{presentation.Title}");
				}
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Deletes the database in order to clean up what was just done
		/// </summary>
		/// <returns></returns>
		private async Task DeleteDatabaseAndCleanupAsync()
		{
			DatabaseResponse databaseResourceResponse = await this.database.DeleteAsync();
			Console.WriteLine("Deleted Database: {0}\n", this.databaseId);
			this.cosmosClient.Dispose();
		}

		/// <summary>
		/// Executes the demo steps
		/// </summary>
		/// <returns></returns>
		public async Task GetStartedDemoAsync()
		{
			// Create a new instance of the Cosmos Client
			this.cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
			await this.CreateDatabaseAsync();
			await this.CreateContainerAsync();
			await this.AddPresentation(ExistingPresentation.TheHitchhikersGuideToTheCosoms);
			await this.AddPresentation(ExistingPresentation.GraphingYourWayThroughTheCosoms);
			await this.AddPresentation(ExistingPresentation.EventDrivenArchitectureInTheCloud);
			await this.QueryPresentationsAsync();
			//await this.DeleteDatabaseAndCleanupAsync();
		}
		
	}

}