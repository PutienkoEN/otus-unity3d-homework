using Lessons.Architecture.PM.Views;
using UnityEngine;

namespace Lessons.Architecture.PM.Mono
{
    public class CustomPopupManager : MonoBehaviour
    {
        [SerializeField] private UserInfoPresenter userInfoPresenter;
        [SerializeField] private UserInfoModel userInfoModel;
        [SerializeField] private UserInfoView userInfoView;

        private void Awake()
        {
            userInfoModel = new UserInfoModel();
            userInfoPresenter = new UserInfoPresenter();
            userInfoPresenter.Construct(userInfoModel, userInfoView);
        }
    }
}