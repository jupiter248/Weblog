using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Favorite
{
    public class FavoriteErrorCodes
    {
        public const string FavoriteItemAlreadyExists = "FAVORITE_ITEM_ALREADY_EXISTS";
        public const string FavoriteListNotFound = "FAVORITE_LIST_NOT_FOUND";
        public const string FavoriteItemNotFound = "FAVORITE_ITEM_NOT_FOUND";
        public const string FavoriteListDeleteForbidden = "FAVORITE_DELETE_FORBIDDEN";
        public const string FavoriteListUpdateForbidden = "FAVORITE_UPDATE_FORBIDDEN";
        public const string FavoriteListNameRequired = "FAVORITE_LIST_NAME_REQUIRED";
        public const string FavoriteListDescriptionRequired = "FAVORITE_LIST_DESCRIPTION_REQUIRED";
        public const string FavoriteListNameMaxLength = "FAVORITE_LIST_NAME_MAX_LENGTH";
        public const string FavoriteListDescriptionMaxLength = "FAVORITE_LIST_DESCRIPTION_MAX_LENGTH";





    }
}