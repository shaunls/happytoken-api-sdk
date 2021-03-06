﻿using HappyTokenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace HappyTokenApi.Client
{
    public class WebRequest
    {
        private string m_ApiUrl;
        private JsonWebToken m_JsonWebToken;
        private MonoBehaviour m_MonoBehaviour;

        public WebRequest SetApiUrl(string apiUrl)
        {
            m_ApiUrl = apiUrl;

            return this;
        }

        public WebRequest SetMonoBehaviour(MonoBehaviour monoBehaviour)
        {
            m_MonoBehaviour = monoBehaviour;

            return this;
        }

        public WebRequest SetJsonWebToken(JsonWebToken jsonWebToken)
        {
            m_JsonWebToken = jsonWebToken;

            Debug.Log($"WebRequest.SetJsonWebToken: AccessToken:{jsonWebToken.AccessToken}, Expires:{jsonWebToken.ExpiresInSecs}");

            return this;
        }

        #region Account Creation & Authentication

        /// <summary>
        /// Used to create a new user given a device ID
        /// </summary>        
        /// <param name="onSuccess">Returns a UserAuthPair. Use Authenticate to generate a Session Token</param>
        public void CreateUser(UserDevice userDevice, Action<UserAuthPair> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/users";
            var data = JsonConvert.SerializeObject(userDevice);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail));
        }

        /// <summary>
        /// Used to authenticate a user by Email and Password
        /// </summary>
        /// <param name="onSuccess">Returns a UserAuthPair. Use Authenticate to generate a Session Token</param>
        public void LoginByEmail(UserEmailLogin userEmailLogin, Action<UserAuthPair> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/accounts/email";
            var data = JsonConvert.SerializeObject(userEmailLogin);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail));
        }

        /// <summary>
        /// Used to update a users Email and Password
        /// </summary>
        public void UpdateEmail(string userId, UserEmailLogin userEmailLogin, Action<RequestResult> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/accounts/email/{userId}";
            var data = JsonConvert.SerializeObject(userEmailLogin);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        /// <summary>
        /// Used to generate a session JWT to allow a user to make requests to authorized API routes
        /// </summary>
        public void Authenticate(UserAuthPair userAuthPair, Action<JsonWebToken> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/token";
            var data = JsonConvert.SerializeObject(userAuthPair);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail));
        }

        #endregion

        #region App Config & User Login

        public void GetAppConfig(Action<AppConfig> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/config";

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, null, onSuccess, onFail, useJwt: true));
        }

        public void GetUser(string userId, Action<UserLogin> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/users/{userId}";

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, null, onSuccess, onFail, useJwt: true));
        }

        #endregion

        #region Messages

        public void GetMessages(Action<List<UserMessage>> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/messages";

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, null, onSuccess, onFail, useJwt: true));
        }

        public void DeleteMessage(string userMessageId, Action<List<UserMessage>> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/messages/delete";
            var data = JsonConvert.SerializeObject(userMessageId);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void SendCakeMessage(UserSendCakeMessage sendCakeMessage, Action<UserMessage> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/messages/cake";
            var data = JsonConvert.SerializeObject(sendCakeMessage);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void ClaimCakeMessage(string userMessageId, Action<List<UserCake>> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/messages/cake/{userMessageId}";

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, null, onSuccess, onFail, useJwt: true));
        }

        #endregion

        #region Friends

        public void GetFriends(Action<List<FriendInfo>> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/friends";

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, null, onSuccess, onFail, useJwt: true));
        }

        public void GetSuggestedFriends(Action<List<FriendInfo>> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/friends/suggested";

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, null, onSuccess, onFail, useJwt: true));
        }

        public void AddFriend(string friendUserId, Action<List<FriendInfo>> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/friends";
            var data = JsonConvert.SerializeObject(friendUserId);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        #endregion

        #region Store

        public void BuyPromotion(string promotionCode, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/promotions";
            var data = JsonConvert.SerializeObject(promotionCode);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void BuyResourceMine(ResourceMineType resourceMineType, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/resourcemines";
            var data = JsonConvert.SerializeObject(resourceMineType);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void BuyCurrency(StoreCurrencySpot currencySpot, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/currencyspots";
            var data = JsonConvert.SerializeObject(currencySpot);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void BuyAvatar(AvatarType avatarType, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/avatars";
            var data = JsonConvert.SerializeObject(avatarType);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void BuyAvatarUpgrade(StoreAvatarUpgrade avatarUpgrade, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/avatarupgrades";
            var data = JsonConvert.SerializeObject(avatarUpgrade);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void BuyBuilding(BuildingType buildingType, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/buildings";
            var data = JsonConvert.SerializeObject(buildingType);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void BuyBuidlingUpgrade(StoreBuildingUpgrade buildingUpgrade, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/buildingupgrades";
            var data = JsonConvert.SerializeObject(buildingUpgrade);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }

        public void BuyO2OProduct(StoreO2OProduct o2OProduct, Action<Wallet> onSuccess, Action<string> onFail)
        {
            var routeUrl = $"{m_ApiUrl}/store/o2oproducts";
            var data = JsonConvert.SerializeObject(o2OProduct);

            m_MonoBehaviour.StartCoroutine(StartWebRequest(routeUrl, data, onSuccess, onFail, useJwt: true));
        }


        #endregion

        #region Helpers

        private IEnumerator StartWebRequest<T>(string url, string data, Action<T> onSuccess, Action<string> onFail, bool useJwt = false)
        {
            // Using scope will ensure the UnityWebRequest is disposed after use
            using (var webRequest = string.IsNullOrEmpty(data) ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, data))
            {
                if (useJwt)
                {
                    if (!string.IsNullOrEmpty(m_JsonWebToken?.AccessToken))
                    {
                        var jwtHeader = $"Bearer {m_JsonWebToken.AccessToken}";
                        webRequest.SetRequestHeader("Authorization", jwtHeader);
                    }
                    else
                    {
                        Debug.LogError($"The JWT has not been set. Likely the user is not authneticatied.");
                        yield break;
                    }
                }

                if (webRequest.method == "POST")
                {
                    var uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(data))
                    {
                        contentType = "application/json"
                    };

                    webRequest.uploadHandler = uploadHandler;
                }

                webRequest.timeout = 60;
                webRequest.Send();

                var time = 0f;

                while (!webRequest.isDone)
                {
                    time += Time.deltaTime;

                    yield return new WaitForEndOfFrame();
                }

                Debug.LogFormat("WebRequest to {0} took {1:N2}s (Response:{2}, Error:{3})", url, time, webRequest.responseCode, webRequest.error);

                if (webRequest.isNetworkError)
                {
                    onFail?.Invoke($"WebRequest Network Error:{webRequest.error}");
                    Debug.LogError($"Received content: {webRequest.downloadHandler.text}");
                }
                else
                {
                    // Did the server return an HTTP error response code?
                    if (webRequest.responseCode >= 400)
                    {
                        onFail?.Invoke($"WebRequest HTTP Error:{webRequest.error}");
                        Debug.LogError($"Received content: {webRequest.downloadHandler.text}");
                    }
                    else
                    {
                        var downloadHandler = webRequest.downloadHandler;

                        if (!string.IsNullOrEmpty(downloadHandler.text))
                        {
                            T dataObject;
                            if (IsValidJson(downloadHandler.text, out dataObject))
                            {
                                onSuccess?.Invoke(dataObject);
                            }
                            else
                            {
                                onFail?.Invoke($"WebRequest Json Deserialization Error: Could not deserialize payload to type {typeof(T)}.");
                            }
                        }
                        else
                        {
                            onFail?.Invoke("WebRequest Payload Error: Data was null or empty.");
                        }
                    }
                }
            }
        }

        private static bool IsValidJson<T>(string json, out T obj)
        {
            json = json.Trim();

            var isJsonObj = json.StartsWith("{") && json.EndsWith("}");
            var isJsonArray = json.StartsWith("[") && json.EndsWith("]");

            if (isJsonObj || isJsonArray)
            {
                try
                {
                    obj = JsonConvert.DeserializeObject<T>(json);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    Debug.LogError(jex);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }

            obj = default(T);
            return false;
        }

        #endregion
    }
}