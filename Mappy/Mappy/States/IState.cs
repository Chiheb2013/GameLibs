using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappy.States
{
    public interface IState : IGameObject
    {
        void Initialize();
        void Dispose();
    }
}
