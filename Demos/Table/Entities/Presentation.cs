using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Table
{
	public	class Presentation : TableEntity
	{

		public Presentation() { }

		public Presentation(string id, string ownerEmailAddress)
		{
			PartitionKey = ownerEmailAddress;
			RowKey = id;
		}

		public string Title { get; set; }

		public string Abstract { get; set; }

		public string ElevatorPitcch { get; set; }

		public string ImportantDetails { get; set; }

		public string LanguageCode { get; set; }

		public string Langauge { get; set; }

	}
}