using CommanderAPI.Models;
using System.Collections.Generic;

namespace CommanderAPI.Repository
{
    public interface ICommanderRepository
    {
        bool SaveChanges();
        IEnumerable<Command> GetCommands();
        Command GetById(int id);
        void CreateCommand(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
    }
}
