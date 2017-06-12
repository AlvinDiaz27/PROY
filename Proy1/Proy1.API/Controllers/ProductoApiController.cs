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
    public class ProductoApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public ProductoApiController()
        {

        }

        public ProductoApiController(IUnityOfWork unityOfWork)
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
            var Productos = _UnityOfWork.Productos.GetAll();

            if (Productos == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var ProductoDTO = new List<ProductoDTO>();

            foreach (var producto in Productos)
                ProductoDTO.Add(Mapper.Map<Producto, ProductoDTO>(producto));

            return Ok(ProductoDTO);
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
            var producto = _UnityOfWork.Productos.Get(id);

            if (producto == null)
                return NotFound();

            return Ok(Mapper.Map<Producto, ProductoDTO>(producto));
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
        public IHttpActionResult Update(int id, ProductoDTO productoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var productoInPersistence = _UnityOfWork.Productos.Get(id);
            if (productoInPersistence == null)
                return NotFound();

            Mapper.Map<ProductoDTO, Producto>(productoDTO, productoInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(productoDTO);
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
        public IHttpActionResult Create(ProductoDTO productoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var producto = Mapper.Map<ProductoDTO, Producto>(productoDTO);

            _UnityOfWork.Productos.Add(producto);
            _UnityOfWork.SaveChanges();

            productoDTO.ProductoId = producto.ProductoId;

            return Created(new Uri(Request.RequestUri + "/" + producto.ProductoId), productoDTO);
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

            var productoInDataBase = _UnityOfWork.Productos.Get(id);
            if (productoInDataBase == null)
                return NotFound();

            _UnityOfWork.Productos.Delete(productoInDataBase);
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