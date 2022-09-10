using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ruleta.Models
{
    public class Bet
    {
        public int Number { get; set; }
        public string Color { get; set; }
        public double value { get; set; }
        public bool ColorWin { get; set; }
        public bool NumberWin { get; set; }
        public double Valuewin { get; set; }

    }
}
