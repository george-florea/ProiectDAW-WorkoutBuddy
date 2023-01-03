using Backend.BusinessLogic.Base;
using Backend.Common.DTOs;
using Backend.Common.Exceptions;
using Backend.Entities;
using Backend.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Splits
{

    public class CommentService : BaseService
    {
        public CommentService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }
        public void AddComment(CommentModel model, CurrentUserDto CurrentUser)
        {
            ExecuteInTransaction(uow =>
            {
                var split = uow.Splits.Get().
                    Include(s => s.Comments)
                    .FirstOrDefault(s => s.Idsplit == model.ParentSplitID);
                if (split == null)
                {
                    throw new NotFoundErrorException("the split does not exist!");
                }

                var parentComm = uow.Comments.Get().FirstOrDefault(c => c.Idcomment == model.ParentCommentId);

                var comment = Mapper.Map<CommentModel, Comment>(model);
                //de luat currentUser din actiune
                comment.Iduser = CurrentUser.Id;
                comment.IdsplitNavigation = split;
                comment.IdparentCommNavigation = parentComm;

                split.Comments.Add(comment);

                uow.Splits.Update(split);
                uow.SaveChanges();
            });
        }



        public bool DeleteComment(Guid id, CurrentUserDto CurrentUser)
        {
            var isDeleted = true;
            ExecuteInTransaction(uow =>
             {
                 var comment = uow.Comments.Get()
                     .Include(c => c.IdsplitNavigation)
                         .ThenInclude(c => c.Comments)
                     .Include(c => c.InverseIdparentCommNavigation)
                     .FirstOrDefault(c => c.Idcomment == id);


                 if (comment != null)
                 {
                     if (!CurrentUser.Roles.Contains(nameof(RoleTypes.Admin)) 
                        && !CurrentUser.Roles.Contains(nameof(RoleTypes.UserOfTheWeek))
                        && !(comment.IdparentComm == null && comment.Iduser == CurrentUser.Id))
                     {
                         throw new ForbiddenErrorException("you cannot delete this comment!");
                     }

                     var split = comment.IdsplitNavigation;
                     uow.Comments.DeleteRange(comment.InverseIdparentCommNavigation);
                     split.Comments.Remove(comment);
                     try
                     {
                         uow.Splits.Update(split);
                         uow.SaveChanges();
                     }
                     catch (Exception e)
                     {
                         isDeleted = false;
                     }
                 }
                 else
                 {
                     throw new NotFoundErrorException("the comment does not exist!");
                 }
             });
            return isDeleted;
        }
    }
}
