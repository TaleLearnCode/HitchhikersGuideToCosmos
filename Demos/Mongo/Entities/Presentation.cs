using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Mongo
{
	public	class Presentation
	{

		[BsonId(IdGenerator =typeof(CombGuidGenerator))]
		public Guid Id { get; set; }

		[BsonElement("OwnerEmailAddress")]
		public string OwnerEmailAddress { get; set; }

		[BsonElement("Title")]
		public string Title { get; set; }

		[BsonElement("Abstract")]
		public string Abstract { get; set; }

		[BsonElement("ElevatorPitcch")]
		public string ElevatorPitcch { get; set; }

		[BsonElement("ImportantDetails")]
		public string ImportantDetails { get; set; }

		[BsonElement("LanguageCode")]
		public string LanguageCode { get; set; }

		[BsonElement("Langauge")]
		public string Langauge { get; set; }

		[BsonElement("LearningObjectives")]
		public LearningObjective[] LearningObjectives { get; set; }

		[BsonElement("Tags")]
		public Tag[] Tags { get; set; }

		[BsonElement("PresentationTypes")]
		public PresentationType[] PresentationTypes { get; set; }

	}
}