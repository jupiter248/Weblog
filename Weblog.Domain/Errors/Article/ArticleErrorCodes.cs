using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors
{
    public class ArticleErrorCodes
    {
        public const string ArticleNotFound = "ARTICLE_NOT_FOUND";
        public const string ArticleTitleRequired = "ARTICLE_TITLE_REQUIRED";
        public const string ArticleTitleMaxLength = "ARTICLE_TITLE_MAX_LENGTH";
        public const string ArticleDescriptionMaxLength = "ARTICLE_DESCRIPTION_MAX_LENGTH";
        public const string ArticleDescriptionRequired = "ARTICLE_DESCRIPTION_REQUIRED";
        public const string ArticleTextRequired = "ARTICLE_TEXT_REQUIRED";
        public const string ArticleTextMaxLength = "ARTICLE_TEXT_MAX_LENGTH";
        public const string ArticleAboveTitleRequired = "ARTICLE_ABOVE_TITLE_REQUIRED";
        public const string ArticleAboveTitleMaxLength = "ARTICLE_ABOVE_TITLE_MAX_LENGTH";
        public const string ArticleBelowTitleRequired = "ARTICLE_BELOW_TITLE_REQUIRED";
        public const string ArticleBelowTitleMaxLength = "ARTICLE_BELOW_TITLE_MAX_LENGTH";
    }
}