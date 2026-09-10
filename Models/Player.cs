using System;
using System.Collections.Generic;
using System.Text;

namespace MUD.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public bool IsDead { get; set; }
        public float Speed { get; set; }
    }
}
