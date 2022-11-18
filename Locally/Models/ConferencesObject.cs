using System;
namespace Locally.Models
{
	public class ConferencesObject
	{
		public List<Conference>? Conferences { get; set; }
    }

	public class Conference
	{
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<Division>? Divisions { get; set; }
    }

    public class Division
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<Team>? Teams { get; set; }
    }
}

