using System;
using System.Threading.Tasks;
using Brreg.Controllers;
using Brreg.Http.Abstractions;
using Brreg.Http.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Brreg.UnitTests.Controllers
{
    public class OrganizationControllerTests
    {
        private AutoMocker _mocker = new AutoMocker();
        
        [Fact]
        public async Task When_Input_is_valid_Should_call_IBrregHttpClient_once()
        {
            var sut = _mocker.CreateInstance<OrganizationController>();
            
            await sut.Index("919300388");

            _mocker.Verify<IBrregHttpClient, Task<OrganizationModel>>(p => p.FetchOrganizationAsync("919300388"), Times.Once);
        }
        
        [Fact]
        public async Task When_Input_is_valid_Should_return_OkResultObject()
        {
            _mocker.Setup<IBrregHttpClient, Task<OrganizationModel>>(p => p.FetchOrganizationAsync("919300388"))
                .ReturnsAsync(new OrganizationModel
                {
                    Navn = "NOVO SOLUTIONS AS",
                    Organisasjonsnummer = "919300388"
                });
            
            var sut = _mocker.CreateInstance<OrganizationController>();
            var result = await sut.Index("919300388");
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task When_org_not_found_Should_return_404()
        {
            var sut = _mocker.CreateInstance<OrganizationController>();
            var result = await sut.Index("919300388");
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public async Task When_Input_is_valid_Should_return_name_and_orgnumber()
        {
            var sut = _mocker.CreateInstance<OrganizationController>();
            
            _mocker.Setup<IBrregHttpClient, Task<OrganizationModel>>(p => p.FetchOrganizationAsync("919300388"))
                .ReturnsAsync(new OrganizationModel
                {
                    Navn = "NOVO SOLUTIONS AS",
                    Organisasjonsnummer = "919300388"
                });
            
            var result = (OkObjectResult)await sut.Index("919300388");

            var type = result.Value.GetType();

            var nameProp = type.GetProperty("Name");
            var orgNumberProp = type.GetProperty("OrganizationNumber");
            
            Assert.NotNull(nameProp);
            Assert.NotNull(orgNumberProp);

            var name = nameProp.GetValue(result.Value);
            var org = orgNumberProp.GetValue(result.Value);
            
            Assert.Equal("NOVO SOLUTIONS AS", name.ToString());
            Assert.Equal("919300388", org.ToString());
            
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Theory]
        [InlineData("abc")]
        [InlineData("9193003888")]
        [InlineData("91930038")]
        [InlineData("abc123456")]
        [InlineData("abcdefghi")]
        public async Task When_input_is_invalid_Should_return_BadRequest(string orgNumber)
        {
            var sut = _mocker.CreateInstance<OrganizationController>();
            var result = await sut.Index(orgNumber);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}