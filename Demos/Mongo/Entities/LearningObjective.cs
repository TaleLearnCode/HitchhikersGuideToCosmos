using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Mongo
{
	public class LearningObjective
	{

		[BsonElement("Objective")]
		public string Objective { get; set; }

	}
}