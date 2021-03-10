using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EstudoProjeto.Utils.Enum;

namespace ProjetoEstudo.Model
{
	[Table("alugado")]
	public class Alugado
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("id_jogo")]
		[Required]
		public long IdJogo { get; set; }

		[Column("id_cliente")]
		[Required]
		public long IdCliente { get; set; }

		[Column("status")]
		[Required]
		public StatusAlugado Status { get; set; }

		[Column("data_aluguel")]
		[Required]
		public DateTime DataAluguel { get; set; }

		[Column("data_entrega")]
		[Required]
		public DateTime DataEntrega { get; set; }

		public Jogo Jogo { get; set; }
	}
}
