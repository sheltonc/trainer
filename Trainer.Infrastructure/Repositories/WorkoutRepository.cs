using System.Threading.Tasks;
using MongoDB.Driver;
using Trainer.Domain.Aggregates.Workout;

namespace Trainer.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly MongoDbContext _context;

        public WorkoutRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Workout> GetById(string id)
        {
            return await _context.Workouts.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}