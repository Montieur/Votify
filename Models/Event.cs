using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotifyTest
{
    public class Event
    {
        public Date Date { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        private int Id;

        public Event(string Start, string End, string Title, string Description, int Id)
        {
            this.Date = new Date(Start, End);
            this.Title = Title;
            this.Description = Description;
            this.Id = Id;
        }
        public override string ToString()
        {
            return "Title:" + this.Title + " Description: "+this.Description+" Start:"+this.Date.Start+" End:"+this.Date.End;
        }
    }

    public class Date
    {
        public DateTime Start;
        public DateTime End;

        public Date(string Start, string End)
        {
            this.Start = DateTime.Parse(Start);
            this.End = DateTime.Parse(End);
        }

        public override string ToString()
        {
            return $"{Start.Hour}:{Start.Minute}";
        }
    }

}