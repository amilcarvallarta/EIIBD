namespace EIIBD;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetItems();
    }

    public void SetItems()
    {
        Items.Add(new MenuItem
        {
            IconImageSource = "purple_frame.png",
        });
        Items.Add(new MenuItem
        {
            IconImageSource = "Blue_frame.png",
        });
    }
}