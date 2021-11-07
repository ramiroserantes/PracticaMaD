using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoService
{

    public class PhotoBlock
    {
        /// <summary>
        /// Gets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        public List<Photo> Photos { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [exist more photos].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exist more photos]; otherwise, <c>false</c>.
        /// </value>
        public bool ExistMorePhotos { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoBlock"/> class.
        /// </summary>
        /// <param name="photos">The photos.</param>
        /// <param name="existMorePhotos">if set to <c>true</c> [exist more photos].</param>
        public PhotoBlock(List<Photo> photos, bool existMorePhotos)
        {
            Photos = photos;
            ExistMorePhotos = existMorePhotos;
        }
    }
}
