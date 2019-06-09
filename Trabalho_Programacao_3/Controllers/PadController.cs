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
using Trabalho_Programacao_3.Models;

namespace Trabalho_Programacao_3.Controllers
{
    /// <summary>
    /// Controle de comandas
    /// </summary>
    [RoutePrefix("RestAPIFurb")]
    [Authorize]
    public class PadController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// Busca todas as comandas cadastradas.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("comandas")]
        public IQueryable<PadModel> GetPads()
        {
            return db.Pads;
        }

        /// <summary>
        /// Busca a comanda a partir de seu ID (Identificador).
        /// </summary>
        /// <param name="id">Identificador utilizado para buscar comanda no banco</param>
        /// <returns>Retorna as informações da comanda encontrada.</returns>
        [AcceptVerbs("GET")]
        [Route("comandas/{id}")]
        [ResponseType(typeof(PadModel))]
        public IHttpActionResult GetPadModel(long id)
        {
            PadModel padModel = db.Pads.Find(id);
            if (padModel == null)
            {
                return NotFound();
            }

            return Ok(padModel);
        }

        /// <summary>
        /// Altera uma comanda a partir de seu ID (Identificador).
        /// </summary>
        /// <param name="id">Identificador utilizado para buscar comanda que será atualizada.</param>
        /// <param name="padModel">Informações que serão atualizadas da comanda.</param>
        /// <returns>Retorna as informações da comanda alterada.</returns>
        [AcceptVerbs("PUT")]
        [Route("comandas/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPadModel(long id, PadModel padModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != padModel.ID)
            {
                return BadRequest();
            }

            db.Entry(padModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PadModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Cadastra uma nova comanda.
        /// </summary>
        /// <param name="padModel">Informações da nova comanda.</param>
        /// <returns>Retorna as informações da comanda criada.</returns>
        [AcceptVerbs("POST")]
        [Route("comandas")]
        [ResponseType(typeof(PadModel))]
        public IHttpActionResult PostPadModel(PadModel padModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pads.Add(padModel);
            db.SaveChanges();

            CreatedAtRoute("DefaultApi", new { id = padModel.ID }, padModel);

            return Ok(padModel);
        }

        /// <summary>
        /// Apaga uma comanda a partir de um ID (Identificador)
        /// </summary>
        /// <param name="id">Identificador da comanda que será excluida.</param>
        /// <returns>Retorna as informações da comanda excluida.</returns>
        [AcceptVerbs("DELETE")]
        [Route("comandas/{id}")]
        [ResponseType(typeof(PadModel))]
        public IHttpActionResult DeletePadModel(long id)
        {
            PadModel padModel = db.Pads.Find(id);
            if (padModel == null)
            {
                return NotFound();
            }

            db.Pads.Remove(padModel);
            db.SaveChanges();

            return Ok(padModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PadModelExists(long id)
        {
            return db.Pads.Count(e => e.ID == id) > 0;
        }
    }
}