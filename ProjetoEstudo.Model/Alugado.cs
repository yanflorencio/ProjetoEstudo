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

		[Column("status")]
		[Required]
		public StatusAlugado Status { get; set; }

		[Column("jogo_id")]
		[Required]
		public long JogoId { get; set; }

		[Column("cliente_id")]
		[Required]
		public long ClienteId { get; set; }

		[Column("data_aluguel")]
		[Required]
		public DateTime DataAluguel { get; set; }

		[Column("data_entrega")]
		[Required]
		public DateTime DataEntrega { get; set; }

		public Jogo Jogo { get; set; }
	}
}
