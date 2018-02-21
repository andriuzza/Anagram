using Anagrams.EFCF.Core.Models;
using Anagrams.Interfaces.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.Helpers
{
    public static class DtoWordHelper
    {
        public static Word ToEntity(this WordDto dto)
        {
            return new Word
            {
                Name = dto.Name
            };
        }
    }
}
