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
using Trabalho_Programacao_3.Helper_Code;
using Trabalho_Programacao_3.Models;

namespace Trabalho_Programacao_3.Controllers
{
    /// <summary>
    /// Controle de usuários
    /// </summary>
    [RoutePrefix("RestAPIFurb")]
    [Authorize]
    public class UserController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// Busca todos os usuários cadastrados.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("usuarios")]
        public IQueryable<UserModel> GetUsers()
        {
            return db.Users;
        }

        /// <summary>
        /// Busca o usuário a partir de seu ID (Identificador).
        /// </summary>
        /// <param name="id">Identificador utilizado para buscar usuário no banco</param>
        /// <returns>Retorna as informações do usuário encontrado.</returns>
        [AcceptVerbs("GET")]
        [Route("usuarios/{id}")]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUserModel(long id)
        {
            UserModel userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        /// <summary>
        /// Altera um usuário a partir de seu ID (Identificador).
        /// </summary>
        /// <param name="id">Identificador utilizado para buscar usuário que será atualizado.</param>
        /// <param name="userModel">Informações que serão atualizadas do usuário.</param>
        /// <returns>Retorna as informações do usuário alterado.</returns>
        [AcceptVerbs("PUT")]
        [Route("usuarios/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserModel(long id, UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userModel.ID)
            {
                return BadRequest();
            }


            if (userModel.Password != null && userModel.Password != "")
            {
                userModel.Password = Encryption.Encode(userModel.Password);
            }

            db.Entry(userModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="userModel">Informações do novo usuário.</param>
        /// <returns>Retorna as informações do usuário criado.</returns>
        [AcceptVerbs("POST")]
        [Route("usuarios")]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult PostUserModel(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userModel.Password = Encryption.Encode(userModel.Password);

            db.Users.Add(userModel);
            db.SaveChanges();

            CreatedAtRoute("DefaultApi", new { id = userModel.ID }, userModel);

            return Ok(userModel);
        }

        /// <summary>
        /// Apaga um usuário a partir de um ID (Identificador)
        /// </summary>
        /// <param name="id">Identificador do usuário que será excluido.</param>
        /// <returns>Retorna as informações do usuário excluido.</returns>
        [AcceptVerbs("DELETE")]
        [Route("usuarios/{id}")]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult DeleteUserModel(long id)
        {
            UserModel userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return NotFound();
            }

            db.Users.Remove(userModel);
            db.SaveChanges();

            return Ok(userModel);
        }

        /// <summary>
        /// Apaga um usuário a partir de seu E-mail
        /// </summary>
        /// <param name="userData">Email do usuário que será excluido.</param>
        /// <returns>Retorna as informações do usuário excluido.</returns>
        [AcceptVerbs("DELETE")]
        [Route("usuarios")]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult DeleteUserModel(UserModel userData)
        {
            UserModel userModel = db.Users.FirstOrDefault(x => x.Email == userData.Email);
            if (userModel == null)
            {
                return NotFound();
            }

            db.Users.Remove(userModel);
            db.SaveChanges();

            return Ok(userModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserModelExists(long id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}