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
    public class TarjetaApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public TarjetaApiController()
        {

        }

        public TarjetaApiController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET api/CategoriaApi
        /*public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias;
        }

        // GET api/CategoriaApi/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult GetCategoria(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        } */
        // GET api/GenresApi
        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var Tarjetas = _UnityOfWork.Tarjetas.GetAll();

            if (Tarjetas == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TarjetaDTO = new List<TarjetaDTO>();

            foreach (var tarjeta in Tarjetas)
                TarjetaDTO.Add(Mapper.Map<Tarjeta, TarjetaDTO>(tarjeta));

            return Ok(TarjetaDTO);
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
            var tarjeta = _UnityOfWork.Tarjetas.Get(id);

            if (tarjeta == null)
                return NotFound();

            return Ok(Mapper.Map<Tarjeta, TarjetaDTO>(tarjeta));
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
        public IHttpActionResult Update(int id, TarjetaDTO tarjetaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tarjetaInPersistence = _UnityOfWork.Tarjetas.Get(id);
            if (tarjetaInPersistence == null)
                return NotFound();

            Mapper.Map<TarjetaDTO, Tarjeta>(tarjetaDTO, tarjetaInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(tarjetaDTO);
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
        public IHttpActionResult Create(TarjetaDTO tarjetaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tarjeta = Mapper.Map<TarjetaDTO, Tarjeta>(tarjetaDTO);

            _UnityOfWork.Tarjetas.Add(tarjeta);
            _UnityOfWork.SaveChanges();

            tarjetaDTO.TarjetaId = tarjeta.TarjetaId;

            return Created(new Uri(Request.RequestUri + "/" + tarjeta.TarjetaId), tarjetaDTO);
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

            var tarjetaInDataBase = _UnityOfWork.Tarjetas.Get(id);
            if (tarjetaInDataBase == null)
                return NotFound();

            _UnityOfWork.Tarjetas.Delete(tarjetaInDataBase);
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