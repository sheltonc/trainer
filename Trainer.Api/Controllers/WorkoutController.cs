using System.Collections.Generic;
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

        public WorkoutController(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        [HttpGet]
        [Produces(typeof(List<Workout>))]
        public IActionResult Index()
        {
            var workouts = _mongoDbContext.Workouts.Find(_ => true);
            var data = workouts.ToList();
            return Ok(data);
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