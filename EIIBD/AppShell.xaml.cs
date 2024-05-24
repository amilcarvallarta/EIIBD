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
        SetItems();
    }

    public void SetItems()
    {
        //TODO: Encontrar la forma de resiclar ImageSource IconImageSource en CommandParameter
        Items.Add(new MenuItem
        {
            IconImageSource = "blue_frame.png",
            Command = SetFrameCommand,
            CommandParameter = ImageSource.FromFile("blue_frame.png"),
        });
        Items.Add(new MenuItem
        {
            IconImageSource = "purple_frame.png",
            Command = SetFrameCommand,
            CommandParameter = ImageSource.FromFile("purple_frame.png"),
        });
    }
}