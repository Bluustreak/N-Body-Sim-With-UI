

using NbodyWithUI.Models;
Console.WriteLine("Hi, I'm Bluu and this is my N-body sim :O\n\n");

while (!OptionsMenu.StartSim && !OptionsMenu.Quit)
{
    OptionsMenu.printMenu();
}

if(!OptionsMenu.Quit)
{
    //the parameters are as follows: mass, radius,
    //initial posX, initial posY,
    //initial velocityX, initial velocityY
    Particle p1 = new Particle(1.00f, 0.1f,
                                -1, 0,
                                0, 0.000001);
    Particle p2 = new Particle(1.00f, 0.1f,
                                1, 0,
                                0, -0.000001);
    Particle p3 = new Particle(0.1f, 0.1f,
                                0, 1,
                                0.000003, 0.0);

    World world = new World(20, 20);
    world.Particles.Add(p1);
    world.Particles.Add(p2);
    world.Particles.Add(p3);

    int totalSimTime = OptionsMenu.TotalSimTime;
    int resolution = OptionsMenu.Resolution; // secs per physics step

    for (int i = 0; i < (totalSimTime / resolution); i++)
    {
        world.stepOnce(resolution);
    }

    world.plotWorldScottPlot("limitToWorld"); // "limitToWorld" ends the graphing when a particle leavs the boundries.
                                              // leave an empty string "" if you don't want it

}
