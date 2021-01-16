using NUnit.Framework;
using Finder.Business.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Finder.UnitTest
{
    public class Tests
    {
        private WordFinder board;
        [SetUp]
        public void Setup()
        {
            string[] list = { "abcgcw", "fgwioi", "chilln", "pqnsdd", "uvdxya", };
            board = new WordFinder(list);
            
        }

        [Test]
        public void Can_Find_One_Word()
        {
            var word = "cold";
            var retval = board.Find(new[] { word });
            Assert.AreEqual(1, retval.Count());
        }

        [Test]
        public void Can_Find_Multiple_Word()
        {
            var retval = board.Find(new[] { "cold", "wind", "snow", "chill" });
            Assert.AreEqual(3, retval.Count());
        }

        [Test]
        public void Can_not_Find_Multiple_Word_Empty()
        {
            var retval = board.Find(new[] { "", " " });
            Assert.AreEqual(0, retval.Count());
        }

        [Test]
        public void Can_Not_Find_Because_Does_Not_Exist()
        {
            var retval = board.Find(new[] { "fear", "solar" });
            Assert.AreEqual(0, retval.Count());
        }

        [Test]
        public void Throw_ArgumentNullException_Wrong_Input()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _board = new WordFinder(null);
            });
        }

        [Test]
        public void Throw_ArgumentException_Wrong_Input()
        {
            var list = new List<string>();
            for (int i = 0; i < 65; i++)
            {
                list.Add($"{i}");
            }
            Assert.Throws<ArgumentException>(() =>
            {
                var _board = new WordFinder(list);
            });
        }
    }
}