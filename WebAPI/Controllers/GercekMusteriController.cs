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
    public class GercekMusteriController : ControllerBase
    {
        private IGercekMusteriService _gercekMusteriService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();//Bütün durumları tek bir yerde kullanmak için Her metod içerisinde yapmak yerine 

        public GercekMusteriController(IGercekMusteriService gercekMusteriService)
        {
            _gercekMusteriService = gercekMusteriService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _gercekMusteriService.GetList();
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int gercekMusteriId)
        {
            var result = _gercekMusteriService.GetById(gercekMusteriId);
            return _calismaDurumlari.CalismaDurumuByid(result);
        }

        [HttpPost("add")]
        public IActionResult AddRange(GercekMusteriler gercekMusteri)
        {
            var result = _gercekMusteriService.AddRange(gercekMusteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(GercekMusteriler gercekMusteri)
        {
            var result = _gercekMusteriService.Delete(gercekMusteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(GercekMusteriler gercekMusteri)
        {
            var result = _gercekMusteriService.Update(gercekMusteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }
    }
}
