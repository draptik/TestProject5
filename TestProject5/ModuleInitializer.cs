using System.Runtime.CompilerServices;
using DiffEngine;

namespace TestProject5;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
       DiffTools.UseOrder(DiffTool.VisualStudioCode); 
    }
}