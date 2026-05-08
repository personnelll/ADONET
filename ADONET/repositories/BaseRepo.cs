using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.repositories
{
    public class BaseRepo
    {
        private Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
        protected string GetFileFromAssemblyAsync(string filename)
        {
            var assemblyFiles = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var assemblyFile = assemblyFiles.FirstOrDefault(f => f.EndsWith(filename));

            if (assemblyFile == null)
            {
                throw new Exception($"File '{filename}' not found in assembly");
            }
            else
            {
                string content = new System.IO.StreamReader(_assembly.GetManifestResourceStream(assemblyFile) ?? throw new Exception($"Could not read file '{filename}' from assembly")).ReadToEnd();
                return content;
            }
        }
    }
}
