using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms.Rest
{
    public class RestClient
    {


        public static T Create<T>(Func<T> mockCreator = null) {

            Type type = typeof(T);
            if (!type.IsInterface)
                throw new ArgumentException("T should be an interface for creating service");

            if (mockCreator != null) {
                return mockCreator();
            }


        }


    }



    
}
