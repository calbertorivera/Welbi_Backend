using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros.Core
{
    public static partial class Container
    {
        public static ITrack Track;
        public static ITaxesYOtrosApp TaxesYOtrosApp;

        static Container()
        {
            Container.Track = new CoreTrack();
        }


    }
}
