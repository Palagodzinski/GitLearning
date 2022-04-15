using Application.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Core.Services.Concrete
{

    public class ImageFormatValidator : IImageFormatValidator
    {
        public Task<string> ValidateFormat(string fileName)
        {
            //  \w - pasuje do znaku słownego (znak alfanumeryczny plus podkreślenie)
            //  \. - oznacza dosłowną kropkę a ukośnik jest znakiem ucieczki
            //  |  - to poprostu lub

            string pattern = @"(\w+)\.(jpg|jpeg|png|gif|tif|tiff|bmp|eps|raw)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(fileName);
            if (match.Success)
                return Task.FromResult("To jest format pliku graficznego");
            else
                return Task.FromResult("To nie jest format pliku graficznego");
        }
    }
}
