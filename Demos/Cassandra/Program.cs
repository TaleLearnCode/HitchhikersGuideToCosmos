using Cassandra;
using Cassandra.Mapping;
using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace HitchhikersGuideToTheCosoms.Demos.CassandraDemo
{
	class Program
	{

		// Cassandra Cluster Configs      
		private const string UserName = "hitchhiker-eastus2-cassandra";
		private const string Password = "Z8QIHdloedwcrx9uODi9FU3xUoHF2328tzu1tewDi2EyOcLtL8q3KEYIihFZPMc0TFCC1wxbKyot8QUeRiCJ4g==";
		private const string CassandraContactPoint = "hitchhiker-eastus2-cassandra.cassandra.cosmos.azure.com";
		private static int CassandraPort = 10350;

		public static void Main(string[] args)
		{
			// Connect to cassandra cluster  (Cassandra API on Azure Cosmos DB supports only TLSv1.2)
			var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
			options.SetHostNameResolver((ipAddress) => CassandraContactPoint);
			Cluster cluster = Cluster.Builder().WithCredentials(UserName, Password).WithPort(CassandraPort).AddContactPoint(CassandraContactPoint).WithSSL(options).Build();
			ISession session = cluster.Connect();

			// Creating KeySpace and table
			session.Execute("DROP KEYSPACE IF EXISTS speakingengagements");
			session.Execute("CREATE KEYSPACE speakingengagements WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'datacenter1' : 1 };");
			Console.WriteLine(String.Format("created keyspace speakingengagements"));
			session.Execute("CREATE TABLE IF NOT EXISTS speakingengagements.presentation (presentation_id text PRIMARY KEY, presentation_owneremailaddress text, presentation_title text, presentation_abstract text, presentation_elevatorpitch text, presentation_importantinformation text, presentation_languagecode text, presentation_language text)");
			Console.WriteLine(String.Format("created table Presentation"));

			session = cluster.Connect("speakingengagements");
			IMapper mapper = new Mapper(session);

			// Inserting Data into presentation table
			mapper.Insert<Presentation>(Generate.Presentation(ExistingPresentation.EventDrivenArchitectureInTheCloud));
			mapper.Insert<Presentation>(Generate.Presentation(ExistingPresentation.GraphingYourWayThroughTheCosoms));
			mapper.Insert<Presentation>(Generate.Presentation(ExistingPresentation.TheHitchhikersGuideToTheCosoms));
			Console.WriteLine("Inserted data into presetnation table");

			Console.WriteLine("Select ALL");
			Console.WriteLine("-------------------------------");
			foreach (Presentation presentation in mapper.Fetch<Presentation>("Select * from presentation"))
			{
				Console.WriteLine(presentation);
			}

			Console.WriteLine("Getting by id Green.3");
			Console.WriteLine("-------------------------------");


			Presentation presentationId3 = mapper.FirstOrDefault<Presentation>("Select * from presentation where presentation_owneremailaddress = ?", "chadgreen@chadgreen.com");
			Console.WriteLine(presentationId3.presentation_title);

			// Clean up of Table and KeySpace
			//session.Execute("DROP table presetnation");
			//session.Execute("DROP KEYSPACE speakingengagements");

			// Wait for enter key before exiting  
			Console.ReadLine();
		}

		public static bool ValidateServerCertificate(
						object sender,
						X509Certificate certificate,
						X509Chain chain,
						SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
				return true;

			Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
			// Do not allow this client to communicate with unauthenticated servers.
			return false;
		}

	}
}
