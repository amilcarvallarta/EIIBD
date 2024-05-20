using Camera.MAUI;

namespace EIIBD;

public partial class PhotographPage : ContentPage
{
    public PhotographPage()
    {
        InitializeComponent();
        cameraView.CamerasLoaded += CameraView_CamerasLoaded;
    }

    private void CameraView_CamerasLoaded(object? sender, EventArgs e)
    {
        if (cameraView.NumCamerasDetected > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (await cameraView.StartCameraAsync() == CameraResult.Success)
                {
                }
            });
        }
    }

    private async void OnCapturarClicked(object sender, EventArgs e)
    {
        var stream = await cameraView.TakePhotoAsync();
        if (stream != null)
        {
            var result = ImageSource.FromStream(() => stream);
            snapPreview.Source = result;
        }
    }

    private async void OnCompartirClicked(object sender, EventArgs e)
    {
        var result = await PhotographSnap.CaptureAsync();
        using MemoryStream memoryStream = new();

        await result.CopyToAsync(memoryStream);

        string fn = $"Retrato_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
        string file = Path.Combine(FileSystem.CacheDirectory, fn);

        File.WriteAllBytes(file, memoryStream.ToArray());

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Retrato eiibd",
            File = new ShareFile(file)
        });
    }

}