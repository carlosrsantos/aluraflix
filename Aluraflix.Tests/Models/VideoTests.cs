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
        public void Test_Video_Title_Is_Empty()
        {
            string title = "";

            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>
            (
                //Act
                () => new Video().Title = title

            );

        }
        
        [Fact]
        public void Test_Video_Description_Is_Empty()
        {
            string description = "";

            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>
            (
                //Act
                () => new Video().Description = description

            );

        }


        [Fact]
        public void Test_Video_Title_Is_Shorter_Than_3_Letters()
        {
            string title = "Oz";

            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>
            (
                //Act
                () => new Video().Title = title
            );

        }

        [Fact]
        public void Test_Video_Url_Is_Invalid()
        {
            string url = "wwwtestecom";

            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>
            (
                //Act
                () => new Video().Url = url
            );

        }
    }
}