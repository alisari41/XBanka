using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMusteriService
    {
        //Arayüz tarafında metod paremetresi sade olmalı
        IDataResult<Musteri> GetById(int musteriId);//Data başarlımı oldu başarısız mı onlarada bakıcam IDataResult ile
        IDataResult<List<Musteri>> GetList();
        IDataResult<List<Musteri>> GetListByKanal(int kanalId);//Kanallara göre ürünleri getir. Yani internet  mi şube mi kayıt yeri
        IResult Add(Musteri musteri);// Data döndürmek istemiyorum.Başarılı mı oldum başarısız mı onlara bakmak istiyorum.
        IResult Delete(Musteri musteri);
        IResult Update(Musteri musteri);

        IResult TransactionalOperation(Musteri musteri);//Bütün metodlar çalışıyorsa başarılı biri çalışmıyorsa başarısız sayılır diğer işlemler geri çekilir
    }
}
