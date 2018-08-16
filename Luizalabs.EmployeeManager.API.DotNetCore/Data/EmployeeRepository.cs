using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Luizalabs.EmployeeManager.API.DotNetCore.Models;
using System;
using System.Collections.Generic;


namespace Luizalabs.EmployeeManager.API.DotNetCore.Data
{
    public interface IEmployeeRepository
    {
        List<Employee> List(int page_size, int page);
        void Add(Employee employee);
        void Delete(long id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        AmazonDynamoDBClient dynamodbClient;
        public EmployeeRepository(IAmazonDynamoDB amazonDynamoDB)
        {
            
            this.dynamodbClient = (AmazonDynamoDBClient)amazonDynamoDB; 
        }
        public List<Employee> List(int page_size, int page)
        {
            ScanResponse returnIWant = null;
            string KeyToStart = string.Empty;
            Dictionary<string, AttributeValue> lastKey = null;

            for (int p = 1; p <= page; p++)
            {
                var query = new ScanRequest()
                {
                    TableName = "employee",
                    Limit = page_size,
                    ExclusiveStartKey = lastKey != null? lastKey : null,
                };

                returnIWant = dynamodbClient.ScanAsync(query).Result;
                lastKey = returnIWant.LastEvaluatedKey;
            }

            var employees = returnIWant?.Items.ConvertAll(new Converter<Dictionary<string, AttributeValue>, Employee>(itemRetorno => 
            {
                
                return new Employee()
                {
                    name = itemRetorno["name"].S,
                    department = itemRetorno["department"].S,
                    email = itemRetorno["email"].S,
                    id = long.Parse(itemRetorno["id"].S)
                };
            }));

            return employees;
        }

        public void Add(Employee employee)
        {
            var request = new PutItemRequest
            {
                TableName = "employee",
                Item = new Dictionary<string, AttributeValue>
                  {
                    { "id", new AttributeValue{  S = DateTime.Now.Ticks.ToString() }},
                    { "name", new AttributeValue { S = employee.name }},
                    { "email", new AttributeValue { S = employee.email }},
                    { "department", new AttributeValue { S = employee.department }}
                  }
            };
            PutItemResponse x = dynamodbClient.PutItemAsync(request).Result;

            if (x.HttpStatusCode != System.Net.HttpStatusCode.OK && x.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Unable to add Employee");

            return ;
        }

        public void Delete(long id)
        {
            var request = new DeleteItemRequest
            {
                TableName = "employee",
                Key = new Dictionary<string, AttributeValue>() { { "id", new AttributeValue { S = id.ToString() } } },
                ReturnValues =  new ReturnValue("ALL_OLD"),
            };

            DeleteItemResponse x = dynamodbClient.DeleteItemAsync(request).Result;

            if (x.HttpStatusCode != System.Net.HttpStatusCode.OK && x.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Unable to delete Employee");

            if (x.Attributes.Count == 0)
                throw new KeyNotFoundException("Employee not found");

            return;
        }

    }
}
