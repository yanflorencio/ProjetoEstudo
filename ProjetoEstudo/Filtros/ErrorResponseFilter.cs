using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoEstudo.Model.Dtos;

namespace ProjetoEstudo.Filtros
{
	public class ErrorResponseFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			ErrorResponseDto errorResponseDto = ErrorResponseDto.GetErrorResposenDtoFromExceprion(context.Exception);

			context.Result = new ObjectResult(errorResponseDto) { StatusCode = 500};
		}
	}
}
