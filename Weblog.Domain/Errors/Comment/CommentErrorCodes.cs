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
        public const string CommentTextRequired = "COMMENT_TEXT_REQUIRED";
        public const string CommentTextMaxLength = "COMMENT_TEXT_MAX_LENGTH";
        public const string CommentEntityTypeRequired = "COMMENT_ENTITY_TYPE_REQUIRED";
        public const string CommentEntityTypeInvalid = "COMMENT_ENTITY_TYPE_INVALID";




    }
}