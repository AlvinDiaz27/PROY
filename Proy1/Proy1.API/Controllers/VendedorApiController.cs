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
    public class VendedorApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public VendedorApiController()
        {

        }

        public VendedorApiController(IUnityOfWork unityOfWork)
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
            var Vendedor = _UnityOfWork.Vendedores.GetAll();

            if (Vendedor == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var VendedorDTO = new List<VendedorDTO>();

            foreach (var vendedor in Vendedor)
                VendedorDTO.Add(Mapper.Map<Vendedor, VendedorDTO>(vendedor));

            return Ok(VendedorDTO);
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
            var vendedor = _UnityOfWork.Vendedores.Get(id);

            if (vendedor == null)
                return NotFound();

            return Ok(Mapper.Map<Vendedor, VendedorDTO>(vendedor));
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
        public IHttpActionResult Update(int id, VendedorDTO vendedorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var vendedorInPersistence = _UnityOfWork.Vendedores.Get(id);
            if (vendedorInPersistence == null)
                return NotFound();

            Mapper.Map<VendedorDTO, Vendedor>(vendedorDTO, vendedorInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(vendedorDTO);
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
        public IHttpActionResult Create(VendedorDTO vendedorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var vendedor = Mapper.Map<VendedorDTO, Vendedor>(vendedorDTO);

            _UnityOfWork.Vendedores.Add(vendedor);
            _UnityOfWork.SaveChanges();

            vendedorDTO.VendedorId = vendedor.VendedorId;

            return Created(new Uri(Request.RequestUri + "/" + vendedor.VendedorId), vendedorDTO);
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

            var vendedorInDataBase = _UnityOfWork.Vendedores.Get(id);
            if (vendedorInDataBase == null)
                return NotFound();

            _UnityOfWork.Vendedores.Delete(vendedorInDataBase);
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