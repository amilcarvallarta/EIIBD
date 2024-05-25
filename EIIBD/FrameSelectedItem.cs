using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIIBD
{
    public class FrameSelectedItem : BindableObject
    {
        public ImageSource FrameItem
        {
            get => frameItem;
            set { frameItem = value; OnPropertyChanged("FrameItem"); }
        }
        ImageSource frameItem = ImageSource.FromResource("blue_frame.png");
    }
}
