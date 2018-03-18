using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trainer.Domain.Aggregates.Workout
{
    public class Workout
    {
        public Workout(DateTime? startDate, IEnumerable<string> tags = null)
        {
            StartDate = startDate ?? DateTime.Now;
            Exercises = new List<Exercise>();
            Tags = new List<string>();

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    Tags.Add(tag);
                }
            }
        }
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        
        public DateTime StartDate { get; private set; }
        
        public DateTime EndDate { get; private set; }
        
        public List<Exercise> Exercises { get; private set; }
        
        public List<string> Tags { get; private set; }

        public void AddExercise(string name, string description, List<string> tags = null)
        {
            var exercise = Exercises.FirstOrDefault(e => e.Name == name);

            if (exercise != null)
            {
                throw new InvalidOperationException("This exercise already exists in the workout");
            }
            
            Exercises.Add(new Exercise(name, description, tags));
        }

        public void AddSet(int reps, decimal weight, List<string> tags = null)
        {
            var currentExercise = Exercises.LastOrDefault();

            if (currentExercise == null)
            {
                throw new InvalidOperationException("There is no exercise to add a set to.");
            }

            currentExercise.AddSet(reps, weight, tags);
        }
    }
    
    public class Exercise
    {
        public Exercise(string name, string description, List<string> tags = null)
        {
            Name = name;
            Description = description;
            Tags = tags;
        }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }

        public  List<string> Tags { get; private set; }
        
        public List<Set> Sets { get; private set; }

        public void AddSet(int reps, decimal weight, List<string> tags = null)
        {
            Sets.Add(new Set(reps, weight, tags));
        }
        
    }
    
    public class Set
    {
        public Set(int reps, decimal weight, List<string> tags)
        {
            Reps = reps;
            Weight = weight;
            Tags = tags;
        }
        
        public int Reps{ get; private set; }
        
        public decimal Weight { get; private set; }
        
        public List<string> Tags { get; private set; }
    }
}