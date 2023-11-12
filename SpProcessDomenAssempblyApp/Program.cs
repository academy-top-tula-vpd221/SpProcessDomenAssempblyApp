using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Runtime.Loader;

/*
var process = Process.GetCurrentProcess();
Console.WriteLine(process.Handle);
Console.WriteLine(process.Id);
Console.WriteLine(process.MachineName);
Console.WriteLine(process.MainModule?.ModuleName);
Console.WriteLine(process.MainModule.FileName);
Console.WriteLine(process.MainModule.ModuleMemorySize);
Console.WriteLine(process.ProcessName);
Console.WriteLine(process.StartTime.ToLongTimeString());
Console.WriteLine(process.PagedMemorySize64 / (1024*1024));
*/

/*
foreach(var process in Process.GetProcesses())
{
    Console.WriteLine($"Id: {process.Id}, Name: {process.ProcessName}");
}
*/

/*
Process process = Process.GetCurrentProcess(); //Process.GetProcessesByName("devenv");
//foreach (var process in vsProcesses)
{
    Console.WriteLine($"Id: {process.Id}, Name: {process.ProcessName}");
    
    Console.WriteLine("--------------");
    foreach(ProcessThread thread in process.Threads)
        Console.WriteLine($"Id: {thread.Id}");
    
    Console.WriteLine("--------------");
    foreach(ProcessModule module in process.Modules)
        Console.WriteLine($"Name: {module.ModuleName}, File name: {module.FileName}");

}
*/

//Process.Start("notepad.exe", "X:\\RPO\\maxim efimov\\index.html");

/*
AppDomain domain = AppDomain.CurrentDomain;
Console.WriteLine(domain.BaseDirectory);
Console.WriteLine(domain.FriendlyName);
var info = domain.SetupInformation;
Console.WriteLine(info.ApplicationBase);
Console.WriteLine(info.TargetFrameworkName);
*/

/*
Assembly assembly = Assembly.LoadFrom("SpAssemblyDllApp.dll");

Console.WriteLine(assembly.FullName);

Type[] types = assembly.GetTypes();
foreach (Type t in types)
    Console.WriteLine(t.Name);
*/

Console.WriteLine(PowerProgram(5, 3));

GC.Collect();
GC.WaitForPendingFinalizers();

Console.WriteLine();
foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
    Console.WriteLine(asm.GetName().Name);



object PowerProgram(double x, int n)
{
    var context = new AssemblyLoadContext("Math", true);
    context.Unloading += ContextUnloading;

    var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "SpMathDllApp.dll");
    Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);

    var typeMath = assembly.GetType("SpMathDllApp.Math");
    object result = 0;

    if (typeMath is not null)
    {
        var powerMethod = typeMath.GetMethod("Power", BindingFlags.Public | BindingFlags.Static);
        result = powerMethod?.Invoke(null, new object[] { x, n });
        Console.WriteLine($"{x} in power {n} = {result}");
    }

    Console.WriteLine();
    foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
        Console.WriteLine(asm.GetName().Name);

    context.Unload();

    return result;
}

void ContextUnloading(AssemblyLoadContext context)
{
    Console.WriteLine("Library Matn unload");
}