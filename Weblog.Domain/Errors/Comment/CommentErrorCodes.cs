using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Comment
{
    public class CommentErrorCodes
    {
        public const string CommentNotFound = "Comment_NOT_FOUND";
        public const string CommentParentIdInvalid = "COMMENT_PARENT_ID_INVALID";
        public const string CommentDeleteForbidden = "COMMENT_DELETE_FORBIDDEN";
        public const string CommentUpdateForbidden = "COMMENT_UPDATE_FORBIDDEN";



    }
}