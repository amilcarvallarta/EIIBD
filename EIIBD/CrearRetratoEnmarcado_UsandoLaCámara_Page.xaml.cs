using Camera.MAUI;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace EIIBD;

public partial class CrearRetratoEnmarcado_UsandoLaCámara_Page : ContentPage
{
    #region Estado


    public bool CapturarFotoActivado
    {
        get => capturarFotoActivado;
        set {
            capturarFotoActivado = value;
            OnPropertyChanged(nameof(CapturarFotoActivado));
            OnPropertyChanged(nameof(RegresarActivado));
        }
    }
    bool capturarFotoActivado = false;

    public bool CompartirRetratoActivado
    {
        get => compartirRetratoActivado;
        set {
            compartirRetratoActivado = value;
            OnPropertyChanged(nameof(CompartirRetratoActivado));
            OnPropertyChanged(nameof(RegresarActivado));
        }
    }
    bool compartirRetratoActivado = false;

    public bool RegresarActivado
    {
        get => CapturarFotoActivado & CompartirRetratoActivado;
    }


    #endregion

    #region inicializacion


    public CrearRetratoEnmarcado_UsandoLaCámara_Page(SeleccionarMarco_Services marcoSeleccionado)
    {
        InitializeComponent();

        Enmarcado_DeLaCamaraEnPantallan.BindingContext = marcoSeleccionado;
        Enmarcado_DeLaComposicion.BindingContext = marcoSeleccionado;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = this;

        //Inicializacion de la CamaraEnPantalla antes de mostrar la vista Camara
        var gestionDeCamaras = CamaraEnPantalla.Cameras;
        
        CamaraEnPantalla.CamerasLoaded += (object? sender, EventArgs e) =>
        {
            var SeDetecto_PorLoMenos_UnaCamara = gestionDeCamaras.Count > 0;
            if (SeDetecto_PorLoMenos_UnaCamara)
            {
                CamaraEnPantalla.Camera = gestionDeCamaras.First();

                //Es necesario regresar al hilo principal para actualizar la Interfaz Grafica
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (await activarLaCamara_y_PermisosNecesarios())
                        activarVistaDeCamara();
                });
            }
        };
    }

    async Task<bool> activarLaCamara_y_PermisosNecesarios()
    {
        //Los permisos son solicitados por el sistema cuando se inicia la camara
        //si es que todavia no se han otorgado en ejecuciones anteriores
        return await CamaraEnPantalla.StartCameraAsync() == CameraResult.Success;
    }


    #endregion

    #region handles Botones


    async void CapturarClicked(object sender, EventArgs e)
    {
        //TODO: Solucion temporal del bug
        CamaraEnPantalla.ZoomFactor = 1.7f;

        var stream_FotografíaDeLaComposición = await CamaraEnPantalla.TakePhotoAsync();
        
        CamaraEnPantalla.ZoomFactor = 1.0f;
        //TODO: Dar seguimiento al bug
        //TODO: https://stackoverflow.com/questions/77139313/camera-maui-snapshot-result-does-not-fit-preview



        if (stream_FotografíaDeLaComposición != null)
        {
            var result_ImageSource = ImageSource.FromStream(() => stream_FotografíaDeLaComposición);
            FotografíaDeLaComposición.Source = result_ImageSource;
            activarVistaDeRetrato();
        }
    }

    async void CompartirClicked(object sender, EventArgs e)
    {
        var composicionFotografica_OrigenDe_UI = await ComposicionFotografica.CaptureAsync();
        using MemoryStream streamComposicion = new();

        if (composicionFotografica_OrigenDe_UI is not null)
            await composicionFotografica_OrigenDe_UI.CopyToAsync(streamComposicion);

        string nombre_Calculado_DeArchivo_EnCache = $"Retrato_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
        string archivoEnCache = Path.Combine(FileSystem.CacheDirectory, nombre_Calculado_DeArchivo_EnCache);

        File.WriteAllBytes(archivoEnCache, streamComposicion.ToArray());

        //API para compartir archivos
        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Retrato eiibd",
            File = new ShareFile(archivoEnCache)
        });

        intercambiar_EntreLasVistasDe_Camara_y_Retrato();
    }


    #endregion

    /**
        Esta region gestiona la visibilidad de los elementos y las vistas.
    Se ha tomado la decisión de mantener dos vista (Camara y Retrato) en
    esta pagina ya que se requiere una alta interacción entre ellas.
    **/
    #region intercambiar_EntreLasVistasDe_Camara_y_Retrato


    private void RegresarClicked(object sender, EventArgs e)
    {
        intercambiar_EntreLasVistasDe_Camara_y_Retrato();
    }

    void activarVistaDeCamara()
    {
        CapturarFotoActivado = true;
        intercambiar_EntreLasVistasDe_Camara_y_Retrato();
    }

    void activarVistaDeRetrato()
    {
        CompartirRetratoActivado = true;
        intercambiar_EntreLasVistasDe_Camara_y_Retrato();
    }

    void intercambiar_EntreLasVistasDe_Camara_y_Retrato()
    {
        var activar = RetratoView.IsVisible;

        CamaraView.IsVisible = activar;
        RetratoView.IsVisible = !activar;
    }


    #endregion
}