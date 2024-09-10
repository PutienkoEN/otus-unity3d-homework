using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PresentationModel
{
    public class UserInfoPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable disposable = new();
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
            userInfoModel.Name
                .Subscribe(ChangeName)
                .AddTo(disposable);

            userInfoModel.Description
                .Subscribe(ChangeDescription)
                .AddTo(disposable);

            userInfoModel.Icon
                .Subscribe(ChangeIcon)
                .AddTo(disposable);
        }

        public void Dispose()
        {
            disposable.Dispose();
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