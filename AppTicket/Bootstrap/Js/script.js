var tbody = document.querySelector('table tbody');
var ticketVar = {};

function Cadastrar() {
    ticketVar.prioridade_ticket = document.querySelector('#prioridade_ticket').value;
    ticketVar.assunto_ticket = document.querySelector('#assunto_ticket').value;
    ticketVar.descricao_ticket = document.querySelector('#descricao_ticket').value;

    if (ticketVar.id_ticket === undefined || ticketVar.id_ticket === 0) 
    {
        salvarTickets('POST', 0, ticketVar);
    }
    else {
        salvarTickets('PUT', ticketVar.id_ticket, ticketVar);
    }

    carregaTickets();

    $('#myModal').modal('hide');
}

function NovoTicket() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');

    document.querySelector('#prioridade_ticket').value = '';
    document.querySelector('#assunto_ticket').value = '';
    document.querySelector('#descricao_ticket').value = '';

    ticketVar = {};

    btnSalvar.textContent = 'Novo';

    tituloModal.textContent = 'Novo Ticket';

    $('#myModal').modal('show');
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');

    document.querySelector('#prioridade_ticket').value = '';
    document.querySelector('#assunto_ticket').value = '';
    document.querySelector('#descricao_ticket').value = '';

    ticketVar = {};

    btnSalvar.textContent = 'Novo';

    tituloModal.textContent = 'Novo Ticket';

    $('#myModal').modal('hide');
}

function carregaTickets() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `http://localhost:57335/api/Ticket/Recuperar`, true);    
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onerror = function() {
        console.log('ERRO', xhr.readyState);
    }

    xhr.onreadystatechange = function() {
        if (this.readyState == 4){
            if(this.status == 200) {
                var tickets = JSON.parse(this.responseText);
                for (var indice in tickets) {
                    adicionaLinha(tickets[indice]);
                }
            }
            else if(this.status == 500) {
                var erro = JSON.parse(this.responseText);
                console.log(erro.message);
                console.log(erro.ExceptionMessage);
            }
        }
    }
    xhr.send();
}

function salvarTickets(metodo, id, corpo) {
    var xhr = new XMLHttpRequest();

    if (id === undefined || id === 0)
        id = '';

    xhr.open(metodo, `http://localhost:57335/api/Ticket/${id}`, false);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(corpo));
}

function excluirTickets(id) {
    var xhr = new XMLHttpRequest();

    xhr.open(`DELETE`, `http://localhost:57335/api/Ticket/${id}`, false);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.send();
}

function atualizaExcluirTickets(ticket) {    
    bootbox.confirm({
        message: `Deseja excluir o Ticket: ${ticket.id_ticket}`,
        buttons: {
            confirm: {
                label: 'SIM',
                className: 'btn-success'
            },
            cancel: {
                label: 'NÂO',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                excluirTickets(ticket.id_ticket);
                carregaTickets();
            }
        }
    });    
}
carregaTickets();

function editarTicket(ticket) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');

    document.querySelector('#prioridade_ticket').value = ticket.prioridade_ticket;
    document.querySelector('#assunto_ticket').value = ticket.assunto_ticket;
    document.querySelector('#descricao_ticket').value = ticket.descricao_ticket;

    btnSalvar.textContent = 'Salvar';

    tituloModal.textContent = `Editar Ticket ${ticket.id_ticket}`;

    ticketVar = ticket;

    //console.log(ticketVar);
}

function adicionaLinha(ticket) {
    var trow = `<tr>
    <td>${ticket.id_ticket}</td>
    <td>${ticket.prioridade_ticket}</td>
    <td>${ticket.assunto_ticket}</td>
    <td>
    <div class="btn-group" role="group">
    <buttom class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='editarTicket(${JSON.stringify(ticket)})'>Editar</buttom>
    <buttom class="btn btn-danger" onclick='atualizaExcluirTickets(${JSON.stringify(ticket)})'>Excluir</buttom>
	</div>    
    </td>
    </tr>`;

    tbody.innerHTML += trow;
}