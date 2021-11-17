using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.CommentService
{

    public class CommentBlock
    {
        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public List<Comment> Comments { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [exist more comments].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exist more comments]; otherwise, <c>false</c>.
        /// </value>
        public bool ExistMoreComments { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentBlock"/> class.
        /// </summary>
        /// <param name="comments">The comments.</param>
        /// <param name="existMoreComments">if set to <c>true</c> [exist more comments].</param>
        public CommentBlock(List<Comment> comments, bool existMoreComments)
        {
            Comments = comments;
            ExistMoreComments = existMoreComments;
        }
    }
}
