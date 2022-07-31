using Microsoft.AspNetCore.Mvc;
using Schedule.Entities;

namespace Schedule.Controllers
{
    public class BaseController : ControllerBase
    {
        //Anexa a conta, caso logado com sucesso, ao contexto da requisição
        public Account Account => (Account)HttpContext.Items["Account"];

    }
}
