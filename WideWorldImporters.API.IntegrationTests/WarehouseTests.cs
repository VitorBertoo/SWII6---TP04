﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WideWorldImporters.API.IntegrationTests;
using WideWorldImporters.API.Models;
using Xunit;
namespace WideWorldImporters.API.IntegrationTests
{
    public class WarehouseTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        public WarehouseTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }
        [Fact]
        public async Task TestGetStockItemsAsync()
        {
            // Arrange
            var request = "/api/v1/Warehouse/StockItem";
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestGetStockItemAsync()
        {
            // Arrange
            var request = "/api/v1/Warehouse/StockItem/1";
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestPostStockItemAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Warehouse/StockItem",
                Body = new
                {
                    StockItemName = string.Format("USB anime flash drive - Vegeta {0}", Guid.NewGuid()),
                    SupplierID = 12,
                    UnitPackageID = 7,
                    OuterPackageID = 7,
                    LeadTimeDays = 14,
                    QuantityPerOuter = 1,
                    IsChillerStock = false,
                    TaxRate = 15.000m,
                    UnitPrice = 32.00m,
                    RecommendedRetailPrice = 47.84m,
                    TypicalWeightPerUnit = 0.050m,
                    CustomFields = "{ \"CountryOfManufacture\": \"Japan\", \"Tags\": [\"32GB\",\"USB Powered\"] }",
                    Tags = "[\"32GB\",\"USB Powered\"]",
                    SearchDetails = "USB anime flash drive - Vegeta",
                    LastEditedBy = 1,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddYears(5)
                }
            };
            // Act
            var response = await Client.PostAsync(request.Url,
            ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestPutStockItemAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Warehouse/StockItem/1",
                Body = new
                {
                    StockItemName = string.Format("USB anime flash drive - Vegeta {0}", Guid.NewGuid()),
                    SupplierID = 12,
                    Color = 3,
                    UnitPrice = 39.00m
                }
            };
            // Act
            var response = await Client.PutAsync(request.Url,
            ContentHelper.GetStringContent(request.Body));
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestDeleteStockItemAsync()
        {
            // Arrange
            var postRequest = new
            {
                Url = "/api/v1/Warehouse/StockItem",
                Body = new
                {
                   
                    StockItemName = "string",
                    SupplierID = 0,
                    ColorID = 0,
                    UnitPackageID = 0,
                    OuterPackageID = 0,
                    Brand = "string",
                    Size = "string",
                    LeadTimeDays = 0,
                    QuantityPerOuter = 0,
                    IsChillerStock = false,
                    Barcode = "string",
                    TaxRate = 0,
                    UnitPrice = 0,
                    RecommendedRetailPrice = 0,
                    TypicalWeightPerUnit = 0,
                    MarketingComments = "string",
                    InternalComments = "string",
                    CustomFields = "string",
                    Tags = "string",
                    SearchDetails = "string",
                    LastEditedBy = 0,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddYears(5)

                }
            };
            // Act
            var postResponse = await Client.PostAsync(postRequest.Url,
            ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await
            postResponse.Content.ReadAsStringAsync();
            var singleResponse =
            JsonConvert.DeserializeObject<SingleResponse<StockItem>>(jsonFromPostResponse);
            var deleteResponse = await
            Client.DeleteAsync(string.Format("/api/v1/Warehouse/StockItem/{0}",
            singleResponse.Model.StockItemID));
            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.False(singleResponse.DidError);
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}