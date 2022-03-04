using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.CalismaDurumu
{
    public class CalismaDurumlari :ControllerBase
    {
        // GetListByKanal ve GetList için
        public IActionResult CalismaDurumuList<T>(IDataResult<List<T>> result)
        {
            if (result.Success)
            {//Eğer doğru çalıştıysa datayı getir
                return Ok(result.Data);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        //GetById için
        public IActionResult CalismaDurumuByid<T>(IDataResult<T> result)
        {
            if (result.Success)
            {//Eğer doğru çalıştıysa datayı getir
                return Ok(result.Data);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }
        
        // Add, Delete, Update, TransactionTest metodları için
        public IActionResult CalismaDurumuCRUD(IResult result)
        {
            if (result.Success)
            {//Eğer doğru çalıştıysa datayı getir
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }
    }
}
