using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class TicariFaaliyetler : IEntity
    {
        public int Id { get; set; }
        public int  GercekMusteriId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
    }
}
