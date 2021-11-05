using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoService
{
    public class TagBlock
    {
        public List<Tag> Tags { get; private set; }

        public bool ExistMoreTags { get; private set; }

        public TagBlock(List<Tag> tags, bool existMoreTags)
        {
            Tags = tags;
            ExistMoreTags = existMoreTags;
        }
    }
}
