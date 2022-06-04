// See https://aka.ms/new-console-template for more information
using NbodyWithUI.Models;

Particle p1 = new Particle(1.00f,0.1f,
                            -1,0,
                            0,0.000001);
Particle p2 = new Particle(1.00f,0.1f,
                            1,0,
                            0,-0.000001);
Particle p3 = new Particle(0.1f, 0.1f,
                            1, 1, 
                            0, 0.000001);

World world = new World();
world.Particles.Add(p1);
world.Particles.Add(p2);
//world.Particles.Add(p3);


for (int i = 0; i < 20000; i++)
{
    world.stepOnce(30);
}

//world.plotWorldScottPlot("limitToWorld");
world.plotWorldScottPlot("");

