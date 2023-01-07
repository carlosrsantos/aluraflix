


using Aluraflix.Models;
using Xunit.Abstractions;

namespace Aluraflix.Tests.Models
{
    public class CategoryTests : IDisposable
    {
        public Category category { get; set; } = null!;

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

        public void ShouldErrorWhenColorIsInvalid()
        {
            var color = "pink";

            var exception = Assert.Throws<Exception>
            (
                () => new Category().Color = color
            );

            Assert.Equal("Formato de cor inválida. Tente seguindo o exemplo: #ffffff, #000000", exception.Message);
        }

        [Fact]
        public void ShouldErrorWhenTitleIsEmpty()
        {
            var title = "";

            var exception = Assert.Throws<Exception>
            (
                () => new Category().Title = title
            );

            Assert.Equal("O título da categoria é necessário.", exception.Message);
        }

        [Fact]
        public void ShouldErrorWhenTitleHasInvalidValue()
        {
            var title = "gKx9TpIHfE2LC4hZJpKzg1UeBK0gLQZ";

            var exception = Assert.Throws<Exception>
            (
                () => new Category().Title = title
            );

            Assert.Equal("O título deve conter entre 3 e 30 caracteres.", exception.Message);
        }
    }
}
