using System;
using System.Collections.Generic;

namespace Trainer.Domain.Commands
{
    public class CreateWorkoutCommand
    {
        public CreateWorkoutCommand()
        {
            
        }
        
        public CreateWorkoutCommand(DateTime? startDate = null, IEnumerable<string> tags = null)
        {
            StartDate = startDate;
            Tags = (List<string>) (tags ?? new List<string>());
        }
        
        public DateTime? StartDate { get; set; }
        
        public List<string> Tags { get; set; }
    }
}