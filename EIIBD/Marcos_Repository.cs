namespace EIIBD
{
    public class Marcos_Repository
    {
        public List<ImageSource> Todos()
        {
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
