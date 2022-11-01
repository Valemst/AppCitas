using AppCitas.Service.DTOs;
using AppCitas.Service.Entities;

namespace AppCitas.Service.Interfaces;

public interface ILikeRespository
{
    Task<UserLike> GetUserLike(int sourceId, int likedUserId);
    Task<IEnumerable<LikeDto>> GetUsersLikes(string predicate, int userId);
    Task<AppUser> GetUserWithLikes(int userId);
}
