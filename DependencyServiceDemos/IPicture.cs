using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyServiceDemos
{
    public interface IPicture
    {
        void SavePictureToDisk(string filename, byte[] imageData);
    }
}
