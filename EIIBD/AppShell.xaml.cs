using System.Diagnostics;

namespace EIIBD;

public partial class AppShell : Shell
{
    public Command SetFrameCommand;

    public AppShell()
    {
        InitializeComponent();
        SetFrameCommand = new Command<ImageSource>(
                execute: (ImageSource FrameImageSource) =>
                {
                    ((PhotographPage)CurrentPage).FrameImageSource = FrameImageSource;
                });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetItems(imageSourceList());
    }

    public void SetItems(List<ImageSource> sourceList)
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
    }

    //TODO: Pasar a un servicio asincrono
    private List<ImageSource> imageSourceList()
    {
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

        //TODO:Borrar
        List<ImageSource> sourceList2 = [];
        sourceList2.Add(ImageSource.FromFile("blue_frame.png"));
        sourceList2.Add(ImageSource.FromFile("purple_frame.png"));
        return sourceList;
    }
}