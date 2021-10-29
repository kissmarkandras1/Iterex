using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;

namespace Iterex.Tests.Entity
{
    [TestFixture]
    public class EntityTest
    {
        private Iterex.Entity.Entity _testingEntity1;
        private Iterex.Entity.Entity _testingEntity2;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Code here will be run once before all tests.

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
            var mockTexture1 = new Mock<ITextureAdapter>();
            mockTexture1.SetupGet(self => self.FrameWidth).Returns(10);
            mockTexture1.SetupGet(self => self.FrameHeight).Returns(10);
            mockTexture1.SetupGet(self => self.Name).Returns("test1");
            mockTexture1.SetupGet(self => self.ImageBox).Returns(new Rectangle(3, 3, 10, 10));

            var mockTexture2 = new Mock<ITextureAdapter>();
            mockTexture2.SetupGet(self => self.FrameWidth).Returns(10);
            mockTexture2.SetupGet(self => self.FrameHeight).Returns(10);
            mockTexture2.SetupGet(self => self.Name).Returns("test2");
            mockTexture2.SetupGet(self => self.ImageBox).Returns(new Rectangle(3, 3, 5, 5));

            _testingEntity1 = new Iterex.Entity.Entity(mockTexture1.Object)
            {
                Random = new Random(),
                Position = new Vector2(1, 1),
                IsSolid = true,
                Attributes = new Iterex.Entity.EntityAttributes()
                {
                    MaxHP = 100,
                    MaxMP = 100,
                    HP = 100,
                    MP = 100,
                    Damage = 20,
                    DodgeChance = 20,
                    Team = 1,
                    Speed = 70f
                }
            };

            _testingEntity2 = new Iterex.Entity.Entity(mockTexture2.Object)
            {
                Random = new Random(),
                Position = new Vector2(2, 2),
                IsSolid = true,
                Attributes = new Iterex.Entity.EntityAttributes()
                {
                    MaxHP = 70,
                    MaxMP = 70,
                    HP = 70,
                    MP = 70,
                    Damage = 30,
                    DodgeChance = 20,
                    Team = 2,
                    Speed = 70f
                }
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Code here will be run once after every test.
        }

        [Test]
        public void Dodge_Dodged_ReturnsTrue()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(self => self.Next(1, 100)).Returns(10);

            _testingEntity1.Random = mockRandom.Object;

            Assert.IsTrue(_testingEntity1.Dodge());
        }

        [Test]
        public void Dodge_DodgedUnsuccesfully_ReturnsFalse()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(self => self.Next(1, 100)).Returns(50);

            _testingEntity1.Random = mockRandom.Object;

            Assert.IsFalse(_testingEntity1.Dodge());
        }

        [Test]
        public void ReceiveDamage_Dodged_NoDamageReceived()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(self => self.Next(1, 100)).Returns(10);

            _testingEntity1.Random = mockRandom.Object;
            _testingEntity1.ReceiveDamage(10);

            Assert.AreEqual(100, _testingEntity1.Attributes.HP);
        }

        [Test]
        public void ReceiveDamage_Damaged_DamageReceived()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(self => self.Next(1, 100)).Returns(50);

            _testingEntity1.Random = mockRandom.Object;
            _testingEntity1.ReceiveDamage(10);

            Assert.AreEqual(90, _testingEntity1.Attributes.HP);
        }

        [Test]
        public void DealDamage_TargetDodged_NoDamageDealed()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(self => self.Next(1, 100)).Returns(10);

            _testingEntity2.Random = mockRandom.Object;
            _testingEntity1.DealDamage(_testingEntity2);

            Assert.AreEqual(70, _testingEntity2.Attributes.HP);
        }

        [Test]
        public void DealDamage_DealedDamageSuccessfully_TargetReceivedDamage()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(self => self.Next(1, 100)).Returns(50);

            _testingEntity2.Random = mockRandom.Object;
            _testingEntity1.DealDamage(_testingEntity2);

            Assert.AreEqual(50, _testingEntity2.Attributes.HP);
        }

        [Test]
        public void ReceiveHeal_HPBelowMaximum_ReceivedAll()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(self => self.Next(1, 100)).Returns(50);

            _testingEntity1.Random = mockRandom.Object;
            _testingEntity2.DealDamage(_testingEntity1);
            _testingEntity1.ReceiveHeal(10);

            Assert.AreEqual(80, _testingEntity1.Attributes.HP);
        }

        [Test]
        public void ReceiveHeal_HPExceedMaximum_ReceivedPartial()
        {
            _testingEntity1.ReceiveHeal(10);

            Assert.AreEqual(100, _testingEntity1.Attributes.HP);
        }
      
    }
}