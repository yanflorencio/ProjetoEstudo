using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Dao.Interface;
using ProjetoEstudo.Model;
using System.Linq;

namespace ProjetoEstudo.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClienteController : ControllerBase
	{
		private readonly IDao<Cliente> _clienteRepositorio;

		public ClienteController(IDao<Cliente> jogoDao)
		{
			_clienteRepositorio = jogoDao;
		}

		[HttpPost]
		public IActionResult CadastrarCliente([FromBody] Cliente cliente)
		{
			if (ModelState.IsValid)
			{
				bool cadastrado = this.VerificaSeCpfJaCadastrado(cliente.Cpf);

				if (!cadastrado)
				{
					_clienteRepositorio.Save(cliente);

					var uri = Url.Action("GetCliente", new { id = cliente.Id });
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
			Cliente jogo = _clienteRepositorio.FindById(id);

			if (jogo == null)
			{
				return NotFound();
			}
			return Ok(jogo);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCliente(long id)
		{
			Cliente cliente = _clienteRepositorio.FindById(id);

			if (cliente != null)
			{
				_clienteRepositorio.Delete(cliente);
				return Ok(true); //Poderia retornar NoContent()
			}

			return BadRequest();
		}

		[HttpPut]
		public IActionResult AlterarCliente([FromBody] Cliente jogo)
		{
			if (ModelState.IsValid)
			{
				_clienteRepositorio.Update(jogo);

				return Ok(jogo);
			}

			return BadRequest();
		}

		[HttpGet("ByCpf/{cpf}")]
		public IActionResult GetClienteByCpf(string cpf)
		{
			Cliente cliente = _clienteRepositorio.GetAll().FirstOrDefault(c => c.Cpf.Equals(cpf));

			return Ok(cliente);
		}

		private bool VerificaSeCpfJaCadastrado(string cpf)
		{
			bool cadastrado = _clienteRepositorio.GetAll()
										.Any(c => c.Cpf.Equals(cpf));

			return cadastrado;
		}
	}
}
