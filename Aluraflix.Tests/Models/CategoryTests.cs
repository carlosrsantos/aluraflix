

using Aluraflix.Models;
using Xunit.Abstractions;

namespace Aluraflix.Tests.Models
{
    public class CategoryTests : IDisposable
    {
        public Category category { get; set; }

        public ITestOutputHelper testOutputHelper;

        public CategoryTests(ITestOutputHelper _testOutputHelper)
        {
            testOutputHelper = _testOutputHelper;
        }


        public void Dispose()
        {
            testOutputHelper.WriteLine("Dispose Invocado");
        }

        [Fact]

        public void Test_Category_Color_Is_Invalid()
        {
            var color = "black";

            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>
            (
                () => new Category().Color = color
            );
        }
    }
}
