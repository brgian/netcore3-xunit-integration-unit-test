using NetCore.Template.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetCore.Template.Test.IntegrationTesting
{
    public class IntegrationTests : IClassFixture<IntegrationTestContext_Fixture>
    {
        private IntegrationTestContext_Fixture fixture;

        public IntegrationTests(IntegrationTestContext_Fixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task TestIntegrationTestContext()
        {
            var response = await fixture.Client.GetAsync(@"/api/alive");

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Entity1")]
        [InlineData("Entity2")]
        [InlineData("Entity3")]
        [InlineData("Entity4")]
        [InlineData("Entity5")]
        public async Task Create_Test(string value)
        {
            var newEntity = new MyEntityDto();
            newEntity.Value = value;

            var response = await fixture.Client.PostAsJsonAsync(@"/api/MyEntity", newEntity);
            var responseDto = JsonConvert.DeserializeObject<MyEntityDto>(await response.Content.ReadAsStringAsync());

            Assert.Equal(newEntity.Value, responseDto.Value);
        }

        [Fact]
        public async Task GetAll_Test()
        {
            var response = await fixture.Client.GetAsync(@"/api/MyEntity");
            var responseDtoList = JsonConvert.DeserializeObject<List<MyEntityDto>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(5, responseDtoList.Count);
        }
    }
}