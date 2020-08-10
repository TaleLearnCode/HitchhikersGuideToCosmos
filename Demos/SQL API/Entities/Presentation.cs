using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.SQL.Entities
{

	public class Presentation
	{

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		public string OwnerEmailAddress { get; set; }

		public string Title { get; set; }

		public string Abstract { get; set; }

		public string ElevatorPitcch { get; set; }

		public string ImportantDetails { get; set; }

		public string LanguageCode { get; set; }

		public string Langauge { get; set; }

		public LearningObjective[] LearningObjectives { get; set; }

		public Tag[] Tags { get; set; }

		public PresentationType[] PresentationTypes { get; set; }

	}

}