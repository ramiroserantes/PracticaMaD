using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoService
{

    public class TagBlock
    {
        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<Tag> Tags { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [exist more tags].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exist more tags]; otherwise, <c>false</c>.
        /// </value>
        public bool ExistMoreTags { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlock"/> class.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="existMoreTags">if set to <c>true</c> [exist more tags].</param>
        public TagBlock(List<Tag> tags, bool existMoreTags)
        {
            Tags = tags;
            ExistMoreTags = existMoreTags;
        }
    }
}
