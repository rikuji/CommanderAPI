using AutoMapper;
using CommanderAPI.DTOs;
using CommanderAPI.Models;
using CommanderAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CommanderAPI.Controllers
{
    [ApiController]
    [Route("api/commands")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommands()
        {
            var commandItems = _repository.GetCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<CommandReadDTO> GetById(int id)
        {
            var command = _repository.GetById(id);

            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(command));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("")]
        public ActionResult<CommandReadDTO> CreateCommand([FromBody] CommandCreateDTO commandCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDTO);

            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDTO>(commandModel);


            return CreatedAtRoute(nameof(GetById), new { commandReadDto.Id }, commandReadDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult UpdateCommand(int id, [FromBody] CommandUpdateDTO commandUpdateDTO)
        {
            var commandModelFromRepo = _repository.GetById(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDTO, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDTO> patchDocument)
        {
            var commandModelFromRepo = _repository.GetById(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandModelFromRepo);

            patchDocument.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }


            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetById(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();

        }
    }
}
