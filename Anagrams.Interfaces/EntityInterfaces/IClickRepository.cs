using Anagrams.Interfaces.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams.Interfaces.EntityInterfaces
{
    public interface IClickRepository
    {

        void Update(IPClickDto model);

        IPClickDto GetEntity(string IP);

        void Add(string ip);

    }
}
