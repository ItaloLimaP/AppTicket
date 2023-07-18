using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace App.Repository
{
    public class TicketDTO
    {
        public int id_ticket { get; set; }

        [Required(ErrorMessage = "A PRIORIDADE é obrigatório.")]
        [StringLength(50, ErrorMessage = "O E-mail deve conter no mínimo 5 e no máximo 50 caracteres.", MinimumLength = 5)]
        public string prioridade_ticket { get; set; }

        [Required(ErrorMessage = "O ASSUNTO é obrigatório.")]
        [Range(1, 99, ErrorMessage = "O Contacto deve conter entre 1 e 99 caracteres.")]
        public string assunto_ticket { get; set; }

        //[RegularExpression(@"[0-9]{4}\-[0-9]{2}", ErrorMessage = "A Data deve seguir o padrão 'YYYY-MM'.")]
        [Required(ErrorMessage = "A DESCRIÇÃO é obrigatório.")]
        public string descricao_ticket { get; set; }
    }

    public class TicketDAO
    {
        private readonly string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
        private IDbConnection conexao;

        public TicketDAO()
        {            
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<TicketDTO> ListarTicketDB(int? id_ticket = null)
        {
            var listaTicket = new List<TicketDTO>();
            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();

                if (id_ticket == null)
                {
                    selectCmd.CommandText = "SELECT * FROM TICKET";
                }
                else
                {
                    selectCmd.CommandText = $"SELECT * FROM TICKET WHERE id_ticket = {id_ticket}";
                }


                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var tick = new TicketDTO()
                    {
                        id_ticket = Convert.ToInt32(resultado["id_ticket"]),
                        prioridade_ticket = Convert.ToString(resultado["prioridade_ticket"]),
                        assunto_ticket = Convert.ToString(resultado["assunto_ticket"]),
                        descricao_ticket = Convert.ToString(resultado["descricao_ticket"])
                    };

                    listaTicket.Add(tick);
                }

                return listaTicket;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }

        public void InserirTicketDB(TicketDTO ticket)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO TICKET (prioridade_ticket, assunto_ticket, descricao_ticket) values (@prioridade_ticket, @assunto_ticket, @descricao_ticket)";

                IDbDataParameter paramPrioridade = new SqlParameter("prioridade_ticket", ticket.prioridade_ticket);
                insertCmd.Parameters.Add(paramPrioridade);

                IDbDataParameter paramAssunto = new SqlParameter("assunto_ticket", ticket.assunto_ticket);
                insertCmd.Parameters.Add(paramAssunto);

                IDbDataParameter paramDescricao = new SqlParameter("descricao_ticket", ticket.descricao_ticket);
                insertCmd.Parameters.Add(paramDescricao);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }

        public void AtualizarTicketDB(TicketDTO ticket)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "UPDATE TICKET SET prioridade_ticket = @prioridade_ticket, assunto_ticket = @assunto_ticket, descricao_ticket = @descricao_ticket WHERE id_ticket = @id_ticket";

                IDbDataParameter paramprioridade_ticket = new SqlParameter("prioridade_ticket", ticket.prioridade_ticket);
                IDbDataParameter paramassunto_ticket = new SqlParameter("assunto_ticket", ticket.assunto_ticket);
                IDbDataParameter paramdescricao_ticket = new SqlParameter("descricao_ticket", ticket.descricao_ticket);

                updateCmd.Parameters.Add(paramprioridade_ticket);
                updateCmd.Parameters.Add(paramassunto_ticket);
                updateCmd.Parameters.Add(paramdescricao_ticket);

                IDbDataParameter paramID = new SqlParameter("id_ticket", ticket.id_ticket);
                updateCmd.Parameters.Add(paramID);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }

        public void DeletarTicketDB(int id_ticket)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM TICKET WHERE id_ticket = @id_ticket";

                IDbDataParameter paramID = new SqlParameter("id_ticket", id_ticket);
                deleteCmd.Parameters.Add(paramID);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }
    }
}