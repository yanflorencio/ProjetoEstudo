using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EstudoProjeto.Utils.Enum;

namespace ProjetoEstudo.Model
{
	[Table("jogo")]
	public class Jogo
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("nome")]
		[Required]
		[MaxLength(255)]
		public string Nome { get; set; }

		[Column("plataforma")]
		[Required]
		public Plataforma Plataforma { get; set; }
	}
}
