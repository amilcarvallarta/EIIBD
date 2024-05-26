namespace EIIBD;

public partial class AppShell : Shell
{
    public Command SetFrameCommand;
    public AppShell(
        FrameCurrentItem currentItem,
        ImageSourceList_FromResources imageSourceList)
    {
        InitializeComponent();

        SetFrameCommand = new Command<ImageSource>(
                execute: (ImageSource FrameImageSource) =>
                {
                    currentItem.FrameItem = FrameImageSource;
                });

        SetItems(imageSourceList.Items());
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
}