using System.Runtime.CompilerServices;
using com.Klazapp.Utility;
using UnityEngine;
using decimal2 = com.Klazapp.Utility.mathExtension.decimal2; 

namespace com.NavigationApp.IndoorMap.Institution
{
    [CreateAssetMenu(fileName = "Location Handler Template", menuName = "Templates/Location/Location Handler Template", order = 1)]
    public class LocationHandlerTemplate : ScriptableObject
    {
        #region Variables
        [Header("Location Handler Component")]
        public LocationHandlerComponent locationHandlerComponentComponent;
        #endregion
        
        #region Public Access
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public decimal2 GetUserLastLocation()
        {
            return locationHandlerComponentComponent.userLastLocation;
        }
               
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUserLastLocation(decimal2 lastLocation)
        {
            locationHandlerComponentComponent.userLastLocation = lastLocation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetUserLastAltitude()
        {
            return locationHandlerComponentComponent.userLastAltitude;
        }
               
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUserLastAltitude(float altitude)
        {
            locationHandlerComponentComponent.userLastAltitude = altitude;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetUserLastHorizontalAccuracy()
        {
            return locationHandlerComponentComponent.userLastHorizontalAccuracy;
        }
               
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUserLastHorizontalAccuracy(float horizontalAccuracy)
        {
            locationHandlerComponentComponent.userLastHorizontalAccuracy = horizontalAccuracy;
        }
        #endregion
    }
}
