using Lessons.Architecture.PM.Views;

namespace Lessons.Architecture.PM.Mono
{
    public class UserInfoPresenter
    {
        private UserInfoModel userInfoModel;
        private UserInfoView userInfoView;

        public void Construct(UserInfoModel userInfoModel, UserInfoView userInfoView)
        {
            this.userInfoModel = userInfoModel;
            this.userInfoView = userInfoView;

            userInfoModel.OnNameChanged += userInfoView.ChangeName;
            userInfoModel.OnDescriptionChanged += userInfoView.ChangeDescription;
            userInfoModel.OnIconChanged += userInfoView.ChangeIcon;
        }
    }
}