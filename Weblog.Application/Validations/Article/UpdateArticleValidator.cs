using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.ArticleDtos;

namespace Weblog.Application.Validations.Article
{
    public class UpdateArticleValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("")
                .MaximumLength(150).WithMessage("");

            RuleFor(d => d.Description)
                .Empty().WithMessage("")
                .MaximumLength(600).WithMessage("");

            RuleFor(c => c.Context)
                .Empty().WithMessage("");
        }        
    }
}