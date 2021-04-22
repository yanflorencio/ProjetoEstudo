using System;

namespace ProjetoEstudo.Model.Dtos
{
	public class ErrorResponseDto
	{
		public int Codigo { get; set; }

		public string Mensagem { get; set; }

		public ErrorResponseDto InnerError { get; set; }

		public ErrorResponseDto()
		{
		}

		public ErrorResponseDto(int codigo, string mensagem, ErrorResponseDto innerError)
		{
			Codigo = codigo;
			Mensagem = mensagem;
			InnerError = innerError;
		}

		public static ErrorResponseDto GetErrorResposenDtoFromExceprion(Exception e)
		{
			if (e == null)
			{
				return null;
			}

			return new ErrorResponseDto(e.HResult, e.Message, GetErrorResposenDtoFromExceprion(e.InnerException));
		}

	}//class
}//namespace
