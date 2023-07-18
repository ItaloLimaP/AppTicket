using App.Repository;
using AppTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AppTicket.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Ticket")]
    //[Authorize(Roles = Funcao.Admin)]

    public class TicketController : ApiController
    {
        // GET: api/Ticket
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                TicketModel ticket = new TicketModel();
                return Ok(ticket.ListarTicket());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);          
            }
        }

        // GET: api/Ticket/5
        [HttpGet]
        [Route("Recuperar/{id}/{email?}/{contacto?}")]

        public IHttpActionResult RecuperarPorId(int id, string email = null, string contacto = null)
        {
            try
            {
                TicketModel ticket = new TicketModel();

                return Ok(ticket.ListarTicket(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [HttpGet]
        [Route(@"RecuperarPorDataEmail/{data:regex([0-9]{4}\-[0-9]{2})}/{email:minlength(5)}")]
        public IHttpActionResult Recuperar(int id_ticket)
        {
            try
            {
                TicketModel ticket = new TicketModel();

                IEnumerable<TicketDTO> tickets = ticket.ListarTicket().Where(x => x.id_ticket == id_ticket);

                if (!tickets.Any())

                    return NotFound();

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        [HttpPost]
        public IHttpActionResult Post(TicketDTO ticket)
        {
           /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            try
            {
                TicketModel _ticket = new TicketModel();

                _ticket.InserirTicket(ticket);

                return Ok(_ticket.ListarTicket());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        [HttpPut]
        public IHttpActionResult Put(TicketDTO ticket)
        {
            try
            {
                TicketModel _ticket = new TicketModel();
                _ticket.AtualizarTicket(ticket);

                return Ok(_ticket.ListarTicket(ticket.id_ticket).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                TicketModel _ticket = new TicketModel();

                _ticket.DeletarTicket(id);

                return Ok("Deletado com Sucesso.");
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }            
        }
    }
}
