// See https://aka.ms/new-console-template for more information
using NbodyWithUI.Models;

Particle p1 = new Particle(10, (0, 1));
Particle p2 = new Particle(10, (1, 1));

p1.totalDistplacementDuringStep(p2, 2);
