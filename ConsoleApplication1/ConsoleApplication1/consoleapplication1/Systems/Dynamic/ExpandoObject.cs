using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EstudosCSharp.Systems.Dynamic
{
    public static class ExpandoObject
    {
        public static void Sample()
        {
            dynamic sampleObject = new System.Dynamic.ExpandoObject();
            sampleObject.test = "Dynamic Property";
            Console.WriteLine(sampleObject.test);
            Console.WriteLine(sampleObject.test.GetType());
            sampleObject.number = 10;
            sampleObject.Increment = (Action)(() => { sampleObject.number++; });

            // Before calling the Increment method.
            Console.WriteLine(sampleObject.number);

            sampleObject.Increment();

            // After calling the Increment method.
            Console.WriteLine(sampleObject.number);

            sampleObject.sampleEvent = null;

            // Add an event handler.  
            sampleObject.sampleEvent += new EventHandler(SampleHandler);

            // Raise an event for testing purposes.  
            sampleObject.sampleEvent(sampleObject, new EventArgs());

            /// enumerando os atributos adicionados ao ExpandoObject
            foreach (var property in (IDictionary<String, Object>)sampleObject)
            {
                Console.WriteLine(property.Key + ": " + property.Value);
            }

            Console.WriteLine("removing property from expando object:");
            ((IDictionary<String, Object>)sampleObject).Remove("sampleEvent");

            foreach (var property in (IDictionary<String, Object>)sampleObject)
            {
                Console.WriteLine(property.Key + ": " + property.Value);
            }

            // checando por mudanças nas propriedades
            ((INotifyPropertyChanged)sampleObject).PropertyChanged +=
            new PropertyChangedEventHandler(HandlePropertyChanges);

            sampleObject.Increment();
            sampleObject.test = "Helder";
        }

        // Event handler.  
        static void SampleHandler(object sender, EventArgs e)
        {
            Console.WriteLine("SampleHandler for {0} event", sender);
        }

        static void HandlePropertyChanges(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("{0} has changed.", e.PropertyName);
        }

    }
}
