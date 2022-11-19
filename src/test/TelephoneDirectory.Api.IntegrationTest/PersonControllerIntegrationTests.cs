using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.Contracts.Dto;

namespace TelephoneDirectory.Api.IntegrationTest
{
    public class PersonControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public PersonControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        /// <summary>
        /// Kayıt eklendikten sonra status 200 ve response boş olmamalı
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task Post_Person_Status_Ok_Response_NotNull()
        {
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange
            var person = new AddPersonRequest()
            {
                Name = "Cemil",
                Surname = "GÜLER",
                Company = "Test"
            };
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/person", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotEqual(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }


        /// <summary>
        /// Kayıt eklendikten sonra silinebilmeli ve silinme sonrası status 200 ve response boş olmalı
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Remove_Person_Status_Ok_Response_Null()
        {
            var newPerson = await AddPerson();

            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;            


            var request = new HttpRequestMessage
            {

                Method = HttpMethod.Delete,
                RequestUri = new Uri("api/person", UriKind.Relative),
                Content = new StringContent(JsonConvert.SerializeObject(newPerson.Id), Encoding.UTF8, "application/json")
            };
            // Act-1
            var response = await _client.SendAsync(request);
            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        /// <summary>
        /// Kaydedilen personel detayı alındığında status 200 ve response boş olmamalı ayrıca eklenen ile detayı alınan personel adı aynı olmalı 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_PersonDetail_Status_Ok_Response_NotNull_Response_PersonName_Equal_Added_PersonName()
        {
            var newPerson = await AddPerson();

            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Act
            var response = await _client.GetAsync($"api/Person/person-detail?id={newPerson.Id}");

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var actualResultObj = JsonConvert.DeserializeObject<PersonDetailResponse>(actualResult);
            // Assert
            Assert.NotEqual(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(newPerson.Name, actualResultObj?.Name);

        }

        /// <summary>
        /// Kaydedilen personel sorgulandığında status 200 ve response boş olmamalı ayrıca eklenen ile sorgulanan personel adı aynı olmalı 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Search_Person_Status_Ok_Response_NotNull_Response_PersonName_Equal_Added_PersonName()
        {
            var newPerson = await AddPerson();

            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Act
            var response = await _client.GetAsync($"/api/person?Name={newPerson.Name}");

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var actualResultObj = JsonConvert.DeserializeObject<List<SearchPersonResponse>>(actualResult);
            // Assert
            Assert.NotEqual(expectedResult, actualResult);
            Assert.NotEmpty(actualResultObj); 
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(newPerson.Name, actualResultObj?.FirstOrDefault()?.Name);

        }

        #region private
        private async Task<AdPersonResponse> AddPerson()
        {
            var person = new AddPersonRequest()
            {
                Name = $"Cemil {Guid.NewGuid().ToString()}",
                Surname = "GÜLER",
                Company = "Test"
            };
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/person", content);
            var result = await response.Content.ReadAsStringAsync();
            return new AdPersonResponse
            {
                Id = JsonConvert.DeserializeObject<Guid>(result),
                Name = person.Name,

            };

        }
        private class AdPersonResponse
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
        }
        #endregion
    }
}
