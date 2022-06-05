

using NbodyWithUI.Models;
Console.WriteLine("Hi, I'm Bluu and this is my N-body sim :O\n\n");



if(false)
{
    //the parameters are as follows: mass, radius,
    //initial posX, initial posY,
    //initial velocityX, initial velocityY
    //Particle p1 = new Particle(1.00f, 0.1f,
    //                            -1, 0,
    //                            0, 0.000001);
    //Particle p2 = new Particle(1.00f, 0.1f,
    //                            1, 0,
    //                            0, -0.000001);
    //Particle p3 = new Particle(0.1f, 0.1f,
    //                            0, 1,
    //                            0.000003, 0.0);

    //World world = new World(20, 20);
    //world.Particles.Add(p1);
    //world.Particles.Add(p2);
    //world.Particles.Add(p3);
}
while (!OptionsMenu.Quit)
{
    OptionsMenu.printMenu();


    World world = new World(20, 20);

    if (true)
    {
        for (int i = 0; i < OptionsMenu.NumberOfParticles; i++)
        {
            Random rnd = new Random();

            Particle p = new Particle((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble() * world.Width * 0.5, (float)rnd.NextDouble() * world.Height * 0.5, (float)rnd.NextDouble() / 1000000, (float)rnd.NextDouble() / 1000000);
            world.Particles.Add(p);
        }
    }



    if (!OptionsMenu.Quit)
    {
        int totalSimTime = OptionsMenu.TotalSimTime;
        int resolution = OptionsMenu.Resolution; // secs per physics step

        for (int i = 0; i < (totalSimTime / resolution); i++)
        {
            world.stepOnce(resolution);
        }
    }

    Plotter.LimitToWorldBoundries = true;
    Plotter.plotWorldScottPlot(world);
}