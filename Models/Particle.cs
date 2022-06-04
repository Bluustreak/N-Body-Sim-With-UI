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
        private byte ParticleCoordTrailLimit { get; set; } = 3;

        private List<(double x, double y)> Last100Coordinates { get; set; } = new List<(double x, double y)>();
        // a function to add coordinates in a controlled fashion, instead of accessing the list directly
        public void addCoordToTrail((double a, double b) coordXY) 
        {
            this.Last100Coordinates.Add(coordXY);

           // if (this.Last100Coordinates.Count > ParticleCoordTrailLimit)
           //     this.Last100Coordinates.RemoveAt(this.Last100Coordinates.Count - 1);
        }
        public (double x, double y, double absDist) currentVelocityXYC(int timestep) // used C, as the hypothenuse is usually named in a ABC triangle
        {
            double dx, dy;
            var lastIndex = this.Last100Coordinates.Count - 1;
            bool dontcheckagain = false;
            if (dontcheckagain || Last100Coordinates.Count() >= 2)
            {
                dontcheckagain = true;
                dx = this.Last100Coordinates[lastIndex].x - this.Last100Coordinates[lastIndex - 1].x;
                dy = this.Last100Coordinates[lastIndex].y - this.Last100Coordinates[lastIndex - 1].y;
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

            var lastIndex = this.Last100Coordinates.Count - 1;
            
            var dx = this.Last100Coordinates[lastIndex].x - this.Last100Coordinates[lastIndex - 1].x;
            var dy = this.Last100Coordinates[lastIndex].y - this.Last100Coordinates[lastIndex - 1].y;
            
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
        public (double dx, double dy) totalDistplacementDuringStep(Particle other, int timeStep)
        {
            var distance = this.distanceBetweenCenters(other);
            double G = 6.67408 * Math.Pow(10, -11);
            var absForce = (G * this.Mass * other.Mass) / Math.Pow(distance.diagDistance,2);

            //var directionX = Math.Sign(distance.dx);
            //var directionY = Math.Sign(distance.dy);
            var acceleration = (((absForce / this.Mass) * (distance.dx / distance.diagDistance)),
                                ((absForce / this.Mass) * (distance.dy / distance.diagDistance)));


            //displacement =initial_position + initial_speed*time+1/2*acceleration*time^2,
            //but I'm not using initial position here, it comes in the world-file, row 33,34
            var currVel = this.currentVelocityXYC(timeStep);
            var displacement = (currVel.x * timeStep + 1 / 2f * acceleration.Item1 * Math.Pow(timeStep, 2),
                                currVel.y * timeStep + 1 / 2f * acceleration.Item2 * Math.Pow(timeStep, 2));

            if (true)
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

        public Particle(float mass, float radius, double positionX, double positionY)
        {
            Mass = mass;
            PositionX = positionX;
            PositionY = positionY;
            Radius = radius;

            Last100Coordinates.Add((positionX, positionY));
        }
    }
}
