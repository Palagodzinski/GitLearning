using Application.Core.Services.Abstract;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Concrete
{
    [MemoryDiagnoser]
    public class StringCreator : IStringCreator
    {
        [Benchmark]
        public Task<string> CreateString()
        {
            var chars = "abcdefghijklmnoprstuwxyz";
            var sb = new StringBuilder();

            for (var i = 0; i < 500; i++)
            {
                sb.Append(chars.ToUpper()[1]);
            }

            return Task.FromResult(sb.ToString());
        }

        [Benchmark]
        public Task<string> WriteString()
        {
            var chars = "abcdefghijklmnoprstuwxyz";
            // var sb = new StringBuilder();
            string test = "";
            for (var i = 0; i < 500; i++)
            {
                test.Append(chars.ToUpper()[1]);

            }

            return Task.FromResult(test);
        }
    }
}
