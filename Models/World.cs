using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbodyWithUI.Models
{
    internal class World
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Particle> Particles { get; set; }
    }
}
