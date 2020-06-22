using CommanderAPI.Data;
using CommanderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderAPI.Repository
{
    public class CommanderRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public CommanderRepository(CommanderContext context)
        {
            _context = context;
        }


        public Command GetById(int id)
        {
            var command = _context.Commands.FirstOrDefault(x => x.Id == id);

            return command;
        }

        public IEnumerable<Command> GetCommands()
        {
            return _context.Commands.ToList();
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentException(nameof(command));
            }

            _context.Commands.Add(command);
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command command)
        {
            
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentException(nameof(command));
            }

            _context.Commands.Remove(command);
        }
    }
}
