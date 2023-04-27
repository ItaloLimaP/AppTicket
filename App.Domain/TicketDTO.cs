using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Domain
{
    public class TicketDTO
    {
        public int id_ticket { get; set; }

        [Required(ErrorMessage = "A PRIORIDADE é de preenchimento obrigatório.")]
        [StringLength(50, ErrorMessage = "O E-mail deve conter no mínimo 5 e no máximo 50 caracteres.", MinimumLength = 5)]
        public string prioridade_ticket { get; set; }

        [Required(ErrorMessage = "O ASSUNTO é de preenchimento obrigatório.")]
        [Range(1, 99, ErrorMessage = "O Contacto deve conter entre 1 e 99 caracteres.")]
        public string assunto_ticket { get; set; }

        //[RegularExpression(@"[0-9]{4}\-[0-9]{2}", ErrorMessage = "A Data deve seguir o padrão 'YYYY-MM'.")]
        [Required(ErrorMessage = "A DESCRIÇÃO é de preenchimento obrigatório.")]
        public string descricao_ticket { get; set; }
    }
}