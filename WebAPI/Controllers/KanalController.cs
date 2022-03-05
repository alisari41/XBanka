using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using WebAPI.CalismaDurumu;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KanalController : ControllerBase
    {
        private IKanalService _kanalService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();//Bütün durumları tek bir yerde kullanmak için Her metod içerisinde yapmak yerine tek bir yerde tanımladım

        public KanalController(IKanalService kanalService)
        {
            _kanalService = kanalService;
        }
       

        [HttpGet("getbyid")]
        public IActionResult GetById(int kanalId)
        {
            var result = _kanalService.GetById(kanalId);
            return _calismaDurumlari.CalismaDurumuByid(result);
        }

        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _kanalService.GetList();
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        [HttpGet("getmusterikanallist")]
        public List<Kanal> GetMusteriKanalList(Kanal kanal)
        {
            var result = _kanalService.GetMusteriKanalList(kanal);
            return result;
        }


        [HttpPost("add")]
        public IActionResult Add(Kanal kanal)
        {
            var result = _kanalService.Add(kanal);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")] //HttpPost kullanılır
        public IActionResult Delete(Kanal kanal)
        {
            var result = _kanalService.Delete(kanal);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Kanal kanal)
        {
            var result = _kanalService.Update(kanal);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

    }
}
