using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotifyTest
{
    public class Event
    {
        public date date;
        public string title;
        public string description;
        private int id;

        public Event(string start, string end, string title, string description, int id)
        {
            this.date = new date(start, end);
            this.title = title;
            this.description = description;
            this.id = id;
        }
        public override string ToString()
        {
            return "Title:" + this.title + " Description: "+this.description+" Start:"+this.date.start+" End:"+this.date.end;
        }
    }

    public class date
    {
        public DateTime start;
        public DateTime end;

        public date(string start, string end)
        {
            this.start = DateTime.Parse(start);
            this.end = DateTime.Parse(end);
        }
    }

}