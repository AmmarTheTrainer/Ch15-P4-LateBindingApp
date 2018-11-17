using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.IO;

namespace Ch15_P4_LateBindingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ****** Fun with Late Binding ****** ");

            // Try to Load a local copy of CarLibrary
            Assembly a = null;
            try
            {
                a = Assembly.Load("Ch14-P2-CarLibrary");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (a != null)
            {
                CreateUsingLateBinding(a);
            }

            Console.ReadLine();
        }

        private static void CreateUsingLateBinding(Assembly asm)
        {
            try
            {
                // Get metadata for the Minivan type.
                Type miniVan = asm.GetType("Ch14_P2_CarLibrary.MiniVan");

                // Create a Minivan Instance on fly
                object obj = Activator.CreateInstance(miniVan);
                Console.WriteLine("Created a {0} using late binding!", obj);

                MethodInfo mi = miniVan.GetMethod("TurboBoost");

                mi.Invoke(obj, null);

                //// Cast to get access to the members of MiniVan?
                //// Nope! Compiler error!
                //object obj = (MiniVan)Activator.CreateInstance(minivan);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void InvokeMethodWithArgsUsingLateBinding(Assembly asm)
        {
            try
            {
                // First, get a metadata description of the sports car.
                Type sport = asm.GetType("Ch14_P2_CarLibrary.SportsCar");
                
                // Now, create the sports car.
                object obj = Activator.CreateInstance(sport);
                
                // Invoke TurnOnRadio() with arguments.
                MethodInfo mi = sport.GetMethod("TurnOnRadio");

                mi.Invoke(obj, new object[] { true, 2 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
