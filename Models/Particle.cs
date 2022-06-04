using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbodyWithUI.Models
{
    public class Particle
    {
        public float Mass { get; set; }
        public float Radius { get; set; }
        public (double x, double y) Position { get; set; }
        private byte ParticleCoordTrailLimit { get; set; } = 100;

        private List<(double x, double y)> Last100Coordinates { get; set; } = new List<(double x, double y)>();
        // a function to add coordinates in a controlled fashion, instead of accessing the list directly
        public void addCoordToTrail((double a, double b) coordXY) 
        {
            this.Last100Coordinates.Add(coordXY);

            if (this.Last100Coordinates.Count > ParticleCoordTrailLimit)
                this.Last100Coordinates.RemoveAt(this.Last100Coordinates.Count - 1);
        }
        public (double x, double y, double absDist) currentVelocityXYC(int timestep) // used C as the hypothenuse is usually named in a ABC triangle
        {
            var lastIndex = this.Last100Coordinates.Count - 1;

            var dx = this.Last100Coordinates[lastIndex].x - this.Last100Coordinates[lastIndex - 1].x;
            var dy = this.Last100Coordinates[lastIndex].y - this.Last100Coordinates[lastIndex - 1].y;

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
            var dx = other.Position.x - this.Position.x;
            var dy = other.Position.y - this.Position.y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            return (dx, dy, distance);
        }
        private double distanceBetweenSurfaces(Particle other)
        {
            var dx = this.Position.x - other.Position.x;
            var dy = this.Position.y - other.Position.y;
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
        public double totalDistplacementDuringStep(Particle other, int timeStep)
        {
            var distance = distanceBetweenCenters(other);
            double G = 6.67408 * Math.Pow(10, -11);
            var absForce = (G * this.Mass * other.Mass) / Math.Pow(distance.diagDistance,2);

            var acceleration = absForce/this.Mass;

            var displacement = acceleration*timeStep;

            if(true)
            {
                Console.WriteLine($"distance: {distance}");
                Console.WriteLine($"absForce: {absForce}");
                Console.WriteLine($"accelera: {acceleration}");
                Console.WriteLine($"displace: {displacement}");
                Console.WriteLine($"collided: {hasCollidedWith(other)}");
                Console.WriteLine($"distSurf: {distanceBetweenSurfaces(other)}");
            }

            return displacement;
        }

        public Particle(float mass, float radius, (double x, double y) position)
        {
            Mass = mass;
            Position = position;
            Radius = radius;
        }
    }
}
