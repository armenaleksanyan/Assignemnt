using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using UserStories.DTO.Group;
using UserStories.DTO.Story;
using UserStories.DTO.User;
using UserStories.Web.Models;

namespace UserStories.Web.Helpers
{
    public static class Convertors
    {
        public static string GetMd5Hash(this string text)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }

        public static List<long> ConvertListOfLong(this string ids)
        {
            List<long> retVal = new List<long>();
            if (string.IsNullOrEmpty(ids))
                return retVal;
            var array = ids.Split(',');
            foreach (var item in array)
            {
                if (!string.IsNullOrEmpty(item))
                    retVal.Add(long.Parse(item));
            }
            return retVal;
        }

        public static GroupInfoModel ToGroupInfoModel(this GroupInfo model) {

            if (model == null)
                return new GroupInfoModel();

            return new GroupInfoModel
            {
                Description = model.Description,
                GroupId = model.Id,
                MembersCount = model.MembersCount,
                Name = model.Name,
                StoriesCount = model.StoriesCount
            };        
        }

        public static Group ToGroup(this GroupModel model)
        {
            if (model == null)
                return new Group();

            return new Group
            {
                Description = model.Description,
                Id = model.GroupId,                
                Name = model.Name                
            };
        }

        public static GroupModel ToGroupModel(this Group model)
        {
            if (model == null)
                return new GroupModel();

            return new GroupModel
            {
                Description = model.Description,
                GroupId = model.Id,
                Name = model.Name
            };
        }

        public static UserInfo ToUserInfo(this User model) 
        {
            if (model == null)
                return new UserInfo();
            return new UserInfo
            {
                Id = model.Id,
                FirstName= model.FirstName,
                LastName=model.LastName,
                UserName=model.UserName
            };
        }

        public static StoryExtendedDataModel ToStoryExtendedDataModel(this StoryExtendedData model)
        {
            if (model == null)
                return new StoryExtendedDataModel();
            return new StoryExtendedDataModel
            {
                Content = model.Content,
                Description = model.Description,
                StoryId = model.Id,
                Title = model.Title,
                PostedOn = model.PostedOn,
                LastModified = model.LastModified,
                GroupIds = string.Join(",", model.Groups.Select(v => v.Id)),
                User = model.User.ToUserInfo(),
                Groups = model.Groups.Select(g => g.ToGroupModel()).ToList(),
            };
        }

        public static Story ToStory(this StoryModel model)
        {
            if (model == null)
                return new Story();
            return new Story
            {
                Id = model.StoryId,
                Content = model.Content,
                Description = model.Description,
                LastModified = model.LastModified,
                PostedOn = model.PostedOn,
                GroupIds = model.GroupIds.ConvertListOfLong(),
                Title = model.Title,
                UserId = model.UserId
            };
        }
    }
}