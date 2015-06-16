using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;
using MyWebRole.Models;
using CloudStorageAccount=Microsoft.WindowsAzure.Storage.CloudStorageAccount;


namespace MyWebRole.Common
{
    public class DataTableStorageHelper
    {

        internal const string TableName = "customer";

        public static CloudTable GetTable()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            
            CloudTable table = tableClient.GetTableReference(TableName);
            table.CreateIfNotExists();           
            return table;
        }

        public static void InsertData(CustomerEntity customer)
        {
            customer.PartitionKey = Guid.NewGuid().ToString();
            customer.RowKey = "";
            TableOperation insertOperation = TableOperation.Insert(customer);        
            GetTable().Execute(insertOperation);
        }
        public static void InsertDatas(List<CustomerEntity> customers)
        {
            TableBatchOperation batchOperation = new TableBatchOperation();

            foreach (var customer in customers)
            {
                   batchOperation.Insert(customer);
            }

            GetTable().ExecuteBatch(batchOperation);
        }

        public static List<CustomerEntity> GetData()
        {
            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>();        
            return GetTable().ExecuteQuery(query).ToList();
        }

        public static List<CustomerEntity> GetRangeData(CustomerEntity customer)
        {
            TableQuery<CustomerEntity> rangeQuery = new TableQuery<CustomerEntity>().Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("FirstName", QueryComparisons.Equal, customer.FirstName),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("LastName", QueryComparisons.Equal, customer.LastName))
                );             
            return GetTable().ExecuteQuery(rangeQuery).ToList();
        }

        public static CustomerEntity GetSingleData(CustomerEntity customer)
        {
            var retrievedResult = RetrievedResult(customer);

            return (CustomerEntity)retrievedResult.Result;
        }    
        public static void DeleteData(CustomerEntity customer)
        {
            var retrievedResult = RetrievedResult(customer);
  
            CustomerEntity deleteEntity = (CustomerEntity)retrievedResult.Result;           
            TableOperation deleteOperation = TableOperation.Delete(deleteEntity);        
            GetTable().Execute(deleteOperation);

        }

        public static void ModifyData(CustomerEntity customer)
        {
           var retrievedResult = RetrievedResult(customer);
         
           CustomerEntity updateEntity = (CustomerEntity)retrievedResult.Result;
            if (updateEntity != null)
            {
                
                updateEntity.FirstName = customer.FirstName;
                updateEntity.LastName = customer.LastName;
                updateEntity.Email = customer.Email;
                updateEntity.PhoneNumber = customer.PhoneNumber;
                updateEntity.Picture = customer.Picture;
              
                TableOperation updateOperation = TableOperation.Replace(updateEntity);               
                GetTable().Execute(updateOperation);
            }
        }

        private static TableResult RetrievedResult(CustomerEntity customer)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(customer.PartitionKey, "");
            TableResult retrievedResult = GetTable().Execute(retrieveOperation);
            return retrievedResult;
        }

    }
}