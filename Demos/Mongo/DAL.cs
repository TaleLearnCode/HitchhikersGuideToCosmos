using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Security.Authentication;

namespace ChadGreen.HitchhikersGuideToTheCosmos.Demos.Mongo
{
	class DAL
	{
		private bool disposed = false;

		private string userName = "hitchhiker-eastus2-mongo";
		private string host = "hitchhiker-eastus2-mongo.documents.azure.com";
		private string password = "54lC0WrBDePos8EF9XYQvwqdV2JcuGWYFU29ZNgxaXdenbxBQgrTYzgTMiPzTK7uq4n6nelIuf1rZTInS8E73A==";

		private string dbName = "SpeakingEngagements";
		private string collectionName = "SpeakingEngagmeents";

		public DAL() { }

		// Gets all Task items from the MongoDB server.        
		public List<Presentation> GetAllPresentations()
		{
			try
			{
				var collection = GetPresentationsCollection();
				return collection.Find(new BsonDocument()).ToList();
			}
			catch (MongoConnectionException)
			{
				return new List<Presentation>();
			}
		}

		// Creates a Task and inserts it into the collection in MongoDB.
		public void CreatePresentation(Presentation presentation)
		{
			var collection = GetPresentationsCollectionForEdit();
			try
			{
				collection.InsertOne(presentation);
			}
			catch (MongoCommandException ex)
			{
				string msg = ex.Message;
			}
		}

		private IMongoCollection<Presentation> GetPresentationsCollection()
		{
			MongoClientSettings settings = new MongoClientSettings();
			settings.Server = new MongoServerAddress(host, 10255);
			settings.UseTls = true;
			settings.SslSettings = new SslSettings();
			settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

			MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
			MongoIdentityEvidence evidence = new PasswordEvidence(password);

			settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

			MongoClient client = new MongoClient(settings);
			var database = client.GetDatabase(dbName);
			var presetnationCollection = database.GetCollection<Presentation>(collectionName);
			return presetnationCollection;
		}

		private IMongoCollection<Presentation> GetPresentationsCollectionForEdit()
		{
			MongoClientSettings settings = new MongoClientSettings();
			settings.Server = new MongoServerAddress(host, 10255);
			settings.UseTls = true;
			settings.SslSettings = new SslSettings();
			settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

			MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
			MongoIdentityEvidence evidence = new PasswordEvidence(password);

			settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

			MongoClient client = new MongoClient(settings);
			var database = client.GetDatabase(dbName);
			var presetationCollection = database.GetCollection<Presentation>(collectionName);
			return presetationCollection;
		}

		#region IDisposable

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
				}
			}

			this.disposed = true;
		}

		#endregion
	}
}
