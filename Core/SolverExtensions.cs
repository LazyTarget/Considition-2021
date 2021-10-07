using DotNet.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Core
{
	public static class GameExtensions
	{
        /// <summary>
        /// Returns true if position is valid inside of the vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="pkg"></param>
        /// <returns></returns>
		public static bool ValidateTruckPosition(this Vehicle vehicle, PointPackage pkg)
        {
            // Width (X)
            var x = pkg.GetValuesForX().ToArray();
            if (x.Any(_ => _ < 0))
                return false;
            if (x.Any(_ => _ > vehicle.Width))
                return false;


            // Length (Y)
            var y = pkg.GetValuesForY().ToArray();
            if (y.Any(_ => _ < 0))
                return false;
            if (y.Any(_ => _ > vehicle.Length))
                return false;


            // Height (Z)
            var z = pkg.GetValuesForZ().ToArray();
            if (z.Any(_ => _ < 0))
                return false;
            if (z.Any(_ => _ > vehicle.Height))
                return false;


            return true;
        }

        /// <summary>
        /// Returns true if is invalid position or space is occupied
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool IsCollisionFree(this GameState state, PointPackage pkg)
		{
            var invalid = ValidateTruckPosition(state.Vehicle, pkg);
            if (!invalid)
                return false;


            //bool DoesPackageFitX(Package package)
            //{
            //    return (_xp + _lastKnownMaxLength + package.Length < _truckX);
            //}

            //bool DoesPackageFitY(Package package)
            //{
            //    return (_yp + _lastKnownMaxWidth + package.Width < _truckY &
            //            _xp + package.Length < _truckX);
            //}

            //bool DoesPackageFitZ(Package package)
            //{
            //    return (_xp + package.Length < _truckX &
            //            _yp + package.Width < _truckY &
            //            _zp + package.Height < _truckZ);
            //}


            return true;
		}


        public static IEnumerable<Package> GetRemainingPackages(this GameState state)
        {
            for (var i = 0; i < state.Game.Dimensions.Count; i++)
            {
                var pkg = state.Game.Dimensions.ElementAtOrDefault(i);
                if (pkg == null)
                    break;

                var isLoaded = state.Solution.Any(x => x.Id == pkg.Id);
                if (isLoaded)
                    continue;

                yield return pkg;
            }
        }

        public static PointPackage Place(this Package package, int x, int y, int z)
		{
            var placedPackage = new PointPackage
            {
                Id = package.Id,
                x1 = x,
                x2 = x,
                x3 = x,
                x4 = x,
                x5 = x + package.Length,
                x6 = x + package.Length,
                x7 = x + package.Length,
                x8 = x + package.Length,
                y1 = y,
                y2 = y,
                y3 = y,
                y4 = y,
                y5 = y + package.Width,
                y6 = y + package.Width,
                y7 = y + package.Width,
                y8 = y + package.Width,
                z1 = z,
                z2 = z,
                z3 = z,
                z4 = z,
                z5 = z + package.Height,
                z6 = z + package.Height,
                z7 = z + package.Height,
                z8 = z + package.Height,
                OrderClass = package.OrderClass,
                WeightClass = package.WeightClass
            };
            return placedPackage;
        }

        public static IEnumerable<int> GetValuesForX(this PointPackage package)
		{
            yield return package.x1;
            yield return package.x2;
            yield return package.x3;
            yield return package.x4;
            yield return package.x5;
            yield return package.x6;
            yield return package.x7;
            yield return package.x8;
        }

        public static IEnumerable<int> GetValuesForY(this PointPackage package)
        {
            yield return package.y1;
            yield return package.y2;
            yield return package.y3;
            yield return package.y4;
            yield return package.y5;
            yield return package.y6;
            yield return package.y7;
            yield return package.y8;
        }

        public static IEnumerable<int> GetValuesForZ(this PointPackage package)
        {
            yield return package.z1;
            yield return package.z2;
            yield return package.z3;
            yield return package.z4;
            yield return package.z5;
            yield return package.z6;
            yield return package.z7;
            yield return package.z8;
        }

        public static Box AsBox(this PointPackage pkg)
		{
            var minX = pkg.GetValuesForX().Min();
            var minY = pkg.GetValuesForY().Min();
            var minZ = pkg.GetValuesForZ().Min();
            
            var maxX = pkg.GetValuesForX().Max();
            var maxY = pkg.GetValuesForY().Max();
            var maxZ = pkg.GetValuesForZ().Max();

            var min = new Point3D
            {
                X = minX,
                Y = minY,
                Z = minZ,
            };

            var max = new Point3D
            {
                X = maxX,
                Y = maxY,
                Z = maxZ,
            };

            var box = new Box
            {
                Min = min,
                Max = max,
            };
            return box;
		}

        public static Box AsBox(this Vehicle vehicle)
        {
            var min = new Point3D
            {
                X = 0,
                Y = 0,
                Z = 0,
            };

            var max = new Point3D
            {
                X = vehicle.Width,
                Y = vehicle.Length,
                Z = vehicle.Height,
            };

            var box = new Box
            {
                Min = min,
                Max = max,
            };
            return box;
        }

        public static bool IntersectsWith(this Box a, Box b)
		{
            var iX = a.Min.X <= b.Max.X && a.Max.X >= b.Min.X;
            var iY = a.Min.Y <= b.Max.Y && a.Max.Y >= b.Min.Y;
            var iZ = a.Min.Z <= b.Max.Z && a.Max.Z >= b.Min.Z;
            var result = iX && iY && iZ;
            return result;
		}
    }
}
