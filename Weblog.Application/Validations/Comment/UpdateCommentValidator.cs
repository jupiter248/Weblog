using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Domain.Errors.Comment;

namespace Weblog.Application.Validations.Comment
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage(CommentErrorCodes.CommentTextRequired)
                .MaximumLength(1000).WithMessage(CommentErrorCodes.CommentTextMaxLength);
        }
    }
}