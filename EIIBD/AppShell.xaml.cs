using System.Diagnostics;

namespace EIIBD;

public partial class AppShell : Shell
{
    public Command SetFrameCommand;
    public AppShell(FrameCurrentItem currentItem)
    {
        InitializeComponent();

        SetFrameCommand = new Command<ImageSource>(
                execute: (ImageSource FrameImageSource) =>
                {
                    currentItem.FrameItem = FrameImageSource;
                });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetItems(imageSourceList());
    }

    public ImageSource? SetItems(List<ImageSource> sourceList)
    {
        //TODO: Pasar a Linq
        foreach (ImageSource imageSource in sourceList)
        {
            Items.Add(new MenuItem
            {
                IconImageSource = imageSource,
                Command = SetFrameCommand,
                CommandParameter = imageSource,
            });
        }

        return sourceList.FirstOrDefault();
    }

    //TODO: Pasar a un servicio asincrono
    private List<ImageSource> imageSourceList()
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