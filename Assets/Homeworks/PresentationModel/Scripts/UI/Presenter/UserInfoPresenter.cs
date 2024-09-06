using System;
using Lessons.Architecture.PM.Views;
using Zenject;

namespace Lessons.Architecture.PM.Mono
{
    public class UserInfoPresenter : IInitializable, IDisposable
    {
        private UserInfoModel userInfoModel;
        private UserInfoView userInfoView;

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
            userInfoView.ChangeDescription(userInfoModel.Description);
            userInfoView.ChangeIcon(userInfoModel.Icon);
        }

        private void ChangeName(string name)
        {
            var nameWithPrefix = $"@{name}";
            userInfoView.ChangeName(nameWithPrefix);
        }

        public void Dispose()
        {
            userInfoModel.OnNameChanged -= userInfoView.ChangeName;
            userInfoModel.OnDescriptionChanged -= userInfoView.ChangeDescription;
            userInfoModel.OnIconChanged -= userInfoView.ChangeIcon;
        }
    }
}