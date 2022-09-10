using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta.Models
{
    public class Roulette
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public List<Bet> Bets { get; set; }
      
    }

}
