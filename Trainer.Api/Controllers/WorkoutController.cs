using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Trainer.Domain.Aggregates.Workout;
using Trainer.Domain.Commands;
using Trainer.Infrastructure;

namespace trainer.api.Controllers
{
    [Route("api/[Controller]")]
    public class WorkoutController : Controller
    {
        private readonly MongoDbContext _mongoDbContext;
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutController(MongoDbContext mongoDbContext, IWorkoutRepository workoutRepository)
        {
            _mongoDbContext = mongoDbContext;
            _workoutRepository = workoutRepository;
        }

        [HttpGet]
        [Produces(typeof(List<Workout>))]
        public IActionResult Index()
        {
            var workouts = _mongoDbContext.Workouts.Find(_ => true);
            var data = workouts.ToList();
            return Ok(data);
        }

        [HttpGet("{workoutId}")]
        [Produces(typeof(Workout))]
        public async Task<IActionResult> GetById(string workoutId)
        {
            var workout = await _workoutRepository.GetById(workoutId);
            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        [HttpPost]
        [Produces(typeof(Workout))]
        public IActionResult Post(CreateWorkoutCommand command)
        {
            var workout = new Workout(command.StartDate, command.Tags);
            _mongoDbContext.Workouts.InsertOne(workout);
            return Ok(workout);
        }
    }
}