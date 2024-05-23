using Camera.MAUI;

namespace EIIBD;

public partial class PhotographPage : ContentPage
{
    //Todo: Crear imagen de captura predeterminada
    //Todo: Crear imagen para camara invalidada
    public static readonly BindableProperty capturarActivadoProperty =
      BindableProperty.Create("capturarActivado", typeof(bool), typeof(PhotographPage), false);

    public static readonly BindableProperty compartirActivadoProperty =
      BindableProperty.Create("compartirActivado", typeof(bool), typeof(PhotographPage), false);

    public static readonly BindableProperty regresarActivadoProperty =
      BindableProperty.Create("regresarActivado", typeof(bool), typeof(PhotographPage), false);

    public bool capturarActivado
    {
        get => (bool)GetValue(capturarActivadoProperty);
        set => SetValue(capturarActivadoProperty, value);
    }

    public bool compartirActivado
    {
        get => (bool)GetValue(compartirActivadoProperty);
        set => SetValue(compartirActivadoProperty, value);
    }

    public bool regresarActivado
    {
        get => (bool)GetValue(regresarActivadoProperty);
        set => SetValue(regresarActivadoProperty, value);
    }

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
                    capturarActivado = true;
                    verPaginaCapturar(true);
                }
            });
        }
    }

    private async void OnCapturarClicked(object sender, EventArgs e)
    {
        //TODO: Dar seguimiento al bug
        //TODO: https://stackoverflow.com/questions/77139313/camera-maui-snapshot-result-does-not-fit-preview
        cameraView.ZoomFactor = 1.7f;
        var stream = await cameraView.TakePhotoAsync();
        cameraView.ZoomFactor = 1.0f;
        if (stream != null)
        {
            var result = ImageSource.FromStream(() => stream);
            snapPreview.Source = result;
            compartirActivado = true;
            verPaginaCapturar(false);
        }
    }

    private async void OnCompartirClicked(object sender, EventArgs e)
    {
        var result = await PhotographSnap.CaptureAsync();
        using MemoryStream memoryStream = new();

        if (result is not null)
            await result.CopyToAsync(memoryStream);

        string fn = $"Retrato_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
        string file = Path.Combine(FileSystem.CacheDirectory, fn);

        File.WriteAllBytes(file, memoryStream.ToArray());

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Retrato eiibd",
            File = new ShareFile(file)
        });

        verPaginaCapturar(true);
    }

    private void OnRegresarClicked(object sender, EventArgs e)
    {
        verPaginaCapturar(PhotographSnapShell.IsVisible);
    }

    private void verPaginaCapturar(bool Ver)
    {
        regresarActivado = compartirActivado & capturarActivado;
        PhotographCameraShell.IsVisible = Ver;
        PhotographSnapShell.IsVisible = !Ver;
    }

    private async void OnverCatálogoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//gallery");
    }
}