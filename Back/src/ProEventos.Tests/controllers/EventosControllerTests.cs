using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Controllers;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using System.Collections.Generic;
using ProEventos.Persistence.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ProEventos.Api.Helpers;


namespace ProEventos.Tests.controllers
{
    public class EventosControllerTests
    {
        private readonly EventosController _controller;
        private readonly Mock<IEventoService> _eventoServiceMock = new();
        private readonly Mock<IUtil> _utilMock = new();
        private readonly Mock<IAccountService> _accountServiceMock = new();

        public EventosControllerTests()
        {
            _controller = new EventosController(
                _eventoServiceMock.Object,
                _utilMock.Object,
                _accountServiceMock.Object
            );

            // Simula um usuário logado
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("uid", "1")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenEventosExistem()
        {
            // Arrange
            var eventos = new PageList<EventoDto>(
                new List<EventoDto> { new EventoDto { Id = 1, Tema = "Teste" } },
                1, 1, 1
            );

            _eventoServiceMock
                .Setup(s => s.GetAllEventosAsync(It.IsAny<int>(), It.IsAny<PageParams>(), true))
                .ReturnsAsync(eventos);

            // Act
            var result = await _controller.Get(new PageParams());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<PageList<EventoDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task Get_ReturnsNoContent_WhenNenhumEventoExiste()
        {
            // Arrange
            _eventoServiceMock
                .Setup(s => s.GetAllEventosAsync(It.IsAny<int>(), It.IsAny<PageParams>(), true))
                .ReturnsAsync((PageList<EventoDto>)null!);

            // Act
            var result = await _controller.Get(new PageParams());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}