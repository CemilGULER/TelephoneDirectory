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
    public class PersonContactControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public PersonContactControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        /// <summary>
        /// Kayıt eklendikten sonra status 200 ve response boş olmamalı
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Post_Person_Contact_Status_Ok_Response_NotNull()
        {
            var newPerson = await AddPerson();
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange
            var personContact = new AddPersonContactRequest()
            {
                PersonId = newPerson.Id,
                ContactDetail = "5555555555",
                ContactType = Contact.Contracts.Enum.ContactType.PhoneNumber
            };
            var content = new StringContent(JsonConvert.SerializeObject(personContact), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/personcontact", content);

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
        public async Task Remove_Person_Contact_Status_Ok_Response_Null()
        {
            var newPersonContact = await AddPersonContact();

            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;


            var request = new HttpRequestMessage
            {

                Method = HttpMethod.Delete,
                RequestUri = new Uri("api/personcontact", UriKind.Relative),
                Content = new StringContent(JsonConvert.SerializeObject(newPersonContact.Id), Encoding.UTF8, "application/json")
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
        /// Personele iletişim bilgisi eklendikten sonra person-detail üzerinden görüntülenebilmeli 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_PersonDetail_Status_Ok_Response_NotNull_Response_PersonContacts_Equal_Added_PersonContacts()
        {
            var newPersonContact = await AddPersonContact();

            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Act
            var response = await _client.GetAsync($"api/Person/person-detail?id={newPersonContact.PersonId}");

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var actualResultObj = JsonConvert.DeserializeObject<PersonDetailResponse>(actualResult);
            var insertedPersonContact = actualResultObj?.PersonContacts.Any(c => c.ContactType == Contact.Contracts.Enum.ContactType.PhoneNumber);
            // Assert
            Assert.NotEqual(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.True(insertedPersonContact);

        }
        #region private
        private async Task<AddPersonResponse> AddPerson()
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
            return new AddPersonResponse
            {
                Id = JsonConvert.DeserializeObject<Guid>(result),
                Name = person.Name,

            };

        }
        private async Task<AddPersonContactResponse> AddPersonContact()
        {
            var newPerson = await AddPerson();
            var personContact = new AddPersonContactRequest()
            {
                PersonId = newPerson.Id,
                ContactDetail = "5555555555",
                ContactType = Contact.Contracts.Enum.ContactType.PhoneNumber
            };
            var content = new StringContent(JsonConvert.SerializeObject(personContact), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/personcontact", content);
            var result = await response.Content.ReadAsStringAsync();
            return new AddPersonContactResponse
            {
                Id = JsonConvert.DeserializeObject<Guid>(result),
                PersonId = newPerson.Id
            };

        }
        private class AddPersonResponse
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
        }
        private class AddPersonContactResponse
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }
        #endregion
    }
}
