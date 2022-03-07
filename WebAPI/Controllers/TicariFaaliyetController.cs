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
    public class TicariFaaliyetController : ControllerBase
    {
        private ITicariFaliyetService _ticariFaliyetService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();//Bütün durumları tek bir yerde kullanmak için Her metod içerisinde yapmak yerine 

        public TicariFaaliyetController(ITicariFaliyetService ticariFaliyetService)
        {
            _ticariFaliyetService = ticariFaliyetService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _ticariFaliyetService.GetList();
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int ticariFaaliyetId)
        {
            var result = _ticariFaliyetService.GetById(ticariFaaliyetId);
            return _calismaDurumlari.CalismaDurumuByid(result);
        }

        [HttpPost("add")]
        public IActionResult Add(TicariFaaliyetler ticariFaaliyetler)
        {
            var result = _ticariFaliyetService.Add(ticariFaaliyetler);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(TicariFaaliyetler ticariFaaliyetler)
        {
            var result = _ticariFaliyetService.Delete(ticariFaaliyetler);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(TicariFaaliyetler ticariFaaliyetler)
        {
            var result = _ticariFaliyetService.Update(ticariFaaliyetler);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }
    }
}
