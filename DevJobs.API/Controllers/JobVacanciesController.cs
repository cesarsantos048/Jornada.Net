namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    [Route("api/[controller]")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Recebe todas as vagas.
        /// </summary>
        /// <returns>Objeto com todas as vagas</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public IActionResult GetAll(){
            var jobVacancies = _repository.GetAll();
            return Ok(jobVacancies);
        }

        /// <summary>
        /// Recebe uma vaga.
        /// </summary>
        /// <param name="id">Id para busca da vaga.</param>
        /// <returns>Objeto com vaga.</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrou vaga com esse id</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var jobVacancy = _repository.GetById(id);
            if(jobVacancy == null){
                return NotFound();
            }

            return Ok(jobVacancy);
        }

        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary>
        /// <remarks>
        /// {
        /// "title": "Dev .Net Junior",
        /// "description": "Vaga para Desenvolvedor .Net Junior",
        /// "company": "Cesar",
        /// "isRemote": true,
        /// "salaryRange": "3000 - 5000"
        /// }
        /// </remarks>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model){

            Log.Warning("POST JobVacancy chamado");

            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            if (jobVacancy.Title.Length > 30) 
                return BadRequest("Título precisa ter no máximo 30 caracteres!");
            
            _repository.Add(jobVacancy);

            return CreatedAtAction(
                "GetById", 
                new { id = jobVacancy.Id},
                jobVacancy);
        }

        /// <summary>
        /// Faz atualização da vaga.
        /// </summary>
        /// <remarks>
        /// {
        /// "title": "Dev .Net Pleno",
        /// "description": "Vaga para Desenvolvedor .Net Pleno",
        /// }
        /// </remarks>
        /// <param name="id">Id para busca da vaga.</param>
        /// <param name="model">Dados atualizados da vaga.</param>
        /// <returns>Objeto atualizado.</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Vaga não encontrada</response>
        /// <response code="204">Vaga não encontrada</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model){
            var jobVacancy = _repository.GetById(id);
            
            if(jobVacancy == null){
                return NotFound();
            }

            jobVacancy.Update(model.Title, model.Description);
            _repository.Update(jobVacancy);

            return NoContent();
        }


    }
}