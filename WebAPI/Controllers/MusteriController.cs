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
    public class MusteriController : ControllerBase
    {
        private IMusteriService _musteriService;
        private CalismaDurumlari _calismaDurumlari=new CalismaDurumlari();//Bütün durumları tek bir yerde kullanmak için Her metod içerisinde yapmak yerine tek bir yerde tanımladım
        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }


        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _musteriService.GetList();
            //if (result.Success)
            //{//Eğer doğru çalıştıysa datayı getir
            //    return Ok(result.Data);
            //}

            //return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
            //Yukardaki kod bloğunu sürekli kullandığım için metod içine taşıdım.
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        [HttpGet("getlisybykanal")]
        public IActionResult GetListByKanal(int kanalId)
        {
            var result = _musteriService.GetListByKanal(kanalId);
            return _calismaDurumlari.CalismaDurumuList(result);
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int musteriId)
        {
            var result = _musteriService.GetById(musteriId);
            //return CalismaDurumuByid(result);
            return _calismaDurumlari.CalismaDurumuByid(result);
        }
        

        [HttpPost("add")]
        public IActionResult Add(Musteri musteri)
        {
            var result = _musteriService.Add(musteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")] //HttpPost kullanılır
        public IActionResult Delete(Musteri musteri)
        {
            var result = _musteriService.Delete(musteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Musteri musteri)
        {
            var result = _musteriService.Update(musteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("transaction")]
        public IActionResult TransactionTest(Musteri musteri)
        {
            var result = _musteriService.TransactionalOperation(musteri);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }



    }
}
