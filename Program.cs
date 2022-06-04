// See https://aka.ms/new-console-template for more information
using NbodyWithUI.Models;

Particle p1 = new Particle(1000,1, 0,0);
Particle p2 = new Particle(1000,1, 3,0);

World world = new World();
world.Particles.Add(p1);
world.Particles.Add(p2);


for (int i = 0; i < 10000; i++)
{
    Console.WriteLine(p1.PositionX);
    world.stepOnce(100);
}



