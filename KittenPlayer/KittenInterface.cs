using System.Diagnostics;
using System.Windows.Forms;

namespace KittenPlayer
{
    internal interface IKittenInterface
    {
        void RenameSelectedItem();
    }

    partial class MainWindow
    {
    }

    partial class MainTabs
    {
        public void RenameSelectedItem()
        {
            Debug.WriteLine("From " + this + " to " + ActiveControl);
            if (ActiveControl is IKittenInterface tab)
            {
                tab.RenameSelectedItem();
                Debug.WriteLine(tab + " is interface");
            }
        }
    }

}