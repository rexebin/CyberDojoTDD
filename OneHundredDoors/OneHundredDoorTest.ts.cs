using System;
using NUnit.Framework;

namespace CyberDojo.OneHundredDoors
{
    [TestFixture]
    public class OneHundredDoorsTest
    {
        private OneHundredDoors Sut { get; set; }

        [SetUp]
        public void CreateOneHundredDoors()
        {
            Sut = new OneHundredDoors();
        }

        [Test]
        public void ShouldHave100Doors()
        {
            Assert.AreEqual(100, Sut.Doors.Count);
        }

        [Test]
        public void DoorsShouldBeClosedInitially()
        {
            foreach (var sutDoor in Sut.Doors)
            {
                Assert.AreEqual(false, sutDoor);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(51)]
        [TestCase(100)]
        public void GivenTurn_ShouldToggleDoorNumberCapableOfBeingModulatedByGivenTurns(int turnCount)
        {
            Sut.Visit(turnCount);
            var index = 0;
            foreach (var door in Sut.Doors)
            {
                Assert.AreEqual((index++ + 1) % turnCount == 0, door);
            }
        }

        [Test]
        public void GivenTurn100Times_ShouldLeaveSquareNumberDoorsOpen()
        {
            for (var i = 1; i <= 100; i++)
            {
                Sut.Visit(i);
            }

            var index = 0;
            foreach (var door in Sut.Doors)
            {
                Assert.AreEqual(Math.Sqrt(index + 1) % 1 == 0, door);
                index++;
            }
        }
    }
}