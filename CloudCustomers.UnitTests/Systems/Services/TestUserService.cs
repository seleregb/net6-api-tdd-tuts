using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var mockHttpHandler = MockHttpMessageHandler<Users>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(mockHttpHandler.Object);
            var endPoint = "https://example.com/users";

             var config = Options.Create(new UsersApiOptions {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);
            // Act
            await sut.GetAllUsers();
            // Assert
           mockHttpHandler
            .Protected()
            .Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>()
               );
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var mockHttpHandler = MockHttpMessageHandler<Users>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(mockHttpHandler.Object);
            var endPoint = "https://example.com/users";

            var config = Options.Create(new UsersApiOptions {
                EndPoint = endPoint
            });

            var sut = new UsersService(httpClient, config);
            // Act
            var result = await sut.GetAllUsers();
            // Assert
          result.Should().BeOfType<List<Users>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var mockHttpHandler = MockHttpMessageHandler<Users>.SetupReturn404();
            var httpClient = new HttpClient(mockHttpHandler.Object);
            var endPoint = "https://example.com/users";

            var config = Options.Create(new UsersApiOptions {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);
            // Act
            var result = await sut.GetAllUsers();
            // Assert
          result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var mockHttpHandler = MockHttpMessageHandler<Users>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(mockHttpHandler.Object);
            var endPoint = "https://example.com/users";

            var config = Options.Create(new UsersApiOptions {
                EndPoint = endPoint
            });

            var sut = new UsersService(httpClient, config);
            // Act
            var result = await sut.GetAllUsers();
            // Assert
          result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var mockHttpHandler = MockHttpMessageHandler<Users>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(mockHttpHandler.Object);            
            var endPoint = "https://example.com/users";

            var config = Options.Create(new UsersApiOptions {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);
            // Act
            var result = await sut.GetAllUsers();
            // Assert
            mockHttpHandler
            .Protected()
            .Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get 
                && req.RequestUri.ToString() == endPoint),
               ItExpr.IsAny<CancellationToken>()
               );

        }
    }
}