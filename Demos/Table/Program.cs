using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Table
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Azure Cosmos DB Table Examples");

			string tableName = "Presentation";

			// Create or reference an existing table
			CloudTable table = await Common.CreateTableAsync(tableName);

			// Demonstrate how to insert the entity
			Console.WriteLine("Inserting the presentations");
			await SamplesUtils.InsertOrMergeEntityAsync(table, Generate.Presentation(ExistingPresentation.EventDrivenArchitectureInTheCloud));
			await SamplesUtils.InsertOrMergeEntityAsync(table, Generate.Presentation(ExistingPresentation.GraphingYourWayThroughTheCosoms));
			await SamplesUtils.InsertOrMergeEntityAsync(table, Generate.Presentation(ExistingPresentation.TheHitchhikersGuideToTheCosoms));

			// Demonstrate how to Read the updated entity using a point query 
			Console.WriteLine("Reading the presentations");
			Presentation presentation = await SamplesUtils.RetrieveEntityUsingPointQueryAsync(table, "chadgreen@chadgreen.com", "Green.1");
			Console.WriteLine($"\t{presentation.Title}");

		}
	}
}
