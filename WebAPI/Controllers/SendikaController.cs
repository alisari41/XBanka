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
    public class SendikaController : ControllerBase
    {
        private ISendikaService _sendikaService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();

        public SendikaController(ISendikaService sendikaService)
        {
            _sendikaService = sendikaService;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int sendikaId)
        {
            var result = _sendikaService.GetById(sendikaId);
            return _calismaDurumlari.CalismaDurumuByid(result);
        }

        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _sendikaService.GetList();
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        
        [HttpPost("add")]
        public IActionResult Add(Sendika sendika)
        {
            var result = _sendikaService.Add(sendika);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")] //HttpPost kullanılır
        public IActionResult Delete(Sendika sendika)
        {
            var result = _sendikaService.Delete(sendika);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Sendika sendika)
        {
            var result = _sendikaService.Update(sendika);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }
    }
}
