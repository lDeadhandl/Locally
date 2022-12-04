using System;
namespace Locally.Models
{
	public class GamesObject
	{
        public List<Game>? Games { get; set; }
    }

    public class Game
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public int? Quarter { get; set; }
        public string? Clock { get; set; }
        public string? Scheduled { get; set; }
        public Home? Home { get; set; }
        public Away? Away { get; set; }
    }
}

