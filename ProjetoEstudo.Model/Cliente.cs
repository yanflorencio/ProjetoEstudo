using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEstudo.Model
{
	[Table("cliente")]
	public class Cliente
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("nome")]
		[Required]
		[MaxLength(255)]
		public string Nome { get; set; }

		[Column("cpf")]
		[Required]
		[MaxLength(11)]
		public string Cpf { get; set; }

		public virtual IList<Alugado> JogosAlugados { get; set; }
	}
}
