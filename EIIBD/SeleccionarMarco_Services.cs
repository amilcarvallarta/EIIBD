using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIIBD
{
    public class SeleccionarMarco_Services : BindableObject
    {
        public ImageSource MarcoSeleccionado
        {
            get => marcoSeleccionado;
            set { marcoSeleccionado = value; OnPropertyChanged(nameof(MarcoSeleccionado)); }
        }
        ImageSource marcoSeleccionado = ImageSource.FromFile("red_frame.png");
    }
}
