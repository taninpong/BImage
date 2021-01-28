using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyServiceDemos.Services
{
    public interface IDirectory
    {
        string CreateDirectory(string directoryName);
        void RemoveDirectory();
        string RenameDirectory(string oldDirectoryName, string newDirectoryName);
    }
}
