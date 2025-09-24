using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Domain.Errors;

namespace Weblog.Application.Validations.Article
{
    public class UpdateArticleValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage(ArticleErrorCodes.ArticleTitleRequired)
                .MaximumLength(150).WithMessage(ArticleErrorCodes.ArticleTitleMaxLength);
                
            RuleFor(t => t.AboveTitle)
                .MaximumLength(150).WithMessage(ArticleErrorCodes.ArticleAboveTitleMaxLength);

            RuleFor(t => t.BelowTitle)
                .MaximumLength(150).WithMessage(ArticleErrorCodes.ArticleBelowTitleMaxLength);
                
            RuleFor(d => d.Description)
                .NotEmpty().WithMessage(ArticleErrorCodes.ArticleDescriptionRequired)
                .MaximumLength(600).WithMessage(ArticleErrorCodes.ArticleDescriptionMaxLength);

            RuleFor(c => c.Context)
                .NotEmpty().WithMessage(ArticleErrorCodes.ArticleTextRequired);
        }        
    }
}