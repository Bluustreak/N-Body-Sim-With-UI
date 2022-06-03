using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbodyWithUI.Models
{
    public class World
    {
        // width and height is a symmetrical meassure outwards from the 0,0 center point, so width=200, would make a 400 wide world in total
        public int Width { get; set; }
        public int Height { get; set; }
        public uint ParticleCoordTrailLimit { get; set; } = 100;

        public List<Particle> Particles { get; set; }

        public void stepOnce()
        {

        }
    }
}
