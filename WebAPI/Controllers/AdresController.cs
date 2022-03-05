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
    public class AdresController : ControllerBase
    {
        private IAdresService _adresService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();

        public AdresController(IAdresService adresService)
        {
            _adresService = adresService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var resutl = _adresService.GetList();
            return _calismaDurumlari.CalismaDurumuList(resutl);
        }

        [HttpPost("add")]
        public IActionResult Add(Adres adres)
        {
            var result = _adresService.Add(adres);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Adres adres)
        {
            var result = _adresService.Delete(adres);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Adres adres)
        {
            var result = _adresService.Update(adres);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }
    }
}
