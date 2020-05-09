using System.Threading.Tasks;
using categorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace learning.tests
{
    [TestClass]
    public class Categorization
    {
        [TestMethod]
        public async Task Text_Returns_Valid_Skill()
        {
            // Arrange
            var settings = new Testing();
            var text = "add entry";
            var predictor = new Skill();

            // Act
            var result = await predictor.Predict(settings, text);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Entry", result.Value);
        }
    }
}