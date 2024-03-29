﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.SQL.Entities
{
	public class Generate
	{

		public static Presentation Presentation(ExistingPresentation presentation)
		{
			switch (presentation)
			{
				case ExistingPresentation.TheHitchhikersGuideToTheCosoms:
					return TheHitchhikersGuideToTheCosmos();
				case ExistingPresentation.GraphingYourWayThroughTheCosoms:
					return GraphingYourWayThroughTheCosoms();
				case ExistingPresentation.EventDrivenArchitectureInTheCloud:
					return EventDrivenArchitectureInTheCloud();
				default:
					return null;
			}
		}

		private static Presentation TheHitchhikersGuideToTheCosmos()
		{
			return new Presentation
			{
				Id = "Green.1",
				OwnerEmailAddress = "chadgreen@chadgreen.com",
				Title = "The Hitchhiker's Guide to the Cosoms",
				Abstract = "<p>Today’s applications are required to be highly responsible and always online.  Cosmos DB was built from the ground up to provide a globally distributed, multi-model database service that enables you to elastically and independently scale throughput and storage across any number of Azure regions worldwide.  Because of its ARS (atoms, records, and sequences) design, Azure Cosmos DB accessing data via SQL, MongoDB, Table, Gremlin, and Cassandra APIs.  All of this with transparent multi-master replication, high availability at 99.999%, and guarantees of less than 10-ms latencies both reads and (indexed) writes.</p><p>In this session, you will learn what you can do with Cosmos DB, the benefits of each of these data models, and how to use everything Cosmos DB has to offer to make your applications rock solid.Come find out when and how to implement Cosmos DB and which options you should use based upon your needs.</p>",
				ElevatorPitcch = "<p>In this session, you will learn what you can do with Cosmos DB, the benefits of each of these data models, and how to use everything Cosmos DB has to offer to make your applications rock solid.  Come find out when and how to implement Cosmos DB and which options you should use based upon your needs.</p>",
				ImportantDetails = null,
				LanguageCode = "en",
				Langauge = "English",
				LearningObjectives = new LearningObjective[]
	{
					new LearningObjective
					{
						Objective = "Learn about the different Azure Cosmos DB data models"
					},
					new LearningObjective
					{
						Objective = "Learn how to use the benefits of Azure Cosmos DB to build the best data solution"
					},
					new LearningObjective
					{
						Objective = "Learn about some of the common Cosmos DB use cases"
					}
	},
				Tags = new Tag[]
	{
					new Tag
					{
						Name = "Azure"
					},
					new Tag
					{
						Name = "Database"
					},
					new Tag
					{
						Name = "Cosmos DB"
					},
					new Tag
					{
						Name = "Cloud"
					},
					new Tag
					{
						Name = "Architecture"
					}
	},
				PresentationTypes = new PresentationType[]
	{
					new PresentationType
					{
						Name = "60-Minute Session",
						Length = 60
					},
					new PresentationType
					{
						Name = "75-Minute Session",
						Length = 75
					}
	}
			};

		}

		private static Presentation GraphingYourWayThroughTheCosoms()
		{
			return new Presentation
			{
				Id = "Green.2",
				OwnerEmailAddress = "chadgreen@chadgreen.com",
				Title = "Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs",
				Abstract = "<p>Data as it appears in the real world is naturally connected, but traditional data modeling focuses on entities which can cause for complicated joins of these naturally connected data.  That is where graph databases come in as they store data more like what happens naturally in the real world.  Sure, there a lot of talk about using graph databases for social networks, recommendation engines, and Internet of Things; but using graph databases can also make a lot of sense when working with common business data problems.</p><p>In this presentation, you will get a better understanding of what graph databases are, how to use the Gremlin API within Azure Cosmos DB to traverse such data structures, and see practical examples to common data problems.</p>",
				ElevatorPitcch = "<p>Data is naturally connected, but data modeling focuses on entities which cause complicated joins; this is where graph databases come in as they store data more naturally. See how to use Azure Cosmos DB Gremlin API to traverse such data structures and see practical examples to common data problems.</p>",
				ImportantDetails = null,
				LanguageCode = "en",
				Langauge = "English",
				LearningObjectives = new LearningObjective[]
	{
					new LearningObjective
					{
						Objective = "Understand the basics of graph databases"
					},
					new LearningObjective
					{
						Objective = "See real world examples of graph databases with common business data problems"
					},
					new LearningObjective
					{
						Objective = "Understand best practices in buidling graphing data solutions"
					}
	},
				Tags = new Tag[]
	{
					new Tag
					{
						Name = "Azure"
					},
					new Tag
					{
						Name = "Database"
					},
					new Tag
					{
						Name = "Cosmos DB"
					},
					new Tag
					{
						Name = "Cloud"
					},
					new Tag
					{
						Name = "Architecture"
					}
	},
				PresentationTypes = new PresentationType[]
	{
					new PresentationType
					{
						Name = "45-Minute Session",
						Length = 45
					},
					new PresentationType
					{
						Name = "60-Minute Session",
						Length = 60
					},
					new PresentationType
					{
						Name = "75-Minute Session",
						Length = 75
					}
	}
			};

		}

		private static Presentation EventDrivenArchitectureInTheCloud()
		{
			return new Presentation
			{
				Id = "Green.3",
				OwnerEmailAddress = "chadgreen@chadgreen.com",
				Title = "Event-Driven Architecture in the Cloud",
				Abstract = "<p>Event-driven architectures is a versatile approach to designing and integrating complex software systems with loosely coupled components.  While not a new concept, event-driven architecture is seeing a new life as applications move to cloud as it provides much more robust possibilities to solve business problems of today and in the future.  In this session, you will learn how to create a loosely coupled architecture for your business that has the domain at the core.  You will learn the basics of event-driven architecture, how to transform your complex systems to become event driven, and what benefits this architecture brings to your businesses.  The overall architecture is applicable to any cloud stack, but this session will have examples using Azure offerings.</p>",
				ElevatorPitcch = "<p>Event-driven architectures is a versatile approach to designing and integrating complex software systems with loosely coupled components.  In this session, you will learn how to create a loosely coupled architecture for your business that has the domain at the core.</p>",
				ImportantDetails = null,
				LanguageCode = "en",
				Langauge = "English",
				LearningObjectives = new LearningObjective[]
	{
					new LearningObjective
					{
						Objective = "Learn the basics of event-driven architecture"
					},
					new LearningObjective
					{
						Objective = "Learn how to transform your complext systems to become event driven"
					},
					new LearningObjective
					{
						Objective = "Learning about the benefits event-driven architecture brings to your business"
					}
	},
				Tags = new Tag[]
	{
					new Tag
					{
						Name = "Architecture"
					},
					new Tag
					{
						Name = "Event-Driven Architecture"
					},
					new Tag
					{
						Name = "Azure"
					},
					new Tag
					{
						Name = "Event Hubs"
					},
					new Tag
					{
						Name = "Cosmos DB"
					},
					new Tag
					{
						Name = "Azure Functions"
					},
					new Tag
					{
						Name = "Cloud"
					}
	},
				PresentationTypes = new PresentationType[]
	{
					new PresentationType
					{
						Name = "45-Minute Session",
						Length = 45
					},
					new PresentationType
					{
						Name = "60-Minute Session",
						Length = 60
					},
					new PresentationType
					{
						Name = "75-Minute Session",
						Length = 75
					}
	}
			};

		}

	}
}