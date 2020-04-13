using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;

namespace DocRefList.Extensions
{
    public static class UserManagerOfTExtension
    {
        public static IPagingList<T> GetUsersPaging<T>(this UserManager<T> userManager, string action, int current, int size) where T : class
        {
            PagingList<T> paging = PagingList.Create<T>(userManager.Users, size, current);
            paging.Action = action;

            return paging;
        }
    }
}
