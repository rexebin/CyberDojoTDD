using System.Collections.Generic;
using NUnit.Framework;

namespace CyberDojo.BowlingGame
{
    [TestFixture]
    public class BowlingGameTest
    {
        private BowlingGame sut;

        [SetUp]
        public void Create()
        {
            sut = new BowlingGame();
        }

        [Test]
        [TestCase("--", new []{0, 0})]
        [TestCase("1-", new[]{1, 0})]
        [TestCase("11", new[]{1, 1})]
        [TestCase("-5", new[]{0, 5})]
        [TestCase("X", new[]{10})]
        [TestCase("XX", new[]{10, 10})]
        [TestCase("1/", new[]{1, 9})]
        [TestCase("5/", new[]{5, 5})]
        public void GivenFrameResult_ShouldReturnFrameScore(string frameResult, int[] score)
        {
            Assert.AreEqual(score, sut.GetFrameScore(frameResult));
        }

        [Test]
        [TestCase("X|X|X|X|X|X|X|X|X|X||XX", new []{"X","X","X","X","X","X","X","X","X","X","XX"})]
        [TestCase("9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||", new []{"9-","9-","9-","9-","9-","9-","9-","9-","9-","9-"})]
        [TestCase("5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5", new []{"5/","5/","5/","5/","5/","5/","5/","5/","5/","5/", "5"})]
        [TestCase("X|7/|9-|X|-8|8/|-6|X|X|X||81", new []{"X", "7/", "9-", "X", "-8", "8/", "-6", "X", "X", "X", "81"})]
        public void GivenTenFrameResult_ShouldReturnListOfFrames(string tenFrameResult, string[] expected)
        {
            Assert.AreEqual(expected,sut.GetFrameResults(tenFrameResult));
        }

        [Test]
        [TestCase("9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||", 90)]
        [TestCase("X|X|X|X|X|X|X|X|X|X||XX", 300)]
        [TestCase("5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5", 150)]
        [TestCase("X|7/|9-|X|-8|8/|-6|X|X|X||81", 167)]
        public void GivenArrayOfResults_ShouldReturnFinalScore(string results, int score)
        {
            Assert.AreEqual(score,sut.GetTotalCore(results));
        }
    }

   
}