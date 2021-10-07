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

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns true if is invalid position or space is occupied
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool HasCollision(this GameState state, PointPackage pkg)
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


            throw new NotImplementedException();
		}
	}
}
