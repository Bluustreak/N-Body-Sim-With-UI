using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbodyWithUI.Models
{
    public class World
    {
        public World(int width, int height)
        {
            Width = width;
            Height = height;
        }

        // width and height is a symmetrical meassure outwards from the 0,0 center point, so width=200, would make a 400 wide world in total
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Particle> Particles { get; set; } = new List<Particle>();

        public void stepOnce(int timeStep)
        {
            foreach (var p1 in Particles)
            {
                double totalDispX = 0;
                double totalDispY = 0;
                foreach (var p2 in Particles)
                {
                    if (p1 != p2)
                    {
                        var displacement = p1.totalDistplacementDuringStep(p2, timeStep, 1);
                        totalDispX += displacement.dx;
                        totalDispY += displacement.dy;
                    }
                }
                p1.PositionX += totalDispX;
                p1.PositionY += totalDispY;
                p1.CoordinateHistory.X.Add(p1.PositionX);
                p1.CoordinateHistory.Y.Add(p1.PositionY);

            }
        }

       
    }
}
