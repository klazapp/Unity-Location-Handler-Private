using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using decimal2 = com.Klazapp.Utility.mathExtension.decimal2;

namespace com.Klazapp.Utility
{
    public abstract class LocationHandler : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        [Tooltip("Toggle this to set script's singleton status. Status will be set on script's OnAwake function")]
        private ScriptBehavior scriptBehavior = ScriptBehavior.None;
        public static LocationHandler Instance { get; private set; }
        
 	[Header("Location Handler Template")]
        public LocationHandlerTemplate locationHandlerTemplate;
        #endregion
        
        #region Lifecycle Flow
        protected void Awake()
        {
            SetScriptBehaviour(scriptBehavior);
        }
        #endregion
        
        #region Public Access
        //TODO: Simplify code
        public static bool IsUserWithinTargetBoundary(decimal2 userLocation, List<decimal2> targetLocationBoundary, decimal radiusInMeters = 0)
        {
            if (targetLocationBoundary == null || targetLocationBoundary.Count == 0)
            {
                return false;
            }

            //Init boundaries
            var top = decimal.MinValue;
            var left = decimal.MaxValue;
            var bottom = decimal.MaxValue;
            var right = decimal.MinValue;

            //Convert radius from meters to degrees
            //Approximate conversion: 1 degree ≈ 111 km
            var radiusInDegrees = radiusInMeters / 111000; 

            //Find boundaries and adjust with radius
            foreach (var point in targetLocationBoundary)
            {
                if (point.x > top) top = point.x;
                if (point.x < bottom) bottom = point.x;
                if (point.y < left) left = point.y;
                if (point.y > right) right = point.y;
            }

            //Adjust boundaries with radius
            top += radiusInDegrees;
            bottom -= radiusInDegrees;
            left -= radiusInDegrees;
            right += radiusInDegrees;

            var latitude = userLocation.x;
            var longitude = userLocation.y;

            //Check latitude bounds first
            if (top < latitude || latitude < bottom) 
                return false;

            //If bounding box does not wrap the date line the value = must be between the bounds
            //If bounding box does wrap the date line, it only needs to be higher than the left bound or lower than the right bound.
            if (left <= right && left <= longitude && longitude <= right)
            {
                return true;
            }
                
            return left > right && (left <= longitude || longitude <= right);
        }

        //Returns the closest target location index, position, and distance to it
        public (int targetIndex, decimal2 targetLocation, double distanceToTarget) FindClosestTargetLocation(decimal2 userLocation, List<decimal2> targetLocations, decimal radiusInMetres = 0)
        {
            var closestIndex = -1;
            var closestLocation = new decimal2(0, 0);
            var closestDistance = double.MaxValue;
            
            for (var i = 0; i < targetLocations.Count; i++)
            {
                var distance = HaversineDistance(userLocation, targetLocations[i]);

                if (distance >= closestDistance) 
                    continue;

                closestLocation = targetLocations[i];
                closestDistance = distance;
                closestIndex = i;
            }

            if (closestIndex != -1)
                return (closestIndex, closestLocation, closestDistance);
                
            Debug.LogWarning("Unable to find closest Poi");
            return (closestIndex, closestLocation, closestDistance);
            
            #region Local Functions
           
            #endregion
        }

	    public static (bool userWithinRadius, float userDistanceToTarget) IsUserWithinRadius(decimal2 userLocation, decimal2 targetLocation, decimal radiusMeters)
        {
            decimal distance = (decimal)HaversineDistance(userLocation, targetLocation);
            return (distance <= radiusMeters, (float)distance);
        }
        #endregion
        
        #region Modules
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double HaversineDistance(decimal2 latLon1, decimal2 latLon2)
        {
            var lat1 = (double)latLon1.x;
            var lon1 = (double)latLon1.y;
            var lat2 = (double)latLon2.x;
            var lon2 = (double)latLon2.y;
                    
            const double R = 6371000.0; 
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var lat1Rad = ToRadians(lat1);
            var lat2Rad = ToRadians(lat2);

            var a = math.sin(dLat / 2) * math.sin(dLat / 2) +
                    math.cos(lat1Rad) * math.cos(lat2Rad) *
                    math.sin(dLon / 2) * math.sin(dLon / 2);
            var c = 2 * math.atan2(math.sqrt(a), math.sqrt(1 - a));
            return R * c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double ToRadians(double degrees)
        {
            return degrees * math.PI / 180;
        }
        
 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void StoreUserLastLocationInfo(float latitude, float longitude, float altitude,
                                                          float horizontalAccuracy);
        // {
        //     //Store user's last location
        //     locationHandlerTemplate.SetUserLastLocation(new decimal2((decimal)latitude, (decimal)longitude));
        //     
        //     //Store user's last altitude
        //     locationHandlerTemplate.SetUserLastAltitude(altitude);
        //     
        //     //Store user's last horizontal accuracy
        //     locationHandlerTemplate.SetUserLastHorizontalAccuracy(horizontalAccuracy);
        // }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetScriptBehaviour(ScriptBehavior behavior)
        {
            if (behavior is not (ScriptBehavior.Singleton or ScriptBehavior.PersistentSingleton)) 
                return;
            
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
                
            Instance = this;

            if (behavior == ScriptBehavior.PersistentSingleton)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        #endregion
    }
}
