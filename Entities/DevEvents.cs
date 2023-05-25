using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDevEvents.Entities
{
    public class DevEvents
    {
        public DevEvents()
        {
         Speakers = new List<DevEventSpeaker>();
         IsDeteleted = false;   
        }
        public Guid Id {get;set;}
        public string? Title {get;set;}
        public string? Description {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public List<DevEventSpeaker> Speakers {get;set;}
        public bool IsDeteleted {get;set;}

        public void Update(string title, string description, DateTime startDate, DateTime endDate)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;

        }

        public void Delete ()
        {
            IsDeteleted = true;
        }
    }
}