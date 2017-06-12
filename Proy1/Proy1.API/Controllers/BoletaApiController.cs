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
    public class BoletaApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();

        private readonly IUnityOfWork _UnityOfWork;

        public BoletaApiController()
        {

        }

        public BoletaApiController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: api/GenresApi
        //public IQueryable<Genre> GetGenres()
        //{
        //	return db.Genres;
        //}

        // GET api/GenresApi
        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var Boleta = _UnityOfWork.Boletas.GetAll();

            if (Boleta == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var BoletaDTO = new List<BoletaDTO>();

            foreach (var boleta in Boleta)
                BoletaDTO.Add(Mapper.Map<Boleta, BoletaDTO>(boleta));

            return Ok(BoletaDTO);
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
            var boleta = _UnityOfWork.Boletas.Get(id);

            if (boleta == null)
                return NotFound();

            return Ok(Mapper.Map<Boleta, BoletaDTO>(boleta));
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
        public IHttpActionResult Update(int id, BoletaDTO boletaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var boletaInPersistence = _UnityOfWork.Boletas.Get(id);
            if (boletaInPersistence == null)
                return NotFound();

            Mapper.Map<BoletaDTO, Boleta>(boletaDTO, boletaInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(boletaDTO);
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
        public IHttpActionResult Create(BoletaDTO boletaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var boleta = Mapper.Map<BoletaDTO, Boleta>(boletaDTO);

            _UnityOfWork.Boletas.Add(boleta);
            _UnityOfWork.SaveChanges();

            boletaDTO.BoletaId = boleta.BoletaId;

            return Created(new Uri(Request.RequestUri + "/" + boleta.BoletaId), boletaDTO);
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

            var boletaInDataBase = _UnityOfWork.Boletas.Get(id);
            if (boletaInDataBase == null)
                return NotFound();

            _UnityOfWork.Boletas.Delete(boletaInDataBase);
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