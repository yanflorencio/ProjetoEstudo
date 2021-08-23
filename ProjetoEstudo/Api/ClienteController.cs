using Microsoft.AspNetCore.Mvc;
using Projeto.Estudo.SenderServiceBus.Interfaces;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using System.Linq;

namespace ProjetoEstudo.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClienteController : ControllerBase
	{
		private readonly IClienteDao _clienteDao;
		private readonly ISender _serviceBusSender;

		public ClienteController(IClienteDao jogoDao, ISender sender)
		{
			_clienteDao = jogoDao;
			_serviceBusSender = sender;
		}

		[HttpPost]
		public IActionResult CadastrarCliente([FromBody] Cliente cliente)
		{
			if (ModelState.IsValid)
			{
				bool cadastrado = this.VerificaSeCpfJaCadastrado(cliente.Cpf);

				if (!cadastrado)
				{
					_clienteDao.Save(cliente);

					var uri = Url.Action("GetCliente", new { id = cliente.Id });

					_serviceBusSender.SendMessageAsync(cliente, "projeto.estudo.cliente");

					return Created(uri, cliente); //201
				}
				else
				{
					return Conflict("CPF já cadastrado");
				}
			}

			return BadRequest();
		}

		[HttpGet("{id}")]
		public IActionResult GetCliente(long id)
		{
			Cliente jogo = _clienteDao.FindById(id);

			if (jogo == null)
			{
				return NotFound();
			}
			return Ok(jogo);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCliente(long id)
		{
			Cliente cliente = _clienteDao.FindById(id);

			if (cliente != null)
			{
				_clienteDao.Delete(cliente);
				return Ok(true); //Poderia retornar NoContent()
			}

			return BadRequest();
		}

		[HttpPut]
		public IActionResult AlterarCliente([FromBody] Cliente cliente)
		{
			if (ModelState.IsValid)
			{
				_clienteDao.Update(cliente);

				return Ok(cliente);
			}

			return BadRequest();
		}

		[HttpGet("ByCpf/{cpf}")]
		public IActionResult GetClienteByCpf(string cpf)
		{
			Cliente cliente = _clienteDao.GetAll()
												.Where(c => c.Cpf.Equals(cpf))
												.FirstOrDefault();

			if (cliente != null)
			{
				return Ok(cliente);
			}

			return NotFound();
		}

		private bool VerificaSeCpfJaCadastrado(string cpf)
		{
			bool cadastrado = _clienteDao.GetAll()
										.Any(c => c.Cpf.Equals(cpf));

			return cadastrado;
		}

		[HttpGet("JogosAlugadosByCpf/{cpf}")]
		public IActionResult GetJogosAlugadosByCpf(string cpf)
		{
			Cliente cliente = _clienteDao.GetClienteIncludeJogosAlugadosByCpf(cpf);

			if (cliente != null)
			{
				return Ok(cliente);
			}

			return NotFound();
		}
	}
}
