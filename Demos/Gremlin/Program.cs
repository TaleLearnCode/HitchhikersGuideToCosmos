using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;

namespace HitchhikerDemo_Gremlin
{
	class Program
	{
		private static string hostname = "hitchhiker-eastus2-gremlin.gremlin.cosmosdb.azure.com";
		private static int port = 443;
		private static string authKey = "e9L1kIrTiIagpV8YErFIf6ccj3qKxl5nOZPfUgto3kkiYnh8xEOPb0Pqsw2FpjgchEiAP3mMyxPUNjtXtIfJMw==";
		private static string database = "SpeakingEngagements";
		private static string collection = "SpeakingEngagements";

		// Gremlin queries that will be executed.
		private static Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
				{
						{ "Cleanup",                                         "g.V().drop()" },
						{ "Add Presentation 1",                              "g.addV('presentation').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'The Hitchhikers Guide to the Cosoms').property('abstract', 'abstract').property('elevatorPitch', 'elevator pitch')" },
						{ "Add Presentation 2",                              "g.addV('presentation').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').property('abstract', '<p>Data as it appears in the real world is naturally connected, but traditional data modeling focuses on entities which can cause for complicated joins of these naturally connected data.  That is where graph databases come in as they store data more like what happens naturally in the real world.  Sure, there a lot of talk about using graph databases for social networks, recommendation engines, and Internet of Things; but using graph databases can also make a lot of sense when working with common business data problems.</p><p>In this presentation, you will get a better understanding of what graph databases are, how to use the Gremlin API within Azure Cosmos DB to traverse such data structures, and see practical examples to common data problems.</p>').property('elevatorPitch', '<p>Data is naturally connected, but data modeling focuses on entities which cause complicated joins; this is where graph databases come in as they store data more naturally. See how to use Azure Cosmos DB Gremlin API to traverse such data structures and see practical examples to common data problems.</p>')" },
						{ "Add Presentation 3",                              "g.addV('presentation').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Event-Driven Architecture in the Cloud').property('abstract', '<p>Event-driven architectures is a versatile approach to designing and integrating complex software systems with loosely coupled components.  While not a new concept, event-driven architecture is seeing a new life as applications move to cloud as it provides much more robust possibilities to solve business problems of today and in the future.  In this session, you will learn how to create a loosely coupled architecture for your business that has the domain at the core.  You will learn the basics of event-driven architecture, how to transform your complex systems to become event driven, and what benefits this architecture brings to your businesses.  The overall architecture is applicable to any cloud stack, but this session will have examples using Azure offerings.</p>').property('elevatorPitch', '<p>Event-driven architectures is a versatile approach to designing and integrating complex software systems with loosely coupled components.  In this session, you will learn how to create a loosely coupled architecture for your business that has the domain at the core.</p>')" },
						{ "Add Tag 1",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Azure')" },
						{ "Add Tag 2",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Database')" },
						{ "Add Tag 3",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Cosmos DB')" },
						{ "Add Tag 4",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Cloud')" },
						{ "Add Tag 5",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Architecture')" },
						{ "Add Tag 6",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Event-Driven Architecture')" },
						{ "Add Tag 7",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Event Hubs')" },
						{ "Add Tag 8",                                       "g.addV('tag').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Azure Functions')" },
						{ "Add Presentation Type 1",                         "g.addV('presentationType').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Session')" },
						{ "Add Presentation Type 2",                         "g.addV('presentationType').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('name', 'Workshop')" },
						{ "Add Language 1",                                  "g.addV('language').property('ownerEmailAddress', 'chadgreen@chadgreen.com').property('code', 'en').property('name', 'English')" },
						{ "Relate Presentation 1 to its Tags 1",             "g.V().hasLabel('presentation').has('name', 'The Hitchhikers Guide to the Cosoms').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Azure'))" },
						{ "Relate Presentation 1 to its Tags 2",             "g.V().hasLabel('presentation').has('name', 'The Hitchhikers Guide to the Cosoms').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Database'))" },
						{ "Relate Presentation 1 to its Tags 3",             "g.V().hasLabel('presentation').has('name', 'The Hitchhikers Guide to the Cosoms').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Cosmos DB'))" },
						{ "Relate Presentation 1 to its Tags 4",             "g.V().hasLabel('presentation').has('name', 'The Hitchhikers Guide to the Cosoms').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Cloud'))" },
						{ "Relate Presentation 1 to its Tags 5",             "g.V().hasLabel('presentation').has('name', 'The Hitchhikers Guide to the Cosoms').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Architecture'))" },
						{ "Relate Presentation 1 to its Langauge",           "g.V().hasLabel('presentation').has('name', 'The Hitchhikers Guide to the Cosoms').addE('presentedIn').to(g.V().hasLabel('language').has('code', 'en'))" },
						{ "Relate Presentation 1 to its Presentation Type",  "g.V().hasLabel('presentation').has('name', 'The Hitchhikers Guide to the Cosoms').addE('is').to(g.V().hasLabel('presentationType').has('name', 'Session')).property('length', 60)" },
						{ "Relate Presentation 2 to its Tags 1",             "g.V().hasLabel('presentation').has('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Azure'))" },
						{ "Relate Presentation 2 to its Tags 2",             "g.V().hasLabel('presentation').has('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Database'))" },
						{ "Relate Presentation 2 to its Tags 3",             "g.V().hasLabel('presentation').has('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Cosmos DB'))" },
						{ "Relate Presentation 2 to its Tags 4",             "g.V().hasLabel('presentation').has('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Cloud'))" },
						{ "Relate Presentation 2 to its Tags 5",             "g.V().hasLabel('presentation').has('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Architecture'))" },
						{ "Relate Presentation 2 to its Langauge",           "g.V().hasLabel('presentation').has('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').addE('presentedIn').to(g.V().hasLabel('language').has('code', 'en'))" },
						{ "Relate Presentation 2 to its Presentation Type",  "g.V().hasLabel('presentation').has('name', 'Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs').addE('is').to(g.V().hasLabel('presentationType').has('name', 'Session')).property('length', 60)" },
						{ "Relate Presentation 3 to its Tags 1",             "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Architecture'))" },
						{ "Relate Presentation 3 to its Tags 2",             "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Event-Driven Architecture'))" },
						{ "Relate Presentation 3 to its Tags 3",             "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Azure'))" },
						{ "Relate Presentation 3 to its Tags 4",             "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Event Hubs'))" },
						{ "Relate Presentation 3 to its Tags 5",             "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Cosmos DB'))" },
						{ "Relate Presentation 3 to its Tags 6",             "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Azure Functions'))" },
						{ "Relate Presentation 3 to its Tags 7",             "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('taggedAs').to(g.V().hasLabel('tag').has('name', 'Cloud'))" },
						{ "Relate Presentation 3 to its Langauge",           "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('presentedIn').to(g.V().hasLabel('language').has('code', 'en'))" },
						{ "Relate Presentation 3 to its Presentation Type",  "g.V().hasLabel('presentation').has('name', 'Event-Driven Architecture in the Cloud').addE('is').to(g.V().hasLabel('presentationType').has('name', 'Session')).property('length', 75)" },
						{ "CountVertices",  "g.V().count()" },
						{ "Project",        "g.V().hasLabel('presentation').values('name')" },
						{ "Sort",           "g.V().hasLabel('presentation').order().by('name', decr)" },
						{ "Traverse",       "g.V().hasLabel('tag').has('name', 'Cosmos DB').in('taggedAs').hasLabel('presentation')" },
						{ "CountEdges",     "g.E().count()" },
				};

		// Starts a console application that executes every Gremlin query in the gremlinQueries dictionary. 
		static void Main(string[] args)
		{
			var gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
																							username: "/dbs/" + database + "/colls/" + collection,
																							password: authKey);

			using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
			{
				foreach (var query in gremlinQueries)
				{
					Console.WriteLine(String.Format("Running this query: {0}: {1}", query.Key, query.Value));

					// Create async task to execute the Gremlin query.
					var resultSet = SubmitRequest(gremlinClient, query).Result;
					if (resultSet.Count > 0)
					{
						Console.WriteLine("\tResult:");
						foreach (var result in resultSet)
						{
							// The vertex results are formed as Dictionaries with a nested dictionary for their properties
							string output = JsonConvert.SerializeObject(result);
							Console.WriteLine($"\t{output}");
						}
						Console.WriteLine();
					}

					// Print the status attributes for the result set.
					// This includes the following:
					//  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
					//  x-ms-total-request-charge   : The total request units charged for processing a request.
					PrintStatusAttributes(resultSet.StatusAttributes);
					Console.WriteLine();
				}
			}

			// Exit program
			Console.WriteLine("Done. Press any key to exit...");
			Console.ReadLine();
		}

		private static Task<ResultSet<dynamic>> SubmitRequest(GremlinClient gremlinClient, KeyValuePair<string, string> query)
		{
			try
			{
				return gremlinClient.SubmitAsync<dynamic>(query.Value);
			}
			catch (ResponseException e)
			{
				Console.WriteLine("\tRequest Error!");

				// Print the Gremlin status code.
				Console.WriteLine($"\tStatusCode: {e.StatusCode}");

				// On error, ResponseException.StatusAttributes will include the common StatusAttributes for successful requests, as well as
				// additional attributes for retry handling and diagnostics.
				// These include:
				//  x-ms-retry-after-ms         : The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will be populated when
				//                              : attribute 'x-ms-status-code' returns 429.
				//  x-ms-activity-id            : Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
				PrintStatusAttributes(e.StatusAttributes);
				Console.WriteLine($"\t[\"x-ms-retry-after-ms\"] : { GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
				Console.WriteLine($"\t[\"x-ms-activity-id\"] : { GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

				throw;
			}
		}

		private static void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
		{
			Console.WriteLine($"\tStatusAttributes:");
			Console.WriteLine($"\t[\"x-ms-status-code\"] : { GetValueAsString(attributes, "x-ms-status-code")}");
			Console.WriteLine($"\t[\"x-ms-total-request-charge\"] : { GetValueAsString(attributes, "x-ms-total-request-charge")}");
		}

		public static string GetValueAsString(IReadOnlyDictionary<string, object> dictionary, string key)
		{
			return JsonConvert.SerializeObject(GetValueOrDefault(dictionary, key));
		}

		public static object GetValueOrDefault(IReadOnlyDictionary<string, object> dictionary, string key)
		{
			if (dictionary.ContainsKey(key))
			{
				return dictionary[key];
			}

			return null;
		}

	}
}
