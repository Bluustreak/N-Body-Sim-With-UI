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
        public int Width { get; set; } = 2;
        public int Height { get; set; } = 2;
        public byte ParticleCoordTrailLimit { get; set; } = 3;

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

        public void plotWorldScottPlot(string options)
        {
            if(Particles.Count >0)
            {
                //(double[] X, double[] Y) dataXY = (new double[] { 1, 2, 3, 4, 5 }, new double[] { 1, 2, 3, 4, 5 });
                var plt = new ScottPlot.Plot(800, 800);
                bool hasLeftTheWorld = false;
                int breakPoint = 0;

                if (options.Contains("limitToWorld"))
                {
                    foreach (var p in Particles)
                    {
                        foreach (var posX in p.CoordinateHistory.X)
                        {
                            if(Math.Abs(posX) > this.Width/2)
                            {
                                hasLeftTheWorld = true;
                                breakPoint = p.CoordinateHistory.X.IndexOf(posX);
                                break;
                            }
                        }
                        foreach (var posY in p.CoordinateHistory.Y)
                        {
                            if (Math.Abs(posY) > this.Width / 2)
                            {
                                hasLeftTheWorld = true;
                                breakPoint = p.CoordinateHistory.Y.IndexOf(posY);
                                break;
                            }
                        }
                        if (hasLeftTheWorld)
                            break;
                    }
                }

                if (breakPoint == 0)
                    breakPoint = Particles[0].CoordinateHistory.X.Count()-1;
                int resultAmount = 0;
                foreach (var particle in Particles)
                {
                    var Xcoords = particle.CoordinateHistory.X.Take(breakPoint).ToArray();
                    var Ycoords = particle.CoordinateHistory.Y.Take(breakPoint).ToArray();
                    plt.AddScatter(Xcoords, Ycoords);
                    resultAmount = particle.CoordinateHistory.X.Take(breakPoint).Count();
                }
                Console.WriteLine("Number of points per particle: " + Particles[0].CoordinateHistory.X.Count());
                Console.WriteLine("Number of points per particle taken: " + resultAmount);
                if (Particles.Count > 0)
                    plt.SaveFig("scottplotfig.png");
            }
        }
    }
}
