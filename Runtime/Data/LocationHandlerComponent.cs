using decimal2 = com.Klazapp.Utility.mathExtension.decimal2;
using System;

namespace com.Klazapp.Utility
{
    [Serializable]
    public class LocationHandlerComponent
    {
        #region Variables
        public decimal2 userLastLocation;
        public float userLastAltitude;
        public float userLastHorizontalAccuracy;
        #endregion
    }
}
