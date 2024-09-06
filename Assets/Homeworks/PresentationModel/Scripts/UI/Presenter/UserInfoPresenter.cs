using System;
using Lessons.Architecture.PM.Views;
using UnityEngine;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Mono
{
    public class UserInfoPresenter : IInitializable, IDisposable
    {
        private readonly UserInfoModel userInfoModel;
        private readonly UserInfoView userInfoView;

        [Inject]
        public UserInfoPresenter(UserInfoModel userInfoModel, UserInfoView userInfoView)
        {
            this.userInfoModel = userInfoModel;
            this.userInfoView = userInfoView;
        }

        public void Initialize()
        {
            userInfoModel.OnNameChanged += ChangeName;
            userInfoModel.OnDescriptionChanged += userInfoView.ChangeDescription;
            userInfoModel.OnIconChanged += userInfoView.ChangeIcon;

            // At this point model already has some data, so we change view with existing data.
            ChangeName(userInfoModel.Name);
            ChangeDescription(userInfoModel.Description);
            ChangeIcon(userInfoModel.Icon);
        }

        public void Dispose()
        {
            userInfoModel.OnNameChanged -= userInfoView.ChangeName;
            userInfoModel.OnDescriptionChanged -= userInfoView.ChangeDescription;
            userInfoModel.OnIconChanged -= userInfoView.ChangeIcon;
        }

        private void ChangeName(string name)
        {
            var nameWithPrefix = $"@{name}";
            userInfoView.ChangeName(nameWithPrefix);
        }

        private void ChangeDescription(string description)
        {
            userInfoView.ChangeDescription(description);
        }

        private void ChangeIcon(Sprite icon)
        {
            userInfoView.ChangeIcon(icon);
        }
    }
}