using System;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var appDomain = System.Threading.Thread.GetDomain();
            var assemblyName = new System.Reflection.AssemblyName("MyAssembly");
            var assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, System.Reflection.Emit.AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("MyModule");
            var typeBuilder = moduleBuilder.DefineType("HoHoHo", System.Reflection.TypeAttributes.Class, typeof(Exception));
            var exceptionType = typeBuilder.CreateType();
            var exception = (Exception)Activator.CreateInstance(exceptionType);
            throw exception;
        }
    }
}
