using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Mongo
{
	class Program
	{

		private static DAL dal = new DAL();

		static void Main(string[] args)
		{
			dal.CreatePresentation(Generate.Presentation(ExistingPresentation.EventDrivenArchitectureInTheCloud));
			dal.CreatePresentation(Generate.Presentation(ExistingPresentation.GraphingYourWayThroughTheCosoms));
			dal.CreatePresentation(Generate.Presentation(ExistingPresentation.TheHitchhikersGuideToTheCosoms));

			//IMongoCollection<Presentation> GetPresentationsCollection
			List<Presentation> presentations = dal.GetAllPresentations();
			foreach (Presentation presentation in presentations)
				Console.WriteLine(presentation.Title);

			Console.Beep();
			Console.ReadKey();
		}
	}
}
