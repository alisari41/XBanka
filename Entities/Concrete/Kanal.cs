using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Kanal : IEntity
    {
        public int  Id { get; set; }
        public string Ad { get; set; }
    }
}
