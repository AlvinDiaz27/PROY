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
    public class ComprobanteApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public ComprobanteApiController()
        {

        }

        public ComprobanteApiController(IUnityOfWork unityOfWork)
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
            var Comprobantes = _UnityOfWork.Comprobantes.GetAll();

            if (Comprobantes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var ComprobanteDTO = new List<FacturaDTO>();

            foreach (var comprobante in Comprobantes)
                ComprobanteDTO.Add(Mapper.Map<Comprobante, FacturaDTO>(comprobante));

            return Ok(ComprobanteDTO);
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
            var comprobante = _UnityOfWork.Comprobantes.Get(id);

            if (comprobante == null)
                return NotFound();

            return Ok(Mapper.Map<Comprobante, FacturaDTO>(comprobante));
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
        public IHttpActionResult Update(int id, FacturaDTO comprobanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var comprobanteInPersistence = _UnityOfWork.Comprobantes.Get(id);
            if (comprobanteInPersistence == null)
                return NotFound();

            Mapper.Map<FacturaDTO, Comprobante>(comprobanteDTO, comprobanteInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(comprobanteDTO);
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
        public IHttpActionResult Create(FacturaDTO comprobanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var comprobante = Mapper.Map<FacturaDTO, Comprobante>(comprobanteDTO);

            _UnityOfWork.Comprobantes.Add(comprobante);
            _UnityOfWork.SaveChanges();

            comprobanteDTO.ComprobanteId = comprobante.ComprobanteId;

            return Created(new Uri(Request.RequestUri + "/" + comprobante.ComprobanteId), comprobanteDTO);
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

            var comprobanteInDataBase = _UnityOfWork.Comprobantes.Get(id);
            if (comprobanteInDataBase == null)
                return NotFound();

            _UnityOfWork.Comprobantes.Delete(comprobanteInDataBase);
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