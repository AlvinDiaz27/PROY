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
    public class TipoApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public TipoApiController()
        {

        }

        public TipoApiController(IUnityOfWork unityOfWork)
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
            var Tipos = _UnityOfWork.Tipos.GetAll();

            if (Tipos == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TipoDTO = new List<TipoDTO>();

            foreach (var tipo in Tipos)
                TipoDTO.Add(Mapper.Map<Tipo, TipoDTO>(tipo));

            return Ok(TipoDTO);
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
            var tipo = _UnityOfWork.Tipos.Get(id);

            if (tipo == null)
                return NotFound();

            return Ok(Mapper.Map<Tipo, TipoDTO>(tipo));
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
        public IHttpActionResult Update(int id, TipoDTO tipoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipoInPersistence = _UnityOfWork.Tipos.Get(id);
            if (tipoInPersistence == null)
                return NotFound();

            Mapper.Map<TipoDTO, Tipo>(tipoDTO, tipoInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(tipoDTO);
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
        public IHttpActionResult Create(TipoDTO tipoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipo = Mapper.Map<TipoDTO, Tipo>(tipoDTO);

            _UnityOfWork.Tipos.Add(tipo);
            _UnityOfWork.SaveChanges();

            tipoDTO.TipoId = tipo.TipoId;

            return Created(new Uri(Request.RequestUri + "/" + tipo.TipoId), tipoDTO);
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

            var tipoInDataBase = _UnityOfWork.Tipos.Get(id);
            if (tipoInDataBase == null)
                return NotFound();

            _UnityOfWork.Tipos.Delete(tipoInDataBase);
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