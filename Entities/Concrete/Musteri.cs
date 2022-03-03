using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
   public class Musteri : IEntity
    {
        public int Id { get; set; }
        public string MusteriNo { get; set; }
        public int  KanalId { get; set; }
    }
}
