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
using Proy1_ENT.DTO;
using Proy1_Per;
using Proy1_ENT.IRepository;
using AutoMapper;

namespace Proy1.API.Controllers
{
    public class FacturaApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public FacturaApiController()
        {

        }

        public FacturaApiController(IUnityOfWork unityOfWork)
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
            var Facturas = _UnityOfWork.Facturas.GetAll();

            if (Facturas == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var FacturaDTO = new List<FacturaDTO>();

            foreach (var factura in Facturas)
                FacturaDTO.Add(Mapper.Map<Factura, FacturaDTO>(factura));

            return Ok(FacturaDTO);
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
            var factura = _UnityOfWork.Facturas.Get(id);

            if (factura == null)
                return NotFound();

            return Ok(Mapper.Map<Factura, FacturaDTO>(factura));
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
        public IHttpActionResult Update(int id, FacturaDTO facturaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var facturaInPersistence = _UnityOfWork.Facturas.Get(id);
            if (facturaInPersistence == null)
                return NotFound();

            Mapper.Map<FacturaDTO, Factura>(facturaDTO, facturaInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(facturaDTO);
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
        public IHttpActionResult Create(FacturaDTO facturaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var factura = Mapper.Map<FacturaDTO, Factura>(facturaDTO);

            _UnityOfWork.Facturas.Add(factura);
            _UnityOfWork.SaveChanges();

            facturaDTO.FacturaId = factura.FacturaId;

            return Created(new Uri(Request.RequestUri + "/" + factura.FacturaId), facturaDTO);
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

            var facturaInDataBase = _UnityOfWork.Facturas.Get(id);
            if (facturaInDataBase == null)
                return NotFound();

            _UnityOfWork.Facturas.Delete(facturaInDataBase);
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