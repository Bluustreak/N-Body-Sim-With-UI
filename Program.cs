// See https://aka.ms/new-console-template for more information
using NbodyWithUI.Models;

Particle p1 = new Particle(10,1, (0, 0));
Particle p2 = new Particle(10,1, (2, 0));

p1.totalDistplacementDuringStep(p2, 2);
