using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Abstract
{
    public interface IStringCreator
    {
        Task<string> CreateString();
        Task<string> WriteString();
    }
}
