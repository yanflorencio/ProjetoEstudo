using ProjetoEstudo.Model.Dtos;

namespace ProjetoEstudo.Service.Interfaces
{
	public interface IDevolverJogo
	{
		DevolverJogoResponseDto DevolverJogo(DevolverJogoRequestDto devolverJogoRequestDto);
	}//interface
}//namespace
