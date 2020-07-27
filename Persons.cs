using System;
using Microsoft.Azure.Documents;

namespace Sample
{
    // Add the schema of the cosmos DB item
    public class Persons
    {
        public string firstname { get; set; }
        public int age { get; set; }
        public string id { get; set; }
        public string _rid { get; set; }
        public string _self { get; set; }
        public string _etag { get; set; }
        public string _attachments { get; set; }
        public int _ts { get; set; }
    }
}