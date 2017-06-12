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
using Proy1_ENT.Entities.DTO;
using Proy1_ENT.IRepository;

using AutoMapper;

namespace Proy1.API.Controllers
{
    public class ClienteApiController : ApiController
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public ClienteApiController()
        {

        }

        public ClienteApiController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET api/ClienteApi
        //public IQueryable<Cliente> GetPersonas()
        //{
        //   return db.Personas;
        // }
        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var Clientes = _UnityOfWork.Clientes.GetAll();

            if (Clientes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var ClienteDTO = new List<ClienteDTO>();

            foreach (var cliente in Clientes)
                ClienteDTO.Add(Mapper.Map<Cliente, ClienteDTO>(cliente));

            return Ok(ClienteDTO);
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
            var cliente = _UnityOfWork.Clientes.Get(id);

            if (cliente == null)
                return NotFound();

            return Ok(Mapper.Map<Cliente, ClienteDTO>(cliente));
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
        public IHttpActionResult Update(int id, ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var clienteInPersistence = _UnityOfWork.Clientes.Get(id);
            if (clienteInPersistence == null)
                return NotFound();

            Mapper.Map<ClienteDTO, Cliente>(clienteDTO, clienteInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(clienteDTO);
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
        public IHttpActionResult Create(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cliente = Mapper.Map<ClienteDTO, ClienteDTO>(clienteDTO);

            _UnityOfWork.Clientes.Add(cliente);
            _UnityOfWork.SaveChanges();

            clienteDTO.ClienteId = cliente.ClienteId;

            return Created(new Uri(Request.RequestUri + "/" + cliente.ClienteId), clienteDTO);
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

            var clienteInDataBase = _UnityOfWork.Clientes.Get(id);
            if (clienteInDataBase == null)
                return NotFound();

            _UnityOfWork.Clientes.Delete(clienteInDataBase);
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