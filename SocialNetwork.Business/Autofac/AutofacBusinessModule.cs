using Autofac;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Concrete;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.DataAccess.Concrete;
using SocialNetwork.DataAccess.Concrete.EntityFramework;

namespace SocialNetwork.Business.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<UserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<PostManager>().As<IPostService>();
            builder.RegisterType<PostDal>().As<IPostDal>();

            builder.RegisterType<ReactionDal>().As<IReactionDal>();
            builder.RegisterType<ReactionManager>().As<IReactionService>();

            builder.RegisterType<CommentDal>().As<ICommentDal>();
            builder.RegisterType<CommentManager>().As<ICommentService>();

            builder.RegisterType<CommentReactionDal>().As<ICommentReactionDal>();
            builder.RegisterType<CommentReactManager>().As<ICommentReactService>();

            builder.RegisterType<ReplyDal>().As<IReplyDal>();
            builder.RegisterType<ReplyManager>().As<IReplyService>();

            builder.RegisterType<RoleDal>().As<IRoleDal>();
            builder.RegisterType<RoleManager>().As<IRoleService>();

            builder.RegisterType<UserRolDal>().As<IUserRoleDal>();
            builder.RegisterType<UserRoleManager>().As<IUserRoleService>();

            builder.RegisterType<FollowDal>().As<IFollowDal>();
            builder.RegisterType<FollowManager>().As<IFollowService>();

            builder.RegisterType<ContactDal>().As<IContactDal>();
            builder.RegisterType<ContactManager>().As<IContactService>();
        }
    }
}