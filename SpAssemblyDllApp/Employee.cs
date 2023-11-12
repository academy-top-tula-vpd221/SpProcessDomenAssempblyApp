namespace SpAssemblyDllApp
{
    public class Employee
    {
        public string Name { set; get; } = null!;
        public int Age { set; get; }
        public Employee(string name, int age) 
        { 
            Name = name;
            Age = age;
        }
    }
}