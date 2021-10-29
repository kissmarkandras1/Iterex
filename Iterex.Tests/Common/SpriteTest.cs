using Iterex.Common;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace Iterex.Tests.Common
{
    [TestFixture]
    public class SpriteTest
    {
        private Sprite _testingSprite;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Code here will be run once before all tests.
            var mockTexture = new Mock<ITextureAdapter>();
            mockTexture.SetupGet(self => self.FrameWidth).Returns(10);
            mockTexture.SetupGet(self => self.FrameHeight).Returns(10);
            mockTexture.SetupGet(self => self.Name).Returns("test");
            mockTexture.SetupGet(self => self.ImageBox).Returns(new Rectangle(3, 3, 10, 10));

            _testingSprite = new Sprite(mockTexture.Object)
            {
                Position = new Vector2(1, 1),
                IsSolid = true
            };
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Code here will be run once after all tests.
        }

        [SetUp]
        public void SetUp()
        {
            // Code here will be run once before every test.
        }

        [TearDown]
        public void TearDown()
        {
            // Code here will be run once after every test.
        }

        [Test]
        public void TextureBox_Normal_ReturnsCorrectRectangle()
        {
            Assert.AreEqual(_testingSprite.TextureBox.X, 1);
            Assert.AreEqual(_testingSprite.TextureBox.Y, 1);
            Assert.AreEqual(_testingSprite.TextureBox.Width, 10);
            Assert.AreEqual(_testingSprite.TextureBox.Height, 10);
        }

        [Test]
        public void CollisionBox_Normal_ReturnsCorrectRectangle()
        {
            Assert.AreEqual(_testingSprite.CollisionBox.X, 4);
            Assert.AreEqual(_testingSprite.CollisionBox.Y, 4);
            Assert.AreEqual(_testingSprite.CollisionBox.Width, 10);
            Assert.AreEqual(_testingSprite.CollisionBox.Height, 10);
        }
    }
}