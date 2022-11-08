using AppCitas.Service.DTOs;
using AppCitas.Service.Entities;
using AppCitas.Service.Helpers;

namespace AppCitas.Service.Interfaces;

public interface ILikeRepository
{
    Task<UserLike> GetUserLike(int sourceId, int likedUserId);
    Task<PagedList<LikeDto>> GetUsersLikes(LikesParams likesParams);
    Task<AppUser> GetUserWithLikes(int userId);
}
