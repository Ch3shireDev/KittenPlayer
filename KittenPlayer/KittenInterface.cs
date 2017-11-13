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
        public void RenameSelectedItem()
        {
            Debug.WriteLine("From " + this + " to " + ActiveControl);
            if (ActiveControl is IKittenInterface tab)
            {
                Debug.WriteLine(tab + " is Interface");
                tab.RenameSelectedItem();
            }
        }
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

    partial class MusicTab
    {
        private int RenameIndex;

        public void RenameSelectedItem()
        {
            if (ActiveControl is ListView listView)
            {
                new RenameBox(listView, 0);
            }
            else if (ActiveControl is RenameBox renameBox)
            {
                renameBox.AcceptChange();
                RenameIndex++;
                RenameIndex %= 4;
                new RenameBox(PlaylistView, RenameIndex);
            }
        }
    }
}