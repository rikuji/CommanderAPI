using CommanderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommanderAPI.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions options) : base(options) { }

        public DbSet<Command> Commands { get; set; }
    }
}
