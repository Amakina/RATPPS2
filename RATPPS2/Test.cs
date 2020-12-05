using NUnit.Framework;
using RATPPS1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RATPPS2
{
    [TestFixture]
    public class Test
    {

        private Input expectedInput = new Input()
        {
            K = 10,
            Sums = new[] { 1.01m, 2.02m },
            Muls = new[] { 1, 4 }
        };

        [Test]
        public async Task getPing()
        {

            var client = new Client("http://localhost:8080");

            var result = await client.Ping();

            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task getInput()
        {
            var client = new Client("http://localhost:8080");

            var result = await client.GetInputData();

            Assert.AreEqual(expectedInput, result);
        }

        [Test]
        public async Task sendOutput()
        {
            var client = new Client("http://localhost:8080");

            var output = new Output(expectedInput);

            var result = await client.WriteAnswer(output);

            Assert.AreEqual(true, result);
        }
    }
}
