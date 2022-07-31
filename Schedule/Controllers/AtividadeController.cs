using Microsoft.AspNetCore.Mvc;
using Schedule.Helpers;
using Schedule.Models.Atividade;
using Schedule.Services;
using System.Collections.Generic;

namespace Schedule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtividadeController : BaseController
    {
        private readonly IAtividadeService AtividadeService;
        public AtividadeController(IAtividadeService AtividadeService)
        {
            this.AtividadeService = AtividadeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AtividadeResponse>> Get()
        {
            return Ok(AtividadeService.GetAll());
        }

        [HttpPost]
        [Authorize]
        public ActionResult<AtividadeResponse> Create(AtividadeCreateRequest request)
        {
            return Created("",AtividadeService.Create(this.Account.Id, request));
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult<AtividadeResponse> Update(int id,AtividadeUpdateRequest request)
        {
            return Ok(AtividadeService.Update(this.Account.Id,id, request));
        }
    }
}
