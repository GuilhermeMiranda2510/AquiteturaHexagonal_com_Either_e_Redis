using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainHexagonal.Entities.Response
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; } // Código de status HTTP, por exemplo, 404, 500
        public string Message { get; set; }  // Mensagem de erro amigável
        public string Details { get; set; }  // Detalhes adicionais (opcional)
        public string ErrorCode { get; set; } // Código de erro específico do domínio (opcional)

        public ErrorResponse(int statusCode, string message, string details = null, string errorCode = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
            ErrorCode = errorCode;
        }
    }
}
