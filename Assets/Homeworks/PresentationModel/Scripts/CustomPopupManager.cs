using Lessons.Architecture.PM.Views;
using UnityEngine;

namespace Lessons.Architecture.PM.Mono
{
    public class CustomPopupManager : MonoBehaviour
    {
        [SerializeField] private UserInfoPresenter userInfoPresenter;
        [SerializeField] private UserInfoModel userInfoModel;
        [SerializeField] private UserInfoView userInfoView;


        [SerializeField] private PlayerLevelPresenter playerLevelPresenter;
        [SerializeField] private PlayerLevelModel playerLevelModel;
        [SerializeField] private PlayerLevelView playerLevelView;

        private void Awake()
        {
            userInfoModel = new UserInfoModel();
            userInfoPresenter = new UserInfoPresenter();
            userInfoPresenter.Construct(userInfoModel, userInfoView);

            playerLevelModel = new PlayerLevelModel();
            playerLevelPresenter = new PlayerLevelPresenter();
            playerLevelPresenter.Construct(playerLevelModel, playerLevelView);
            playerLevelView.Construct(playerLevelPresenter);
        }
    }
}