using Microsoft.AspNetCore.Mvc.Testing;

namespace TestProjekt_60134
{
	public class UnitTest1 : IClassFixture<WebApplicationFactory<Projekt_60134_ST3.Controllers.UsersController>>
	{
		private readonly WebApplicationFactory<Projekt_60134_ST3.Controllers.UsersController> _factory;
		private readonly HttpClient httpClient;

		public UnitTest1(WebApplicationFactory<Projekt_60134_ST3.Controllers.UsersController> factory)
		{
			_factory = factory;
			httpClient = _factory.CreateClient();
		}

		[Theory]
		[InlineData("/")]
		[InlineData("/Users/Add")]
		[InlineData("/Users/Index")]
		[InlineData("/Users/View")]

		public async Task TestIfAllPagesLoads(string URL)
		{
			//Arrane
			var client = _factory.CreateClient();
			//Act
			var response = await client.GetAsync(URL);
			int code = (int)response.StatusCode;
			//Assert
			Assert.Equal(200, code);
		}

		[Theory]
		[InlineData("Pierwszy")]
		public async Task TestForUserName(String Name)
		{
			//Arrange

			//Act
			var response = await httpClient.GetAsync("/Users/Index");
			var pageContent = await response.Content.ReadAsStringAsync();
			var contentString = pageContent.ToString();
			//Assert
			Assert.Contains(Name, contentString);

		}
	}
}