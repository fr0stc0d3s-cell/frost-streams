using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static frost.Core.Main.UI;

namespace frost.Core
{
    [BepInPlugin("frost", "Frost", "0.0.1")]
    public class Main : BaseUnityPlugin
    {

        public void Start()
        {
            GameObject Object = new GameObject("frost");
            Object.AddComponent<UI>();
            DontDestroyOnLoad(Object);
        }


        public class UI : MonoBehaviour
        {
            public static Rect Rect = new Rect(20, 150, 200, 60);

            public void OnGUI()
            {
                GUI.backgroundColor = Color.black;
                GUI.contentColor = Color.white;

                GUI.skin.label.fontSize = 14;
                GUI.skin.window.fontSize = 24;

                GUI.matrix = Matrix4x4.Scale(new Vector3(2f, 2f, 1f));

                Rect = GUILayout.Window(0, Rect, Draw, "");
            }

            public static string RoomJoin;

            public static void Draw(int i)
            {
                GUI.backgroundColor = Color.black;
                GUI.contentColor = Color.white;
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();

                if (PhotonNetwork.InRoom)
                {
                    GUILayout.Label($"Room: {PhotonNetwork.CurrentRoom.Name}");
                    GUILayout.Label($"Players: {PhotonNetwork.CurrentRoom.PlayerCount}");
                }
                else
                {
                    GUILayout.Label("Not Connected");
                }
                GUILayout.Space(5);
                if (PhotonNetwork.InRoom)
                {
                    if (GUILayout.Button("Leave Room")) { PhotonNetwork.Disconnect(); }
                }
                else
                {
                    GUILayout.Label("Enter Room Code Below.");
                    string input = GUILayout.TextField(RoomJoin ?? "", 12);
                    RoomJoin = input.ToUpperInvariant();
                    if (GUILayout.Button($"Join {RoomJoin}"))
                    {
                        PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(RoomJoin, JoinType.Solo);
                        RoomJoin = "";
                    }
                }

                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                GUI.DragWindow();
            }

        }
    }
}
