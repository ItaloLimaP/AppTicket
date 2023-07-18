using App.Repository;
using System;
using System.Collections.Generic;

namespace AppTicket.Models
{
    public class TicketModel
    {
        private object ticketDB;

        public List<TicketDTO> ListarTicket(int? id_ticket = null)
        {
            try
            {
                var ticketDB = new TicketDAO();
                return ticketDB.ListarTicketDB(id_ticket);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao listar os Tickets: Erro = {ex.Message}");
            }
        }

        internal void Add(TicketDTO ticket)
        {
            throw new NotImplementedException();
        }

        public void InserirTicket(TicketDTO ticket)
        {
            try
            {
                var ticketDB = new TicketDAO();
                ticketDB.InserirTicketDB(ticket);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir o Ticket: Erro => {ex.Message}");
            }

        }

        public void AtualizarTicket(TicketDTO ticket)
        {
            try
            {
                var ticketDB = new TicketDAO();
                ticketDB.AtualizarTicketDB(ticket);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao atualizar o Ticket: Erro => {ex.Message}");
            }

        }

        public void DeletarTicket(int id_ticket)
        {
            try
            {
                var ticketDB = new TicketDAO();
                ticketDB.DeletarTicketDB(id_ticket);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao deletar o Ticket: Erro => {ex.Message}");
            }
        }
    }
}