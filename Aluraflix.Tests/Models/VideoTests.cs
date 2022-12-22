
using Aluraflix.Models;
using Xunit.Abstractions;

namespace Aluraflix.Tests.Models
{
    public class VideoTests : IDisposable
    {
        public Video video;

        public ITestOutputHelper testOutputHelper;

        public VideoTests(ITestOutputHelper _testOutputHelper)
        {
            testOutputHelper = _testOutputHelper;
        }


        public void Dispose()
        {
            testOutputHelper.WriteLine("Dispose Invocado");
        }

        [Fact]
        public void ShouldErrorWhenTitleIsEmpty()
        {   
            //Arrange
            string title = "";

            var exception = Assert.Throws<Exception>
            (
                //Act
                () => new Video().Title = title

            );

            //Assert
            Assert.Equal("O título do vídeo é necessário.",exception.Message);
        }
        
        [Fact]
        public void ShouldErrorWhenDescriptionIsNull()
        {
            string description = "";

            var exception = Assert.Throws<Exception>
            (
                //Act
                () => new Video().Description = description

            );

            Assert.Equal("A descrição do vídeo é necessária.", exception.Message);
        }


        [Fact]
        public void ShouldErrorWhenTitleHasInvalidTitle()
        {
            string title = "Oz";

            var exception = Assert.Throws<Exception>
            (
                //Act
                () => new Video().Title = title
            );

            Assert.Equal("O título deve conter entre 3 e 30 caracteres.",exception.Message);
        }

        [Fact]
        public void ShouldErrorWhenUrlIsInvalid()
        {
            string url = "wwwtestecom";

            var exception = Assert.Throws<Exception>
            (
                //Act
                () => new Video().Url = url
            );

            Assert.Equal("É preciso informar uma url válida.Ex.http://meusite.com", exception.Message);

        }
    }
}