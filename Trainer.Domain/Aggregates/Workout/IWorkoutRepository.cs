using System.Threading.Tasks;

namespace Trainer.Domain.Aggregates.Workout
{
    public interface IWorkoutRepository
    {
        Task<Workout> GetById(string id);
    }
}