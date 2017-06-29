using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomRoundBorderEffect : RoutingEffect
    {

        /// <summary>
        /// 
        /// </summary>
        public AtomRoundBorderEffect():base("UIAtomsEffects.AtomRoundBorderEffect")
        {
            BackgroundColor = Color.Transparent;
            ForegroundColor = Color.Gray;
            CornerRadius = 10;
            StrokeWidth = 2;
            StrokeColor = Color.Gray;
        }


        /// <summary>
        /// 
        /// </summary>
        public Color BackgroundColor { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Color ForegroundColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CornerRadius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StrokeWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color StrokeColor { get; set; }

    }
}
