using FluentAssertions;
using TechCheck.WebAPI.Utils;

namespace TechCheck.UnitTests.Tests
{
    public class StringHelperTests
    {
        [Fact]
        public void WhenNullInput_ThenReturnEmptyList()
        {
            var result = StringHelper.CutUpString(null);

            Assert.Empty(result);
        }

        [Fact]
        public void WhenOneWord_ThenReturnListWithOneItem()
        {
            var result = StringHelper.CutUpString("hello");

            result.Should().ContainSingle("hello");
        }

        [Fact]
        public void WhenTwoWords_ThenReturnListWithTwoItems()
        {
            var result = StringHelper.CutUpString("hello world");

            result.Should().BeEquivalentTo(["hello", "world"]);
        }
    }
}
