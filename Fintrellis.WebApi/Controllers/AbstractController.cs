using Fintrellis.Common.Extensions;
using Fintrellis.Models;
using Fintrellis.Repositories.Common;

using Microsoft.AspNetCore.Mvc;


namespace Fintrellis.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AbstractController<TRepository, TEntity> : ControllerBase
        where TRepository: IRepository<TEntity>
        where TEntity: AbstractEntity
    {
        private readonly ILogger _logger;
        private readonly IRepository<TEntity> _repository;

        public AbstractController(IRepository<TEntity> repository, ILogger logger)
		{
            _repository = repository;
			_logger = logger;
		}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all");
                var entityList = await _repository.GetAll();
                return Ok(entityList);
            }
            catch (Exception ex)
            {
                ex.LogError(_logger, "Error fetching all");
                return StatusCode(500, "Internal server error - Please contact your administrator");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching record with id {id}");
                var entity = await _repository.GetById(id);
                if (entity == null)
                {
                    _logger.LogWarning($"Record with id {id} not found");
                    return NotFound();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching record with id {id}");
                return StatusCode(500, "Internal server error - Please contact your administrator");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Adding entity record");
                var createdEntity = await _repository.Add(entity);
                return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding record");
                return StatusCode(500, "Internal server error - Please contact your administrator");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                _logger.LogWarning($"Entity id {id} does not match the updated entity id {entity.Id}");
                return BadRequest();
            }

            try
            {
                _logger.LogInformation($"Updating entity with id {id}");
                await _repository.Update(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating entity with id {id}");
                return StatusCode(500, "Internal server error - Please contact your administrator");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting entity with id {id}");
                var result = await _repository.Delete(id);
                if (!result)
                {
                    _logger.LogWarning($"Entity with id {id} not found");
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting entity with id {id}");
                return StatusCode(500, "Internal server error - Please contact your administrator");
            }
        }
    }
}

