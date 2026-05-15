using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class BaseRepo
    {
        private Assembly _assembly = Assembly.GetExecutingAssembly();
        protected string GetFileFromAssemblyAsync(string filename)
        {
            var assemblyFiles = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var assemblyFile = assemblyFiles.FirstOrDefault(f => f.EndsWith(filename));

            if (assemblyFile == null)
            {
                throw new Exception($"File '{filename}' not found in assembly");
            }
            else
            {
                string content = new StreamReader(_assembly.GetManifestResourceStream(assemblyFile) ?? throw new Exception($"Could not read file '{filename}' from assembly")).ReadToEnd();
                return content;
            }
        }
    }
}
