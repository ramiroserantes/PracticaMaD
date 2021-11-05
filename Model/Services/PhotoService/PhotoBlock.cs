using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoService
{
    public class PhotoBlock
    {
        public List<Photo> Photos { get; private set; }

        public bool ExistMorePhotos { get; private set; }

        public PhotoBlock(List<Photo> photos, bool existMorePhotos)
        {
            Photos = photos;
            ExistMorePhotos = existMorePhotos;
        }
    }
}
