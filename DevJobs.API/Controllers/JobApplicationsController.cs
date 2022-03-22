
namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobApplicationsController(IJobVacancyRepository repository)
        {
            _repository = repository;   
        }


        /// <summary>
        /// Cadastro do aplicante a vaga.
        /// </summary>
        /// <remarks>
        /// {
        /// "applicantName": "Antônio César",
        /// "applicantEmail": "cesarmassa1@hotmail.com"
        /// </remarks>
        /// <param name="id">Id da vaga a se aplicar</param>
        /// <param name="model">Dados do aplicante a vaga.</param>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrou a vaga.</response>
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _repository.GetById(id);
            
            if(jobVacancy == null) return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id
            );

            _repository.AddApplication(application);

            return NoContent();
        }
    }
}