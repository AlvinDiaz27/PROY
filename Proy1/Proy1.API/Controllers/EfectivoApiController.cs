using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Proy1_ENT.Entities;
using Proy1_Per;
using Proy1_ENT.IRepository;
using Proy1_ENT.DTO;
using AutoMapper;

namespace Proy1.API.Controllers
{
    public class EfectivoApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public EfectivoApiController()
        {

        }

        public EfectivoApiController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        // GET api/ComprobanteApi
        //public IQueryable<Comprobante> GetComprobantes()
        //{
        //  return db.Comprobantes;
        //}
        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var Efectivo = _UnityOfWork.Efectivos.GetAll();

            if (Efectivo == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var EfectivoDTO = new List<EfectivoDTO>();

            foreach (var efectivo in Efectivo)
                EfectivoDTO.Add(Mapper.Map<Efectivo, EfectivoDTO>(efectivo));

            return Ok(EfectivoDTO);
        }

        // GET: api/GenresApi/5
        //[ResponseType(typeof(Genre))]
        //public IHttpActionResult GetGenre(int id)
        //{
        //	Genre genre = db.Genres.Find(id);
        //	if (genre == null)
        //	{
        //		return NotFound();
        //	}

        //	return Ok(genre);
        //}

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var efectivo = _UnityOfWork.Efectivos.Get(id);

            if (efectivo == null)
                return NotFound();

            return Ok(Mapper.Map<Efectivo, EfectivoDTO>(efectivo));
        }

        // PUT: api/GenresApi/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutGenre(int id, Genre genre)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return BadRequest(ModelState);
        //	}

        //	if (id != genre.GenreId)
        //	{
        //		return BadRequest();
        //	}

        //	db.Entry(genre).State = EntityState.Modified;

        //	try
        //	{
        //		db.SaveChanges();
        //	}
        //	catch (DbUpdateConcurrencyException)
        //	{
        //		if (!GenreExists(id))
        //		{
        //			return NotFound();
        //		}
        //		else
        //		{
        //			throw;
        //		}
        //	}

        //	return StatusCode(HttpStatusCode.NoContent);
        //}
        [HttpPut]
        public IHttpActionResult Update(int id, EfectivoDTO efectivoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var efectivoInPersistence = _UnityOfWork.Efectivos.Get(id);
            if (efectivoInPersistence == null)
                return NotFound();

            Mapper.Map<EfectivoDTO, Efectivo>(efectivoDTO, efectivoInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(efectivoDTO);
        }


        // POST: api/GenresApi
        //[ResponseType(typeof(Genre))]
        //public IHttpActionResult PostGenre(Genre genre)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return BadRequest(ModelState);
        //	}

        //	db.Genres.Add(genre);
        //	db.SaveChanges();

        //	return CreatedAtRoute("DefaultApi", new { id = genre.GenreId }, genre);
        //}
        [HttpPost]
        public IHttpActionResult Create(EfectivoDTO efectivoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var efectivo = Mapper.Map<EfectivoDTO, Efectivo>(efectivoDTO);

            _UnityOfWork.Efectivos.Add(efectivo);
            _UnityOfWork.SaveChanges();

            efectivoDTO.EfectivoId = efectivo.EfectivoId;

            return Created(new Uri(Request.RequestUri + "/" + efectivo.EfectivoId), efectivoDTO);
        }

        // DELETE: api/GenresApi/5
        //[ResponseType(typeof(Genre))]
        //public IHttpActionResult DeleteGenre(int id)
        //{
        //	Genre genre = db.Genres.Find(id);
        //	if (genre == null)
        //	{
        //		return NotFound();
        //	}

        //	db.Genres.Remove(genre);
        //	db.SaveChanges();

        //	return Ok(genre);
        //}
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var efectivoInDataBase = _UnityOfWork.Efectivos.Get(id);
            if (efectivoInDataBase == null)
                return NotFound();

            _UnityOfWork.Efectivos.Delete(efectivoInDataBase);
            _UnityOfWork.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}