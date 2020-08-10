using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Mongo
{
	public class PresentationType
	{

		[BsonElement("Name")]
		public string Name { get; set; }

		[BsonElement("Length")]
		public int Length { get; set; }

	}

}