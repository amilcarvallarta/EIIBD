using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIIBD
{
    //TODO: Crear Interface y pasar a componente de bajo nivel
    public class ImageSourceList_FromResources
    {
        public List<ImageSource> Items()
        {
            //TODO: Dejar pendiente hasta cambiar el directorio de Frames
            /*
            List<ImageSource> sourceList = [];
            string mainDir = FileSystem.Current.AppDataDirectory;

            try
            {
                var frameFiles = Directory.EnumerateFiles(mainDir, "*.png");
                //TODO: Pasar a Linq
                foreach (string currentframeFile in frameFiles)
                {
                    sourceList.Add(ImageSource.FromFile(currentframeFile));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            */

            //TODO:Borrar
            List<ImageSource> sourceList = [];
            sourceList.Add(ImageSource.FromFile("black_frame.png"));
            sourceList.Add(ImageSource.FromFile("blue_frame.png"));
            sourceList.Add(ImageSource.FromFile("green_frame.png"));
            sourceList.Add(ImageSource.FromFile("purple_frame.png"));
            sourceList.Add(ImageSource.FromFile("red_frame.png"));
            return sourceList;
        }
    }
}
