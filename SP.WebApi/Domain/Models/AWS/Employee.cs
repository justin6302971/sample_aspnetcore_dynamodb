using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;


namespace SP.WepApi.Domain.Models.AWS
{
    [DynamoDBTable("Employee")]
    public class Employee
    {
        [DynamoDBHashKey]
        public string LoginAlias { get; set; }

        [DynamoDBProperty]
        public string LastName { get; set; }
        [DynamoDBProperty]
        public string FirstName { get; set; }

        [DynamoDBProperty]
        public string ManagerLoginAlias { get; set; }

        [DynamoDBProperty]
        public List<string> Skills { get; set; }

    }
}
