using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using WebAPI.CalismaDurumu;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuzelMusteriController : ControllerBase
    {
        private ITuzelMusteriService _tuzelMusteriService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();//Bütün durumları tek bir yerde kullanmak için Her metod içerisinde yapmak yerine tek bir yerde tanımladım

        public TuzelMusteriController(ITuzelMusteriService tuzelMusteriService)
        {
            _tuzelMusteriService = tuzelMusteriService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _tuzelMusteriService.GetList();
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int tuzelMusteriId)
        {
            var result = _tuzelMusteriService.GetById(tuzelMusteriId);
            return _calismaDurumlari.CalismaDurumuByid(result);
        }

        [HttpPost("add")]
        public IActionResult Add(TuzelMusteriler tuzelMusteri)
        {
            var result = _tuzelMusteriService.Add(tuzelMusteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(TuzelMusteriler tuzelMusteri)
        {
            var result = _tuzelMusteriService.Delete(tuzelMusteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(TuzelMusteriler tuzelMusteri)
        {
            var result = _tuzelMusteriService.Update(tuzelMusteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("transaction")]
        public IActionResult Transaction(TuzelMusteriler tuzelMusteri)
        {
            var result = _tuzelMusteriService.TransactionalOperation(tuzelMusteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }
    }
}
