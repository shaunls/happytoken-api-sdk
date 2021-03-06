using HappyTokenApi.Models;
using UnityEngine;

namespace HappyTokenApi.Debugging
{
    public class StoreWindow : DebugWindow
    {
        private bool m_IsBuying;

        public StoreWindow(int id, string title) : base(id, title) { }

        public override void Draw()
        {
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();

            if (ApiDebugger.Instance.IsConfigDataLoaded)
            {
                DrawCurrencySpots();

                DrawAvatars();

                DrawAvatarUpgrades();

                DrawBuildings();

                DrawBuildingUpgrades();

                DrawPromotions();
            }
            else
            {
                GUILayout.Label("Config data is not loaded. Please Authenticate and Get Config.");
            }

            GUILayout.EndHorizontal();
        }

        private void BuyPromotion(string promotionCode)
        {
            if (m_IsBuying)
            {
                return;
            }

            m_IsBuying = true;

            ApiDebugger.Instance.WebRequest.BuyPromotion(promotionCode, (wallet) =>
            {
                ApiDebugger.Instance.CoreDataStore.Wallet = wallet;

                Debug.Log("BuyPromotion: Success!");

                m_IsBuying = false;
            }, s =>
            {
                Debug.LogError("BuyPromotion: Failed");

                m_IsBuying = false;
            });
        }

        private void BuyCurrency(StoreCurrencySpot currencySpot)
        {
            if (m_IsBuying)
            {
                return;
            }

            m_IsBuying = true;

            ApiDebugger.Instance.WebRequest.BuyCurrency(currencySpot, (wallet) =>
            {
                ApiDebugger.Instance.CoreDataStore.Wallet = wallet;

                Debug.Log($"BuyCurrency: Success!");

                m_IsBuying = false;
            }, s =>
            {
                Debug.LogError("BuyCurrency: Failed");

                m_IsBuying = false;
            });
        }

        private void BuyAvatar(AvatarType avatarType)
        {
            if (m_IsBuying)
            {
                return;
            }

            m_IsBuying = true;

            ApiDebugger.Instance.WebRequest.BuyAvatar(avatarType, (wallet) =>
            {
                ApiDebugger.Instance.CoreDataStore.Wallet = wallet;

                Debug.Log($"BuyAvatar: Success!");

                m_IsBuying = false;
            }, s =>
            {
                Debug.LogError("BuyAvatar: Failed");

                m_IsBuying = false;
            });
        }

        private void BuyAvatarUpgrade(StoreAvatarUpgrade avatarUpgrade)
        {
            if (m_IsBuying)
            {
                return;
            }

            m_IsBuying = true;

            ApiDebugger.Instance.WebRequest.BuyAvatarUpgrade(avatarUpgrade, (wallet) =>
            {
                ApiDebugger.Instance.CoreDataStore.Wallet = wallet;

                Debug.Log($"BuyAvatarUpgrade: Success!");

                m_IsBuying = false;
            }, s =>
            {
                Debug.LogError("BuyAvatarUpgrade: Failed");

                m_IsBuying = false;
            });
        }

        private void BuyBuilding(BuildingType buildingType)
        {
            if (m_IsBuying)
            {
                return;
            }

            m_IsBuying = true;

            ApiDebugger.Instance.WebRequest.BuyBuilding(buildingType, (wallet) =>
            {
                ApiDebugger.Instance.CoreDataStore.Wallet = wallet;

                Debug.Log($"BuyBuilding: Success!");

                m_IsBuying = false;
            }, s =>
            {
                Debug.LogError("BuyBuilding: Failed");

                m_IsBuying = false;
            });
        }

        private void BuyBuildingUpgrade(StoreBuildingUpgrade buildingUpgrade)
        {
            if (m_IsBuying)
            {
                return;
            }

            m_IsBuying = true;

            ApiDebugger.Instance.WebRequest.BuyBuidlingUpgrade(buildingUpgrade, (wallet) =>
            {
                ApiDebugger.Instance.CoreDataStore.Wallet = wallet;

                Debug.Log($"BuyBuidlingUpgrade: Success!");

                m_IsBuying = false;
            }, s =>
            {
                Debug.LogError("BuyBuidlingUpgrade: Failed");

                m_IsBuying = false;
            });
        }

        private void DrawPromotions()
        {
            GUILayout.BeginVertical(GUIContent.none, "box");

            GUILayout.Label("Promotions");

            var promotions = ApiDebugger.Instance.ConfigDataStore.Store.Promotions;

            if (promotions != null)
            {
                foreach (var promotion in promotions)
                {
                    GUILayout.BeginVertical(GUIContent.none, "box");
                    GUILayout.Label($"  Code:{promotion.Code}");
                    GUILayout.Label($"  Start:{promotion.StartDate}");
                    GUILayout.Label($"  End:{promotion.EndDate}");
                    if (GUILayout.Button("Buy"))
                    {
                        BuyPromotion(promotion.PromotionId);
                    }
                    GUILayout.EndVertical();
                }
            }

            GUILayout.EndVertical();
        }

        private void DrawCurrencySpots()
        {
            GUILayout.BeginVertical(GUIContent.none, "box");

            GUILayout.Label("CurrencySpots");

            var items = ApiDebugger.Instance.ConfigDataStore.Store.CurrencySpots;

            if (items != null)
            {
                foreach (var item in items)
                {
                    GUI.enabled = item.IsVisible;
                    GUILayout.BeginVertical(GUIContent.none, "box");
                    GUILayout.Label($"Wallet:{item.Wallet}");
                    GUILayout.Label($"Cost:{item.Cost}");
                    if (GUILayout.Button("Buy"))
                    {
                        BuyCurrency(item);
                    }
                    GUILayout.EndVertical();
                    GUI.enabled = true;

 
                }
            }

            GUILayout.EndVertical();
        }

        private void DrawAvatars()
        {
            GUILayout.BeginVertical(GUIContent.none, "box");

            GUILayout.Label("Avatars");

            var items = ApiDebugger.Instance.ConfigDataStore.Store.Avatars;

            if (items != null)
            {
                foreach (var item in items)
                {
                    GUI.enabled = item.IsVisible;
                    GUILayout.BeginVertical(GUIContent.none, "box");
                    GUILayout.Label($"AvatarType:{item.AvatarType}");
                    GUILayout.Label($"Cost:{item.Cost}");
                    if (GUILayout.Button("Buy"))
                    {
                        BuyAvatar(item.AvatarType);
                    }
                    GUILayout.EndVertical();
                    GUI.enabled = true;
                }
            }

            GUILayout.EndVertical();
        }

        private void DrawAvatarUpgrades()
        {
            GUILayout.BeginVertical(GUIContent.none, "box");

            GUILayout.Label("AvatarUpgrades");

            var items = ApiDebugger.Instance.ConfigDataStore.Store.AvatarUpgrades;

            if (items != null)
            {
                foreach (var item in items)
                {
                    GUI.enabled = item.IsVisible;
                    GUILayout.BeginVertical(GUIContent.none, "box");
                    GUILayout.Label($"AvatarType:{item.AvatarType}");
                    GUILayout.Label($"Level:{item.Level}");
                    GUILayout.Label($"Cost:{item.Cost}");
                    if (GUILayout.Button("Buy"))
                    {
                        BuyAvatarUpgrade(item);
                    }
                    GUILayout.EndVertical();
                    GUI.enabled = true;
                }
            }

            GUILayout.EndVertical();
        }

        private void DrawBuildings()
        {
            GUILayout.BeginVertical(GUIContent.none, "box");

            GUILayout.Label("Buildings");

            var items = ApiDebugger.Instance.ConfigDataStore.Store.Buildings;

            if (items != null)
            {
                foreach (var item in items)
                {
                    GUI.enabled = item.IsVisible;
                    GUILayout.BeginVertical(GUIContent.none, "box");
                    GUILayout.Label($"BuildingType:{item.BuildingType}");
                    GUILayout.Label($"Cost:{item.Cost}");
                    if (GUILayout.Button("Buy"))
                    {
                        BuyBuilding(item.BuildingType);
                    }
                    GUILayout.EndVertical();
                    GUI.enabled = true;
                }
            }

            GUILayout.EndVertical();
        }

        private void DrawBuildingUpgrades()
        {
            GUILayout.BeginVertical(GUIContent.none, "box");

            GUILayout.Label("BuildingUpgrades");

            var items = ApiDebugger.Instance.ConfigDataStore.Store.BuildingUpgrades;

            if (items != null)
            {
                foreach (var item in items)
                {
                    GUI.enabled = item.IsVisible;
                    GUILayout.BeginVertical(GUIContent.none, "box");
                    GUILayout.Label($"BuildingType:{item.BuildingType}");
                    GUILayout.Label($"Level:{item.Level}");
                    GUILayout.Label($"Cost:{item.Cost}");
                    if (GUILayout.Button("Buy"))
                    {
                        BuyBuildingUpgrade(item);
                    }
                    GUILayout.EndVertical();
                    GUI.enabled = true;
                }
            }

            GUILayout.EndVertical();
        }
    }
}
