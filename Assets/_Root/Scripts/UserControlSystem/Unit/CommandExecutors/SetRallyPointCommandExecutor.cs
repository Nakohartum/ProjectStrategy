using System.Threading.Tasks;
using _Root.Scripts.Abstractions;
using Abstractions;

namespace _Root.Scripts.UserControlSystem.Unit.CommandExecutors
{
    public class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override async Task ExecuteSpecificCommand(ISetRallyPointCommand command)
        {
            GetComponent<MainBuilding>().RallyPoint = command.RallyPoint;
        }
    }
}