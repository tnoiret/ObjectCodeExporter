using Microsoft.VisualStudio.DebuggerVisualizers;
using ObjectCodeExporter.Visualizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Diagnostics.DebuggerVisualizer(typeof(WeakReferenceVisualizer), typeof(WeakReferenceVisualizerObjectSource), Target = typeof(WeakReference), Description = "WeakReference Visualizer for Object code exporter")]

namespace ObjectCodeExporter.Visualizer
{
    /// <summary>
    /// WeakReference Visualizer, need this for get Literal from object (because we can't make visualizer for objet :'( )
    /// </summary>
    public class WeakReferenceVisualizerObjectSource : VisualizerObjectSource
    {
        /// <summary>
        /// Retrive data from object, translate here 
        /// </summary>
        /// <param name="target">cible</param>
        /// <param name="outgoingData">sortie</param>
        public override void GetData(object target, Stream outgoingData)
        {
            ObjectCodeExporter olt = new ObjectCodeExporter(false);
            StreamWriter writer = new StreamWriter(outgoingData);
            writer.WriteLine(olt.Translate(((WeakReference)target).Target));
            writer.Flush();
        }
    }

    /// <summary>
    /// Debuger part
    /// </summary>
    public class WeakReferenceVisualizer : DialogDebuggerVisualizer
    {
        /// <summary>
        /// Show visualizer
        /// </summary>
        /// <param name="windowService"></param>
        /// <param name="objectProvider"></param>
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            using (StreamReader sr = new StreamReader(objectProvider.GetData()))
            {
                using (VisualizerForm vf = new VisualizerForm(sr.ReadToEnd()))
                {
                    vf.ShowDialog();
                }
            }
        }

        /// <summary>
        /// For test only
        /// </summary>
        /// <param name="objectToVisualize"></param>
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(WeakReferenceVisualizer), typeof(WeakReferenceVisualizerObjectSource));
            visualizerHost.ShowVisualizer();
        }
    }
}
