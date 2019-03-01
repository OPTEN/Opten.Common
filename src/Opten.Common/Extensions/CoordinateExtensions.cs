using System;

namespace Opten.Common.Extensions
{
    /// <summary>
    /// The Coordinate Extensions.
    /// </summary>
    public static class CoordinateExtensions
    {

        /**
        * The earth's radius, in meters.
        * Mean radius as defined by IUGG.
        */
        // https://github.com/googlemaps/android-maps-utils/blob/master/library/src/com/google/maps/android/MathUtil.java
        public const double EARTH_RADIUS = 6371009;

        /**
        * Returns haversine(angle-in-radians).
        * hav(x) == (1 - cos(x)) / 2 == sin(x / 2)^2.
        */
        public static double Hav(double x)
        {
            double sinHalf = Math.Sin(x * 0.5);

            return sinHalf * sinHalf;
        }

        /**
         * Returns hav() of distance from (lat1, lng1) to (lat2, lng2) on the unit sphere.
         */
        public static double HavDistance(double lat1, double lat2, double dLng)
        {
            return Hav(lat1 - lat2) + Hav(dLng) * Math.Cos(lat1) * Math.Cos(lat2);
        }

        /// Given h==hav(x), returns sin(abs(x)).
        public static double SinFromHav(double h)
        {
            return 2 * Math.Sqrt(h * (1 - h));
        }

        /// Returns hav(asin(x)).
        public static double HavFromSin(double x)
        {
            double x2 = x * x;

            return x2 / (1 + Math.Sqrt(1 - x2)) * .5;
        }

        /// Returns sin(arcHav(x) + arcHav(y)).
        public static double SinSumFromHav(double x, double y)
        {
            double a = Math.Sqrt(x * (1 - x));
            double b = Math.Sqrt(y * (1 - y));

            return 2 * (a + b - 2 * (a * y + b * x));
        }

        /**
        * Returns sin(initial bearing from (lat1,lng1) to (lat3,lng3) minus initial bearing
        * from (lat1, lng1) to (lat2,lng2)).
        */
        public static double SinDeltaBearing(double lat1, double lng1, double lat2, double lng2,
            double lat3, double lng3)
        {
            double sinLat1 = Math.Sin(lat1);
            double cosLat2 = Math.Cos(lat2);
            double cosLat3 = Math.Cos(lat3);
            double lat31 = lat3 - lat1;
            double lng31 = lng3 - lng1;
            double lat21 = lat2 - lat1;
            double lng21 = lng2 - lng1;
            double a = Math.Sin(lng31) * cosLat3;
            double c = Math.Sin(lng21) * cosLat2;
            double b = Math.Sin(lat31) + 2 * sinLat1 * cosLat3 * Hav(lng31);
            double d = Math.Sin(lat21) + 2 * sinLat1 * cosLat2 * Hav(lng21);
            double denom = (a * a + b * b) * (c * c + d * d);

            return denom <= 0 ? 1 : (a * d - b * c) / Math.Sqrt(denom);
        }

        /**
        * Returns tan(latitude-at-lng3) on the great circle (lat1, lng1) to (lat2, lng2). lng1==0.
        * See http://williams.best.vwh.net/avform.htm .
        */
        public static double TanLatGeodesic(double lat1, double lat2, double lng2, double lng3)
        {
            return (Math.Tan(lat1) * Math.Sin(lng2 - lng3) + Math.Tan(lat2) * Math.Sin(lng3)) / Math.Sin(lng2);
        }

        /**
        * Returns mercator(latitude-at-lng3) on the Rhumb line (lat1, lng1) to (lat2, lng2). lng1==0.
        */
        public static double MercatorLatRhumb(double lat1, double lat2, double lng2, double lng3)
        {
            return (Mercator(lat1) * (lng2 - lng3) + Mercator(lat2) * lng3) / lng2;
        }

        /// Returns true if segment is on geodesic.
        public static bool IsOnSegmentGeodesic(double lat1, double lng1, double lat2, double lng2,
            double lat3, double lng3, double havTolerance)
        {
            double havDist13 = HavDistance(lat1, lat3, lng1 - lng3);

            if (havDist13 <= havTolerance)
            {
                return true;
            }

            double havDist23 = HavDistance(lat2, lat3, lng2 - lng3);

            if (havDist23 <= havTolerance)
            {
                return true;
            }

            double sinBearing = SinDeltaBearing(lat1, lng1, lat2, lng2, lat3, lng3);
            double sinDist13 = SinFromHav(havDist13);
            double havCrossTrack = HavFromSin(sinDist13 * sinBearing);

            if (havCrossTrack > havTolerance)
            {
                return false;
            }

            double havDist12 = HavDistance(lat1, lat2, lng1 - lng2);
            double term = havDist12 + havCrossTrack * (1 - 2 * havDist12);

            if (havDist13 > term || havDist23 > term)
            {
                return false;
            }

            if (havDist12 < 0.74)
            {
                return true;
            }

            double cosCrossTrack = 1 - 2 * havCrossTrack;
            double havAlongTrack13 = (havDist13 - havCrossTrack) / cosCrossTrack;
            double havAlongTrack23 = (havDist23 - havCrossTrack) / cosCrossTrack;
            double sinSumAlongTrack = SinSumFromHav(havAlongTrack13, havAlongTrack23);

            return sinSumAlongTrack > 0;  // Compare with half-circle == PI using sign of sin().
        }

        /**
        * Returns mercator Y corresponding to latitude.
        * See http://en.wikipedia.org/wiki/Mercator_projection .
        */
        public static double Mercator(double lat)
        {
            return Math.Log(Math.Tan(lat * 0.5 + Math.PI / 4));
        }

        /**
        * Wraps the given value into the inclusive-exclusive interval between min and max.
        */
        public static double Wrap(double n, double min, double max)
        {
            return (n >= min && n < max) ? n : (Mod(n - min, max - min) + min);
        }

        /**
        * Returns the non-negative remainder of x / m.
        */
        public static double Mod(double x, double m)
        {
            return ((x % m) + m) % m;
        }

        /**
        * Returns latitude from mercator Y.
        */
        public static double InverseMercator(double y)
        {
            return 2 * Math.Atan(Math.Exp(y)) - Math.PI / 2;
        }

        /**
        * Restrict x to the range [low, high].
        */
        public static double Clamp(double x, double low, double high)
        {
            return x < low ? low : (x > high ? high : x);
        }

        /**
        * Convert an angle in Degrees to Radians
        */
        public static double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

    }
}