using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
// using Azure.Cosmos;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // These properties can be found in the Azure portal.
            var documentDbUri = "your-documentdb-endpoint";
            var Pkey = "your-primary-key-of-cosmos-db";
            var databaseId = "SampleDB";
            var collectionId = "Persons";

            var client = new DocumentClient(new Uri(documentDbUri), Pkey);

            var containerLink2 = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

            SqlQuerySpec query = new SqlQuerySpec("SELECT * FROM Persons c WHERE StringEquals(c.firstname,@firstname,false)");
            query.Parameters = new SqlParameterCollection();
            query.Parameters.Add(new SqlParameter("@firstname", "john"));
            var option = new FeedOptions { EnableCrossPartitionQuery = true };

            foreach (var person in client.CreateDocumentQuery(containerLink2, query, option))
            {
                Console.WriteLine("\tRead {0} from parameterized SQL", person.firstname);
            }

            var count = client.CreateDocumentQuery<Persons>(
                    UriFactory.CreateDocumentCollectionUri(databaseId, collectionId))
                    .Where( r => r.firstname == "john" )
                    .Count();
            // var count2 = client.CreateDocumentQuery<Persons>(
            //         UriFactory.CreateDocumentCollectionUri(databaseId, collectionId))
            //         .Where( r => STRINGEQUALS(r.firstname,"John",false) )
            //         .Count();
            string mySearchStringLowered = "john".ToLower();
            var count3 = client.CreateDocumentQuery<Persons>(
                    UriFactory.CreateDocumentCollectionUri(databaseId, collectionId))
                    .Where( r => r.firstname.ToLower() == mySearchStringLowered)
                    .Count();

            Console.WriteLine(count);
            // Console.WriteLine(count2);
            Console.WriteLine(count3);
            Console.WriteLine("Done!!");
        }
    }
}
