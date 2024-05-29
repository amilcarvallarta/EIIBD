namespace EIIBD;

public partial class AppShell : Shell
{
    readonly SeleccionarMarco_Services seleccionarMarco;
    readonly Marcos_Repository listaDe_Marcos;

    public AppShell( SeleccionarMarco_Services seleccionarMarco,
                     Marcos_Repository listaDe_Marcos)
    {
        InitializeComponent();
     
        this.seleccionarMarco = seleccionarMarco;
        this.listaDe_Marcos = listaDe_Marcos;

        agregarMarcos_a_LaGaleria();
    }

    public void agregarMarcos_a_LaGaleria()
    {
        foreach (ImageSource marco in listaDe_Marcos.Todos())
        {
            Items.Add(new MenuItem
            {
                IconImageSource = marco,
                CommandParameter = marco,
                Command = new Command<ImageSource>(
                    execute: (ImageSource marcoSeleccionado_PorEl_Usuario) =>
                    {
                        seleccionarMarco.MarcoSeleccionado = marcoSeleccionado_PorEl_Usuario;
                    })
            });
        }
    }
}