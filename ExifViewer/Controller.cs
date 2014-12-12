using System.Windows.Forms;
using ExifLibrary;

namespace ExifViewer
{
    /// <summary>
    /// Controller
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Load File
        /// </summary>
        /// <param name="path"></param>
        public void LoadFile(string path)
        {
            this.File = new JpegFile(path);
        }

        /// <summary>
        /// Gets File
        /// </summary>
        public JpegFile File { get; private set; }

        /// <summary>
        /// Build Root Node
        /// </summary>
        /// <returns></returns>
        public TreeNode BuildRootNode()
        {
            TreeNode node = new TreeNode(this.File.Path);
            this.BuildMarkerNodes(node);
            return node;
        }

        /// <summary>
        /// Build Marker Nodes
        /// </summary>
        /// <param name="parentNode"></param>
        private void BuildMarkerNodes(TreeNode parentNode)
        {
            foreach (Marker marker in this.File.Markers)
            {
                string title = string.Format("Marker {0} (0x{1:x4}) {2}", marker.MarkerType, (int)marker.MarkerType, (marker.Data != null?marker.Data.GetType().Name:""));
                TreeNode node = new TreeNode(title);
                if (marker.Data != null)
                {
                    if (marker.Data.GetType() == typeof(Tiff))
                    {
                        this.BuildTiffNodes(node, (Tiff)marker.Data);
                    }
                    if (marker.Data.GetType() == typeof(Jfif))
                    {
                        Jfif jfif = (Jfif)marker.Data;
                        node.Nodes.Add(new TreeNode(string.Format("{0}v{1} ({2}x{3})", jfif.VersionMaj, jfif.VersionMin, jfif.Thumbnail.Width, jfif.Thumbnail.Height)));
                    }
                    if (marker.Data.GetType() == typeof(Jfxx))
                    {
                        Jfxx jfxx = (Jfxx)marker.Data;
                        node.Nodes.Add(new TreeNode(string.Format("{0} {1}", jfxx.Format, jfxx.Data.GetType().Name)));
                    }
                    if (marker.Data.GetType() == typeof(Xmp))
                    {
                        Xmp xmp = (Xmp)marker.Data;
                        node.Nodes.Add(new TreeNode(string.Format("{0}", xmp.XmlString)));
                    }
                    if (marker.Data.GetType() == typeof(QuantizationTable))
                    {
                        QuantizationTable quantizationTable = (QuantizationTable)marker.Data;
                        node.Nodes.Add(new TreeNode(string.Format("Id {0}", quantizationTable.Id)));
                    }
                    if (marker.Data.GetType() == typeof(string))
                    {
                        node.Nodes.Add(new TreeNode(marker.Data.ToString()));
                    }
                }
                parentNode.Nodes.Add(node);
            }
        }

        /// <summary>
        /// Build Tiff Nodes
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="tiff"></param>
        private void BuildTiffNodes(TreeNode parentNode, Tiff tiff)
        {
            string title = string.Format("Tiff {0}", tiff.ByteAlignment);
            TreeNode node = new TreeNode(title);
            foreach (ImageFileDirectory ifd in tiff.ImageFileDirectories)
            {
                this.BuildIfdNodes(node, ifd);
            }
            parentNode.Nodes.Add(node);
        }

        /// <summary>
        /// Build Image File Directory Nodes
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="imageFileDirectories"></param>
        private void BuildIfdNodes(TreeNode parentNode, ImageFileDirectory imageFileDirectory)
        {
            string title = string.Format("IFD");
            TreeNode node = new TreeNode(title);
            foreach (TiffTag tag in imageFileDirectory.Tags)
            {
                this.BuildTagNodes(node, tag);
            }
            parentNode.Nodes.Add(node);
        }
        
        /// <summary>
        /// Build Tag Nodes
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="tag"></param>
        private void BuildTagNodes(TreeNode parentNode, Tag tag)
        {
            string title = string.Format("{0} {1} (0x{2:x2}) {3} [{4}]", tag.GetType().Name, tag.TagName, (int)tag.TagId, tag.DataString, tag.StreamPosition);
            TreeNode node = new TreeNode(title);
            if (tag.GetType() == typeof(TiffTag) && ((TiffTag)tag).SubTags != null)
            {
                this.BuildTagCollectionNodes(node, ((TiffTag)tag).SubTags);
            }
            parentNode.Nodes.Add(node);
        }

        /// <summary>
        /// Build Tag Collection Nodes
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="collection"></param>
        private void BuildTagCollectionNodes(TreeNode parentNode, TagCollection collection)
        {
            string title = string.Format("{0}", collection.GetType().Name);
            TreeNode node = new TreeNode(title);
            foreach (Tag tag in collection.Tags)
            {
                this.BuildTagNodes(node, tag);
            }
            parentNode.Nodes.Add(node);
        }
    }
}
