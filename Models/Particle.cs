//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace NbodyWithUI.Models
{
    public class Particle
    {
        public float Mass { get; set; }
        public float Radius { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double VelX { get; set; }
        public double VelY { get; set; }
        private byte ParticleCoordTrailLimit { get; set; } = 3;

        public (List<double> X, List<double> Y) CoordinateHistory { get; set; } = (new List<double>(), new List<double>());

        // a function to add coordinates in a controlled fashion, instead of accessing the list directly
        public void addCoordToHistory( double coordX, double coordY)  // CoordinateHistory is currently not limited
        {
            this.CoordinateHistory.X.Add(coordX);
            this.CoordinateHistory.Y.Add(coordY);

           // if (this.CoordinateHistory.Count > ParticleCoordTrailLimit)
           //     this.CoordinateHistory.RemoveAt(this.CoordinateHistory.Count - 1);
        }
        public (double x, double y, double absDist) currentVelocityXYC(int timestep) // used C, as the hypothenuse is usually named in a ABC triangle
        {
            double dx, dy;
            var lastIndex = this.CoordinateHistory.X.Count - 1;
            bool dontcheckagain = false;
            if (dontcheckagain || CoordinateHistory.X.Count() >= 2)
            {
                dontcheckagain = true;
                dx = this.CoordinateHistory.X[lastIndex] - this.CoordinateHistory.X[lastIndex - 1];
                dy = this.CoordinateHistory.Y[lastIndex] - this.CoordinateHistory.Y[lastIndex - 1];
            }
            else
            {
                dx = 0;
                dy = 0;
            }

            var absDist = Math.Sqrt(dx * dx + dy * dy);

            return (dx / timestep, dy / timestep, absDist / timestep);
        }

        private double currentAngleInDegrees()
        {

            var lastIndex = this.CoordinateHistory.X.Count - 1;

            var dx = this.CoordinateHistory.X[lastIndex] - this.CoordinateHistory.X[lastIndex - 1];
            var dy = this.CoordinateHistory.Y[lastIndex] - this.CoordinateHistory.Y[lastIndex - 1];

            var radToDeg = 180 / Math.PI;
            var result = Math.Atan2(dx, dy) * radToDeg;

            return result;
        }
        public (double dx,double dy, double diagDistance) distanceBetweenCenters(Particle other)
        {
            var dx = other.PositionX - this.PositionX;
            var dy = other.PositionY - this.PositionY;
            var diagDistance = Math.Sqrt(dx * dx + dy * dy);
            return (dx, dy, diagDistance);
        }
        private double distanceBetweenSurfaces(Particle other)
        {
            var dx = this.PositionX - other.PositionX;
            var dy = this.PositionY - other.PositionY;
            var distance = Math.Sqrt(dx*dx + dy*dy);
            var result = distance - this.Radius - other.Radius;
            return result;
        }
        public bool hasCollidedWith(Particle other)
        {
            if (distanceBetweenSurfaces(other) <= 0)
                return true;

            return false;
        }
        public (double dx, double dy) totalDistplacementDuringStep(Particle other, int timeStep, int collission)
        {
            var distance = this.distanceBetweenCenters(other);
            double G = 6.67408 * Math.Pow(10, -11);
            var absForce = (G * this.Mass * other.Mass) / Math.Pow(distance.diagDistance,2);

            //var directionX = Math.Sign(distance.dx);
            //var directionY = Math.Sign(distance.dy);
 
            double pushback = -1 * Math.Pow(this.distanceBetweenSurfaces(other), 3);

            var acceleration = (((absForce / this.Mass) * (distance.dx / distance.diagDistance)) + pushback*0,
                                ((absForce / this.Mass) * (distance.dy / distance.diagDistance)) + pushback*0);


            //displacement =initial_position + initial_speed*time+1/2*acceleration*time^2,
            //but I'm not using initial position here, it comes in the world-file, row 33,34
            
            var displacement = (this.VelX * timeStep + 1 / 2f * acceleration.Item1 * Math.Pow(timeStep, 2),
                                this.VelY * timeStep + 1 / 2f * acceleration.Item2 * Math.Pow(timeStep, 2));
            //var currVel = this.currentVelocityXYC(timeStep);
            //this.VelX = currVel.x;
            //this.VelY = currVel.y;
            this.VelX = displacement.Item1/timeStep;
            this.VelY = displacement.Item2/timeStep;



            if (false)
            {
                Console.WriteLine($"distance: {distance}");
                Console.WriteLine($"absForce: {absForce}");
                Console.WriteLine($"accelera: {acceleration}");
                Console.WriteLine($"displace: {displacement}");
                Console.WriteLine($"collided: {hasCollidedWith(other)}");
                Console.WriteLine($"distSurf: {distanceBetweenSurfaces(other)}");
                Console.WriteLine($"");
            }

            return displacement;
        }

        public Particle(float mass, float radius, double positionX, double positionY, double velX, double velY)
        {
            Mass = mass;
            PositionX = positionX;
            PositionY = positionY;
            Radius = radius;
            VelX = velX;
            VelY = velY;

            CoordinateHistory.X.Add(positionX);
            CoordinateHistory.Y.Add(positionY);
        }
    }
}
