using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors
{
    public class ArticleErrorCodes
    {
        public const string ArticleNotFound = "ARTICLE_NOT_FOUND";
        public const string ArticleNameRequired = "ARTICLE_NAME_REQUIRED";
        public const string ArticleNameMaxLength = "ARTICLE_NAME_MAX_LENGTH";
        public const string ArticleDescriptionMaxLength = "ARTICLE_DESCRIPTION_MAX_LENGTH";
        public const string ArticleDescriptionRequired = "ARTICLE_DESCRIPTION_REQUIRED";
        public const string ArticleTextRequired = "ARTICLE_TEXT_REQUIRED";
        public const string ArticleTextMaxLength = "ARTICLE_TEXT_MAX_LENGTH";
    }
}