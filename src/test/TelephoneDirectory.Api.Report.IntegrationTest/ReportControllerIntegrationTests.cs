using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Common;
using TelephoneDirectory.Report.Contracts.Dto;

namespace TelephoneDirectory.Api.Report.IntegrationTest
{
    public  class ReportControllerIntegrationTests : IClassFixture<TestingWebAppFactory<ProgramReport>>
    {
        private readonly HttpClient _client;

        public ReportControllerIntegrationTests(TestingWebAppFactory<ProgramReport> factory)
            => _client = factory.CreateClient();

        /// <summary>
        /// Kayıt eklendikten sonra status 200 ve response boş olmamalı
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Post_Report_Status_Ok_Response_NotNull()
        {
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;           
            

            // Act
            var response = await _client.PostAsync("/api/report/report-request", null);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotEqual(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        /// <summary>
        /// Yeni bir rapor talep isteği henüz tamamlanmadan detail alınırsa 500 döner  
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Report_Detail_Status_InternalServerError()
        {

            var reportRequestId = await ReportRequest();
            var expectedResult = "Henüz rapor hazırlanmadı";
            var expectedStatusCode = HttpStatusCode.InternalServerError;


            // Act
            var response = await _client.GetAsync($"/api/report/report-detail?id={reportRequestId}");

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var actualResultObj = JsonConvert.DeserializeObject<ExceptionResponse>(actualResult);
            // Assert

            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedResult, actualResultObj?.Message);
        }

        /// <summary>
        /// Rapor durumunu güncelleme işleminden sonra status 200 ve response boş olmamalı
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Post_Completed_Report_Status_Ok_Response_Null()
        {
            var reportRequestId = await ReportRequest();
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange
            var completedReportRequest = new CompletedReportRequest()
            {
              Id = reportRequestId,
              ReportFilName="test-file",
              ReportPath="test-path"
               
            };
            var content = new StringContent(JsonConvert.SerializeObject(completedReportRequest), Encoding.UTF8, "application/json");


            // Act
            var response = await _client.PostAsync("/api/report/completed-report", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        #region private
        private async Task<Guid> ReportRequest()        {
           
            var response = await _client.PostAsync("/api/report/report-request", null);            
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Guid>(result);
        }
        #endregion
    }
}
